using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.DTOs;
using Domain.Entities;
using Newtonsoft.Json;
using Services.Services.Interfaces;

namespace Prueba_Tecnica_Net.Services
{
    public class ExternalApiService : IExternalApiService
    {
        public async Task<IEnumerable<Retailer>?> GetRetailers(string url)
        {
            IEnumerable<Retailer> retailers = new List<Retailer>();

            try
            {
                var httpClient = new HttpClient();
                var jsonResponse = await httpClient.GetStringAsync(url);

                return string.IsNullOrEmpty(jsonResponse) ? retailers : JsonConvert.DeserializeObject<List<Retailer>>(jsonResponse);
            }
            catch (Exception ex)
            {
                throw new Exception("Error connecting to external service");
            }
        }
    }
}
