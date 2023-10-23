namespace webapi.Core.DTOs
{
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
