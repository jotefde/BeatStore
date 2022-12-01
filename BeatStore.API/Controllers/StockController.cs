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

namespace BeatStore.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class StockController : ControllerBase
    {
        #region UseCases
        GetStockUseCase _getStockUseCase;
        ListAllStockUseCase _listAllStockUseCase;
        CreateStockUseCase _createStockUseCase;
        UpdateStockUseCase _updateStockUseCase;
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
            var result = await _listAllStockUseCase.Handle();
            if (result)
                return _listAllStockUseCase.OutputPort?.Data;
            return new StandardResponse("OutputPort is empty", 500).Data;
        }
        #endregion

        #region GET /stock/:id
        [HttpGet("{stockId}")]
        public async Task<ActionResult> GetStock([FromRoute] string stockId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var isGUIDValid = Guid.TryParse(stockId, out _);
            if (!isGUIDValid)
                return new StandardResponse("Wrong Id format", 400).Data;

            var result = await _getStockUseCase.Handle(stockId);
            if (result)
                return _getStockUseCase.OutputPort?.Data;
            return new StandardResponse("OutputPort is empty", 500).Data;
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
            var result = await _createStockUseCase.Handle(stock);
            if (result)
                return _createStockUseCase.OutputPort?.Data;
            return new StandardResponse("OutputPort is empty", 500).Data;
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

            var result = await _updateStockUseCase.Handle(stock);
            if (result)
                return _updateStockUseCase.OutputPort?.Data;
            return new StandardResponse("OutputPort is empty", 500).Data;
        }
        #endregion
    }
}
