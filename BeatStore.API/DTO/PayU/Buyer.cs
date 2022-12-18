namespace BeatStore.API.DTO.PayU
{
    public class Buyer
    {
        public Buyer() { }
        public string email { get; set; }
        public string phone { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string language { get; set; }
        public Buyer(string email, string phone, string firstName, string lastName, string language = "pl")
        {
            this.email = email;
            this.phone = phone;
            this.firstName = firstName;
            this.lastName = lastName;
            this.language = language;
        }
    }
}
