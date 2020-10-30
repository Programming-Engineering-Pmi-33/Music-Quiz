using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DbTestProject.Models
{
    public class Song
    {
        [Key]
        public int Id { get; set; }

        [Required, MinLength(30)]
        public string Link { get; set; }

        public List<QuizSong> QuizSong { get; set; }

        public Song()
        {
            QuizSong = new List<QuizSong>();
        }
    }
}
