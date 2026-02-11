namespace Application.Helpers
{

    public static class StringHelper
    {
        public static bool EqualsIgnoreCase(string? a, string? b)
            => string.Equals(a, b, StringComparison.OrdinalIgnoreCase);

        public static bool ContainsIgnoreCase(string source, string value)
            => source.Contains(value, StringComparison.OrdinalIgnoreCase);

        public static string Normalize(string value)
            => value.Trim().ToUpperInvariant();
    }

}
