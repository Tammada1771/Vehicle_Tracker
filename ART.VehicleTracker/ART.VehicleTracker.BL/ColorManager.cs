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

        public async static Task<Guid> Insert(int code, string description, bool rollback = false)
        {
            try
            {
                Models.Color color = new Models.Color { Code = code, Description = description };
                await Insert(color);
                return color.Id;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async static Task<int> Delete(Guid id, bool rollback = false)
        {

        }

        public async static Task<int> Update(Models.Color color, bool rollback = false)
        {

        }

        public async static Task<Models.Color> LoadById(Guid id)
        {
            try
            {
                using(VehicleEntities dc = new VehicleEntities())
                {
                    tblColor tblcolor = dc.tblColors.FirstOrDefault(c => c.Id == id);
                    Models.Color color = new Models.Color();

                    if(tblcolor != null)
                    {
                        color.Id = tblcolor.Id;
                        color.Description = tblcolor.Description;
                        color.Code = tblcolor.Code;
                    }
                    else
                    {
                        throw new Exception("Could not find the row");
                    }
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async static Task<List<Models.Color>> Load()
        {

        }


    }
}
