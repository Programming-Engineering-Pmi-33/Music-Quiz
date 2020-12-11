using System.ComponentModel.DataAnnotations;

namespace StorageLayer.Models
{
    public class Genre
    {
        [Key, MinLength(1), MaxLength(50)]
        public string Name { get; set; }
    }
}