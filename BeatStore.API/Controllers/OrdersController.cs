using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BeatStore.API.Context;
using BeatStore.API.Entities;
using BeatStore.API.DTO.Responses;
using BeatStore.API.DTO.Requests.Orders;
using BeatStore.API.UseCases.Orders;
using BeatStore.API.Helpers.Enums;
using System.Net;

namespace BeatStore.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        #region UseCases
        private readonly CreateOrderUseCase _createOrderUseCase;
        #endregion

        public OrdersController(CreateOrderUseCase createOrderUseCase)
        {
            _createOrderUseCase = createOrderUseCase;
        }

        #region POST /orders
        [HttpPost]
        public async Task<ActionResult> MakeOrder([FromBody] MakeOrderRequest orderModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (orderModel.Items?.Count() < 1)
                return new StandardResponse("Item list is empty", HttpStatusCode.BadRequest)
                    .GetResult();

            foreach (var trackId in orderModel.Items)
            {
                var isGUIDValid = Guid.TryParse(trackId, out _);
                if (!isGUIDValid)
                    return new StandardResponse("Wrong Id format", HttpStatusCode.BadRequest)
                        .GetResult();
            }

            var order = new OrderDetails 
            {
                Description= orderModel.Description,
                CurrencyCode= orderModel.CurrencyCode,
                PayMethod= orderModel.PayMethod,
                CustomerIP = HttpContext.Connection.RemoteIpAddress?.ToString(),
                CustomerEmail = orderModel.CustomerEmail,
                CustomerPhone = orderModel.CustomerPhone,
                CustomerFirstName= orderModel.CustomerFirstName,
                CustomerLastName= orderModel.CustomerLastName
            };

            await _createOrderUseCase.Handle(order, orderModel.Items);
            return _createOrderUseCase.OutputPort.GetResult();
        }
        #endregion
    }
}
