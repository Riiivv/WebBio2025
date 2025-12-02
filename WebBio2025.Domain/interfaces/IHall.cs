using System;
using System.Collections.Generic;
using System.Text;
using WebBio2025.Domain.entities;

namespace WebBio2025.Domain.interfaces
{
    public interface IHall
    {
        Task<List<Hall>> GetAllHalls();
    }
}
