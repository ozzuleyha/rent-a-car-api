using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using RentACarProject.Models;

namespace RentACarProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public UserController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        //Login kontrolü
        [HttpPost("login")]
        public JsonResult LoginControl(User user)
        {
            string query = @"SELECT * FROM dbo.[User] WHERE UserName='" + user.UserName + "' AND Password='" + user.Password + "'";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("RentACarAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                }
            }
            if (table.Rows.Count == 0)
            {
                return new JsonResult("Yanlış bilgi girildi!");
            }
            else
            {
                return new JsonResult(table);
            }
        }
    }
}
