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
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var retailer = await RetailerService.GetRetailerById(id);
                if (retailer == null)
                    return NoContent();
                else
                    return Ok(retailer);
            }
            catch (Exception ex) 
            {
                return Problem(ex.Message);
            }
            
        }

        [HttpGet("Retailers")]
        public async Task<IActionResult> GetRetailers(string? code, string? country, string? name)
        {
            try
            {
                var retailers = await RetailerService.GetRetailersAsync(code, country, name);
                if (retailers == null || retailers.Count() == 0)
                    return NoContent();
                else
                    return Ok(retailers);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpGet("GetRetailersFromAPI")]
        public async Task<IActionResult> InsertRetailersFromApi()
        {
            var result = await RetailerService.GetAllRetailers(Options.Value.AllRetailersPath);

            return Ok();
        }

        [HttpDelete("DeleteRetailers")]
        public async Task<IActionResult> DeleteAll()
        {
            await RetailerService.DeleteAllAsync();

            return Ok();
        }

        ///crear función para delete, try catch para errores por unique
    }
}
