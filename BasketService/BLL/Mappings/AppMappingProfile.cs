using AutoMapper;
using BasketService.BLL.Entities.Insert;
using BasketService.BLL.Entities.Update;
using BasketService.BLL.Entities.View;
using BasketService.Domain.Entities;

namespace BasketService.BLL.Mappings
{
    public class AppMappingProfile : Profile
    {
        public AppMappingProfile()
        {
            CreateMap<ImageInfo, ImageInfoViewModel>();
            CreateMap<Item, ItemViewModel>();
            CreateMap<Cart, CartViewModel>();

            CreateMap<ImageInfoInsertViewModel, ImageInfo>();
            CreateMap<ItemInsertViewModel, Item>();
            CreateMap<CartInsertViewModel, Cart>();

            CreateMap<ImageUpdateViewModel, ImageInfo>();
            CreateMap<ItemUpdateViewModel, Item>();
        }
    }
}