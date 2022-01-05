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
    public class RentInformationController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public RentInformationController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        [HttpPost("add-rent-information")]
        public JsonResult AddRentInformation(RentInformation rentInformation)
        {
            string sqlDataSource = _configuration.GetConnectionString("RentACarAppCon");
            SqlDataReader myReader;

            string CompanyQuery = @"
                            insert into dbo.[RentInformation]
                            (SituationId, RentPrice, RentCustomerId, RentCarId)
                            values (@SituationId, @RentPrice, @RentCustomerId, @RentCarId)
                            ";

            DataTable CompanyTable = new DataTable();
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(CompanyQuery, myCon))
                {
                    myCommand.Parameters.AddWithValue("@SituationId", rentInformation.SituationId);
                    myCommand.Parameters.AddWithValue("@RentPrice", rentInformation.RentPrice);
                    myCommand.Parameters.AddWithValue("@RentCustomerId", rentInformation.RentCustomerId);
                    myCommand.Parameters.AddWithValue("@RentCarId", rentInformation.RentCarId);
                    myReader = myCommand.ExecuteReader();
                    CompanyTable.Load(myReader);
                    myReader.Close();
                }
            }

            return new JsonResult("Added Successfully");
        }
        [HttpGet("rent-request-list")]
        public JsonResult GetRentRequests()
        {
            string query = @"
                            SELECT customer.id, customer.CustomerName, customer.CustomerSurname, car.CarName, rent.id As rentInformationId
                            FROM dbo.RentInformation rent
                            INNER JOIN dbo.Customer customer
                            ON rent.RentCustomerId = customer.id
                            INNER JOIN dbo.Car car
                            ON rent.RentCarId = car.id
                            WHERE rent.SituationId=1
                            ORDER BY rent.id;
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

        [HttpPost("rent-result-list")]
        public JsonResult GetRentResult(Customer customer)
        {
            string query = @"
                            SELECT car.id, car.CarName, car.CarModel, rent.SituationId, rent.id As rentInformationId
                            FROM dbo.RentInformation rent
                            INNER JOIN dbo.Customer customer
                            ON rent.RentCustomerId = customer.id
                            INNER JOIN dbo.Car car
                            ON rent.RentCarId = car.id
                            WHERE customer.id=@CustomerId
                            ORDER BY rent.id;
                                                        ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("RentACarAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@CustomerId", customer.CustomerId);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                }
            }
            return new JsonResult(table);
        }

        [HttpPut("update-rent-information")]
        public JsonResult UpdateRentInformation(RentInformation rentInformation)
        {
            string query = @"
                             UPDATE dbo.RentInformation
                             SET SituationId=@SituationId
                             WHERE id=@id
                             ";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("RentACarAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@id", rentInformation.RentId);
                    myCommand.Parameters.AddWithValue("@SituationId", rentInformation.SituationId);

                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                }
            }
            return new JsonResult("Updated Successfully");
        }

    }
}
