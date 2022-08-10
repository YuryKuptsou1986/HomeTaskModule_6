namespace ViewModel.Insert
{
    public class ItemInsertModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public Uri? Image { get; set; }
        public int CategoryId { get; set; }
        public decimal Price { get; set; }
        public int Amount { get; set; }
    }
}
