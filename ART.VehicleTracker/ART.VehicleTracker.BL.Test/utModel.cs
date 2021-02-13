using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace ART.VehicleTracker.BL.Test
{
    [TestClass]
    public class utModel
    {
        [TestMethod]
        public void LoadTest()
        {
            //Async call to an Async method
            Task.Run(async () =>
            {
                var task = await ModelManager.Load();
                IEnumerable<Models.Model> models = task;
                Assert.AreEqual(3, models.ToList().Count);
            });

            //Sync call to an Async method
            //var task = ModelManager.Load();
            //task.Wait();
            //IEnumerable<Models.Model> modellist = task.Result;
            //Assert.AreEqual(3, modellist.ToList().Count);
            
        }

        [TestMethod]
        public void InsertTest()
        {
            Task.Run(async () =>
            {
                int results = await ModelManager.Insert(new Models.Model { Description = "NewModel" }, true);
                Assert.IsTrue(results > 0);
            });
        }


        [TestMethod]
        public void UpdateTest()
        {
            Task.Run(async () =>
            {
                var task = await ModelManager.Load();
                IEnumerable<Models.Model> models = task;
                Models.Model model = models.FirstOrDefault(m => m.Description == "NewModel");
                model.Description = "UpdateModel";
                int results = await ModelManager.Update(model, true);
                Assert.IsTrue(results > 0);
            });
        }

        [TestMethod]
        public void DeleteTest()
        {
            Task.Run(async () =>
            {
                var task = await ModelManager.Load();
                IEnumerable<Models.Model> models = task;
                Models.Model model = models.FirstOrDefault(m => m.Description == "UpdateModel");
                int results = await ModelManager.Delete(model.Id);
                Assert.IsTrue(results > 0);
            });
        }
    }
}
