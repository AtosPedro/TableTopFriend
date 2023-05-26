using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace TableTopFriend.Infrastructure.Utils;

public class PrivateResolver : DefaultContractResolver
{
    protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
    {
        var prop = base.CreateProperty(member, memberSerialization);
        if (!prop.Writable)
        {
            var property = member as PropertyInfo;
            if (property != null)
                prop.Writable = property.GetSetMethod(true) is not null;
        }

        return prop;
    }
}
