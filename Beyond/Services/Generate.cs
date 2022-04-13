using System.Collections.Generic;
using Beyond.Data.Models;
using Beyond.Services.Interfaces;

namespace Beyond.Services
{
    public class Generate:IGenerate
    {
        public List<Seat> Seats(int count)
        {
            var seats = new List<Seat>();
            for (var i = 1; i <= count; i++)
            {
                seats.Add(new Seat()
                {
                    IsTaken = false,
                    SeatNumber = i
                });
            }
            return seats;
        }
    }
}