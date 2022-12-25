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
        private readonly GetStockByIdUseCase _getStockByIdUseCase;
        private readonly GetStockBySlugUseCase _getStockBySlugUseCase;
        private readonly ListAllStockUseCase _listAllStockUseCase;
        private readonly CreateStockUseCase _createStockUseCase;
        private readonly UpdateStockUseCase _updateStockUseCase;
        #endregion

        public StockController(GetStockByIdUseCase getStockByIdUseCase, ListAllStockUseCase listAllStockUseCase, CreateStockUseCase createStockUseCase, UpdateStockUseCase updateStockUseCase, GetStockBySlugUseCase getStockBySlugUseCase)
        {
            _getStockByIdUseCase = getStockByIdUseCase;
            _listAllStockUseCase = listAllStockUseCase;
            _createStockUseCase = createStockUseCase;
            _updateStockUseCase = updateStockUseCase;
            _getStockBySlugUseCase = getStockBySlugUseCase;
        }

        #region GET /stock/id/:id
        [HttpGet("id/{stockId}")]
        public async Task<ActionResult> GetStockById([FromRoute] [GUID] string stockId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _getStockByIdUseCase.Handle(stockId);
            return _getStockByIdUseCase.OutputPort.GetResult();
        }
        #endregion
        #region GET /stock
        [HttpGet]
        public async Task<ActionResult> GetAllStock()
        {
            await _listAllStockUseCase.Handle();
            return _listAllStockUseCase.OutputPort.GetResult();
        }
        #endregion

        #region GET /stock/slug/:slug
        [HttpGet("slug/{trackSlug}")]
        public async Task<ActionResult> GetStockBySlug([FromRoute] string trackSlug)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _getStockBySlugUseCase.Handle(trackSlug);
            return _getStockBySlugUseCase.OutputPort.GetResult();
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
