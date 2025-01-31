using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using naijamama.Models;
using System.Collections.Generic;
using System.Linq;
using Dapper;
using BCrypt.Net;

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
            try
            {
                using var connection = new MySqlConnection(_connectionString);
                
                // Hash the password before storing
                user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);

                var query = "INSERT INTO users (name, email, password, phone) VALUES (@Name, @Email, @Password, @Phone)";
                connection.Execute(query, user);
                
                return Ok("User added successfully");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }
        }
    
    }
}