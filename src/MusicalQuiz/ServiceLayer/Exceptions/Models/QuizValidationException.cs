using System;
using System.Runtime.Serialization;

namespace ServiceLayer.Exceptions.Models
{
    public class QuizValidationException : Exception
    {
        public QuizValidationException()
        {
        }

        public QuizValidationException(string message) : base(message)
        {
        }

        protected QuizValidationException(SerializationInfo info, StreamingContext context) : base(info,
            context)
        {
        }

        public QuizValidationException(string message, Exception innerException) : base(message,
            innerException)
        {
        }
    }
}