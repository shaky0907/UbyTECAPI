using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Npgsql;
using System.Data;
using UbyTECAPI.Models;

namespace UbyTECAPI.Controllers
{
    [Route("producto")]
    [ApiController]
    public class ProductoController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public ProductoController(IConfiguration configuration)
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
        public JsonResult Get()
        {
            string query = @"select producto.id_producto as ""ID"", cedula_a as ""AffiliateID"",nombre as ""Name"",categoria as ""Category"",
                            precio as ""Price"", t.""Picture""
                            from producto left join 
	                            (select distinct on (id_producto) foto as ""Picture"", id_producto
	                             from fotos_producto
	                             order by id_producto) as t
                            on t.id_producto = producto.id_producto";

            DataTable table = execquery(query);

            return new JsonResult(table);

        }

        [HttpGet]
        [Route("get/{id}")]
        public JsonResult Get(string id)
        {
            string query = @"select producto.id_producto as ""ID"", cedula_a as ""AffiliateID"",nombre as ""Name"",categoria as ""Category"",
                            precio as ""Price"", t.""Picture""
                            from producto left join 
	                            (select distinct on (id_producto) foto as ""Picture"", id_producto
	                             from fotos_producto
	                             order by id_producto) as t
                            on t.id_producto = producto.id_producto
                            where id_producto = " + id + ";";

            DataTable table = execquery(query);

            return new JsonResult(table);

        }


        [HttpGet]
        [Route("get_afiliado/{id}")]
        public JsonResult Get2(string id)
        {
            string query = @"select producto.id_producto as ""ID"", cedula_a as ""AffiliateID"",nombre as ""Name"",categoria as ""Category"",
                            precio as ""Price"", t.""Picture""
                            from producto left join 
	                            (select distinct on (id_producto) foto as ""Picture"", id_producto
	                             from fotos_producto
	                             order by id_producto) as t
                            on t.id_producto = producto.id_producto
                            where cedula_a = '" + id + "';";

            DataTable table = execquery(query);

            return new JsonResult(table);

        }

        [HttpPost]
        [Route("guardar")]
        public JsonResult Post(Producto prod)
        {
            
            string query = @"Insert into producto (cedula_a,nombre,categoria,precio)
                             Values  ('" + prod.AffiliateID + "','" + prod.Name + "','" + prod.Category + "'," + prod.Price + @");";

            execquery(query);

            query = @"Insert into fotos_producto
                             Values  (" + prod.ID + ",'" + prod.Picture + @"');";

            execquery(query);

            return new JsonResult("Insert Success");

        }


        [HttpPut]
        [Route("update")]
        public JsonResult Put(Producto prod)
        {
            string query = @"
                        update producto set 
                        cedula_a = '" + prod.AffiliateID + @"',
                        nombre= '" + prod.Name + @"',
                        categoria = '" + prod.Category + @"',
                        precio = " + prod.Price + @"
                        where id_producto = " + prod.ID + @"
                        ";

            execquery(query);

            query = @"
                        update fotos_producto set 
                        foto = '" + prod.Picture + @"'
                        where id_producto = " + prod.ID + @"
                        ";

            execquery(query);

            return new JsonResult("Update Success");

        }


        [HttpDelete]
        [Route("delete/{id}")]
        public JsonResult Delete(string id)
        {
            string query = @"delete from producto
                                 where producto.id_producto = " + id + "";

            execquery(query);

            query = @"delete from fotos_producto
                                 where fotos_producto.id_producto = " + id + "";

            return new JsonResult("Delete Success");

        }




    }
}
