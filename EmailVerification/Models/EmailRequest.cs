using System.ComponentModel.DataAnnotations;

namespace EmailVerification.Models
{
    public class EmailRequest
    {
        public required string Email { get; set; } // Добавлено required
    }
}
