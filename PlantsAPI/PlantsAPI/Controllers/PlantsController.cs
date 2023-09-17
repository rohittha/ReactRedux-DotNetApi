using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PlantsAPI.Models;
using System.Net;
using System.Numerics;

namespace PlantsAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PlantsController : Controller
    {
        private readonly Plants1DbContext _context;
        public PlantsController(Plants1DbContext plants1DbContext)
        {
            _context = plants1DbContext;
        }
        //public IActionResult Index()
        //{
        //    return View();
        //}

        [Route("getallplants")]
        [HttpGet]
        public async Task<IActionResult> GetAllPlants()
        {
            var plants = await _context.Plants.ToListAsync();
            return Ok(plants);
        }


        [Route("setplantstatus")]
        [HttpPost]
        public async Task<IActionResult> SetPlantStatus(Plant plantData)
        {
            try
            {
                var plant = await _context.Plants.FindAsync(plantData.PlantId);
                plant.IsStatusWatering = plantData.IsStatusWatering;

                TimeSpan timeDiff = DateTime.Now - plant.LastWatered;
                if (plant.IsStatusWatering && timeDiff.TotalSeconds < 30)
                {
                    var errorResponse = new HttpResponseMessage(HttpStatusCode.BadRequest)
                    {
                        Content = new StringContent("Cannot water the plant again."),
                        ReasonPhrase = "30SecondLimit"
                    };

                    // Return the custom error response as an IActionResult.
                    return new ObjectResult(errorResponse)
                    {
                        StatusCode = (int)HttpStatusCode.BadRequest
                    };
                }
                else
                {
                    plant.LastWatered = DateTime.Now;
                    _context.Update(plant);
                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            var plants = await _context.Plants.ToListAsync();
            return Ok(plants);
        }

    }
}
