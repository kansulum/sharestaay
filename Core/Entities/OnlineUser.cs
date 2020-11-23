using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Core.Entities
{
    public class OnlineUser
    {
        [Key]
        public int OnlineUserID { get; set; }
        public string UserID { get; set; }
        public string ConnectionID { get; set; }
        public bool IsOnline { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }
        public bool IsActive { get; set; }
    }
}
