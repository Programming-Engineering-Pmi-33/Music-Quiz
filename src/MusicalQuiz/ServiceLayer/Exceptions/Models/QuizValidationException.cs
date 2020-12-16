using System;

namespace ServiceLayer.Exceptions.Models
{
    public class QuizValidationException : Exception
    {
        public QuizValidationException(string message) : base(message) { }
    }
}