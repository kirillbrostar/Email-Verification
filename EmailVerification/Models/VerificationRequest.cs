namespace EmailVerification.Models
{
    public class VerificationRequest
    {
        public required string Email { get; set; } // Добавлено required
        public required string Code { get; set; }   // Добавлено required
    }
}
