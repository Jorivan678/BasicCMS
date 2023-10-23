namespace webapi.Core.DTOs.Usuario
{
    public class UserLoginRequestDto
    {
        public virtual string UserName { get; set; } = null!;

        public virtual string Password { get; set; } = null!;
    }
}
