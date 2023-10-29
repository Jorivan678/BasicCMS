using System.Diagnostics.CodeAnalysis;

namespace webapi.Application.Tools.Options
{
    public sealed class TokenServiceOptions
    {
        private string _sectionName = string.Empty;

        /// <summary>
        /// Sets the section name that token service will take to create a token.
        /// </summary>
        /// <param name="sectionName">The section name.</param>
        /// <exception cref="ArgumentNullException"></exception>
        public void SetSectionName([NotNull]string? sectionName)
        {
            ArgumentException.ThrowIfNullOrEmpty(sectionName, nameof(sectionName));

            _sectionName = sectionName;
        }

        public string SectionName => _sectionName;
    }
}
