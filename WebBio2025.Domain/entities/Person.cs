using System;
using System.Collections.Generic;
using System.Text;

namespace WebBio2025.Domain.entities
{
    public class Person
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public string Lastname { get; set; }
        public string Mail { get; set; }

        public Movies? Movies { get; set; }
        public int? MoviesId { get; set; }

        public Hall? Hall { get; set; }
        public int? HallId { get; set; }
    }
}
