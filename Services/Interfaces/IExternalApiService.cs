using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Services.Services.Interfaces
{
    public interface IExternalApiService
    {
        Task<IEnumerable<Retailer>?> GetRetailers(string url);
    }
}
