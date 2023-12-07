using Microsoft.EntityFrameworkCore;
using MusicStore.Data;

namespace MusicStore.Models
{
    public class SeedData
    {

        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new MusicStoreContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<MusicStoreContext>>()))
            {
                // Look for any movies.
                if (context.Music.Any())
                {
                    return;   // DB has been seeded
                }
          //      context.Music.AddRange(
                    
                context.SaveChanges();
            }
        }
    }
}