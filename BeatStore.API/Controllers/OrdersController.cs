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
using System.Diagnostics;
using BeatStore.API.Interfaces.DTO.Responses;

namespace BeatStore.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        #region UseCases
        private readonly CreateOrderUseCase _createOrderUseCase;
        private readonly UpdateNotificationUseCase _updateNotificationUseCase;
        private readonly SendOwnedProductsLinkUseCase _sendOwnedProductsLinkUseCase;
        #endregion

        public OrdersController(CreateOrderUseCase createOrderUseCase, UpdateNotificationUseCase updateNotificationUseCase, SendOwnedProductsLinkUseCase sendOwnedProductsLinkUseCase)
        {
            _createOrderUseCase = createOrderUseCase;
            _updateNotificationUseCase = updateNotificationUseCase;
            _sendOwnedProductsLinkUseCase = sendOwnedProductsLinkUseCase;
        }

        #region POST /orders
        [HttpPost]
        public async Task<ActionResult> MakeOrder([FromBody] MakeOrderRequest orderModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
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

        #region POST /orders/notify
        [HttpPost("notify")]
        public async Task<ActionResult> NotifyOrder([FromBody] NotifyOrderRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await _updateNotificationUseCase.Handle(request);
            var outputPort = _updateNotificationUseCase.OutputPort as ABaseResponse<OrderDetails>;
            if (outputPort?.Success == true && outputPort?.Data?.Status == OrderStatus.COMPLETED)
            {
                await _sendOwnedProductsLinkUseCase.Handle(outputPort.Data);
                return _sendOwnedProductsLinkUseCase.OutputPort.GetResult();
            }
            return Ok();
        }
        #endregion
    }
}
