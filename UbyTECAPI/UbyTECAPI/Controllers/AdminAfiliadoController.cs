using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Npgsql;
using System.Data;
using UbyTECAPI.Models;

namespace UbyTECAPI.Controllers
{
    
    [ApiController]
    [Route("adminAf")]
    public class AdminAfiliadoController : ControllerBase
    {

        private readonly IConfiguration _configuration;
        public AdminAfiliadoController(IConfiguration configuration)
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
            string query = @"select * from admin_afiliado;";

            DataTable table = execquery(query);

            return new JsonResult(table);

        }

        [HttpGet]
        [Route("get/{id}")]
        public JsonResult Get(string id)
        {
            string query = @"select * from admin_afiliado As A where A.Cedula = '" + id + "'";

            DataTable table = execquery(query);

            return new JsonResult(table);

        }

        [HttpPost]
        [Route("guardar")]
        public JsonResult Post(AdminAfiliado adm)
        {
            /*
            string query = @"insert into admleado 
                             ('" + adm.ID + "','" + adm.FirstN + "','" + adm.FirstLN + "','" + adm.SecondLN + "','" + adm.Username + "','" + adm.Password + "','" + adm.Province + "','" + adm.Canton + "','" + adm.District + "'," + adm.ProfilePic + @")";
            */
            string query = @"Insert into admin_afiliado 
                             Values  ('" + adm.ID + "','" + adm.FirstN + "','" + adm.FirstLN + "','" + adm.SecondLN + "','" + adm.Email + "','" + adm.Username + "','" + adm.Password + "','" + adm.Province + "','" + adm.Canton + "','" + adm.District + @"');";
            Console.WriteLine("---------------------------");
            Console.WriteLine(query);
            Console.WriteLine("---------------------------");
            DataTable table = execquery(query);

            return new JsonResult("Insert Success");

        }


        [HttpPut]
        [Route("update")]
        public JsonResult Put(AdminAfiliado adm)
        {
            string query = @"
                    update admin_afiliado set 
                    cedula = '" + adm.ID + @"'
                    nombre = '" + adm.FirstN + @"',
                    apellido1 = '" + adm.FirstLN + @"',
                    apellido2 = '" + adm.SecondLN + @"',
                    email = '" + adm.Email + @"',
                    usuario = '" + adm.Username + @"'
                    contra = '" + adm.Password + @"'
                    provincia = '" + adm.Province + @"'
                    canton = '" + adm.Canton + @"'
                    distrito = '" + adm.District + @"'
                    where cedula = '" + adm.ID + @"'
                    ";

            DataTable table = execquery(query);

            return new JsonResult("Update Success");

        }


        [HttpDelete]
        [Route("delete/{id}")]
        public JsonResult Delete(string id)
        {
            string query = @"delete from admin_afiliado
                             where cedula = '" + id + "'";

            DataTable table = execquery(query);

            return new JsonResult("Delete Success");

        }



    }
}
