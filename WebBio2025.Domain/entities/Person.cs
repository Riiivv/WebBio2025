using System;
using System.Collections.Generic;
using System.Text;

namespace WebBio2025.Domain.entities
{
    public class Person
    {
        public string Name { get; set; }
        public string Lastname { get; set; }
        public string Mail { get; set; }
        public int Id { get; set; } // kan staves på alle måder + className
                                    //    public int PersonId { get; set; }
        public Movies Movies { get; set; } // navigation property
        public Hall Hall { get; set; } // navigation property
    }
}
