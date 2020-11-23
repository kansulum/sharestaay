using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities
{
    public class Favourite
    {
        public Room Room { get; set; }

        [Key]
        [Column(Order=1)]
        public int RoomId { get; set; }

        [Key]
        [Column(Order=2)]
        public string AppUserId { get; set; }
    }
}