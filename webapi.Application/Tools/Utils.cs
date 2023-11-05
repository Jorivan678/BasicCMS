namespace webapi.Application.Tools
{
    public static class Utils
    {
        /// <summary>
        /// Custom extension to convert the categories string received in controller to the expected
        /// category array.
        /// </summary>
        /// <param name="str">The current <see cref="string"/> instance.</param>
        /// <returns>A category array.</returns>
        public static string[] ToCategoryArray(this string str)
        {
            str = str.TrimStart().TrimEnd().ToUpper();

            return str.Contains('+') ? str.Split('+').Where(x => !string.IsNullOrWhiteSpace(x)).ToArray() : str.Split(' ').Where(x => !string.IsNullOrWhiteSpace(x)).ToArray();
        }
    }
}
