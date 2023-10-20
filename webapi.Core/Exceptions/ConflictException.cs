namespace webapi.Core.Exceptions
{
    /// <summary>
    /// Represents a HTTP 409 message error.
    /// </summary>
    public class ConflictException : Exception
    {
        public ConflictException() : base() { }

        public ConflictException(string message) : base(message) { }

        public ConflictException(string message, Exception innerException) : base(message, innerException) { }
    }
}
