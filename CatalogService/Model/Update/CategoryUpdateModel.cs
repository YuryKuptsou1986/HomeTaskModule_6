namespace ViewModel.Update
{
    public class CategoryUpdateModel
    {
        public string Name { get; set; }
        public Uri? Image { get; set; }
        public int? ParentCategoryId { get; set; }
    }
}
