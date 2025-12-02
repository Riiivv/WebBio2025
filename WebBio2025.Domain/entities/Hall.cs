using System;
using System.Collections.Generic;
using System.Text;

namespace WebBio2025.Domain.entities
{
    public class Hall
    {
        public int HallId { get; set; }
        public int HallNumber { get; set; }
        public int Capacity { get; set; }
        public List<Person> Persons { get; set; } = new(); // navigation property

    }
}
