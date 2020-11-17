using Core.Entities;

namespace Core.Specifications
{
    public class RoomWithFilterForCountSpecifications:BaseSpecification<Room>
    {
        public RoomWithFilterForCountSpecifications(RoomSpecParams roomParams):base(x=> 
            (string.IsNullOrEmpty(roomParams.Search) || x.Location.ToLower().Contains(roomParams.Search))
            //  (!productParams.BrandId.HasValue || x.ProductBrandId == productParams.BrandId) && 
            //  (!productParams.TypeId.HasValue || x.ProductTypeId == productParams.TypeId)
        )
        {
        }
    }
}