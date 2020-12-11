namespace ServiceLayer.Models
{
    public class Score
    {
        public string Username { get; }
        public int Points { get; }

        public Score(string username, int points)
        {
            Username = username;
            Points = points;
        }
    }
}