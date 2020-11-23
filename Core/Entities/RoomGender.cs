namespace Core.Entities
{
    public class RoomGender
    {
        public int PreferedGenderId { get; set; }
        public int RoomId { get; set; }

        public Room Room { get; set; }
        public Gender PreferedGender { get; set; }
    }
}