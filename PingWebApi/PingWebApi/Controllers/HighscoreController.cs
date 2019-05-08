using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using PingWebApi.Model;

namespace PingWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HighscoreController : ControllerBase
    {
        // GET: api/Highscore
        [HttpGet]
        public IEnumerable<UserScore> GetAllScores()
        {
            return DatabaseCommand.ReadScore("SELECT * FROM User_Score");
        }

        // GET: api/Highscore/5
        [HttpGet("{id}", Name = "Get")]
        public IEnumerable<UserScore> GetAllScoresFromSingleUser(string userId)
        {
            return DatabaseCommand.ReadScore($"SELECT * FROM User_Score WHERE UserId ='{userId}'");
        }

        [HttpGet("top", Name = "Top")]
        public IEnumerable<UserScore> GetTop100(int amount)
        {
            return DatabaseCommand.ReadScore($"SELECT TOP {amount} * FROM User_Score ORDER BY Score DESC");
        }

        // POST: api/Highscore
        [HttpPost]
        public void Post([FromBody] string userId, int score)
        {
            DatabaseCommand.ExecuteQuery($"INSERT INTO User_Score(UserId, Score) VALUES('{userId}', '{score}')");
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void DeleteAllSpecificUserScores(string userId)
        {
            DatabaseCommand.ExecuteQuery($"DELETE FROM User_Score WHERE UserId = '{userId}'");
        }
    }
}
