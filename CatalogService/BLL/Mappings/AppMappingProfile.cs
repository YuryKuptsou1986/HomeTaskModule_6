using AutoMapper;
using Domain.Entities;
using DAL.Models.Update;
using ViewModel.View;
using ViewModel.Insert;
using ViewModel.Update;
using ViewModel.Page;

namespace BLL.Mappings
{
    internal class AppMappingProfile : Profile
    {
        public AppMappingProfile()
        {
            CreateMap<CategoryViewModel, Category>()
                .ForMember(x => x.ParentCategory, opt => opt.Ignore());
            CreateMap<Category, CategoryViewModel>();

            
            CreateMap<ItemViewModel, Item>()
                .ForMember(x => x.Category, opt => opt.Ignore());
            CreateMap<Item, ItemViewModel>();

            CreateMap<ItemInsertModel, Item>()
                .ForMember(x=>x.Id, opt => opt.Ignore())
                .ForMember(x=>x.Category, opt => opt.AllowNull());
            
            CreateMap<ItemUpdateModel, ItemUpdateDataModel>()
                .ForMember(x=>x.Id, opt => opt.Ignore());

            CreateMap<CategoryInsertModel, Category>()
                .ForMember(x => x.Id, opt => opt.Ignore())
                .ForMember(x => x.ParentCategory, opt => opt.AllowNull());

            CreateMap<CategoryUpdateModel, CategoryUpdateDataModel>()
                .ForMember(x => x.Id, opt => opt.Ignore());
        }
    }
}
