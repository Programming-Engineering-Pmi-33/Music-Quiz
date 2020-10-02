using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DbTestProject.Models
{
    public class Song
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Link { get; set; }

        public List<QuizSong> QuizSong { get; set; }
    }
}
