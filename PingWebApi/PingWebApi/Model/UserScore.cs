namespace PingWebApi.Model
{
    public class UserScore
    {
        public UserScore(string userId, int score)
        {
            UserId = userId;
            Score = score;
        }

        public string UserId { get; set; }

        public int Score { get; set; }
    }
}