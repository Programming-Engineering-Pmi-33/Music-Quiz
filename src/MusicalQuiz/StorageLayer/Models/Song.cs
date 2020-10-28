using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace StorageLayer.Models
{
    public class Song
    {
        [Key]
        public int Id { get; set; }

        [Required, MinLength(30)]
        public string Link { get; set; }

        public List<QuizSong> QuizSong { get; set; }
    }
}