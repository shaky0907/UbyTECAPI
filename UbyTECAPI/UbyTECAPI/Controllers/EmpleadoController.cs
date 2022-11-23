using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Npgsql;
using System.Data;
using System.Runtime.CompilerServices;
using UbyTECAPI.Models;

namespace UbyTECAPI.Controllers
{

    [ApiController]
    [Route("empleado")]
    public class EmpleadoController : ControllerBase
    {

        private readonly IConfiguration _configuration;
        public EmpleadoController(IConfiguration configuration)
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
            string query = @"select * from Empleado;";

            DataTable table = execquery(query);

            return new JsonResult(table);

        }

        [HttpGet]
        [Route("get/{id}")]
        public JsonResult Get(string id)
        {
            string query = @"select * from Empleado As E where E.Cedula = '"+id+"'";

            DataTable table = execquery(query);

            return new JsonResult(table);

        }

        [HttpPost]
        [Route("guardar")]
        public JsonResult Post(Empleado emp)
        {
            /*
            string query = @"insert into Empleado 
                             ('" + emp.ID + "','" + emp.FirstN + "','" + emp.FirstLN + "','" + emp.SecondLN + "','" + emp.Username + "','" + emp.Password + "','" + emp.Province + "','" + emp.Canton + "','" + emp.District + "'," + emp.ProfilePic + @")";
            */
            string query = @"Insert into Empleado (Cedula,Nombre,Apellido1,Apellido2,Usuario,Contra,Provincia,Canton,Distrito,Profile_Pic)
                             Values  ('" + emp.ID + "','" + emp.FirstN + "','" + emp.FirstLN + "','" + emp.SecondLN + "','" + emp.Username + "','" + emp.Password + "','" + emp.Province + "','" + emp.Canton + "','" + emp.District + "','" + emp.ProfilePic + @"');";
            Console.WriteLine("---------------------------");
            Console.WriteLine(query);
            Console.WriteLine("---------------------------");
            DataTable table = execquery(query);

            return new JsonResult("Insert Success");

        }


        [HttpPut]
        [Route("update")]
        public JsonResult Put(Empleado emp)
        {
            string query = @"
                    update Empleado set 
                    nombre = '" + emp.FirstN + @"',
                    apellido1 = '" + emp.FirstLN + @"',
                    apellido2 = '" + emp.SecondLN + @"',
                    usuario = '" + emp.Username + @"',
                    contra = '" + emp.Password + @"',
                    provincia = '" + emp.Province + @"',
                    canton = '" + emp.Canton + @"',
                    distrito = '" + emp.District + @"',
                    profile_pic = '" + emp.ProfilePic + @"'
                    where cedula = '" + emp.ID + @"'
                    ";

            DataTable table = execquery(query);

            return new JsonResult("Update Success");

        }


        [HttpDelete]
        [Route("delete/{id}")]
        public JsonResult Delete(string id)
        {
            string query = @"delete from Empleado
                             where Cedula = '"+id+"'";

            DataTable table = execquery(query);

            return new JsonResult("Delete Success");

        }


    }
}
