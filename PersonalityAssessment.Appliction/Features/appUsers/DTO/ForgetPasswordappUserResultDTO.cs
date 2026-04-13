namespace PersonalityAssessment.Application.Features.Options.DTO
{
    public class ForgetPasswordappUserResultDTO
    {
        public string Message { get; set; } = string.Empty;

        /// <summary>
        /// Identity password-reset token when the email matches an account; otherwise null.
        /// In production, send this via email instead of returning it in the API response.
        /// </summary>
        public string? ResetToken { get; set; }
    }
}
