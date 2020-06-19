using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CainAbel.Models;
using Microsoft.AspNetCore.Mvc;
using System.Configuration;
using System.Data.SqlClient;
using Microsoft.Data.SqlClient;
using Microsoft.AspNetCore.Identity;
using System.Data;
using Microsoft.AspNetCore.Http;
using System.Web;
using Microsoft.Extensions.Configuration;
using System.Security.Cryptography;
using CainAbel.Util;

namespace CainAbel.Controllers
{
    public class LoginController : Controller
    {
       private readonly IConfiguration config;

       public LoginController(IConfiguration configuration)
        {
            this.config = configuration;
        }

        public IActionResult Login() {
            return View();
        }
        [HttpPost]
        public IActionResult Login(LoginModel lm)
        {
            {
                string sql_command= "SELECT Email,Password FROM  [dbo].[UserTable] WHERE Password = @Password AND Email=@Email"; 
                string conectionString = config.GetConnectionString("ConectorUser");        
                SqlConnection conection = new SqlConnection(conectionString);
                 conection.Open();
                 SqlCommand cmd = new SqlCommand(sql_command, conection);
                cmd.Parameters.AddWithValue("@Email", lm.Email);              
                cmd.Parameters.AddWithValue("@Password", Encoder.Md5_encrypt(lm.Password));

                SqlDataReader sdr = cmd.ExecuteReader();
                if (sdr.Read())
                {
                    HttpContext.Session.SetString("UserEmail", lm.Email);

                    return RedirectToAction("welcome");
                }
                conection.Close(); 
             }

         

                return View();

            
        }
        public IActionResult welcome()
        {    
            var Email= ViewBag.Email = @HttpContext.Session.GetString("UserEmail");

            ViewBag.e = Email;
            {
                string conectionString = config.GetConnectionString("ConectorUser");
                SqlConnection conection = new SqlConnection(conectionString);
                conection.Open();
                SqlCommand callStorProcedure = new SqlCommand("[dbo].[SelectEmail]", conection);
                callStorProcedure.Connection = conection;
                callStorProcedure.CommandType = CommandType.StoredProcedure;
                callStorProcedure.Parameters.AddWithValue("@Email", Email);
                
                SqlDataReader reader = callStorProcedure.ExecuteReader();

                
                while (reader.Read())
                    {
                    ViewBag.r = reader["Phone"].ToString();
                            
                        
                    }
                

            }
            //selectam din tabelul ifouser unde emailul este asta |;
            return View();
        }
    }
}