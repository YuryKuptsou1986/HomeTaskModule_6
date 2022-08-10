namespace BasketService.Domain.Entities
{
    public class Item
    {
        public int Id { get; private set; }
        public int ItemId { get; private set; }
        public string Name { get; private set; }
        public ImageInfo ImageInfo { get; private set; }
        public decimal Price { get; private set; }
        public int Quantity { get; private set; }

        private Item()
        {

        }

        public Item(string name, decimal price, int quantity, ImageInfo imageInfo = null)
        {
            Update(name, price, quantity, imageInfo);
        }

        public void Update(string name, decimal price, int quantity, ImageInfo imageInfo = null)
        {
            Name = name;
            Price = price;
            Quantity = quantity;
            ImageInfo = imageInfo;
        }
    }
}
