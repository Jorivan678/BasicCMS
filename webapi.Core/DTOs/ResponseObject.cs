namespace webapi.Core.DTOs
{
    /// <summary>
    /// Represents a generic response of an any HTTP code (other than 400 Bad Request).
    /// </summary>
    public class ResponseObject
    {
        public ResponseObject(string title, string type, int statusCode, string message)
        {
            Title = title;
            Type = type;
            StatusCode = statusCode;
            Message = message;
        }

        public string Title { get; }
        public string Message { get; }
        public int StatusCode { get; }
        public string Type { get; }
    }
}
