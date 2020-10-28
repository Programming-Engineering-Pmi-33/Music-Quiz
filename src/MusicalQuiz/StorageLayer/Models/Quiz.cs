using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace StorageLayer.Models
{
    public class Quiz
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int OwnerUserId { get; set; }

        [Required, MinLength(1), MaxLength(50)]
        public string Title { get; set; }

        [Required, Range(2, 25)]
        public int PlayTime { get; set; }

        [Required, Range(10, int.MaxValue)]
        public int AnswerTime { get; set; }

        public int GenreId { get; set; }

        public Genre Genre { get; set; }
        public List<QuizSong> QuizSongs { get; set; }
    }
}