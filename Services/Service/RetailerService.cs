using AutoMapper;
using Domain.DTOs;
using Domain.EnumsResult;
using Services.Repositories.Interfaces;
using Services.Services.Interfaces;

namespace Services.Services.Service
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
            var retailer = await RetailerRepository.GetRetailerById(id);

            return Mapper.Map<RetailerDto>(retailer);
        }

        public async Task DeleteAllAsync()
        {
            await RetailerRepository.DeleteAllAsync();
        }

        public async Task<IEnumerable<RetailerDto>> GetRetailersAsync(string? code, string? country, string? name)
        {
            var retailers = await RetailerRepository.GetRetailersAsync(code, country, name);

            return Mapper.Map<IEnumerable<RetailerDto>>(retailers);
        }



    }
}
