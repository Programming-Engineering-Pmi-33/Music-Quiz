using System.ComponentModel.DataAnnotations;

namespace DbTestProject.Models
{
    public class Genre
    {
        [Key, MinLength(1), MaxLength(50)] 
        public string Name { get; set; }
    }
}
