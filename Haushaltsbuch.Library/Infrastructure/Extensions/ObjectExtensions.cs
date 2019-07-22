using Newtonsoft.Json;

namespace Haushaltsbuch.Library.Infrastructure.Extensions
{
    public static class ObjectExtensions
    {
        public static string ToJson(this object value) => JsonConvert.SerializeObject(value: value);
    }
}