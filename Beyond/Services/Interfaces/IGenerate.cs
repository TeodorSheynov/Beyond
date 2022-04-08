using System.Collections.Generic;
using Beyond.Data.Models;

namespace Beyond.Services.Interfaces
{
    public interface IGenerate
    {
        public List<Seat> Seats(int count);
    }
}