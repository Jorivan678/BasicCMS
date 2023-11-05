namespace webapi.Core.DTOs
{
    /// <summary>
    /// Represents the response of an HTTP 400 Bad Request.
    /// </summary>
    public class ResponseErrorObject
    {
        public ResponseErrorObject(string type, string title, int status, IEnumerable<string> keys, IEnumerable<IEnumerable<string>> messages)
        {
            Type = type;
            Title = title;
            Status = status;
            Errors = new List<ModelError>();

            for (int i = 0; i < keys.Count() && i < messages.Count(); i++)
                Errors.Add(new ModelError(keys.ElementAt(i), messages.ElementAt(i)));
        }
        public string Type { get; }
        public string Title { get; }
        public int Status { get; }
        public ICollection<ModelError> Errors { get; }

        public class ModelError
        {
            public ModelError(string key, IEnumerable<string> messages)
            {
                Key = key;
                Messages = messages;
            }
            public string Key { get; }

            public IEnumerable<string> Messages { get; }
        }
    }
}
