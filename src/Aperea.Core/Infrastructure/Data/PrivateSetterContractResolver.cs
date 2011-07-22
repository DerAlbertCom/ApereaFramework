using System.Linq;
using System.Reflection;
using Newtonsoft.Json.Serialization;

namespace Aperea.Infrastructure.Data
{
    public class PrivateSetterContractResolver : DefaultContractResolver
    {
        public PrivateSetterContractResolver(bool shareCache):base(shareCache)
        {
            DefaultMembersSearchFlags = BindingFlags.Public | BindingFlags.Instance | BindingFlags.NonPublic;
        }
        protected override System.Collections.Generic.List<MemberInfo> GetSerializableMembers(System.Type objectType)
        {
            return base.GetSerializableMembers(objectType).Where(m => m.MemberType == MemberTypes.Property).ToList();
        }
    }
}