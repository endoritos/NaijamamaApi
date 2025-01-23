using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using naijamama.Models;
using System.Collections.Generic;
using System.Linq;
using Dapper;

namespace naijamama.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly string _connectionString;

        public UsersController(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        [HttpGet]
        public IActionResult GetAllUsers()
        {
            using var connection = new MySqlConnection(_connectionString);
            var users = connection.Query<User>("SELECT * FROM users").ToList();
            return Ok(users);
        }

        [HttpPost]
        public IActionResult AddUser(User user)
        {
            using var connection = new MySqlConnection(_connectionString);
            var query = "INSERT INTO users (name, email, password) VALUES (@Name, @Email, @Password)";
            connection.Execute(query, user);
            return Ok("User added successfully");
        }
    }
}