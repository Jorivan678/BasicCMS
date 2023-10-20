namespace webapi.Core.Exceptions
{
    /// <summary>
    /// Represents a HTTP 422 message error.
    /// </summary>
    public class UnprocessableEntityException : Exception
    {
        public UnprocessableEntityException() : base() { }
        public UnprocessableEntityException(string message) : base(message) { }
        public UnprocessableEntityException(string message, Exception innerException) : base(message, innerException) { }
    }
}
