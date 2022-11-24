﻿using Microsoft.AspNetCore.Http;
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
        /// retorna todos los admin afiliados
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("get")]
        public JsonResult Get()
        {
            //query
            string query = @"select A.cedula as ""ID"",A.nombre as ""FirstN"",A.apellido1 as ""FirstLN"", A.apellido2 as ""SecondLN"",
                            A.correo as ""Email"", A.usuario as ""Username"",A.contra as ""Password"",
                            A.provincia as ""Province"", A.canton as ""Canton"", A.distrito as ""District"", A.profilepic as ""ProfilePic""
                            from admin_afiliado as A;";

            DataTable table = execquery(query);

            return new JsonResult(table);

        }

        /// <summary>
        /// retorna admin afiliado por id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("get/{id}")]
        public JsonResult Get(string id)
        {
            //query 
            string query = @"select A.cedula as ""ID"",A.nombre as ""FirstN"",A.apellido1 as ""FirstLN"", A.apellido2 as ""SecondLN"",
                            A.correo as ""Email"", A.usuario as ""Username"",A.contra as ""Password"",
                            A.provincia as ""Province"", A.canton as ""Canton"", A.distrito as ""District"", A.profilepic as ""ProfilePic"", t.""PhoneNum""
                            from admin_afiliado As A left join 
	                            (select distinct on (cedula_a) telefono as ""PhoneNum"", cedula_a 
	                             from telefonos_admin_afiliado
	                             order by cedula_a) as t
                            on cedula_a = cedula 
                            where A.Cedula = '" + id + "'";

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
                             Values  ('" + adm.ID + "','" + adm.FirstN + "','" + adm.FirstLN + "','" + adm.SecondLN + "','" + adm.Email + "','" + adm.Username + "','" + adm.Password + "','" + adm.Province + "','" + adm.Canton + "','" + adm.District + "','" + adm.ProfilePic + @"');";
            
            execquery(query);

            query = @"Insert into Telefonos_admin_afiliado
                             Values  ('" + adm.ID + "','" + adm.PhoneNum + @"');";

            execquery(query);

            return new JsonResult("Insert Success");

        }


        [HttpPut]
        [Route("update")]
        public JsonResult Put(AdminAfiliado adm)
        {
            string query = @"
                    update admin_afiliado set 
                    nombre = '" + adm.FirstN + @"',
                    apellido1 = '" + adm.FirstLN + @"',
                    apellido2 = '" + adm.SecondLN + @"',
                    correo = '" + adm.Email + @"',
                    usuario = '" + adm.Username + @"',
                    contra = '" + adm.Password + @"',
                    provincia = '" + adm.Province + @"',
                    canton = '" + adm.Canton + @"',
                    distrito = '" + adm.District + @"',
                    profilepic = '" + adm.ProfilePic + @"'
                    where cedula = '" + adm.ID + @"'
                    ";

            execquery(query);

            query = @"
                    update Telefonos_admin_afiliado set 
                    telefono = '" + adm.PhoneNum + @"'
                    where cedula_a = '" + adm.ID + @"'
                    ";

            execquery(query);

            return new JsonResult("Update Success");

        }


        [HttpDelete]
        [Route("delete/{id}")]
        public JsonResult Delete(string id)
        {
            string query = @"delete from Telefonos_admin_afiliado
                             where cedula_a = '" + id + "'";

            execquery(query);

            query = @"delete from admin_afiliado
                             where cedula = '" + id + "'";

            DataTable table = execquery(query);

            return new JsonResult("Delete Success");

        }



    }
}
