using System;

namespace PingWebApi.Model
{
    public class UserScore
    {
        public UserScore(string userId, int score, string type)
        {
            UserId = userId;
            Score = score;
            Type = type;
            Time = DateTime.Now;
        }

        public string UserId { get; set; }

        public int Score { get; set; }

        public DateTime Time { get; set; }

        public string Type { get; set; }
    }
}