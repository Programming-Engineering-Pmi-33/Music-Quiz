using System.Collections.Generic;
using ServiceLayer.Exceptions.Models;

namespace ServiceLayer.Models
{
    public class Quiz
    {
        public int Id { get; }
        public string OwnerId { get; }

        public string Title { get; }
        public int AnswerTime { get; }
        public int PlayTime { get; }
        public string Genre { get; }
        public IEnumerable<string> Songs { get; }

        public Quiz(string ownerId, string title, int playTime, int answerTime, string genre, IEnumerable<string> songs, int id = 0)
        {
            if (answerTime < 10)
            {
                throw new QuizValidationException(nameof(AnswerTime));
            }

            if (playTime < 2 || playTime > 25)
            {
                throw new QuizValidationException(nameof(PlayTime));
            }

            if (title.Length < 1 || title.Length > 50)
            {
                throw new QuizValidationException(nameof(Title));
            }

            Id = id;
            OwnerId = ownerId;
            Title = title;
            PlayTime = playTime;
            AnswerTime = answerTime;
            Genre = genre;
            Songs = songs;
        }
    }
}
