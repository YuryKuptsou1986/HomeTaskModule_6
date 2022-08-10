using BasketService.BLL.Entities.Update;
using ServiceMessaging.Items;
using ServiceMessaging.MessageQueue.Handler;

namespace BasketService.BLL.Services
{
    public class ItemChangeMessageQueueEvent : IMessageQueueEventHadler<ItemMessage>
    {
        private readonly ICartService _cartService;
        public ItemChangeMessageQueueEvent(ICartService cartService)
        {
            _cartService = cartService;
        }

        public Task Handle(ItemMessage queueEvent)
        {
            var itemUpdate = new ItemUpdateViewModel() {
                ItemId = queueEvent.ItemId,
                Name = queueEvent.Name,
                Price = queueEvent.Price,
                Quantity = queueEvent.Amount,
                ImageInfo = new ImageUpdateViewModel() {
                    AltText = queueEvent.ImageInfo.AltText,
                    Url = queueEvent.ImageInfo.Url
                }
            };

            _cartService.UpdateItem(itemUpdate);

            return Task.CompletedTask;
        }
    }
}
