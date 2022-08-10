using ServiceMessaging.MessageQueue;

namespace ServiceMessaging.Items
{
    public class ItemMessage : MessageQueueEvent
    {
        public int ItemId { get; set; }
        public string Name { get; set; }
        public ImageInfo ImageInfo { get; set; }
        public decimal Price { get; set; }
        public int Amount { get; set; }

        public ItemMessage() { }

        public ItemMessage(int itemId, string name, ImageInfo imageInfo, decimal price, int amount)
        {
            ItemId = itemId;
            Name = name;
            ImageInfo = imageInfo;
            Price = price;
            Amount = amount;
        }
    }
}
