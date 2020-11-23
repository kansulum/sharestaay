namespace Core.Entities
{
    public class RoomAmenity
    {
        public int AmenitiesId { get; set; }
        public int RoomId { get; set; }

        public Amenity Amenities { get; set; }
        public Room Room { get; set; }
    }
}