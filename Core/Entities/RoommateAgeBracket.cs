namespace Core.Entities
{
    public class RoommateAgeBracket
    {
         public int PreferedAgeId { get; set; }
        public int RoomId { get; set; }

        public Room Roommate { get; set; }
        public AgeBracket AgeBracket { get; set; }
    }
}