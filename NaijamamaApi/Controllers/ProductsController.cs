using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using naijamama.Models;
using System.Collections.Generic;
using System.Linq;
using Dapper;
using Microsoft.Extensions.Configuration;

namespace naijamama.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly string _connectionString;

        public ProductsController(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        [HttpGet]
        public IActionResult GetAllProducts()
        {
            using var connection = new MySqlConnection(_connectionString);
            var products = connection.Query<Products>("SELECT * FROM Products").ToList();
            return Ok(products);
        }

        [HttpPost]
        public IActionResult AddProducts(Products product)
        {
            using var connection = new MySqlConnection(_connectionString);
            var query = "INSERT INTO Products (name, description, Price, stock) VALUES (@Name, @Description, @Price ,@Stock)";
            connection.Execute(query, product);
            return Ok("Products added successfully");
        }
    }
}