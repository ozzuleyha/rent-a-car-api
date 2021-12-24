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
    public class CompanyController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public CompanyController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet("company-count")]
        public JsonResult CompanyCount()
        {
            string query = @"
                            SELECT COUNT(*) AS 'company_count' FROM
                            dbo.Company
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

        [HttpGet("company-list")]
        public JsonResult getCompanyList()
        {
            string query = @"
                            SELECT * FROM
                            dbo.Company
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

        [HttpPost("add-company")]
        public JsonResult AddCompany(Company company)
        {
            string sqlDataSource = _configuration.GetConnectionString("RentACarAppCon");
            SqlDataReader myReader;

            string EmployeeQuery = @"
                            insert into dbo.[Company]
                            (CompanyName, CompanyCity,CompanyAdress)
                            values (@CompanyName,@CompanyCity,@CompanyAdress)
                            ";

            DataTable CustomerTable = new DataTable();
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(EmployeeQuery, myCon))
                {
                    myCommand.Parameters.AddWithValue("@CompanyName", company.CompanyName);
                    myCommand.Parameters.AddWithValue("@CompanyCity", company.CompanyCity);
                    myCommand.Parameters.AddWithValue("@CompanyAdress", company.CompanyAdress);
                    myReader = myCommand.ExecuteReader();
                    CustomerTable.Load(myReader);
                    myReader.Close();
                }
            }

            return new JsonResult("Added Successfully");
        }

        [HttpPut("update-company")]
        public JsonResult UpdateCompany(Company company)
        {
            string query = @"UPDATE dbo.[Company]
                             SET (CompanyName=@CompanyName, CompanyCity=@CompanyCity, CompanyAdress=@CompanyAdress)
                             WHERE (id=@id)
                             ";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("RentACarAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@id", company.CompanyId);
                    myCommand.Parameters.AddWithValue("@CompanyName", company.CompanyName);
                    myCommand.Parameters.AddWithValue("@CompanyCity", company.CompanyCity);
                    myCommand.Parameters.AddWithValue("@CompanyAdress", company.CompanyAdress);

                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                }
            }
            return new JsonResult("Updated Successfully");
        }

        [HttpDelete("delete-company")]
        public JsonResult DeleteCompany(Company company)
        {
            string query = @"
                            delete from dbo.[Company]
                            where id = @id
                            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("RentACarAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@id", company.CompanyId);

                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                }
            }
            return new JsonResult("Deleted Successfully");
        }
    }
}
