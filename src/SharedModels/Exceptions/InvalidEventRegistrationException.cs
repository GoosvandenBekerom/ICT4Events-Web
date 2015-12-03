using System;

namespace SharedModels.Exceptions
{
    public class InvalidEventRegistrationException : Exception
    {
        public InvalidEventRegistrationException(string message) : base(message)
        {
        }
    }
}