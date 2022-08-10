namespace DAL.Models.Update
{
    public class CategoryUpdateDataModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Uri? Image { get; set; }
        public int? ParentCategoryId { get; set; }
    }
}
