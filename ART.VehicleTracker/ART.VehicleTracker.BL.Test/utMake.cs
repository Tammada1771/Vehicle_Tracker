using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace ART.VehicleTracker.BL.Test
{
    [TestClass]
    public class utMake
    {
        [TestMethod]
        public void LoadTest()
        {
            //Async call to an Async method
            Task.Run(async () =>
            {
                var task = await MakeManager.Load();
                IEnumerable<Models.Make> makes = task;
                Assert.AreEqual(3, makes.ToList().Count);
            });

            //Sync call to an Async method
            //var task = MakeManager.Load();
            //task.Wait();
            //IEnumerable<Models.Make> makelist = task.Result;
            //Assert.AreEqual(3, makelist.ToList().Count);
            
        }

        [TestMethod]
        public void InsertTest()
        {
            Task.Run(async () =>
            {
                int results = await MakeManager.Insert(new Models.Make { Description = "NewMake" }, true);
                Assert.IsTrue(results > 0);
            });
        }


        [TestMethod]
        public void UpdateTest()
        {
            Task.Run(async () =>
            {
                var task = await MakeManager.Load();
                IEnumerable<Models.Make> makes = task;
                Models.Make make = makes.FirstOrDefault(m => m.Description == "NewMake");
                make.Description = "UpdateMake";
                int results = await MakeManager.Update(make, true);
                Assert.IsTrue(results > 0);
            });
        }

        [TestMethod]
        public void DeleteTest()
        {
            Task.Run(async () =>
            {
                var task = await MakeManager.Load();
                IEnumerable<Models.Make> makes = task;
                Models.Make make = makes.FirstOrDefault(m => m.Description == "UpdateMake");
                int results = await MakeManager.Delete(make.Id);
                Assert.IsTrue(results > 0);
            });
        }
    }
}
