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

        public ICollection<Amenity> Amenities { get; set; }
        public ICollection<Rule> Rules { get; set; }

        public string AppUserEmail { get; set; }

        // public ICollection<RoommatePreferredGender>  RoommatePreferredGenders { get; set; }
        // public ICollection<RoomPreferredAge> RoomPreferredAge { get; set; }

        // public ICollection<Photo> Photos { get; set; }

        public Room()
        {
            Amenities = new Collection<Amenity>();
            Rules = new Collection<Rule>();
            // RoommatePreferredGenders = new Collection<RoommatePreferredGender>();
            // RoomPreferredAge = new Collection<RoomPreferredAge>();
            // Photos = new Collection<Photo>();
        }

    }
}