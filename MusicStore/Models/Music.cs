using System.ComponentModel.DataAnnotations;

namespace MusicStore.Models
{
    public class Music
    {
        public required int id { get; set; }
        public required string title { get; set; }
        public required int year { get; set; }
        public required string performer { get; set; }
        public required string genre { get; set; }
        public required string typeOfPurchase { get; set; }
        public required decimal price { get; set; } 

    }
}
