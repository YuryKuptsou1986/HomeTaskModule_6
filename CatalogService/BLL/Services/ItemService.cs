using AutoMapper;
using DAL.Interfaces;
using Domain.Entities;
using DAL.Models.Update;
using ViewModel.Update;
using ViewModel.View;
using ViewModel.Insert;
using ViewModel.Page;
using ViewModel.Query;
using ServiceMessaging.MessageQueue;
using ServiceMessaging.Items;

namespace BLL.Services
{
    internal class ItemService : IItemService
    {
        private readonly IItemRepository _repository;
        private readonly IMessageQueueSender _mqService;
        private readonly IMapper _mapper;

        public ItemService(IItemRepository repository, IMessageQueueSender mqService, IMapper mapper)
        {
            _repository = repository;
            _mqService = mqService;
            _mapper = mapper;
        }

        public async Task DeleteAsync(int id)
        {
            await _repository.DeleteAsync(id);
        }

        public async Task<ItemViewModel> GetAsync(int id)
        {
            var item = await _repository.GetAsync(id);
            return _mapper.Map<ItemViewModel>(item);
        }

        public async Task AddAsync(ItemInsertModel item)
        {
            var newItem = _mapper.Map<Item>(item);
            await _repository.AddAsync(newItem);
        }

        public IQueryable<ItemViewModel> List()
        {
            var items = _repository.List();

            return _mapper.ProjectTo<ItemViewModel>(items);
        }

        public async Task<IPagedCollection<ItemViewModel>> ListAsync(ItemQuery query)
        {
            var items = await _repository.ListAsync(query);

            var itemsMapped = _mapper.Map<IEnumerable<ItemViewModel>>(items);

            return new PagedCollection<ItemViewModel>(
               itemsMapped,
               items.ItemCount,
               items.CurrentPageNumber,
               items.PageSize
               );
        }

        public async Task UpdateAsync(int id, ItemUpdateModel item)
        {
            var updatedItem = _mapper.Map<ItemUpdateDataModel>(item);
            updatedItem.Id = id;
            await _repository.UpdateAsync(updatedItem);

            var messageItem = new ItemMessage {
                ItemId = updatedItem.Id,
                Name = updatedItem.Name,
                Price = updatedItem.Price,
                Amount = updatedItem.Amount,
                ImageInfo = new ImageInfo() { Url = updatedItem.Image, AltText = updatedItem.Description }
            };

            _mqService.Publish(messageItem);
        }
    }
}
