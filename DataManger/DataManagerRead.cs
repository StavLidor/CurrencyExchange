
using Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;



namespace DBManger
{
    public interface IDataManagerRead
    {
        Task<List<CurrencyRate>> GetCurrencyRates();
    }
    public class DataManagerRead : IDataManagerRead
    {
        private readonly IServiceProvider _serviceProvider;

        public DataManagerRead(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }



        public async Task<List<CurrencyRate>> GetCurrencyRates()
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                var query = from currencyRates in dbContext.CurrencyRates
                            select currencyRates;
                var result = await query.ToListAsync();
                return result;
            }
        }

    }
}
