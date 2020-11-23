namespace Core.Entities
{
    public class RoomRule
    {
        public int RoomId  { get; set; }
        public int RuleId { get; set; }

        public Room Room { get; set; }
        public Rule Rule { get; set; }
    }
}