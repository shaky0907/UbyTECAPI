using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Npgsql;
using System.Data;
using UbyTECAPI.Models;

namespace UbyTECAPI.Controllers
{
    [Route("repartidor")]
    [ApiController]
    public class RepartidorController : ControllerBase
    {

        private readonly IConfiguration _configuration;
        public RepartidorController(IConfiguration configuration)
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
            string query = @"select cedula as ""ID"", nombre as ""FirstN"",apellido1 as ""FirstLN"",apellido2 as ""SecondLN"",
                            usuario as ""Username"",contra as ""Password"",correo as ""Email"", 
                            provincia as Province, canton as Canton, distrito as District, disponibilidad as ""Status""
                            from repartidor";

            DataTable table = execquery(query);

            return new JsonResult(table);

        }

        [HttpGet]
        [Route("get/{id}")]
        public JsonResult Get(string id)
        {
            string query = @"select cedula as ""ID"", nombre as ""FirstN"",apellido1 as ""FirstLN"",apellido2 as ""SecondLN"",
                            usuario as ""Username"",contra as ""Password"",correo as ""Email"", 
                            provincia as Province, canton as Canton, distrito as District, disponibilidad as ""Status"" 
                            from tipo_comercio
                             where cedula = '" + id + "';";

            DataTable table = execquery(query);

            return new JsonResult(table);

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
        [Route("update")]
        public JsonResult Put(Repartidor rep)
        {
            string query = @"
                        update repartidor set 
                        nombre = '" + rep.FirstN + @"',
                        apellido1 = '" + rep.FirstLN + @"',
                        apellido2 = '" + rep.SecondLN + @"',
                        usuario = '" + rep.Username + @"',
                        contra = '" + rep.Password + @"',
                        provincia = '" + rep.Province + @"',
                        canton = '" + rep.Canton + @"',
                        distrito = '" + rep.District + @"',
                        disponibilidad = '" + rep.Status + @"'
                        where cedula = '" + rep.ID + @"'
                        ";

            DataTable table = execquery(query);

            return new JsonResult("Update Success");

        }


        [HttpDelete]
        [Route("delete/{id}")]
        public JsonResult Delete(string id)
        {
            string query = @"delete from repartidor
                                 where cedula = '" + id + "'";

            DataTable table = execquery(query);

            return new JsonResult("Delete Success");

        }






    }
}
