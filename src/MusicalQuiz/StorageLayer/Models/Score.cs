using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StorageLayer.Models
{
    public class Score
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string OwnerUserId { get; set; }

        [Required]
        public int QuizId { get; set; }

        [Required]
        public int Points { get; set; }


        public Quiz Quiz { get; set; }

        
        [ForeignKey("OwnerUserId")]
        public User OwnerUser { get; set; }
    }
}