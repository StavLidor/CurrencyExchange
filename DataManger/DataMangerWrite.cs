using Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace DBManger
{
    public interface IDataMangerWrite
    {
        Task SaveCurrencyRate(CurrencyRate newCurrencyRate);
    }
    public class DataMangerWrite : IDataMangerWrite
    {
        private readonly IServiceProvider _serviceProvider;

        public DataMangerWrite(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task SaveCurrencyRate(CurrencyRate newCurrencyRate)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                var query = from currencyRates in dbContext.CurrencyRates
                            where currencyRates.FromCurrency == newCurrencyRate.FromCurrency &&
                            currencyRates.ToCurrency == newCurrencyRate.ToCurrency
                            select currencyRates;

                var currencyRate = await query.FirstOrDefaultAsync();
                if (currencyRate != null)
                {
                    currencyRate.Rate = newCurrencyRate.Rate;
                    currencyRate.UpdateTime = newCurrencyRate.UpdateTime;
                }
                else
                {
                    currencyRate = newCurrencyRate;
                    dbContext.CurrencyRates.Add(currencyRate);
                }

                await dbContext.SaveChangesAsync();
            }
        }
    }
}
