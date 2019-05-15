using System.Collections.Generic;
using System.Linq;
using System.Net;
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
        public Users GetUser(string userId)
        {
            var list = DatabaseCommand.ReadUsers($"SELECT * FROM Users WHERE Id = '{userId}'");
            if (list.Count == 0)
            {
                throw new System.Web.Http.HttpResponseException(HttpStatusCode.NotFound);
            }
            return list.First();
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] Users user)
        {
            DatabaseCommand.ExecuteQuery($"INSERT INTO Users(Id, Username) VALUES('{user.Id}', '{user.Username}')");
        }

        // PUT api/values/5
        [HttpPut("{userId}")]
        public void Put(string userId, [FromBody] Users user)
        {
            DatabaseCommand.ExecuteQuery($"UPDATE Users SET Username = '{user.Username}', Ball_Color = '{user.BallColor}', Player_Color = '{user.PlayerColor}', Ball_Size = '{user.BallSize}' WHERE Id = '{userId}'");
        }

        // DELETE api/values/5
        [HttpDelete("{userId}")]
        public void Delete(string userId)
        {
            DatabaseCommand.ExecuteQuery($"DELETE FROM Users WHERE Id = '{userId}'");
        }
    }
}
