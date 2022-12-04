using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BeatStore.API.Context;
using BeatStore.API.Entities;
using BeatStore.API.UseCases.Stock;
using BeatStore.API.DTO.Responses;
using BeatStore.API.DTO.Requests.Stock;
using System.Net;
using BeatStore.API.Extensions.RequestAttributes;

namespace BeatStore.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class StockController : ControllerBase
    {
        #region UseCases
        private readonly GetStockUseCase _getStockUseCase;
        private readonly ListAllStockUseCase _listAllStockUseCase;
        private readonly CreateStockUseCase _createStockUseCase;
        private readonly UpdateStockUseCase _updateStockUseCase;
        #endregion

        public StockController(GetStockUseCase getStockUseCase, ListAllStockUseCase listAllStockUseCase, CreateStockUseCase createStockUseCase, UpdateStockUseCase updateStockUseCase)
        {
            _getStockUseCase = getStockUseCase;
            _listAllStockUseCase = listAllStockUseCase;
            _createStockUseCase = createStockUseCase;
            _updateStockUseCase = updateStockUseCase;
        }


        #region GET /stock
        [HttpGet("stock")]
        public async Task<ActionResult> GetAllStock()
        {
            await _listAllStockUseCase.Handle();
            return _listAllStockUseCase.OutputPort.GetResult();
        }
        #endregion

        #region GET /stock/:id
        [HttpGet("{stockId}")]
        public async Task<ActionResult> GetStock([FromRoute] [GUID] string stockId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _getStockUseCase.Handle(stockId);
            return _getStockUseCase.OutputPort.GetResult();
        }
        #endregion

        #region POST /stock
        [HttpPost]
        public async Task<ActionResult> AddStock([FromBody] AddStockRequest stockModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var stock = new Stock
            {
                Track = new Track { Id = stockModel.TrackId },
                Amount = stockModel.Amount,
                IsUnlimited= stockModel.IsUnlimited,
                IsPublished= stockModel.IsPublished
            };
            await _createStockUseCase.Handle(stock);
            return _createStockUseCase.OutputPort.GetResult();
        }
        #endregion

        #region PUT /stock
        [HttpPut]
        public async Task<ActionResult> ChangeStock([FromBody] ChangeStockRequest stockModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var stock = new Stock { Id = stockModel.StockId };
            if (stockModel.Amount != null)
                stock.Amount = stockModel.Amount.Value;
            if (stockModel.IsUnlimited != null)
                stock.IsUnlimited = stockModel.IsUnlimited.Value;
            if (stockModel.IsPublished != null)
                stock.IsPublished = stockModel.IsPublished.Value;

            await _updateStockUseCase.Handle(stock);
            return _updateStockUseCase.OutputPort.GetResult();
        }
        #endregion
    }
}
