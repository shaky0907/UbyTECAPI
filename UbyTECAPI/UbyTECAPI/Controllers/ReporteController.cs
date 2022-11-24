using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Npgsql;
using System.Data;

namespace UbyTECAPI.Controllers
{
    [Route("reporte")]
    [ApiController]
    public class ReporteController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public ReporteController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        /// <summary>
        /// Ejecuta el query en la base de datos 
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        private DataTable execquery(string query)
        {
            //crear tabla
            DataTable table = new DataTable();
            //estableces conexion
            string sqlDataSource = _configuration.GetConnectionString("UbyTecCon");

            NpgsqlDataReader myReader;

            //ejecutar query
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

        /// <summary>
        /// retorna los valores del reporte 1
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("get/1")]
        public JsonResult Get()
        {
            //query
            string query = @"select cliente as ""Client"", afiliado as ""Affiliate"", productos as ""Products"", total as ""Total"", servicio as ""Service""
                          from ventasxcliente";

            //ejecutar query
            DataTable table = execquery(query);

            return new JsonResult(table);

        }

        /// <summary>
        /// retorna los valores del reporte 2
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("get/2")]
        public JsonResult Get2()
        {
            //query
            string query = @"select afiliado as ""Affiliate"", compras as ""Sells"", total as ""Total"", servicio as ""Service""
                          from ventasxafiliados";

            //ejecutar query
            DataTable table = execquery(query);

            return new JsonResult(table);

        }
    }
}
