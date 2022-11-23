namespace UbyTECAPI.Models
{
    public class OrderView
    {

        public string ID { get; set; }
        public string RestID { get; set; }
        public string RestName { get; set; }
        public string ClienteID { get; set; }
        public string ClienteN { get; set; }
        public string ClienteLN { get; set; }
        public string Province { get; set; }
        public string Canton { get; set; }
        public string District { get; set; }
        public string Status { get; set; }
        public string Price { get; set; }
        public string[] Products { get; set; }
        public string[] ProductPrices { get; set; }
        public string[] ProductQuantities { get; set; }
        public string[] ProductIDs { get; set; }
    }
}
