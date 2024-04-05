using DBManger;

namespace ReadCurrency
{
    public interface ICurrencyRateReadService
    {
        Task PrintCurrencyRates();
    }
    public class CurrencyRateReadService : ICurrencyRateReadService
    {
        private readonly IDataManagerRead _dataManagerRead;

        public CurrencyRateReadService(IDataManagerRead dataManagerRead)
        {
            _dataManagerRead = dataManagerRead;
        }
        public async Task PrintCurrencyRates()
        {
            var rates = await _dataManagerRead.GetCurrencyRates();
            rates.ForEach(rate => Console.WriteLine($"{rate.UpdateTime} {rate.FromCurrency}/{rate.ToCurrency}: {rate.Rate}"));
        }
    }
}
