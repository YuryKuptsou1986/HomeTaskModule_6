using Microsoft.AspNetCore.Mvc;
using System.Runtime.Serialization;

namespace WebAPI.Resources
{
    [DataContract(Name = "UserList", Namespace = "")]
    [KnownType(typeof(ResourceBase))]
    public class ItemResourceList : ResourceBase
    {
        public ItemResourceList(IUrlHelper urlHelper, IReadOnlyCollection<ItemResource> items) : base(urlHelper)
        {
            Items = items ?? throw new ArgumentNullException(nameof(items));
        }

        [DataMember(Order = 1)]
        public IEnumerable<ItemResource> Items { get; }
    }
}
