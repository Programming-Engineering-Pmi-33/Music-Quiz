using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DbTestProject.Models
{
    public class Quiz
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string OwnerUserId { get; set; }

        [ForeignKey("OwnerUserId")]
        public User OwnerUser { get; set; }

        [Required, MinLength(1), MaxLength(50)]
        public string Title { get; set; }

        [Required, Range(2, 25)]
        public int PlayTime { get; set; }

        [Required, Range(10, int.MaxValue)]
        public int AnswerTime { get; set; }

        public string GenreId { get; set; }

        public Genre Genre { get; set; }
        public List<QuizSong> QuizSongs { get; set; }

        public Quiz()
        {
            QuizSongs = new List<QuizSong>();
        }
    }
}
