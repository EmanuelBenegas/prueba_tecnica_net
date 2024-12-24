using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.DTOs;
using Domain.Entities;
using Domain.EnumsResult;

namespace Services.Services.Interfaces
{
    public interface IRetailerService
    {
        Task<RetailerDto> GetRetailerById(int id);

        Task<ResultData> ImportRetailers(string url);

        Task DeleteAllAsync();

        Task<IEnumerable<RetailerDto>> GetRetailersAsync(string? code, string? country, string? name);
    }
}
