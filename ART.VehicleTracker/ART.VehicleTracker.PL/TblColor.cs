using System;
using System.Collections.Generic;

#nullable disable

namespace ART.VehicleTracker.PL
{
    public partial class tblColor
    {
        public tblColor()
        {
            tblVehicles = new HashSet<tblVehicle>();
        }

        public Guid Id { get; set; }
        public string Description { get; set; }
        public int Code { get; set; }

        public virtual ICollection<tblVehicle> tblVehicles { get; set; }
    }
}
