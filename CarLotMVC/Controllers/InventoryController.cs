using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using AutoLotDAL.Models;
using Newtonsoft.Json;


namespace CarLotMVC.Controllers
{
    public class InventoryController : Controller
    {
        private const String BaseUrl = "http://localhost:61981/api/Inventory";

        // GET: Inventory
        public async Task<ActionResult> Index()
        {
            var client = new HttpClient();
            var response = await client.GetAsync(BaseUrl);
            if (response.IsSuccessStatusCode)
            {
                var items = JsonConvert.DeserializeObject<List<inventory>>(await response.Content.ReadAsStringAsync());
                return View(items);
            }

            return HttpNotFound();
        }

        // GET: Inventory/Details/5
        public async Task<ActionResult> Details(Int32? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var client = new HttpClient();
            var response = await client.GetAsync($"{BaseUrl}/{id.Value}");
            if (response.IsSuccessStatusCode)
            {
                var inventory = JsonConvert.DeserializeObject<inventory>(await response.Content.ReadAsStringAsync());
                return View(inventory);
            }

            return HttpNotFound();
        }

        // GET: Inventory/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Inventory/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Make,Color,PetName")] inventory inventory)
        {
            if (!ModelState.IsValid) return View(inventory);
            try
            {
                var client = new HttpClient();
                String json = JsonConvert.SerializeObject(inventory);
                var response = await client.PostAsync(BaseUrl, new StringContent(json, Encoding.UTF8, "application/json"));
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(String.Empty, $"Unable to create record:{ex.Message}"); // He удается создать запись
            }

            return View(inventory);
        }


        // GET: Inventory/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var client = new HttpClient();
            var response = await client.GetAsync($"{BaseUrl}/{id.Value}");
            if (response.IsSuccessStatusCode)
            {
                var inventory = JsonConvert.DeserializeObject<inventory>(await response.Content.ReadAsStringAsync());
                return View(inventory);
            }

            return new HttpNotFoundResult();
        }


        // POST: Inventory/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Make,Color,PetName,Timestamp")]
            inventory inventory)
        {
            if (!ModelState.IsValid) return View(inventory);
            var client = new HttpClient();
            string json = JsonConvert.SerializeObject(inventory);
            var response = await client.PutAsync(
                $"{BaseUrl}/{inventory.Id}",
                new StringContent(json, Encoding.UTF8, "application/json"));
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }

            return View(inventory);
        }

        // GET: Inventory/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var client = new HttpClient();
            var response = await client.GetAsync($"{BaseUrl}/{id.Value}");
            if (response.IsSuccessStatusCode)
            {
                var inventory = JsonConvert.DeserializeObject<inventory>(await response.Content.ReadAsStringAsync());
                return View(inventory);
            }

            return new HttpNotFoundResult();
        }

        // POST: Inventory/Delete/5
        [HttpPost, ActionName(name: "Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete([Bind(Include = "Id,Timestamp")] inventory inventory)
        {
            try
            {
                var client = new HttpClient();

                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Delete, $"{BaseUrl}/{inventory.Id}")
                {
                    Content = new StringContent(JsonConvert.SerializeObject(inventory), Encoding.UTF8, "application/json")
                };
                var response = await client.SendAsync(request);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"Unable to create record: {ex.Message}");
            }

            return View(inventory);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
            }

            base.Dispose(disposing);
        }
    }
}