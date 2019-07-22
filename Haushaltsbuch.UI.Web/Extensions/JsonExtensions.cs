using System.Linq;
using System.Text;

namespace Haushaltsbuch.UI.Web.Extensions
{
    public static class JsonExtensions
    {
        private const string INDENT_STRING = "    ";
        
        public static string FormatJson(this string str)
        {
            int indent = 0;
            bool quoted = false;
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < str.Length; i++)
            {
                char ch = str[index: i];
                switch (ch)
                {
                    case '{':
                    case '[':
                        sb.Append(value: ch);
                        if (!quoted)
                        {
                            sb.AppendLine();
                            Enumerable.Range(start: 0, count: ++indent).ForEach(action: item => sb.Append(value: INDENT_STRING));
                        }
                        break;
                    case '}':
                    case ']':
                        if (!quoted)
                        {
                            sb.AppendLine();
                            Enumerable.Range(start: 0, count: --indent).ForEach(action: item => sb.Append(value: INDENT_STRING));
                        }
                        sb.Append(value: ch);
                        break;
                    case '"':
                        sb.Append(value: ch);
                        bool escaped = false;
                        int index = i;
                        while (index > 0 && str[index: --index] == '\\')
                            escaped = !escaped;
                        if (!escaped)
                            quoted = !quoted;
                        break;
                    case ',':
                        sb.Append(value: ch);
                        if (!quoted)
                        {
                            sb.AppendLine();
                            Enumerable.Range(start: 0, count: indent).ForEach(action: item => sb.Append(value: INDENT_STRING));
                        }
                        break;
                    case ':':
                        sb.Append(value: ch);
                        if (!quoted)
                            sb.Append(value: " ");
                        break;
                    default:
                        sb.Append(value: ch);
                        break;
                }
            }
            return sb.ToString();
        }
    }
}
