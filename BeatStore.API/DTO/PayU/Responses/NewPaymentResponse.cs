namespace BeatStore.API.DTO.PayU.Responses
{
    public class NewPaymentResponse
    {
        public class Status
        {
            public string statusCode { get; set; }
            public string code { get; set; }
            public string statusDesc { get; set; }
        }

        public string redirectUri { get; set; }
        public string orderId { get; set; }
        public Status status { get; set; }
    }
}
