using System.ComponentModel.DataAnnotations;

namespace StorageLayer.Models
{
    public class Score
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        public int QuizId { get; set; }

        [Required]
        public int Points { get; set; }


        public Quiz Quiz { get; set; }
    }
}