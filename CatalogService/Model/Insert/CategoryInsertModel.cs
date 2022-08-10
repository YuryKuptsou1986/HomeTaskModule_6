namespace ViewModel.Insert
{
    public class CategoryInsertModel
    {
        public string Name { get; set; }
        public Uri? Image { get; set; }
        public int? ParentCategoryId { get; set; }
    }
}
