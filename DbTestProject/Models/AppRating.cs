using System.ComponentModel.DataAnnotations;

namespace DbTestProject.Models
{
    public class AppRating
    {
        [Key]
        public int UserId { get; set; }

        [Required]
        public int Points { get; set; }
    }
}
