using System;
using System.Collections.Generic;

namespace ShelterApi.Models
{
    public partial class Shelters
    {
        public Guid Id { get; set; }

        public string? Name { get; set; }
        public string? City { get; set; }
        public int Capacity { get; set; }

        public DateTime RegTime { get; set; }

        public virtual ICollection<Animals> Animal { get; set; } = new List<Animals>();

    }
}
