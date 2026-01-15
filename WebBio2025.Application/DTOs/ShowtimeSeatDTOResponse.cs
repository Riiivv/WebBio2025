using System;
using System.Collections.Generic;
using System.Text;
using WebBio2025.Domain.entities;

namespace WebBio2025.Application.DTOs
{
    public class ShowTimeSeatDTOResponse
    {
        public int SeatId { get; set; }
        public int RowNumber { get; set; }
        public int SeatNumber { get; set; }

        public int HallId { get; set; }
        public SeatType SeatType { get; set; }

        public SeatStatus Status { get; set; }
    }

}
