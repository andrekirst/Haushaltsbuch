using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Haushaltsbuch.Library.Infrastructure.Implementations
{
    public class PrivateSetterContractResolver : DefaultContractResolver
    {
        protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
        {
            JsonProperty prop = base.CreateProperty(member: member, memberSerialization: memberSerialization);

            if (!prop.Writable)
            {
                PropertyInfo property = member as PropertyInfo;
                if (property != null)
                {
                    prop.Writable = property.GetSetMethod(nonPublic: true) != null;
                }
            }

            return prop;
        }
    }
}