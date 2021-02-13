using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace ART.VehicleTracker.BL.Test
{
    [TestClass]
    public class utVehicle
    {
        [TestMethod]
        public void LoadTest()
        {
            //Async call to an Async method
            Task.Run(async () =>
            {
                var task = await VehicleManager.Load();
                IEnumerable<Models.Vehicle> vehicles = task;
                Assert.AreEqual(3, vehicles.ToList().Count);
            });

            //Sync call to an Async method
            //var task = VehicleManager.Load();
            //task.Wait();
            //IEnumerable<Vehicles.Vehicle> vehiclelist = task.Result;
            //Assert.AreEqual(3, vehiclelist.ToList().Count);
            
        }

        [TestMethod]
        public void InsertTest()
        {
            Task.Run(async () =>
            {
                int results = await VehicleManager.Insert(new Models.Vehicle { ColorId = new Guid(), MakeId = new Guid(), ModelId = new Guid(), VIN = "NewVIN", Year = -99 }, true);
                Assert.IsTrue(results > 0);
            });
        }

        [TestMethod]
        public void InsertFailedTest()
        {
            Task.Run(async () =>
            {
                int results = await VehicleManager.Insert(new Models.Vehicle { ColorId = new Guid() }, true);
                Assert.IsTrue(results > 0);
            });
        }


        [TestMethod]
        public void UpdateTest()
        {
            Task.Run(async () =>
            {
                var task = await VehicleManager.Load();
                IEnumerable<Models.Vehicle> vehicles = task;
                Models.Vehicle vehicle = vehicles.FirstOrDefault(v => v.Year == -99);
                vehicle.Year = -100;
                int results = await VehicleManager.Update(vehicle, true);
                Assert.IsTrue(results > 0);
            });
        }

        [TestMethod]
        public void DeleteTest()
        {
            Task.Run(async () =>
            {
                var task = await VehicleManager.Load();
                IEnumerable<Models.Vehicle> vehicles = task;
                Models.Vehicle vehicle = vehicles.FirstOrDefault(v => v.Year == -100);
                int results = await VehicleManager.Delete(vehicle.Id);
                Assert.IsTrue(results > 0);
            });
        }
    }
}
