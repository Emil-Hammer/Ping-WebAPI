using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using PingWebApi.Model;
using System.Net;

namespace PingWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HighscoreController : ControllerBase
    {
        private UsersController _usersController = new UsersController();

        // GET: api/Highscore
        [HttpGet("type/{type}", Name ="GetType")]
        public IEnumerable<UserScore> GetAllScoresFromType(string type)
        {
            var list = DatabaseCommand.ReadScore($"SELECT DISTINCT UserId, Score FROM User_Score ORDER BY Score DESC WHERE Type = '{type}'");
            var newList = new List<UserScore>();
            foreach (var variable in list)
            {
                var username = _usersController.GetUser(variable.UserId).Username;
                newList.Add(new UserScore(username, variable.Score, variable.Type));
            }
            return newList;
        }

        // GET: api/Highscore/5
        [HttpGet("{userId}", Name = "Get")]
        public IEnumerable<UserScore> GetAllScoresFromSingleUser(string userId)
        {
            var list = DatabaseCommand.ReadScore($"SELECT * FROM User_Score WHERE UserId ='{userId}' ORDER BY Score DESC");
            var newList = new List<UserScore>();
            foreach (var variable in list)
            {
                var username = _usersController.GetUser(variable.UserId).Username;
                newList.Add(new UserScore(username, variable.Score, variable.Type));
            }
            return newList;
        }

        // GET: api/Highscore/top/5
        [HttpGet("top/{amount}", Name = "Top")]
        public IEnumerable<UserScore> GetTop(int amount)
        {
            var list = DatabaseCommand.ReadScore($"SELECT TOP {amount} UserId, MAX(Score) as Score FROM User_Score GROUP BY UserId Order by SCore DESC");
            var newList = new List<UserScore>();
            foreach (var variable in list)
            {
                var username = _usersController.GetUser(variable.UserId).Username;
                newList.Add(new UserScore(username, variable.Score, variable.Type));
            }
            return newList;
        }

        // POST: api/Highscore
        [HttpPost]
        public HttpStatusCode Post([FromBody] UserScore userScore)
        {
            int status = DatabaseCommand.ExecuteQuery($"INSERT INTO User_Score(UserId, Score) VALUES('{userScore.UserId}', '{userScore.Score}')");

            if (status == 1)
            {
                return HttpStatusCode.OK;
            }
            return HttpStatusCode.BadRequest;
        }

        // DELETE: api/5
        [HttpDelete("{userId}")]
        public void DeleteAllSpecificUserScores(string userId)
        {
            DatabaseCommand.ExecuteQuery($"DELETE FROM User_Score WHERE UserId = '{userId}'");
        }
    }
}
