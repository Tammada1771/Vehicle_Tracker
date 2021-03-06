using ART.VehicleTracker.BL.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace ART.VehicleTracker.Web.Controllers
{
    public class VehicleController : Controller
    {
        // GET: VehicleController
        public ActionResult Index()
        {
            HttpClient client = InitializeClient();
            HttpResponseMessage response;
            string result;
            dynamic items;

            response = client.GetAsync("Vehicle").Result;
            result = response.Content.ReadAsStringAsync().Result;
            items = (JArray)JsonConvert.DeserializeObject(result);
            List<Vehicle> vehicles = items.TOObject<List<Vehicle>>();

            return View(vehicles);
        }


        // GET: VehicleController/Create
        public ActionResult Create()
        {
            return View();
        }

        private static HttpClient InitializeClient()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44325/api/");
            return client;
        }

        // POST: VehicleController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Vehicle vehicle)
        {
            try
            {
                HttpResponseMessage response;
                string result;
                dynamic items;

                HttpClient client = InitializeClient();

                response = client.GetAsync("Vehicle/" + vehicle.ColorName).Result;
                result = response.Content.ReadAsStringAsync().Result;
                items = (JArray)JsonConvert.DeserializeObject(result);
                List<Vehicle> vehicles = items.ToObject<List<Vehicle>>();

                return View(nameof(Index), vehicles);
            }
            catch (Exception ex)
            {

                return View(vehicle);
            }
        }

        // GET: VehicleController/Edit/5
        public ActionResult Edit(Guid id)
        {
            HttpResponseMessage response;
            string result;
            dynamic item;

            HttpClient client = InitializeClient();

            response = client.GetAsync("Vehicle/" + id).Result;
            result = response.Content.ReadAsStringAsync().Result;
            item = JsonConvert.DeserializeObject(result);
            Vehicle vehicle = item.ToObject<Vehicle>();


            return View(vehicle);
        }

        // POST: VehicleController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Guid id, Vehicle vehicle)
        {
            try
            {
                HttpClient client = InitializeClient();
                string SerializedObject = JsonConvert.SerializeObject(vehicle);
                var content = new StringContent(SerializedObject);
                content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
                HttpResponseMessage response = client.PutAsync("Vehicle/" + vehicle.Id, content).Result;

                return RedirectToAction(nameof(Index));
            }
            catch(Exception ex)
            {
                return View(vehicle);
            }
        }

        // GET: VehicleController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: VehicleController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
