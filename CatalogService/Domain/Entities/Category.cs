namespace Domain.Entities
{
    public class Category
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public Uri? Image { get; private set; }
        public int? ParentCategoryId { get; private set; }
        public Category? ParentCategory { get; private set; }

        private Category() { }

        public Category(string name, Uri? image, int? parentCategoryId)
        {
            Name = name;
            Image = image;
            ParentCategoryId = parentCategoryId;
        }

        public void Update(string name, Uri? image, int? paretnCategoryId)
        {
            Name = name;
            Image = image;
            ParentCategoryId = paretnCategoryId;
        }
    }
}
