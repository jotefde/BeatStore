using BeatStore.API.DTO.Responses;
using BeatStore.API.Interfaces.Repositories;

namespace BeatStore.API.UseCases.Stock
{
    public class ListAllStockUseCase : ABaseUseCase
    {
        private readonly IStockRepository _stockRepository;
        public ListAllStockUseCase(IStockRepository stockRepository)
        {
            _stockRepository = stockRepository;
        }
        public async Task<bool> Handle()
        {
            try
            {
                var response = await _stockRepository.GetAll();
                OutputPort = response;
                return response != null;
            }
            catch(Exception e)
            {
                OutputPort = new StandardResponse(e.Message, 500);
                return true;
            }
        }
    }
}
