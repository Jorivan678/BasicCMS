namespace webapi.Core.Exceptions
{
    /// <summary>
    /// Represents a HTTP 400 message error.
    /// </summary>
    public class BusinessException : Exception
    {
        public BusinessException() : this("A validation error has ocurred.", null) { }

        public BusinessException(string? message) : this(message, null) { }

        public BusinessException(string? message, Exception? innerException) : base(message, innerException) 
        { 
            Keys = Array.Empty<string>(); 
            Values = Array.Empty<string[]>(); 
            HasKeysAndValues = false; 
        }

        public BusinessException(IEnumerable<string> keys, IEnumerable<IEnumerable<string>> values) : base()
        {
            if (keys is null || values is null)
                throw new ArgumentNullException(keys is null ? nameof(keys) : nameof(values), "No parameters can be null.");

            HasKeysAndValues = true;
            Keys = keys;
            Values = values;
        }

        public bool HasKeysAndValues { get; }

        public IEnumerable<string> Keys { get; }

        public IEnumerable<IEnumerable<string>> Values { get; }
    }
}
