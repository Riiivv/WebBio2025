using System;
using System.Collections.Generic;
using System.Text;

namespace WebBio2025.Domain.entities;
public class SeatHold
{
    public int ShowtimeId { get; set; }
    public int SeatId { get; set; }

    public string HoldToken { get; set; } = null!;
    public DateTime ExpiresAtUtc { get; set; }
    public DateTime CreatedAtUtc { get; set; } = DateTime.UtcNow;
}