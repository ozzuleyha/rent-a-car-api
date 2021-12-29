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
    public class CustomerController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public CustomerController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet("customer-count")]
        public JsonResult CustomerCount()
        {
            string query = @"
                            SELECT COUNT(*) AS 'customer_count' FROM
                            dbo.Customer
                            ";

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
            return new JsonResult(table);
        }

        [HttpGet("customer-list")]
        public JsonResult getCustomerList()
        {
            string query = @"
                            SELECT * FROM
                            dbo.Customer
                            ";

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
            return new JsonResult(table);
        }

        [HttpPost("add-customer")]
        public JsonResult AddCustomer(Customer customer)
        {
            string sqlDataSource = _configuration.GetConnectionString("RentACarAppCon");
            SqlDataReader myReader;
            

            string UserQuery = @"SELECT TOP 1 * FROM dbo.[User] ORDER BY id DESC";  

            DataTable UserTable = new DataTable();
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(UserQuery, myCon))
                {
                    myReader = myCommand.ExecuteReader();
                    UserTable.Load(myReader);
                    myReader.Close();
                }
            }

            

            int userId = UserTable.Rows[0].Field<int>("id");

            string CustomerQuery = @"
                            insert into dbo.[Customer]
                            (CustomerName, CustomerSurname,CustomerEmail,UserId)
                            values (@CustomerName,@CustomerSurname,@CustomerEmail,@UserId)
                            ";

            DataTable CustomerTable = new DataTable();
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(CustomerQuery, myCon))
                {
                    myCommand.Parameters.AddWithValue("@CustomerName", customer.CustomerName);
                    myCommand.Parameters.AddWithValue("@CustomerSurname", customer.CustomerSurname);
                    //myCommand.Parameters.AddWithValue("@CustomerBirthDay", customer.CustomerBirthDay);
                    //myCommand.Parameters.AddWithValue("@CustomerDrivingLicenseDate", customer.CustomerDrivingLicenseDate);
                    myCommand.Parameters.AddWithValue("@CustomerEmail", customer.CustomerEmail);
                    myCommand.Parameters.AddWithValue("@UserId", userId);
                    myReader = myCommand.ExecuteReader();
                    CustomerTable.Load(myReader);
                    myReader.Close();
                }
            }

            return new JsonResult("Added Successfully");
        }



        
    }
}

















