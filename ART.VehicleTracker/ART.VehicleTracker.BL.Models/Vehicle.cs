using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ART.VehicleTracker.BL.Models
{
    public class Vehicle
    {
        public Guid Id { get; set; }
        public string VIN { get; set; }
        public int Year { get; set; }
        public Guid ColorId { get; set; }
        public Guid MakeId { get; set; }
        
        public Guid ModelId { get; set; }

    }
}
