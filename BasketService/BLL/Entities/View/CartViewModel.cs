using System.Collections.Generic;

namespace BasketService.BLL.Entities.View
{
    public class CartViewModel
    {
        public string Id { get; set; }

        public IEnumerable<ItemViewModel> Items { get; set; }
    }
}
