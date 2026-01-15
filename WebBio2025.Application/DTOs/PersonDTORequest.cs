using System;
using System.Collections.Generic;
using System.Text;
using WebBio2025.Domain.entities;

namespace WebBio2025.Application.DTOs
{
    public class PersonDTORequest
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Lastname { get; set; }
        public string Mail { get; set; }
    }
}
