using Microsoft.AspNetCore.Mvc.Rendering;

namespace MusicStore.Models
{
    public class MusicGerneViewModel
    {
        public List<Music>? Music {  get; set; }
        public SelectList? Gernes {  get; set; }
        public string? MusicGenre { get; set; }
        public SelectList? Performer { get; set; }
        public string? MusicPerformer { get; set; }
    }
}
