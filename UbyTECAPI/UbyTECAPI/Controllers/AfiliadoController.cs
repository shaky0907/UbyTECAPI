using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Npgsql;
using System.Data;
using UbyTECAPI.Models;

namespace UbyTECAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AfiliadoController : ControllerBase
    {

        private readonly IConfiguration _configuration;
        public AfiliadoController(IConfiguration configuration)
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


        private string get_tipo(Afiliado af)
        {
            string query2 = @"select id_tipo as ""ID"", nombre as ""Name"" from tipo_comercio where id_tipo = " + af.Type + ";";
            DataTable table2 = execquery(query2);
            string json2 = JsonConvert.SerializeObject(table2);
            List<Tipo_Comercio> tipo = JsonConvert.DeserializeObject<List<Tipo_Comercio>>(json2);

            return tipo[0].Name;

        }

       
        private string get_phone(Afiliado af)
        {
            string query3 = @"select * from telefonos_afiliado where cedula_j_a = '" + af.ID + "';";
            DataTable table3 = execquery(query3);
            string json3 = JsonConvert.SerializeObject(table3);
            List<Telefonos_afiliado> tel_af = JsonConvert.DeserializeObject<List<Telefonos_afiliado>>(json3);

            try
            {
                return tel_af[0].telefono;
            }
            catch (Exception)
            {
                return "";
                throw;
            }
            
            
        }


        [HttpGet]
        [Route("get")]
        public string Get()
        {
            string query = @"select E.cedula_j as ID, E.cedula_a as AdminID, E.id_tipo as Type, E.nombre as Name, E.correo as Email,E.sinpe as SINPE, E.banner as Banner, E.provincia as Province, E.canton, E.distrito as District
                             from Afiliado as E;";

            DataTable table = execquery(query);

            string json = JsonConvert.SerializeObject(table);
            List<Afiliado> afiliados = JsonConvert.DeserializeObject<List<Afiliado>>(json);

            if (afiliados != null)
            {

                foreach (Afiliado af in afiliados)
                {
                    //get tipo
                    string tipo_name = get_tipo(af);

                    //get telefono
                    string telefono = get_phone(af);

                    //get status
                    
                    //asign telefono
                    af.Type = tipo_name;
                    af.PhoneNum = telefono;
                }

                var jsonC = JsonConvert.SerializeObject(afiliados, Formatting.Indented);

                return jsonC;
            }
            else
            {
                return new JsonResult(table).ToString();
            }

        }

        [HttpGet]
        [Route("get/{id}")]
        public string Get(string id)
        {
            string query = @"select E.cedula_j as ID, E.cedula_a as AdminID, E.id_tipo as Type, E.nombre as Name, E.correo as Email,E.sinpe as SINPE, E.banner as Banner, E.provincia as Province, E.canton, E.distrito as District
                             from Afiliado as E
                             where E.cedula_j = '"+ id+"'";
            //string query = @"select  from Afiliado As A where A.cedula_J = '" + id + "'";

            DataTable table = execquery(query);

            string json = JsonConvert.SerializeObject(table);
            List<Afiliado> afiliados = JsonConvert.DeserializeObject<List<Afiliado>>(json);

            if(afiliados != null)
            {
                
                Afiliado af = afiliados[0];

                //get tipo
                string tipo_name = get_tipo(af);

                //get telefono
                string telefono = get_phone(af);


                //asign telefono
                af.Type = tipo_name;
                af.PhoneNum = telefono;


                var jsonC = JsonConvert.SerializeObject(afiliados, Formatting.Indented);

                return jsonC;
            }
            else
            {
                return new JsonResult(table).ToString();
            }


            

        }

        [HttpPost]
        [Route("guardar")]
        public JsonResult Post(Afiliado adm)
        {



            
            string query = @"Insert into afiliado 
                             Values  ('" + adm.ID + "','" + adm.AdminID + "'," + adm.Type.ToString() + ",'" + adm.Name + "','" + adm.Email + "','" + adm.SINPE + "','" + adm.Banner+"','" + adm.Province + "','" + adm.Canton + "','" + adm.District + @"');";
            Console.WriteLine("---------------------------");
            Console.WriteLine(query);
            Console.WriteLine("---------------------------");
            DataTable table = execquery(query);

            return new JsonResult("Insert Success");

        }


        [HttpPut]
        [Route("update")]
        public JsonResult Put(Afiliado adm)
        {



            string query = @"
                    update afiliado set 
                    cedula_a = '" + adm.AdminID + @"',
                    id_tipo = " + adm.Type + @",
                    nombre = '" + adm.Name + @"',
                    correo = '" + adm.Email + @"',
                    sinpe = '" + adm.SINPE + @"',
                    banner = '" + adm.Banner + @"',
                    provincia = '" + adm.Province + @"',
                    canton = '" + adm.Canton + @"',
                    distrito = '" + adm.District + @"'
                    where cedula_j = '" + adm.ID + @"'
                    ";

            DataTable table = execquery(query);




            return new JsonResult("Update Success");

        }


        [HttpDelete]
        [Route("delete/{id}")]
        public JsonResult Delete(string id)
        {
            string query = @"delete from afiliado
                             where Cedula_j = '" + id + "'";

            DataTable table = execquery(query);

            return new JsonResult("Delete Success");

        }





    }
}
