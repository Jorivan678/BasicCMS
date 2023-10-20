namespace webapi.Core.Exceptions
{
    /// <summary>
    /// Represents a HTTP 401 message error.
    /// </summary>
    public class UnauthorizedException : Exception
    {
        public UnauthorizedException() : base() { }

        public UnauthorizedException(string message) : base(message) { }

        public UnauthorizedException(string message, Exception innerException) : base(message, innerException) { }
    }
}
