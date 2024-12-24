using Domain.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Prueba_Tecnica_Net.Options;
using Services.Services.Interfaces;

namespace Prueba_Tecnica_Net.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RetailerController : Controller
    {
        private IRetailerService RetailerService;
        private IOptions<ApiRetailerOptions> Options;
        public RetailerController(IRetailerService retailerService, IOptions<ApiRetailerOptions> options) 
        { 
            RetailerService = retailerService;
            Options = options;
        }

        [HttpGet("GetById")]
        public async Task<RetailerDto> GetById(int id)
        {

            var retail = await RetailerService.GetRetailerById(id);
            return new RetailerDto()
            {
                ReCode = "sdad"
            };
        }

        [HttpGet("Retailers")]
        public async Task<IActionResult> GetRetailers(string code, string country, string name)
        {
            return Ok();
        }

        [HttpGet("GetRetaillersFromAPI")]
        public async Task<IActionResult> InsertRetailersFromApi()
        {
            var result = await RetailerService.GetAllRetailers(Options.Value.AllRetailersPath);

            return Ok();
        }

        ///crear función para delete, try catch para errores por unique
    }
}
