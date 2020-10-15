using System.ComponentModel.DataAnnotations.Schema;

namespace DbTestProject.Models
{
    public class QuizSong
    {
        [ForeignKey(nameof(QuizId))]
        public int QuizId { get; set; }

        public int SongId { get; set; }


        [ForeignKey(nameof(QuizId))]
        public Quiz Quiz { get; set; }

        [ForeignKey(nameof(SongId))]
        public Song Song { get; set; }
    }
}
