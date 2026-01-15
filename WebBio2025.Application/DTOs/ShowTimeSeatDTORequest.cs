using System;
using System.Collections.Generic;
using System.Text;

namespace WebBio2025.Application.DTOs
{
    public class ShowTimeSeatDTORequest
    {
        // Unikt token fra frontend (fx GUID), som identificerer brugerens session
        public string HoldToken { get; set; } = string.Empty;

        // De sæder brugeren har valgt
        public List<int> SeatIds { get; set; } = new();
    }
}
