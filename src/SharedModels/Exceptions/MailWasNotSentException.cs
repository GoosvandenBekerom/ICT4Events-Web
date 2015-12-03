using System;

namespace SharedModels.Exceptions
{
    public class MailWasNotSentException : Exception
    {
        public override string Message => "Confirmationmail was not sent due to an error.";
    }
}