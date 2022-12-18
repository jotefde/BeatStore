namespace BeatStore.API.DTO.PayU.Requests
{
    public class NewPaymentRequest
    {
        public NewPaymentRequest(string customerIp, string description, string currencyCode, string totalAmount, Buyer buyer, List<Product> products)
        {
            this.notifyUrl = "";
            this.customerIp = customerIp;
            this.merchantPosId = "";
            this.description = description;
            this.currencyCode = currencyCode;
            this.totalAmount = totalAmount;
            this.buyer = buyer;
            this.products = products;
        }

        public string notifyUrl { get; set; }
        public string customerIp { get; set; }
        public string merchantPosId { get; set; }
        public string description { get; set; }
        public string currencyCode { get; set; }
        public string totalAmount { get; set; }
        public Buyer buyer { get; set; }
        public List<Product> products { get; set; }
    }


}
