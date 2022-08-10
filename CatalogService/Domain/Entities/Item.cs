namespace Domain.Entities
{
    public class Item
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public Uri? Image { get; private set; }
        public int CategoryId { get; private set; }
        public Category Category { get; private set; }
        public decimal Price { get; private set; }
        public int Amount { get; private set; }

        private Item() { }

        public Item(string name, string description, Uri? image, int categoryId, decimal price, int amount)
        {
            Name = name;
            Description = description;
            Image = image;
            CategoryId = categoryId;
            Price = price;
            Amount = amount;
        }

        public void Update(string name, string description, Uri? image, int categoryId, decimal price, int amount)
        {
            Name = name;
            Description = description;
            Image = image;
            CategoryId = categoryId;
            Price = price;
            Amount = amount;
        }
    }
}
