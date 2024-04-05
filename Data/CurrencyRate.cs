using Microsoft.EntityFrameworkCore;
using Shard;

namespace Data
{
    public class CurrencyRate
    {
        public int Id { get; set; }
        public string FromCurrency { get; set; }
        public string ToCurrency { get; set; }
        public double Rate { get; set; }
        public DateTime UpdateTime { get; set; }
    }

    public class AppDbContext : DbContext
    {
        public DbSet<CurrencyRate> CurrencyRates { get; set; }
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            if (!options.IsConfigured)
            {
                options.UseSqlite(Consts.CONNECTION_APP_DATA_BASE);
            }
        }
    }
}
