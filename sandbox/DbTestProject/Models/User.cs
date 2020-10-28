using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DbTestProject.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Username { get; set; }
        
        [Required] 
        public string Password { get; set; }

        public List<Quiz> Quizzes { get; set; }
        public List<Score> Scores { get; set; }
        public int AppRating { get; set; }
    }
}
