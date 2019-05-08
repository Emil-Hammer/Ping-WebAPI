namespace PingWebApi.Model
{
    public class Users
    {
        public Users(string id, string username)
        {
            Id = id;
            Username = username;
            BallColor = "ff9900";
            PlayerColor = "33ccff";
            BallSize = 5;
        }

        public string Id { get; set; }

        public string Username { get; set; }

        public string BallColor { get; set; }

        public string PlayerColor { get; set; }

        public int BallSize { get; set; }
    }
}