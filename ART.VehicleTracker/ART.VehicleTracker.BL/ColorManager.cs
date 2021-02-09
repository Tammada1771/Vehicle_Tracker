using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ART.VehicleTracker.BL.Models;
using ART.VehicleTracker.PL;
using Microsoft.EntityFrameworkCore.Storage;

namespace ART.VehicleTracker.BL
{
    public static class ColorManager
    {
        public async static Task<int> Insert(Models.Color color, bool rollback = false)
        {
            try
            {
                IDbContextTransaction transaction = null;

                using(VehicleEntities dc = new VehicleEntities())
                {
                    if (rollback) transaction = dc.Database.BeginTransaction();

                    tblColor newrow = new tblColor();
                    newrow.Id = Guid.NewGuid();
                    newrow.Code = color.Code;
                    newrow.Description = color.Description;

                    color.Id = newrow.Id;

                    dc.tblColors.Add(newrow);
                    int results = dc.SaveChanges();

                    if (rollback) transaction.Rollback();

                    return results;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
