﻿using Microsoft.AspNetCore.Http;
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
        public JsonResult getCompanyList()
        {
            string query = @"
                            SELECT customer.id, customer.CustomerName, customer.CustomerSurname, car.CarName
                            FROM dbo.RentInformation rent
                            INNER JOIN dbo.Customer customer
                            ON rent.RentCustomerId = customer.id
                            INNER JOIN dbo.Car car
                            ON rent.RentCarId = car.id
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
    }
}
