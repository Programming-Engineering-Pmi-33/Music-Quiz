using System.ComponentModel.DataAnnotations;

namespace DbTestProject.Models
{
    public class Genre
    {
        [Key]
        public string Name { get; set; }
    }
}
