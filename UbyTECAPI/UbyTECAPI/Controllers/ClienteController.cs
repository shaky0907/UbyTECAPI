using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Npgsql;
using System.Data;
using UbyTECAPI.Models;

namespace UbyTECAPI.Controllers
{
    [Route("cliente")]
    [ApiController]
    public class ClienteController : ControllerBase
    {

        private readonly IConfiguration _configuration;
        public ClienteController(IConfiguration configuration)
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
                            fechanacim as ""BDate"",usuario as ""Username"",contra as ""Password"", telefono as ""PhoneNum"",
                            provincia as Province, canton as Canton, distrito as District
                            from cliente ";

            DataTable table = execquery(query);

            return new JsonResult(table);

        }

        [HttpGet]
        [Route("get/{id}")]
        public JsonResult Get(string id)
        {
            string query = @"select cedula as ""ID"", nombre as ""FirstN"",apellido1 as ""FirstLN"",apellido2 as ""SecondLN"",
                            fechanacim as ""BDate"",usuario as ""Username"",contra as ""Password"", telefono as ""PhoneNum"",
                            provincia as ""Province"", canton as ""Canton"", distrito as ""District""
                            from cliente 
                            where cedula = '" + id + "';";

            DataTable table = execquery(query);

            return new JsonResult(table);

        }

        [HttpPost]
        [Route("guardar")]
        public JsonResult Post(Cliente rep)
        {
            /*
            string query = @"insert into admleado 
                             ('" + adm.ID + "','" + adm.FirstN + "','" + adm.FirstLN + "','" + adm.SecondLN + "','" + adm.Username + "','" + adm.Password + "','" + adm.Province + "','" + adm.Canton + "','" + adm.District + "'," + adm.ProfilePic + @")";
            */
            string query = @"Insert into cliente
                             Values  ('" + rep.ID + "','" + rep.FirstN + "','" + rep.FirstLN + "','" + rep.SecondLN + "','" + rep.Username + "','" + rep.Password + "','" + rep.PhoneNum +"','" + rep.BDate +"','" + rep.Province + "','" + rep.Canton + "','" + rep.District + @"');";

            DataTable table = execquery(query);

            return new JsonResult("Insert Success");

        }


        [HttpPut]
        [Route("update")]
        public JsonResult Put(Cliente rep)
        {
            string query = @"
                        update cliente set 
                        nombre = '" + rep.FirstN + @"',
                        apellido1 = '" + rep.FirstLN + @"',
                        apellido2 = '" + rep.SecondLN + @"',
                        usuario = '" + rep.Username + @"',
                        contra = '" + rep.Password + @"',
                        provincia = '" + rep.Province + @"',
                        canton = '" + rep.Canton + @"',
                        distrito = '" + rep.District + @"',
                        telefono = '" + rep.PhoneNum + @"',
                        fechanacim = '" + rep.BDate + @"'
                        where cedula = '" + rep.ID + @"'
                        ";

            DataTable table = execquery(query);

            return new JsonResult("Update Success");

        }


        [HttpDelete]
        [Route("delete/{id}")]
        public JsonResult Delete(string id)
        {
            string query = @"delete from cliente
                                 where cedula = '" + id + "'";

            DataTable table = execquery(query);

            return new JsonResult("Delete Success");

        }

    }
}
