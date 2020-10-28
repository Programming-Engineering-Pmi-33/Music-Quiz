using System.ComponentModel.DataAnnotations;

namespace StorageLayer.Models
{
    public class Genre
    {
        [Key] 
        public int Id { get; set; }

        [MinLength(1), MaxLength(50)] 
        public string Name { get; set; }
    }
}