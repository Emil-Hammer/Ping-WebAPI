using System.Collections.Generic;
using System.Linq;
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
        [HttpGet("{type}", Name ="GetType")]
        public IEnumerable<UserScore> GetAllScoresFromType(string type)
        {
            var list = DatabaseCommand.ReadScore($"SELECT DISTINCT UserId, Score, Time, Type FROM User_Score WHERE Type = '{type}' ORDER BY Score DESC");
            var newList = new List<UserScore>();
            foreach (var variable in list)
            {
                var username = _usersController.GetUser(variable.UserId).Username;
                newList.Add(new UserScore(username, variable.Score, variable.Time, variable.Type));
            }
            return newList;
        }

        // GET: api/Highscore/{type}/{userid}
        [HttpGet("{type}/{userId}", Name = "Get")]
        public IEnumerable<UserScore> GetAllScoresFromSingleUser(string userId, string type)
        {
            var list = DatabaseCommand.ReadScore($"SELECT * FROM User_Score WHERE UserId ='{userId}' AND Type ='{type}' ORDER BY Score DESC");
            var newList = new List<UserScore>();
            foreach (var variable in list)
            {
                var username = _usersController.GetUser(variable.UserId).Username;
                newList.Add(new UserScore(username, variable.Score, variable.Time, variable.Type));
            }
            return newList;
        }

        // GET: api/Highscore/{type}/top/{amount}
        [HttpGet("{type}/top/{amount}", Name = "Top")]
        public IEnumerable<UserScore> GetTop(int amount, string type)
        {
            var list = DatabaseCommand.ReadScore(
                $"SELECT TOP {amount} MQ.UserId, MQ.Score, MQ.Time, MQ.Type " +
                $"FROM dbo.User_Score AS MQ " +
                $"JOIN(SELECT UserId, MAX(Score) AS Score " +
                    $"FROM dbo.User_Score " +
                    $"GROUP BY UserId) AS SQ " +
                $"ON SQ.UserId = MQ.UserId AND SQ.Score = MQ.Score " +
                $"WHERE Type = '{type}' " +
                $"ORDER BY Score DESC");
 
            var newList = new List<UserScore>();
            foreach (var variable in list)
            {
                var username = _usersController.GetUser(variable.UserId).Username;
                newList.Add(new UserScore(username, variable.Score, variable.Time, variable.Type));
            }
            return newList;
        }

        // POST: api/Highscore
        [HttpPost]
        public HttpStatusCode Post([FromBody] UserScore userScore)
        {
            int status = DatabaseCommand.ExecuteQuery($"INSERT INTO User_Score(UserId, Score, Time, Type) VALUES('{userScore.UserId}', '{userScore.Score}', '{userScore.Time}', '{userScore.Type}')");

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
