using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace ART.VehicleTracker.BL.Test
{
    [TestClass]
    public class utColor
    {
        [TestMethod]
        public void LoadTest()
        {
            //Async call to an Async method
            Task.Run(async () =>
            {
                var task = await ColorManager.Load();
                IEnumerable<Models.Color> colors = task;
                Assert.AreEqual(3, colors.ToList().Count);
            });

            //Sync call to an Async method
            //var task = ColorManager.Load();
            //task.Wait();
            //IEnumerable<Models.Color> colorlist = task.Result;
            //Assert.AreEqual(3, colorlist.ToList().Count);
            
        }

        [TestMethod]
        public void InsertTest()
        {
            Task.Run(async () =>
            {
                int results = await ColorManager.Insert(new Models.Color { Code = -99, Description = "NewColor" }, true);
                Assert.IsTrue(results > 0);
            });
        }

        [TestMethod]
        public void InsertFailedTest()
        {
            Task.Run(async () =>
            {
                int results = await ColorManager.Insert(new Models.Color { Code = -99}, true);
                Assert.IsTrue(results > 0);
            });
        }

        [TestMethod]
        public void UpdateTest()
        {
                var task = ColorManager.Load();
                IEnumerable<Models.Color> colors = task.Result;
                Models.Color color = colors.FirstOrDefault(c => c.Code == -99);
                color.Description = "Update Color";
                var results = ColorManager.Update(color, true);
                Assert.IsTrue(results.Result > 0);

        }

        [TestMethod]
        public void DeleteTest()
        {
            var task = ColorManager.Load();
            IEnumerable<Models.Color> colors = task.Result;
            Models.Color color = colors.FirstOrDefault(c => c.Code == -99);
            color.Description = "Update Color";
            var results = ColorManager.Delete(color.Id, true);
            Assert.IsTrue(results.Result > 0);

        }
    }
}
