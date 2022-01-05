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
using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace RentACarProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _env;

        public CarController(IConfiguration configuration, IWebHostEnvironment env)
        {
            _configuration = configuration;
            _env = env;
        }

        [HttpPost("add-car")]
        public JsonResult AddCar(Car car)
        {
            string sqlDataSource = _configuration.GetConnectionString("RentACarAppCon");
            SqlDataReader myReader;

            string CarQuery = @"
                            insert into dbo.[Car]
                            (CarName, CarModel ,RentPrice, RequiredLicenseAge, SeatingCapacity, Airbag, CompanyId)
                            values (@CarName,@CarModel,@RentPrice, @RequiredLicenseAge, @SeatingCapacity, @Airbag, @CompanyId)
                            ";

            DataTable CarTable = new DataTable();
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(CarQuery, myCon))
                {
                    myCommand.Parameters.AddWithValue("@CarName", car.CarName);
                    myCommand.Parameters.AddWithValue("@CarModel", car.CarModel);
                    myCommand.Parameters.AddWithValue("@RentPrice", car.RentPrice);
                    myCommand.Parameters.AddWithValue("@RequiredLicenseAge", car.RequiredLicenseAge);
                    myCommand.Parameters.AddWithValue("@SeatingCapacity", car.SeatingCapacity);
                    myCommand.Parameters.AddWithValue("@Airbag", car.Airbag);
                    myCommand.Parameters.AddWithValue("@CompanyId", car.CompanyId);
                    myReader = myCommand.ExecuteReader();
                    CarTable.Load(myReader);
                    myReader.Close();
                }
            }

            return new JsonResult("Added Successfully");
        }

        [HttpPut("update-car")]
        public JsonResult UpdateCar(Car  car)
        {
            string query = @"
                             UPDATE dbo.Car
                             SET CarName=@CarName, CarModel=@CarModel, RentPrice=@RentPrice, RequiredLicenseAge=@RequiredLicenseAge,SeatingCapacity=@SeatingCapacity,Airbag=@Airbag,CompanyId=@CompanyId
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
                    myCommand.Parameters.AddWithValue("@id", car.CarId);
                    myCommand.Parameters.AddWithValue("@CarName", car.CarName);
                    myCommand.Parameters.AddWithValue("@CarModel", car.CarModel);
                    myCommand.Parameters.AddWithValue("@RentPrice", car.RentPrice);
                    myCommand.Parameters.AddWithValue("@RequiredLicenseAge", car.RequiredLicenseAge);
                    myCommand.Parameters.AddWithValue("@SeatingCapacity", car.SeatingCapacity);
                    myCommand.Parameters.AddWithValue("@Airbag", car.Airbag);
                    myCommand.Parameters.AddWithValue("@CompanyId", car.CompanyId);

                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                }
            }
            return new JsonResult("Updated Successfully");
        }

        [HttpPut("update-car-situation")]
        public JsonResult UpdateCarSituation(Car car)
        {
            string query = @"
                             UPDATE dbo.Car
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
                    myCommand.Parameters.AddWithValue("@id", car.CarId);
                    myCommand.Parameters.AddWithValue("@SituationId", car.SituationId);

                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                }
            }
            return new JsonResult("Updated Successfully");
        }

        [HttpGet("car-count")]
        public JsonResult CarCount()
        {
            string query = @"
                            SELECT COUNT(*) AS 'car_count' FROM
                            dbo.Car
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

        [HttpDelete("delete-car")]
        public JsonResult DeleteCar(Car car)
        {
            string query = @"
                            delete from dbo.[Car]
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
                    myCommand.Parameters.AddWithValue("@id", car.CarId);

                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                }
            }
            return new JsonResult("Deleted Successfully");
        }

        [HttpGet("car-list")]
        public JsonResult getCarList()
        {
            string query = @"
                            SELECT * FROM
                            dbo.Car
                            WHERE SituationId=3
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

        [Route("SaveFile")]
        [HttpPost]
        public JsonResult SaveFile()
        {
            try
            {
                var httpRequest = Request.Form;
                var postedFile = httpRequest.Files[0];
                string filename = postedFile.FileName;
                var physicalPath = _env.ContentRootPath + "/Photos/" + filename;

                using (var stream = new FileStream(physicalPath, FileMode.Create))
                {
                    postedFile.CopyTo(stream);
                }

                return new JsonResult(filename);
            }
            catch (Exception)
            {

                return new JsonResult("anonymous.png");
            }
        }
    }
}
