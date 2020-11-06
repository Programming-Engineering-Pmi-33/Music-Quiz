using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace StorageLayer.Models
{
    public class User
    {
        [Key, MinLength(2), MaxLength(50)]
        public string Username { get; set; }

        [Required, MinLength(4), MaxLength(255)]
        public string Password { get; set; }
        
        [Range(1,5)]
        public int? AppRating { get; set; }
        
        public List<Quiz> Quizzes { get; set; }
        public List<Score> Scores { get; set; }

        public User()
        {
            Quizzes = new List<Quiz>();
            Scores = new List<Score>();
        }
    }
}