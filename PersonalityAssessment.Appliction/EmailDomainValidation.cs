using System.Text.RegularExpressions;

namespace PersonalityAssessment.Application
{
    /// <summary>
    /// Local, format-only checks (no DNS or external verification).
    /// </summary>
    public static class EmailDomainValidation
    {
        private static readonly Regex LabelRegex = new(
            @"^[a-zA-Z0-9]([a-zA-Z0-9-]*[a-zA-Z0-9])?$",
            RegexOptions.Compiled | RegexOptions.CultureInvariant);

        public static bool IsGmailCompatibleDomain(string? email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;
            var at = email.AsSpan().LastIndexOf('@');
            if (at < 0 || at == email.Length - 1)
                return false;
            var domain = email[(at + 1)..].Trim().ToLowerInvariant();
            return domain is "gmail.com" or "googlemail.com";
        }

        /// <summary>
        /// Requires a plausible hostname: labels, dot-separated, TLD alphabetic and at least 2 chars.
        /// </summary>
        public static bool HasPlausibleMailDomain(string? email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            var trimmed = email.Trim();
            var at = trimmed.LastIndexOf('@');
            if (at <= 0 || at == trimmed.Length - 1)
                return false;

            var domain = trimmed[(at + 1)..];
            if (string.IsNullOrWhiteSpace(domain) || !domain.Contains('.'))
                return false;

            var labels = domain.Split('.');
            if (labels.Length < 2)
                return false;

            var tld = labels[^1];
            if (tld.Length < 2 || !tld.All(c => c >= 'a' && c <= 'z' || c >= 'A' && c <= 'Z'))
                return false;

            foreach (var label in labels)
            {
                if (label.Length is 0 or > 63)
                    return false;
                if (!LabelRegex.IsMatch(label))
                    return false;
            }

            return true;
        }
    }
}
