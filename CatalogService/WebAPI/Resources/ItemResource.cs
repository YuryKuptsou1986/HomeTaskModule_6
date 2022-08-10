using Microsoft.AspNetCore.Mvc;
using System.Runtime.Serialization;
using ViewModel.View;

namespace WebAPI.Resources
{
    [DataContract(Name = "User", Namespace = "")]
    [KnownType(typeof(ResourceBase))]
    public class ItemResource : ResourceBase
    {
        public ItemResource(IUrlHelper urlHelper) : base(urlHelper)
        {
        }

        public ItemResource(IUrlHelper urlHelper, ItemViewModel item) : base(urlHelper)
        {
            if (item is null)
                throw new System.ArgumentNullException(nameof(item));

            Id = item.Id;
            Name = item.Name;
            Description = item.Description;
            Image = item.Image;
            CategoryId = item.CategoryId;
            Price = item.Price;
            Amount = item.Amount;
        }

        [DataMember(Order = 1)]
        public int Id { get; set; }

        [DataMember(Order = 2)]
        public string Name { get; set; }

        [DataMember(Order = 3)]
        public string Description { get; set; }

        [DataMember(Order = 4)]
        public Uri? Image { get; set; }

        [DataMember(Order = 5)]
        public int CategoryId { get; set; }

        [DataMember(Order = 6)]
        public decimal Price { get; set; }

        [DataMember(Order = 7)]
        public int Amount { get; set; }
    }
}
