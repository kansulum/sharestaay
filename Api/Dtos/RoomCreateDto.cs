using System;

namespace Api.Dtos
{
    public class RoomCreateDto
    {

        public int Id { get; set; }
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

        public string AppUserEmail { get; set; }

    }
}