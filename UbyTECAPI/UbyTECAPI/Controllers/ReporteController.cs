using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Npgsql;
using System.Data;
using UbyTECAPI.Models;

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
        public string Get()
        {
            //query
            string query = @"select cliente as ""Client"", afiliado as ""Affiliate"", productos as ""Products"", total as ""Total"", servicio as ""Service""
                          from ventasxcliente";

            //ejecutar query
            DataTable table = execquery(query);
            string json2 = JsonConvert.SerializeObject(table);
            List<ventasxcliente> vxp = JsonConvert.DeserializeObject<List<ventasxcliente>>(json2);

            List<Reporte1> rps = new List<Reporte1>();
            try
            {
                string cliente = vxp[0].Client;

                List<string> Af = new List<string>();
                List<string> Pr = new List<string>();
                List<string> Ser = new List<string>();
                List<string> Tot = new List<string>();

                Af.Add(vxp[0].Affiliate);                
                Pr.Add(vxp[0].Products);
                Ser.Add(vxp[0].Service);
                Tot.Add(vxp[0].Total);

                for (int i = 1; i < vxp.Count; i++)
                {
                    if (vxp[i].Client == cliente)
                    {
                        Af.Add(vxp[i].Affiliate);
                        Pr.Add(vxp[i].Products);
                        Ser.Add(vxp[i].Service);
                        Tot.Add(vxp[i].Total);
                    }
                    else
                    {
                        Reporte1 reporte= new Reporte1();
                        reporte.Client = cliente;
                        reporte.Affiliates = Af.ToArray();
                        reporte.Products = Pr.ToArray();
                        reporte.Servicio = Ser.ToArray();
                        reporte.Totales = Tot.ToArray();

                        rps.Add(reporte);

                        Af = new List<string>();
                        Pr = new List<string>();
                        Ser = new List<string>();
                        Tot = new List<string>();

                        cliente = vxp[i].Client;
                        Af.Add(vxp[i].Affiliate);
                        Pr.Add(vxp[i].Products);
                        Ser.Add(vxp[i].Service);
                        Tot.Add(vxp[i].Total);

                    }

                    
                }
                Reporte1 reporte2 = new Reporte1();
                reporte2.Client = cliente;
                reporte2.Affiliates = Af.ToArray();
                reporte2.Products = Pr.ToArray();
                reporte2.Servicio = Ser.ToArray();
                reporte2.Totales = Tot.ToArray();

                rps.Add(reporte2);
                var jsonC = JsonConvert.SerializeObject(rps, Formatting.Indented);
                return jsonC;


            }
            catch (Exception)
            {
                return new JsonResult(table).ToString() ;
                throw;
            }
            




            

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
