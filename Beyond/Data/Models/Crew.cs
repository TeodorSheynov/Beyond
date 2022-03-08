using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Beyond.Data.Models.Enums;

namespace Beyond.Data.Models
{
    public class Crew
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Name { get; set; }
        public int Age { get; set; }
        public string Description { get; set; }
        public Rank Rank { get; set; }


    }
}
