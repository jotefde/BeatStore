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
        public async Task Handle()
        {
            try
            {
                var response = await _stockRepository.GetAll();
                OutputPort = response;
            }
            catch(Exception e)
            {
                OutputPort = new StandardResponse(e.Message, System.Net.HttpStatusCode.InternalServerError);
            }
        }
    }
}
