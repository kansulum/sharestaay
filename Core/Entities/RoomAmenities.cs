using System;
namespace Core.Entities
{
    public class RoomAmenities
    {
        
        public int AmenitiesId { get; set; }
        public int RoomId { get; set; }

        public Amenity Amenities { get; set; }
        public Room Room { get; set; }
    }
}