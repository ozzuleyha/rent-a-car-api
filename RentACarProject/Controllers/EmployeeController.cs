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
    public class EmployeeController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public EmployeeController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        //Employee bilgi çekme
        [HttpGet("employee-count")]
        public JsonResult EmployeeCount()
        {
            string query = @"
                            SELECT COUNT(*) AS 'employee_count' FROM
                            dbo.Employee
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

        //Employee veri ekleme
        [HttpPost]
        public JsonResult Post(Employee emp)
        {
            //user tabkosuna eklemek gerekiyor öncelikle
            string query = @"
                            insert into dbo.Employee
                            (CompanyId,EmployeeName,EmployeeSurname, UserId)
                            values (@CompanyId,@EmployeeName,@EmployeeSurname,@UserId)
                            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("RentACarAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@CompanyId", emp.CompanyId);
                    myCommand.Parameters.AddWithValue("@EmployeeName", emp.EmployeeName);
                    myCommand.Parameters.AddWithValue("@EmployeeSurname", emp.EmployeeSurname);
                    myCommand.Parameters.AddWithValue("@UserId", emp.UserId);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                }
            }

            return new JsonResult("Added Successfully");
        }

        [HttpGet("employee-list")]
        public JsonResult getEmployeeList()
        {
            string query = @"
                            SELECT * FROM
                            dbo.Employee
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
        [HttpPost("add-employee")]
        public JsonResult AddEmployee(Employee employee)
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

            string EmployeeQuery = @"
                            insert into dbo.[Employee]
                            (EmployeeName, EmployeeSurname,CompanyId,UserId)
                            values (@EmployeeName,@EmployeeSurname,@CompanyId,@UserId)
                            ";

            DataTable CustomerTable = new DataTable();
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(EmployeeQuery, myCon))
                {
                    myCommand.Parameters.AddWithValue("@EmployeeName", employee.EmployeeName);
                    myCommand.Parameters.AddWithValue("@EmployeeSurname", employee.EmployeeSurname);
                    myCommand.Parameters.AddWithValue("@CompanyId",employee.CompanyId);
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
