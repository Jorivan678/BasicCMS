namespace webapi.Core.DTOs
{
    public sealed class TokenResponseDto
    {
        public TokenResponseDto(string token)
        {
            Token = token;
        }

        public string Token { get; }
    }
}
