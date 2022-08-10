using System.Collections.Generic;

namespace BasketService.Domain.Entities
{
    public class Cart
    {
        public string Id { get; private set; }
        public IList<Item> Items { get; private set; } = new List<Item>();
    }
}
