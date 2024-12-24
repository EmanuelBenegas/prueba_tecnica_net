using System.ComponentModel.DataAnnotations;
using Domain.DTOs;
using Domain.EnumsResult;
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

        private readonly ILogger<RetailerController> Logger;
        public RetailerController(IRetailerService retailerService, IOptions<ApiRetailerOptions> options, ILogger<RetailerController> logger) 
        { 
            RetailerService = retailerService;
            Options = options;
            Logger = logger;
        }

        /// <summary>
        /// Get One Retailer by filter Id, type integer
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("GetById")]
        public async Task<IActionResult> GetById([Required]int id)
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
                Logger.LogError(ex.Message);
                return Problem(ex.Message);
            }
            
        }

        /// <summary>
        /// Get retailers filtered by code, country and name
        /// returns 200 if there is at least one result
        /// returns 204 if there is no data
        /// returns 500 if there is some kind of error
        /// </summary>
        /// <param name="code"></param>
        /// <param name="country"></param>
        /// <param name="name"></param>
        /// <returns></returns>
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
                Logger.LogError(ex.Message);
                return Problem(ex.Message);
            }
        }

        /// <summary>
        /// Get retailers from external API and insert the result in Database
        /// </summary>
        /// <returns></returns>
        [HttpPost("ImportRetailersFromAPI")]
        public async Task<IActionResult> InsertRetailersFromApi()
        {
            try
            {
                var result = await RetailerService.ImportRetailers(Options.Value.AllRetailersPath);

                if(result == ResultData.EmptyContent)
                    return NoContent();
                else
                    return Created();
            }
            catch (Exception ex) 
            {
                Logger.LogError(ex.Message);
                return Problem(ex.Message);
            }

            
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpDelete("DeleteRetailers")]
        public async Task<IActionResult> DeleteAll()
        {
            try
            {
                await RetailerService.DeleteAllAsync();

                return Ok();
            }
            catch (Exception ex) 
            {
                Logger.LogError(ex.Message);
                return Problem(ex.Message);
            }

        }
    }
}
