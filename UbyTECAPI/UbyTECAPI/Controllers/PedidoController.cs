using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Npgsql;
using System.Data;
using UbyTECAPI.Models;

namespace UbyTECAPI.Controllers
{
    [Route("cart-pedido")]
    [ApiController]
    public class PedidoController : ControllerBase
    {


        private readonly IConfiguration _configuration;
        public PedidoController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        private DataTable execquery(string query)
        {
            DataTable table = new DataTable();

            string sqlDataSource = _configuration.GetConnectionString("UbyTecCon");

            NpgsqlDataReader myReader;

            using (NpgsqlConnection myCon = new NpgsqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (NpgsqlCommand myCommand = new NpgsqlCommand(query, myCon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    myCon.Close();
                }
            }

            return table;

        }

        [HttpGet]
        [Route("get")]
        public string Get()
        {
            string query = @"select ca.num_carrito as ""ID"", ca.cedula_c as ""ClienteID"", 
                            C.nombre as ""ClienteN"",C.apellido1 as ""ClienteLN"",
                            C.provincia as ""Province"", C.canton as ""Canton"", C.distrito as ""District""
                            from carrito as Ca,cliente as C";

            DataTable table = execquery(query);

            string json = JsonConvert.SerializeObject(table);
            List<OrderView> orders = JsonConvert.DeserializeObject<List<OrderView>>(json);

            foreach (OrderView order in orders)
            {

                query = @"select ""ID"",""Name"",""Price"",""Cantidad"",""Cedula_a""
                          from productoview
                          where ""ID_Carrito"" = "+ order.ID +"";

                DataTable pxctable = execquery(query);
                string jsonpxc = JsonConvert.SerializeObject(pxctable);
                List<ProductoView> pxc = JsonConvert.DeserializeObject<List<ProductoView>>(jsonpxc);

                string[] Names = new string[pxc.Count];
                string[] Prices = new string[pxc.Count];
                string[] Quant = new string[pxc.Count];
                string[] Ids = new string[pxc.Count];
                int i = 0;
                int price = 0;
                string cedula_a = "";
                foreach (ProductoView prod in pxc)
                {
                    Names[i] = prod.Name;
                    Prices[i] = prod.Price;
                    Quant[i] = prod.Cantidad;
                    Ids[i] = prod.ID;
                    price += Int32.Parse(prod.Price)* Int32.Parse(prod.Cantidad);

                    cedula_a = prod.Cedula_a;
                    i++;
                }

                order.Products = Names;
                order.ProductPrices = Prices;
                order.ProductQuantities= Quant;
                order.ProductIDs = Ids;


                order.Price = price.ToString();

                query = @"select cedula_j as ""ID"", nombre as ""Name""
                          from afiliado
                          where cedula_j = '"+ cedula_a+"';";

                DataTable atable = execquery(query);
                string jsonaf = JsonConvert.SerializeObject(atable);
                List<Afiliado> af = JsonConvert.DeserializeObject<List<Afiliado>>(jsonaf);

                order.RestID = af[0].ID;
                order.RestName = af[0].Name;

                query = @"select estado as ""Estado""
                          from pedido
                          where id_pedido = " + order.ID;


                DataTable ptable = execquery(query);

                if(ptable.Rows.Count > 0)
                {
                    string jsonP = JsonConvert.SerializeObject(ptable);
                    List<Pedido> pedidos = JsonConvert.DeserializeObject<List<Pedido>>(jsonP);
                    order.Status = pedidos[0].Estado;
                }
                else
                {
                    order.Status = "pending";
                }




            }




            var jsonC = JsonConvert.SerializeObject(orders, Formatting.Indented);

            return jsonC;

        }

        [HttpGet]
        [Route("get/{id}")]
        public string Get(string id)
        {
            string query = @"select ca.num_carrito as ""ID"", ca.cedula_c as ""ClienteID"", 
                            C.nombre as ""ClienteN"",C.apellido1 as ""ClienteLN"",
                            C.provincia as ""Province"", C.canton as ""Canton"", C.distrito as ""District""
                            from carrito as Ca,cliente as C
                            where ca.num_carrito = " + id;

            DataTable table = execquery(query);

            string json = JsonConvert.SerializeObject(table);
            List<OrderView> orders = JsonConvert.DeserializeObject<List<OrderView>>(json);

            foreach (OrderView order in orders)
            {

                query = @"select ""ID"",""Name"",""Price"",""Cantidad"",""Cedula_a""
                          from productoview
                          where ""ID_Carrito"" = " + order.ID + "";

                DataTable pxctable = execquery(query);
                string jsonpxc = JsonConvert.SerializeObject(pxctable);
                List<ProductoView> pxc = JsonConvert.DeserializeObject<List<ProductoView>>(jsonpxc);

                string[] Names = new string[pxc.Count];
                string[] Prices = new string[pxc.Count];
                string[] Quant = new string[pxc.Count];
                string[] Ids = new string[pxc.Count];
                int i = 0;
                int price = 0;
                string cedula_a = "";
                foreach (ProductoView prod in pxc)
                {
                    Names[i] = prod.Name;
                    Prices[i] = prod.Price;
                    Quant[i] = prod.Cantidad;
                    Ids[i] = prod.ID;
                    price += Int32.Parse(prod.Price) * Int32.Parse(prod.Cantidad);

                    cedula_a = prod.Cedula_a;
                    i++;
                }

                order.Products = Names;
                order.ProductPrices = Prices;
                order.ProductQuantities = Quant;
                order.ProductIDs = Ids;


                order.Price = price.ToString();

                query = @"select cedula_j as ""ID"", nombre as ""Name""
                          from afiliado
                          where cedula_j = '" + cedula_a + "';";

                DataTable atable = execquery(query);
                string jsonaf = JsonConvert.SerializeObject(atable);
                List<Afiliado> af = JsonConvert.DeserializeObject<List<Afiliado>>(jsonaf);

                order.RestID = af[0].ID;
                order.RestName = af[0].Name;

                query = @"select estado as ""Estado""
                          from pedido
                          where id_pedido = " + order.ID;


                DataTable ptable = execquery(query);

                if (ptable.Rows.Count > 0)
                {
                    string jsonP = JsonConvert.SerializeObject(ptable);
                    List<Pedido> pedidos = JsonConvert.DeserializeObject<List<Pedido>>(jsonP);
                    order.Status = pedidos[0].Estado;
                }
                else
                {
                    order.Status = "pending";
                }
            }
            var jsonC = JsonConvert.SerializeObject(orders, Formatting.Indented);

            return jsonC;

        }

        [HttpPost]
        [Route("guardar")]
        public JsonResult Post(Repartidor rep)
        {
            /*
            string query = @"insert into admleado 
                             ('" + adm.ID + "','" + adm.FirstN + "','" + adm.FirstLN + "','" + adm.SecondLN + "','" + adm.Username + "','" + adm.Password + "','" + adm.Province + "','" + adm.Canton + "','" + adm.District + "'," + adm.ProfilePic + @")";
            */
            string query = @"Insert into repartidor
                             Values  ('" + rep.ID + "','" + rep.FirstN + "','" + rep.FirstLN + "','" + rep.SecondLN + "','" + rep.Username + "','" + rep.Password + "','" + rep.Email + "','" + rep.Province + "','" + rep.Canton + "','" + rep.District + "','" + rep.Status + @"');";

            DataTable table = execquery(query);

            return new JsonResult("Insert Success");

        }




        [HttpPut]
        [Route("crear_carrito")]
        public JsonResult carrito(NewOrder ord)
        {
            string query = @"insert into carrito (cedula_c)
                             values ('" + ord.ID_cliente + "')";

            execquery(query);

            query = @"select num_carrito as ""Num_Carrito""
                      from carrito
                      order by num_carrito ASC";

            DataTable table = execquery(query);
            string jsonP = JsonConvert.SerializeObject(table);
            List<Carrito> carrito = JsonConvert.DeserializeObject<List<Carrito>>(jsonP);

            query = @"insert into productoxcarrito
                      values("+ ord.ID_producto + ", " + carrito[0].Num_Carrito +","+ ord.Cantidad+")";

            execquery(query);

            return new JsonResult(carrito[0].Num_Carrito);

        }



        [HttpPut]
        [Route("add_product")]
        public JsonResult addproduct2cart(NewOrder ord)
        {
            

            string query = @"insert into productoxcarrito
                      values(" + ord.ID_producto + ", " + ord.ID_Carrito + "," + ord.Cantidad + ")";

            execquery(query);

            return new JsonResult("Insert Success");

        }



        [HttpGet]
        [Route("crear_pedido/{id}")]
        public JsonResult createPedido(string id)
        {
            string query = @"call hacerpedido("+ id+")";

            DataTable table = execquery(query);

            return new JsonResult("Crear pedido Success");

        }


        [HttpGet]
        [Route("alistar_pedido/{id}")]
        public JsonResult alistarPedido(string id)
        {
            string query = @"call pedidoelaborado(" + id + ")";

            DataTable table = execquery(query);

            return new JsonResult("alistar pedido Success");

        }


        [HttpGet]
        [Route("recibir_pedido/{id}")]
        public JsonResult recibirPedido(string id)
        {
            string query = @"call pedidoentregado(" + id + ")";

            DataTable table = execquery(query);

            return new JsonResult("alistar pedido Success");

        }





        [HttpDelete]
        [Route("delete/{id}")]
        public JsonResult Delete(string id)
        {
            string query = @"delete from pedido
                             where id_pedido = " + id + "";

            DataTable table = execquery(query);

            return new JsonResult("Delete Success");

        }




    }
}
