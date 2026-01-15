using System;
using System.Collections.Generic;
using System.Text;

namespace WebBio2025.Domain.entities
{

    public enum SeatStatus
    {
        Available = 0,
        Sold = 1,
        HeldByOther = 2,
        SelectedByYou = 3
    }
    public enum SeatType
    {
        Standard = 0,
        Wheelchair = 1,
        Companion = 2
    }
    public class Seat
    {
        public int SeatId { get; set; }
        public int RowNumber { get; set; }
        public int SeatNumber { get; set; }
        public int HallId { get; set; }
       // public Hall Hall { get; set; } = new Hall();

        public SeatType SeatType { get; set; } = SeatType.Standard;
    }
}
