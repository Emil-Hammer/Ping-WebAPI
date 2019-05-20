using System;

namespace PingWebApi.Model
{
    public class UserScore
    {
        public UserScore(string userId, int score, DateTime time, string type)
        {
            UserId = userId;
            Score = score;
            Time = time;
            Type = type;           
        }

        public string UserId { get; set; }

        public int Score { get; set; }

        public DateTime Time { get; set; }

        public string Type { get; set; }
    }
}