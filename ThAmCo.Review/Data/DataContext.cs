namespace ThAmCo.Review.Data
{
    using Microsoft.EntityFrameworkCore;

    public class DataContext : DbContext
    {

        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        {
        }

        public DbSet<ReviewData> ReviewData { get; set; }

    }
}
