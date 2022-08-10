using BasketService.Domain.Entities;

namespace BasketService.BLL.Entities.View
{
    public class ItemViewModel
    {
        public int Id { get; set; }
        public int ItemId { get; set; }
        public string Name { get; set; }
        public ImageInfoViewModel ImageInfo { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}
