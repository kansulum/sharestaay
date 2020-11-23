using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Core.Entities
{
    public class Room:BaseEntity
    {
                
        public string Location { get; set; }

        public decimal Rent { get; set; }
     
        public decimal InitialDeposit { get; set; }

        public DateTime MoveInDate { get; set; }

        public string StayDuration { get; set; }

        public string Layout { get; set; }

        public string RoommateDescription { get; set; }

        public string DescribeNeighborhood { get; set; }

        public int NumberBedRooms { get; set; }

        public int NumberBathRooms { get; set; }

        public int NumberRoommateAllowed { get; set; }

        public string SpaceDescription { get; set; }

        public bool IsSecurityChecked { get; set; }

        public ICollection<RoomAmenities> Amenities { get; set; }
        public ICollection<RoomRule> Rules { get; set; }

        public string AppUserEmail { get; set; }

        public ICollection<RoomGender>  RoomGenders { get; set; }
        public ICollection<RoommateAgeBracket> RoommateAgeBracket { get; set; }

        // public ICollection<Photo> Photos { get; set; }

        public Room()
        {
            Amenities = new Collection<RoomAmenities>();
            Rules = new Collection<RoomRule>();
            RoomGenders = new Collection<RoomGender>();
            RoommateAgeBracket = new Collection<RoommateAgeBracket>();
            // Photos = new Collection<Photo>();
        }

    }
}