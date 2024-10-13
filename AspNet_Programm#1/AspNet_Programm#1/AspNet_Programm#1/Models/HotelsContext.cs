using Microsoft.EntityFrameworkCore;
namespace AspNet_Programm_1.Models
{
    public class HotelsContext  : DbContext
    {
           public DbSet<Hotel> Hotel {get; set;}
        public HotelsContext (DbContextOptions<HotelsContext>options) : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
