using System;
using System.Collections.Generic;
using System.Text;

namespace WebBio2025.Application.DTOs
{
    public class ShowTimeSeatHoldDTOResponse
    {
        public string HoldToken { get; set; } = string.Empty;
        public DateTime ExpiresAtUtc { get; set; }
    }

}
