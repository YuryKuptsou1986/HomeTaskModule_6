namespace BasketService.BLL.Entities.Update
{
    public class ItemUpdateViewModel
    {
        public int ItemId { get; set; }
        public string Name { get; set; }
        public ImageUpdateViewModel ImageInfo { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}
