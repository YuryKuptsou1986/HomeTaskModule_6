using Microsoft.AspNetCore.Mvc;
using ViewModel.Page;
using ViewModel.Query;
using ViewModel.View;

namespace WebAPI.Resources
{
    public class ItemResourceFactory
    {
        private readonly IUrlHelper _urlHelper;

        public ItemResourceFactory(IUrlHelper urlHelper)
        {
            _urlHelper = urlHelper ?? throw new ArgumentNullException(nameof(urlHelper));

        }

        public ItemResource CreateItemResource(ItemViewModel item)
        {
            if (item is null)
                throw new ArgumentNullException(nameof(item));

            return (ItemResource)new ItemResource(_urlHelper, item)
                .AddDelete("delete-item", "DeleteItem", new { itemId = item.Id })
                .AddGet("self", "GetItemById", new { itemId = item.Id })
                .AddGet("items", "GetItemList", new PagedQueryParams())
                .AddPost("create-item", "CreateItem")
                .AddPut("update-item", "UpdateItem", new { itemId = item.Id });
        }

        public ResourceBase CreateUserResourceList(IPagedCollection<ItemViewModel> items, ItemQuery query)
        {
            var userResources = items
                .Select(item => CreateItemResource(item))
                .ToList();

            var routeName = "GetItemList";

            return new ItemResourceList(_urlHelper, userResources)
                .AddCurrentPage(routeName, items, query)
                .AddNextPage(routeName, items, query)
                .AddPreviousPage(routeName, items, query)
                .AddFirstPage(routeName, items, query)
                .AddLastPage(routeName, items, query);
        }
    }
}
