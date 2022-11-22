namespace UbyTECAPI.Models
{
    public class Pedido
    {
        public int ID { get; set; }
        public int ID_Carrito { get; set; }
        public string ID_Repartidor { get; set; }
        public string Estado { get; set; }
        public string Feedback { get; set; }
        public string Comprobante { get; set; }
        public string Province { get; set; }
        public string Canton { get; set; }
        public string District { get; set; }
    }
}
