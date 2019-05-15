using System.Collections.Generic;
using System.Net;
using System.Web.Http;
using Microsoft.AspNetCore.Mvc;
using PingWebApi.Model;

namespace PingWebApi.Controllers
{
    [Microsoft.AspNetCore.Mvc.Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        // GET api/values
        [Microsoft.AspNetCore.Mvc.HttpGet]
        public IEnumerable<Users> GetAllUsers()
        {
            return DatabaseCommand.ReadUsers("SELECT * FROM Users");
        }

        // GET api/values/5
        [Microsoft.AspNetCore.Mvc.HttpGet("{userId}")]
        public IEnumerable<Users> GetUser(string userId)
        {
            var list = DatabaseCommand.ReadUsers($"SELECT * FROM Users WHERE Id = '{userId}'");
            if (list.Count == 0)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            return list;
        }

        // POST api/values
        [Microsoft.AspNetCore.Mvc.HttpPost]
        public void Post([Microsoft.AspNetCore.Mvc.FromBody] Users user)
        {
            DatabaseCommand.ExecuteQuery($"INSERT INTO Users(Id, Username) VALUES('{user.Id}', '{user.Username}')");
        }

        // PUT api/values/5
        [Microsoft.AspNetCore.Mvc.HttpPut("{userId}")]
        public void Put(string userId, [Microsoft.AspNetCore.Mvc.FromBody] Users user)
        {
            DatabaseCommand.ExecuteQuery($"UPDATE Users SET Username = '{user.Username}', Ball_Color = '{user.BallColor}', Player_Color = '{user.PlayerColor}', Ball_Size = '{user.BallSize}' WHERE Id = '{userId}'");
        }

        // DELETE api/values/5
        [Microsoft.AspNetCore.Mvc.HttpDelete("{userId}")]
        public void Delete(string userId)
        {
            DatabaseCommand.ExecuteQuery($"DELETE FROM Users WHERE Id = '{userId}'");
        }
    }
}
