using System;
using System.Collections.Generic;

namespace ShelterApi.Models
{
    public partial class Animals
    {
        public Guid Id { get; set; }

        public string? Name { get; set; }
        public int Species { get; set; }
        public int Age { get; set; }
        public double Weight { get; set; }

        public DateTime RegTime { get; set; }
        public string? ShelterId { get; set; }
        public virtual ICollection<Shelters> Shelter { get; set; } = new List<Shelters>();

    }
}
