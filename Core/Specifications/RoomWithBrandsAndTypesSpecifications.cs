using Core.Entities;

namespace Core.Specifications
{
    public class RoomWithBrandsAndTypesSpecifications: BaseSpecification<Room>
    {
       public RoomWithBrandsAndTypesSpecifications(RoomSpecParams specParams)
        :base(x=> 
             (string.IsNullOrEmpty(specParams.Search) || x.Location.ToLower().Contains(specParams.Search))
            //  &&
            //  (!specParams.BrandId.HasValue || x.ProductBrandId == specParams.BrandId)
            //  && 
            //  (!specParams.TypeId.HasValue || x.ProductTypeId == specParams.TypeId)
        )
        {
            
            // AddInclude(x => x.Location);
            // AddOrderBy(x => x.DescribeNeighborhood);
            ApplyPaging(specParams.PageSize * (specParams.PageIndex -1),specParams.PageSize);

            if (!string.IsNullOrEmpty(specParams.Sort))
            {
                switch (specParams.Sort)
                {
                    case "priceAsc":
                        AddOrderBy(x => x.Location);
                        break;
                    case "priceDesc":
                        AddOrderByDescending(x => x.Rent);
                        break;
                    default:
                        AddOrderBy(x => x.DescribeNeighborhood);
                        break;
                }
            }

        }

        public RoomWithBrandsAndTypesSpecifications(int id) : base(x => x.Id == id)
        {
            // AddInclude(x => x.ProductBrand);
            // AddInclude(x => x.ProductType);
        } 
    }
}