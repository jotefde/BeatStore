using BeatStore.API.DTO.Responses;
using BeatStore.API.Entities;
using BeatStore.API.Helpers;
using BeatStore.API.Interfaces.Services;
using BeatStore.API.Interfaces.Repositories;
using System;
using System.IO;
using System.Xml.Linq;

namespace BeatStore.API.UseCases.Stock
{
    public class CreateStockUseCase : ABaseUseCase
    {
        private readonly IStockRepository _stockRepository;
        public CreateStockUseCase(IStockRepository stockRepository)
        {
            _stockRepository = stockRepository;
        }

        public async Task Handle(Entities.Stock stock)
        {
            try
            {
                stock.Id = Guid.NewGuid().ToString();
                var response = await _stockRepository.Create(stock);
                OutputPort = response;
            }
            catch(Exception e)
            {
                OutputPort = new StandardResponse(e.Message, System.Net.HttpStatusCode.InternalServerError);
            }
        }
    }
}
