using System;

namespace Business.CustomException
{
    internal class EMailException : Exception
    {
        public EMailException()
        {
        }

        public EMailException(string message) : base($"Failed To Send Mail{message}")
        {
        }

        public EMailException(string message, Exception innerException) : base($"Failed To Send Mail{message}", innerException)
        {
        }

    }
}
