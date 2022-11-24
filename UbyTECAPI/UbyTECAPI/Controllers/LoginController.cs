using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Npgsql;
using System.Data;
using UbyTECAPI.Models;

namespace UbyTECAPI.Controllers
{
    [Route("login")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public LoginController(IConfiguration configuration)
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

        private string get_idA(string id_admin)
        {
            string query2 = @"select cedula_j as ""ID"" from afiliado where cedula_a = '" + id_admin + "';";
            DataTable table2 = execquery(query2);
            string json2 = JsonConvert.SerializeObject(table2);
            List<Afiliado> af = JsonConvert.DeserializeObject<List<Afiliado>>(json2);
            try
            {
                return af[0].ID;
            }
            catch (Exception)
            {
                return "";
                throw;
            }
           
        }

        private string get_status(string id_admin)
        {
            string query2 = @"select sa.estado as ""Status""
                                from afiliado as a, solicitud_afiliado as sa, admin_afiliado as af
                                where a.cedula_j = sa.cedula_a and af.cedula = a.cedula_a and af.cedula = '" + id_admin + "';";
            DataTable table2 = execquery(query2);
            string json2 = JsonConvert.SerializeObject(table2);
            List<Afiliado> af = JsonConvert.DeserializeObject<List<Afiliado>>(json2);
            try
            {
                return af[0].Status;
            }
            catch (Exception)
            {
                return "";
                throw;
            }
        }

        [HttpGet]
        [Route("get/{username}/{password}")]
        public string Get(string username, string password)
        {
            string query = @"select usuario as ""Username"",contra as ""Password""
                             from empleado
                             where usuario = '" + username + "'AND contra = '" + password + "'";

            DataTable table1 = execquery(query);
            

            string query2 = @"select usuario as ""Username"",contra as ""Password"",cedula as ""ID""
                             from cliente
                             where usuario = '" + username + "' AND contra = '" + password + "'";

            DataTable table2 = execquery(query2);
            


            string query3 = @"select usuario, as ""Username"",contra as ""Password"",cedula as ""ID"",estado
                             from admin_afiliado 
                             where usuario = '" + username + "'AND contra = '" + password + "'";

            DataTable table3 = execquery(query3);





            if (table1.Rows.Count > 0)
            {
                Login login = new Login();
                login.Type = "admin";
                List<Login> lst = new List<Login>();
                lst.Add(login);
                
                var jsonC = JsonConvert.SerializeObject(lst, Formatting.Indented);
                return jsonC;

            }

            else if (table2.Rows.Count > 0)
            {
                string json = JsonConvert.SerializeObject(table2);
                List<Cliente> af = JsonConvert.DeserializeObject<List<Cliente>>(json);

                Login login = new Login();
                login.Type = "cliente";
                login.ID_client = af[0].ID;


                List<Login> lst = new List<Login>();
                lst.Add(login);

                var jsonC = JsonConvert.SerializeObject(lst, Formatting.Indented);
                return jsonC;

            }

            else if (table3.Rows.Count > 0)
            {
                string json = JsonConvert.SerializeObject(table3);
                List<AdminAfiliado> af = JsonConvert.DeserializeObject<List<AdminAfiliado>>(json);



                Login login = new Login();
                login.Type = "affiliate";
                login.ID_Affiliate = get_idA(af[0].ID);
                login.ID_Admin = af[0].ID;






                login.ID_client = get_status(af[0].ID);
                List<Login> lst = new List<Login>();
                lst.Add(login);

                var jsonC = JsonConvert.SerializeObject(lst, Formatting.Indented);
                return jsonC;
            }
            else
            {
                Login login = new Login();
                List<Login> lst = new List<Login>();
                lst.Add(login);

                var jsonC = JsonConvert.SerializeObject(lst, Formatting.Indented);
                return jsonC;
            }

         

        }





    }
}
