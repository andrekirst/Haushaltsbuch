namespace Haushaltsbuch.Library.Infrastructure.Extensions
{
    public static class PrefixRemover
    {
        public static string RemovePrefix(this string value, string prefix) =>
            value.StartsWith(value: prefix) ? value.Substring(startIndex: prefix.Length) : value;
    }
}