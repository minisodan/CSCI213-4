namespace MusicStore.Models
{
    public class CartItem
    {
        public int MusicId { get; set; }
        public string Title { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }

        public CartItem(int musicId, string title, decimal price, int quantity)
        {
            MusicId = musicId;
            Title = title;
            Price = price;
            Quantity = quantity;
        }
    }
}
