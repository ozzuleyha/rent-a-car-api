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
            //int role = user.UserRoleId;
            string sqlDataSource = _configuration.GetConnectionString("RentACarAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    //role = (Int32)myCommand.ExecuteScalar();
                    myReader.Close();
                }
            }
            //int UserRole = Convert.ToInt32(table.Rows[0])
            if (table.Rows.Count == 0)
            {
                return new JsonResult("Yanlış bilgi girildi!");
            }
            else
            {
                int userRoleId = table.Rows[0].Field<int>("UserRoleId");
                int userId = table.Rows[0].Field<int>("id");
                switch (userRoleId)
                {
                    case 1:
                        string customerQuery = @"SELECT * FROM dbo.[Customer] WHERE UserId='" + userId + "'";

                        DataTable customerTable = new DataTable();
                        using (SqlConnection myCon = new SqlConnection(sqlDataSource))
                        {
                            myCon.Open();
                            using (SqlCommand myCommand = new SqlCommand(customerQuery, myCon))
                            {
                                myReader = myCommand.ExecuteReader();
                                customerTable.Load(myReader);
                                myReader.Close();
                            }
                            if (table.Rows.Count == 0)
                            {
                                return new JsonResult("customer tablosunda kullanıcı yok!");
                            }
                            else
                            {
                                customerTable.Columns.Add("UserRoleId", typeof(int));
                                customerTable.Rows[0]["UserRoleId"] = userRoleId;
                                return new JsonResult(customerTable);
                            }
                        }
                    case 2:
                        string employeeQuery = @"SELECT * FROM dbo.[Employee] WHERE UserId='" + userId + "'";
                        DataTable employeeTable = new DataTable();
                        using (SqlConnection myCon = new SqlConnection(sqlDataSource))
                        {
                            myCon.Open();
                            using (SqlCommand myCommand = new SqlCommand(employeeQuery, myCon))
                            {

                                myReader = myCommand.ExecuteReader();
                                employeeTable.Load(myReader);
                                myReader.Close();
                            }
                            if (table.Rows.Count == 0)
                            {
                                return new JsonResult("Employee tablosunda kullanıcı yok!");
                            }
                            else
                            {
                                employeeTable.Columns.Add("UserRoleId", typeof(int));
                                employeeTable.Rows[0]["UserRoleId"] = userRoleId;
                                return new JsonResult(employeeTable);
                            }
                        }
                    case 3:
                        return new JsonResult(table);
                    default:
                        return new JsonResult("Yanlıs bilgi");
                }
            }
        }
    }
}

