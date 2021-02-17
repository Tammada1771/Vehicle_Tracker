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
    public static class VehicleManager
    {
        public async static Task<int> Insert(Models.Vehicle vehicle, bool rollback = false)
        {
            try
            {
                IDbContextTransaction transaction = null;

                using (VehicleEntities dc = new VehicleEntities())
                {
                    if (rollback) transaction = dc.Database.BeginTransaction();

                    tblVehicle newrow = new tblVehicle();

                    newrow.Id = Guid.NewGuid();
                    newrow.ColorId = vehicle.ColorId;
                    newrow.MakeId = vehicle.MakeId;
                    newrow.ModelId = vehicle.MakeId;
                    newrow.VIN = vehicle.VIN;
                    newrow.Year = vehicle.Year;

                    vehicle.Id = newrow.Id;

                    dc.tblVehicles.Add(newrow);
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

        public async static Task<Guid> Insert( Guid colorId, Guid makeId, Guid modelId, string vin, int year, bool rollback = false)
        {
            try
            {
                Models.Vehicle vehicle = new Models.Vehicle 
                { 
                    ColorId = colorId,
                    MakeId = makeId,
                    ModelId =modelId,
                    VIN = vin,
                    Year = year
                };
                await Insert(vehicle);
                return vehicle.Id;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async static Task<int> Delete(Guid id, bool rollback = false)
        {
            try
            {
                IDbContextTransaction transaction = null;
                using (VehicleEntities dc = new VehicleEntities())
                {
                    tblVehicle row = dc.tblVehicles.FirstOrDefault(c => c.Id == id);
                    int results = 0;
                    if (row != null)
                    {
                        if (rollback) transaction = dc.Database.BeginTransaction();

                        dc.tblVehicles.Remove(row);

                        results = dc.SaveChanges();
                        if (rollback) transaction.Rollback();
                        return results;
                    }
                    else
                    {
                        throw new Exception("Row was not found");
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async static Task<int> Update(Models.Vehicle vehicle, bool rollback = false)
        {
            try
            {
                IDbContextTransaction transaction = null;
                using (VehicleEntities dc = new VehicleEntities())
                {
                    tblVehicle row = dc.tblVehicles.FirstOrDefault(c => c.Id == vehicle.Id);
                    int results = 0;
                    if (row != null)
                    {
                        if (rollback) transaction = dc.Database.BeginTransaction();

                        row.ColorId = vehicle.ColorId;
                        row.MakeId = vehicle.MakeId;
                        row.ModelId = vehicle.ModelId;
                        row.VIN = vehicle.VIN;
                        row.Year = vehicle.Year;

                        results = dc.SaveChanges();
                        if (rollback) transaction.Rollback();
                        return results;
                    }
                    else
                    {
                        throw new Exception("Row was not found");
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async static Task<Models.Vehicle> LoadById(Guid id)
        {
            try
            {
                using (VehicleEntities dc = new VehicleEntities())
                {
                    tblVehicle tblvehicle = dc.tblVehicles.FirstOrDefault(c => c.Id == id);
                    Models.Vehicle vehicle = new Models.Vehicle();

                    if (tblvehicle != null)
                    {
                        vehicle.Id = tblvehicle.Id;
                        vehicle.ColorId = tblvehicle.ColorId;
                        vehicle.MakeId = tblvehicle.MakeId;
                        vehicle.ModelId = tblvehicle.ModelId;
                        vehicle.VIN = tblvehicle.VIN;
                        vehicle.Year = tblvehicle.Year;
                        vehicle.ColorName = tblvehicle.Color.Description;
                        vehicle.MakeName = tblvehicle.Make.Description;
                        vehicle.ModelName = tblvehicle.Model.Description;

                        return vehicle;
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

        public async static Task<IEnumerable<Models.Vehicle>> Load()
        {
            try
            {
                List<Vehicle> vehicles = new List<Vehicle>();

                using (VehicleEntities dc = new VehicleEntities())
                {
                    dc.tblVehicles
                        .ToList()
                        .ForEach(c => vehicles.Add(new Vehicle
                        {
                            Id = c.Id,
                            ColorId = c.ColorId,
                            MakeId = c.MakeId,
                            ModelId = c.ModelId,
                            VIN = c.VIN,
                            Year = c.Year,
                            ColorName = c.Color.Description,
                            MakeName = c.Make.Description,
                            ModelName = c.Model.Description
                }));
                    return vehicles;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


    }
}
