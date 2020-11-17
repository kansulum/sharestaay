
using Api.Dtos;
using AutoMapper;
using Core.Entities;

namespace Api.Helpers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Room,RoomCreateDto>()
                .ForMember(r => r.Location, opt => opt.MapFrom(rr => rr.Location))
                .ForMember(r => r.Rent, opt => opt.MapFrom(rr => rr.Rent))
                .ForMember(r => r.InitialDeposit, opt => opt.MapFrom(rr => rr.InitialDeposit))
                .ForMember(r => r.MoveInDate, opt => opt.MapFrom(rr => rr.MoveInDate))
                .ForMember(r => r.DescribeNeighborhood, opt => opt.MapFrom(rr => rr.DescribeNeighborhood))
                .ForMember(r => r.RoommateDescription, opt => opt.MapFrom(rr => rr.RoommateDescription))
                .ForMember(r => r.SpaceDescription, opt => opt.MapFrom(rr => rr.SpaceDescription))
                .ForMember(r => r.IsSecurityChecked, opt => opt.MapFrom(rr => rr.IsSecurityChecked))
                .ForMember(r => r.NumberBathRooms, opt => opt.MapFrom(rr => rr.NumberBathRooms))
                .ForMember(r => r.NumberBedRooms, opt => opt.MapFrom(rr => rr.NumberBedRooms))
                .ForMember(r => r.NumberRoommateAllowed, opt => opt.MapFrom(rr => rr.NumberRoommateAllowed));

                 CreateMap<RoomCreateDto, Room>()
                .ForMember(v => v.Id, opt => opt.Ignore())
                .ForMember(v => v.Location, opt => opt.MapFrom(rr => rr.Location))
                .ForMember(v => v.Rent, opt => opt.MapFrom(rr => rr.Rent))
                .ForMember(v => v.InitialDeposit, opt => opt.MapFrom(rr => rr.InitialDeposit))
                .ForMember(v => v.MoveInDate, opt => opt.MapFrom(rr => rr.MoveInDate))
                .ForMember(v => v.DescribeNeighborhood, opt => opt.MapFrom(rr => rr.DescribeNeighborhood))
                .ForMember(v => v.RoommateDescription, opt => opt.MapFrom(rr => rr.RoommateDescription))
                .ForMember(r => r.SpaceDescription, opt => opt.MapFrom(rr => rr.SpaceDescription))
                .ForMember(r => r.IsSecurityChecked, opt => opt.MapFrom(rr => rr.IsSecurityChecked))
                .ForMember(r => r.NumberBathRooms, opt => opt.MapFrom(rr => rr.NumberBathRooms))
                .ForMember(r => r.NumberBedRooms, opt => opt.MapFrom(rr => rr.NumberBedRooms))
                .ForMember(r => r.NumberRoommateAllowed, opt => opt.MapFrom(rr => rr.NumberRoommateAllowed))
                .ForMember(r => r.AppUserEmail, opt => opt.MapFrom(rr => rr.AppUserEmail));
       
                
        }
    }
}