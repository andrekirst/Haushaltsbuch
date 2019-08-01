using System.Text.RegularExpressions;

namespace Haushaltsbuch.Library.Infrastructure.Extensions
{
    public static class EMailValidatorExtensions
    {
        public static bool IsValidEMailAddress(this string email) =>
            Regex.IsMatch(
                input: email,
                pattern: @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$");
    }
}