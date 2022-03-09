namespace Core.Entities
{
    public class ShoppingCardItem
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public string PictureUrl { get; set; }
        public string Comment { get; set; }
        public string Brand { get; set; }
        public string Type { get; set; }
    }
}