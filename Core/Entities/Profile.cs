using System;

namespace Core.Entities
{
    public class Profile
    {
        public int Id { get; set; }
        public string UserEmail { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }

        public string FirstName { get; set; }
        
        public string LastName { get; set; }

        public DateTime Birthday { get; set; }

        public string Gender { get; set; }

        public string Phone { get; set; }
        
        public string ContactEmail { get; set; }
        
        
    }
}