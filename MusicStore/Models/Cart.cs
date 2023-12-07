namespace MusicStore.Models
{

    public static class CartManager
    {
        public static Cart cart { get; } = new();
    }

    public class Cart
    {
        public List<CartItem> Items { get; set; } = new List<CartItem>();

        //add stuff to cart
        public void AddItem(CartItem item)
        {
            var existingItem = Items.FirstOrDefault(i => i.MusicId == item.MusicId);
            if (existingItem != null)
            {
                existingItem.Quantity += item.Quantity;
            }
            else
            {
                Items.Add(item);
            }
        }

        //removes an item from the cart
        public void RemoveItem(int musicId)
        {
            var item = Items.FirstOrDefault(i => i.MusicId == musicId);
            if (item.Quantity > 1)
            {
                item.Quantity--;
            }
            else
            {
                Items.Remove(item);
            }
        }

        //clears all items from the cart
        public void Clear()
        {
            Items.Clear();
        }

        //calculates the total price of all items in the cart
        public decimal TotalPrice()
        {
            return Items.Sum(i => i.Price * i.Quantity);
        }
    }
}
