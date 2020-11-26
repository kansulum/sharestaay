
using System.Linq;
using Api.Dtos;
using AutoMapper;
using Core.Entities;

namespace Api.Helpers
{
    public class MappingProfile : AutoMapper.Profile
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
                .ForMember(r => r.AppUserEmail, opt => opt.MapFrom(rr => rr.AppUserEmail))
                .ForMember(r => r.RoommateAgeBracket, opt => opt.Ignore())
                .ForMember(r => r.RoomGenders, opt => opt.Ignore())
                .ForMember(r => r.Amenities, opt => opt.Ignore())
                .ForMember(r => r.Rules, opt => opt.Ignore())
                .AfterMap((rr, r) =>
                {
                    // remove unselected rules
                    var removedRules = r.Rules.Where(u => !rr.Rules.Contains(u.RuleId)).ToList();
                    foreach (var rule in removedRules)
                        r.Rules.Remove(rule);

                    //add new rules
                    var addRules = rr.Rules.Where(id => !r.Rules.Any(u => u.RuleId == id))
                    .Select(id => new RoomRule { RuleId = id }).ToList();
                    foreach (var rule in addRules)
                        r.Rules.Add(rule);

                    // remove unselected ages
                    var removedAges = r.RoommateAgeBracket.Where(u => !rr.Ages.Contains(u.PreferedAgeId)).ToList();
                    foreach (var age in removedAges)
                        r.RoommateAgeBracket.Remove(age);

                    //add new ages
                    var addAges = rr.Ages.Where(id => !r.RoommateAgeBracket.Any(u => u.PreferedAgeId == id))
                    .Select(id => new RoommateAgeBracket { PreferedAgeId = id }).ToList();
                    foreach (var age in addAges)
                        r.RoommateAgeBracket.Add(age);

                    // remove unselected genders
                    var removedGenders = r.RoomGenders.Where(u => !rr.Genders.Contains(u.PreferedGenderId)).ToList();
                    foreach (var gender in removedGenders)
                        r.RoomGenders.Remove(gender);

                    //add new genders
                    var addGenders = rr.Genders.Where(id => !r.RoomGenders.Any(u => u.PreferedGenderId == id))
                    .Select(id => new RoomGender { PreferedGenderId = id }).ToList();
                    foreach (var gender in addGenders)
                        r.RoomGenders.Add(gender);

                    // remove unselected rules
                    var removedAmenities = r.Amenities.Where(u => !rr.Amenities.Contains(u.AmenitiesId)).ToList();
                    foreach (var rule in removedAmenities)
                        r.Amenities.Remove(rule);

                    //add new rules
                    var addAmenities = rr.Amenities.Where(id => !r.Amenities.Any(u => u.AmenitiesId == id))
                    .Select(id => new RoomAmenities { AmenitiesId = id }).ToList();
                    foreach (var rule in addAmenities)
                        r.Amenities.Add(rule);

                });
            
            CreateMap<AppUser, RoommateDto>()
                .ForMember(u => u.Email,opt => opt.MapFrom(rr => rr.Email));


                            
        }
    }
}