using System.ComponentModel.DataAnnotations;

namespace MusicStore.Models
{
    public class Music
    {
        public int id { get; set; }
        public string? title { get; set; }
        public int year { get; set; }
        public string? performer { get; set; }
        public string? genre { get; set; }
        public string typeOfPurchase { get; set; }
        public decimal price { get; set; } 

    }
}
