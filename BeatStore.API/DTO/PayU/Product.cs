namespace BeatStore.API.DTO.PayU
{
    public class Product
    {
        public string name { get; set; }
        public string unitPrice { get; set; }
        public string quantity { get; set; }
        public Product() { }
        public Product(string name, string unitPrice, string quantity)
        {
            this.name = name;
            this.unitPrice = unitPrice;
            this.quantity = quantity;
        }
    }
}
