using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Api.Dtos
{
    public class RoomCreateDto
    {

        public int Id { get; set; }
        public string Location { get; set; }

        public string Name {get;set;}

        public decimal Rent { get; set; }

        public decimal InitialDeposit { get; set; }

        public DateTime MoveInDate { get; set; }

        public string StayDuration { get; set; }

        public string Layout { get; set; }

        public string RoommateDescription { get; set; }

        public string DescribeNeighborhood { get; set; }

        public string NumberBedRooms { get; set; }

        public string NumberBathRooms { get; set; }

        public string NumberRoommateAllowed { get; set; }

        public string SpaceDescription { get; set; }

        public bool IsSecurityChecked { get; set; }

        public string AppUserEmail { get; set; }

         public ICollection<int> Amenities { get; set; }
        public ICollection<int> Rules { get; set; }
        public ICollection<int> Genders { get; set; }
        public ICollection<int> Ages { get; set; }

        public RoomCreateDto()
        {
            Amenities = new Collection<int>();
            Rules = new Collection<int>();
            Genders = new Collection<int>();
            Ages = new Collection<int>();
        }

    }
}