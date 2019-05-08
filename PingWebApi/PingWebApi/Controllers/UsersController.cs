using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PingWebApi.Model;

namespace PingWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        // GET api/values
        [HttpGet]
        public IEnumerable<Users> GetAllUsers()
        {
            return DatabaseCommand.ReadUsers("SELECT * FROM Users");
        }

        // GET api/values/5
        [HttpGet("{userId}")]
        public IEnumerable<Users> GetUser(string userId)
        {
            return DatabaseCommand.ReadUsers($"SELECT * FROM Users WHERE Id = '{userId}'");
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string userId, string username)
        {
            DatabaseCommand.ExecuteQuery($"INSERT INTO Users(Id, Username) VALUES('{userId}', '{username}')");
        }

        // PUT api/values/5
        [HttpPut("{userId}")]
        public void Put(string userId, [FromBody] string username, string ballColor, string playerColor, int ballSize)
        {
            DatabaseCommand.ExecuteQuery($"UPDATE Users SET Username = '{username}', Ball_Color = '{ballColor}', Player_Color = '{playerColor}', Ball_Size = '{ballSize}' WHERE Id = '{userId}'");
        }

        // DELETE api/values/5
        [HttpDelete("{userId}")]
        public void Delete(string userId)
        {
            DatabaseCommand.ExecuteQuery($"DELETE FROM Users WHERE Id = '{userId}'");
        }
    }
}
