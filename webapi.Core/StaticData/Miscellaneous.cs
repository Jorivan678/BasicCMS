namespace webapi.Core.StaticData
{
    public static class Miscellaneous
    {
        /// <summary>
        /// Represents the password regular expression.
        /// <para>Read it as: A string must contain at least one lowercase letter, one uppercase letter, one digit, and one special character. </para>
        /// </summary>
        public const string PasswordRegex = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*\W).+$";

        /// <summary>
        /// Represents the user name regular expression.
        /// <para>Read it as: A string must consist of letters, numbers, or underscores.</para>
        /// </summary>
        public const string UserNameRegex = @"^[\p{L}\p{N}_]+$";

        /// <summary>
        /// Maximum length of most varchar fields in the database.
        /// </summary>
        public const int RegularMaxLength = 30;

        /// <summary>
        /// The password minimum length.
        /// </summary>
        public const int PasswordMinLength = 8;

        /// <summary>
        /// The description maximum length.
        /// </summary>
        public const int DescriptionMaxLength = 150;

        /// <summary>
        /// Article title maximum length.
        /// </summary>
        public const int ArticleTitleMaxLength = 50;

        /// <summary>
        /// Comment text maximum length.
        /// </summary>
        public const int CommentTextMaxLength = 650;

        /// <summary>
        /// User name maximum length.
        /// </summary>
        public const int UserNameMaxLength = 50;
    }
}
