namespace BasketService.BLL.Entities.Insert
{
    public class ItemInsertViewModel
    {
        public int ItemId { get; set; }
        public string Name { get; set; }
        public ImageInfoInsertViewModel ImageInfo { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}
