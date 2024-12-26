using AutoMapper;
using Domain.DTOs;
using Domain.EnumsResult;
using Services.Repositories.Interfaces;
using Services.Services.Interfaces;

namespace Prueba_Tecnica_Net.Services
{
    public class RetailerService : IRetailerService
    {
        IRetailerRepository RetailerRepository;
        IMapper Mapper;
        IExternalApiService ExternalApi;

        public RetailerService(IRetailerRepository retailerRepository, IMapper mapper, IExternalApiService externalApi)
        {
            RetailerRepository = retailerRepository;
            Mapper = mapper;
            ExternalApi = externalApi;
        }

        /// <summary>
        /// Get retailers data from an external api and then insert in database
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public async Task<ResultData> ImportRetailers(string url)
        {
            try
            {
                var list = await ExternalApi.GetRetailers(url);

                if (list == null || list.Count() == 0)
                    return ResultData.EmptyContent;

                await RetailerRepository.AddRetailers(list);

                return ResultData.Content;
            }
            catch (Exception ex)
            {
                throw;
            }

        }

        public async Task<RetailerDto> GetRetailerById(int id)
        {
            try
            {
                var retailer = await RetailerRepository.GetRetailerById(id);

                return Mapper.Map<RetailerDto>(retailer);
            }
            catch(Exception ex)
            {
                throw;
            }
        }

        public async Task DeleteAllAsync()
        {
            try
            {
                await RetailerRepository.DeleteAllAsync();
            }
            catch(Exception ex)
            {
                throw;
            }            
        }

        public async Task<IEnumerable<RetailerDto>> GetRetailersAsync(string? code, string? country, string? name)
        {
            try
            {
                var retailers = await RetailerRepository.GetRetailersAsync(code, country, name);

                return Mapper.Map<IEnumerable<RetailerDto>>(retailers);
            }
            catch(Exception ex)
            {
                throw;
            }

        }



    }
}
