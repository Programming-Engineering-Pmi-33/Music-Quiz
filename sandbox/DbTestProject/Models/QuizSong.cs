﻿using System.ComponentModel.DataAnnotations.Schema;

namespace DbTestProject.Models
{
    public class QuizSong
    {
        public int QuizId { get; set; }

        public int SongId { get; set; }


        public Quiz Quiz { get; set; }

        public Song Song { get; set; }
    }
}
