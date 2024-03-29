﻿using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

namespace BeatStore.API.DTO.PayU
{
    public class Order
    {
        public Order() { }
        public Order(string orderId, string extOrderId, DateTime orderCreateDate, string notifyUrl, PayMethod payMethod, string customerIp, string merchantPosId, string description, string additionalDescription, string currencyCode, string totalAmount, Buyer buyer, List<Product> products, string status)
        {
            this.orderId = orderId;
            this.extOrderId = extOrderId;
            this.orderCreateDate = orderCreateDate;
            this.notifyUrl = notifyUrl;
            this.customerIp = customerIp;
            this.merchantPosId = merchantPosId;
            this.description = description;
            this.additionalDescription = additionalDescription;
            this.currencyCode = currencyCode;
            this.totalAmount = totalAmount;
            this.payMethod = payMethod;
            this.buyer = buyer;
            this.products = products;
            this.status = status;
        }
        public Order(string extOrderId, string customerIp, string additionalDescription, string currencyCode, string totalAmount, Buyer buyer, List<Product> products)
        {
            this.extOrderId = extOrderId;
            this.customerIp = customerIp;
            this.description = "prod. olzoo";
            this.additionalDescription = additionalDescription;
            this.currencyCode = currencyCode;
            this.totalAmount = totalAmount;
            this.buyer = buyer;
            this.products = products;
        }

        public string? orderId { get; set; }
        public string? extOrderId { get; set; }
        public DateTime? orderCreateDate { get; set; }
        public string? notifyUrl { get; set; }
        public string customerIp { get; set; }
        public string? merchantPosId { get; set; }
        public string description { get; set; }
        public string? additionalDescription { get; set; }
        public string currencyCode { get; set; }
        public string totalAmount { get; set; }
        public Buyer? buyer { get; set; }
        public PayMethod? payMethod { get; set; }
        public List<Product> products { get; set; }
        public string status { get; set; }
    }
}
