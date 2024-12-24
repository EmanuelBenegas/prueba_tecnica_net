using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Services.Repositories.Interfaces
{
    public interface IRetailerRepository
    {
        public Task<Retailer?> GetRetailerById(int id);

        Task DeleteAllAsync();

        Task AddRetailers(IEnumerable<Retailer> retailers);

        Task<IEnumerable<Retailer>> GetRetailersAsync(string? code, string? country, string? name);
    }
}
