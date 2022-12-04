﻿using BeatStore.API.DTO.Responses;
using BeatStore.API.Interfaces.Factories;
using BeatStore.API.Interfaces.Repositories;
using System.Diagnostics;

namespace BeatStore.API.UseCases.Stock
{
    public class GetStockUseCase : ABaseUseCase
    {
        private readonly IStockRepository _stockRepository;
        public GetStockUseCase(IStockRepository stockRepository)
        {
            _stockRepository = stockRepository;
        }

        public async Task Handle(string id)
        {
            try
            {
                var response = await _stockRepository.GetById(id);
                OutputPort = response;
            }
            catch(Exception e)
            {
                OutputPort = new StandardResponse(e.Message, System.Net.HttpStatusCode.InternalServerError);
            }
        }
    }
}
