namespace webapi.Core.Exceptions
{
    /// <summary>
    /// Represents a HTTP 403 message error.
    /// </summary>
    public class ForbiddenException : Exception
    {
        public ForbiddenException() : base() { }

        public ForbiddenException(string message) : base(message) { }

        public ForbiddenException(string message, Exception innerException) : base(message, innerException) { }
    }
}
