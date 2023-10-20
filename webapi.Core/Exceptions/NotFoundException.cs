namespace webapi.Core.Exceptions
{
    /// <summary>
    /// Represents a HTTP 404 messsage error.
    /// </summary>
    public class NotFoundException : Exception
    {
        public NotFoundException() : base() { }
        public NotFoundException(string message) : base(message) { }
        public NotFoundException(string message, Exception innerException) : base(message, innerException) { }
    }
}
