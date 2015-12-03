using System;

namespace SharedModels.Exceptions
{
    /// <summary>
    /// This exception should be thrown when 2 passwords do not match.
    /// </summary>
    public class PasswordsDontMatchException : Exception
    {
        public override string Message => "Ingevulde wachtwoorden komen niet overeen.";
    }
}