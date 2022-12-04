using BeatStore.API.DTO.Responses;
using BeatStore.API.Entities;
using BeatStore.API.Helpers;
using BeatStore.API.Interfaces.Factories;
using BeatStore.API.Interfaces.Repositories;
using System;
using System.IO;
using System.Xml.Linq;

namespace BeatStore.API.UseCases.Stock
{
    public class UpdateStockUseCase : ABaseUseCase
    {
        private readonly IStockRepository _stockRepository;
        public UpdateStockUseCase(IStockRepository stockRepository)
        {
            _stockRepository = stockRepository;
        }

        public async Task Handle(Entities.Stock stock)
        {
            try
            {
                var response = await _stockRepository.Update(stock);
                OutputPort = response;
            }
            catch(Exception e)
            {
                OutputPort = new StandardResponse(e.Message, System.Net.HttpStatusCode.InternalServerError);
            }
        }
    }
}
