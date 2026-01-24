namespace SafeVault.Web.Services
{
    public static class InputSanitizer
    {
        public static string Sanitize(string input)
        {
            if (string.IsNullOrEmpty(input))
                return input;

            // Remove single quotes
            input = input.Replace("'", "");

            // Remove SQL comment patterns
            input = input.Replace("--", "");

            // Remove dangerous SQL keywords (basic example)
            string[] sqlKeywords = { "DROP", "DELETE", "INSERT", "UPDATE", "SELECT" };
            foreach (var keyword in sqlKeywords)
            {
                input = input.Replace(keyword, "", StringComparison.OrdinalIgnoreCase);
            }

            // Remove basic XSS tags
            string[] xssTags = { "<script>", "</script>", "alert" };
            foreach (var tag in xssTags)
            {
                input = input.Replace(tag, "", StringComparison.OrdinalIgnoreCase);
            }

            return input;
        }
    }
}
