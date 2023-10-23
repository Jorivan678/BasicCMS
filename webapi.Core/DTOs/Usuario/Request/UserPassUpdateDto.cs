namespace webapi.Core.DTOs.Usuario
{
    public class UserPassUpdateDto
    {
        public virtual int Id { get; set; }

        public virtual string OldPassword { get; set; } = null!;

        public virtual string NewPassword { get; set; } = null!;
    }
}
