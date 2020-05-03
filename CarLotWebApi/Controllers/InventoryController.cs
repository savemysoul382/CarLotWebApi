using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using AutoLotDAL.Models;
using AutoLotDAL.Repos;
using AutoMapper;

namespace CarLotWebApi.Controllers
{
    [RoutePrefix("api/Inventory")]
    public class InventoryController : ApiController
    {
        private readonly inventoryRepo repo = new inventoryRepo();

        public InventoryController()
        {
            Mapper.Initialize(
                cfg =>
                {
                    cfg.CreateMap<inventory, inventory>()
                        .ForMember(x => x.Orders, opt => opt.Ignore());
                });
        }

        // GET: api/Inventory
        [HttpGet, Route("")]
        public IEnumerable<inventory> Getlnventory()
        {
            var inventories = this.repo.GetAll();
            return Mapper.Map<List<inventory>, List<inventory>>(inventories);
        }

        // GET: api/Inventory/5
        [HttpGet, Route("{id}", Name = "DisplayRoute")]
        [ResponseType(typeof(inventory))]
        public IHttpActionResult GetInventory(int id)
        {
            inventory inventory = this.repo.GetOne(id);
            if (inventory == null)
            {
                return NotFound();
            }
            return Ok(Mapper.Map<inventory, inventory>(inventory));
        }

        [HttpPut, Route("{id}")]
        [ResponseType(typeof(void))]
        public IHttpActionResult PutInventory(int id, inventory inventory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (id != inventory.Id)
            {
                return BadRequest();
            }

            try
            {
                this.repo.Save(inventory);
            }
            catch (Exception ex)
            {
                // В производственном приложении здесь должны быть дополнительные действия
                throw;
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // DELETE: api/Inventory/5
        [HttpDelete, Route("{id}")]
        [ResponseType(typeof(void))]
        public IHttpActionResult DeleteInventory(int id, inventory inventory)
        {
            if (id != inventory.Id)
            {
                return BadRequest();
            }

            try
            {
                this.repo.Delete(inventory);
            }
            catch (Exception ex)
            {
                // В производственном приложении здесь должны быть дополнительные действия
                throw;
            }

            return Ok();
        }
        //TestMethod
        //[HttpGet, Route("")]
        //public IEnumerable<String> Get()
        //{
        //    return new String[] {"value1", "value2"};
        //}

            //TestMethod
            // GET api/values/5
            //[HttpGet, Route("{id}")]
            //public String Get(Int32 id)
            //{
            //    return id.ToString();
            //}

            //TestMethod
            //[HttpGet, Route("Response")]
            //public HttpResponseMessage GetResponseMessage()
            //{
            //    HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, "value");
            //    response.Content = new StringContent("hello", Encoding.Unicode);
            //    response.Headers.CacheControl = new CacheControlHeaderValue()
            //    {
            //        MaxAge = TimeSpan.FromMinutes(20)
            //    };
            //    return response;
            //}

        protected override void Dispose(Boolean disposing)
        {
            if (disposing)
            {
                this.repo.Dispose();
            }

            base.Dispose(disposing);
        }
    }
}