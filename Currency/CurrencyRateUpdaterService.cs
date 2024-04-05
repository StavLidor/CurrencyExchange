using System.Threading.Tasks.Dataflow;
using DBManger;

namespace CurrencyExchange
{
    public interface ICurrencyRateUpdaterService
    {
        void Start(TimeSpan updateInterval);
        void Stop();
    }
    public class CurrencyRateUpdaterService : ICurrencyRateUpdaterService
    {
        private readonly ICurrencyRateFetcher _currencyRateFetcher;
        private  Timer?  _timer;
        private readonly ActionBlock<(string from, string to)> _processCurrencyPairBlock;
        private readonly IDataMangerWrite _dataManagerWrite;
        private readonly ILogger<CurrencyRateUpdaterService> _logger;

        public CurrencyRateUpdaterService(ICurrencyRateFetcher fetcher, IDataMangerWrite dataManagerWrite, ILogger<CurrencyRateUpdaterService> logger)
        {
            _dataManagerWrite = dataManagerWrite;
            _currencyRateFetcher = fetcher;
            _logger = logger;

            _processCurrencyPairBlock = new ActionBlock<(string from, string to)>(async (pair) =>
            {
                try
                {
                    var currencyRate = _currencyRateFetcher.FetchCurrencyRateAsync(pair.from, pair.to);
                    await _dataManagerWrite.SaveCurrencyRate(currencyRate);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, $"An error occurred while fetching and saving currency rate of {pair.from}/{pair.to}.");
                }

            }, new ExecutionDataflowBlockOptions
            {
                MaxDegreeOfParallelism = DataflowBlockOptions.Unbounded,
            });
            
        }

        public void Start(TimeSpan updateInterval)
        {
            _timer = new Timer(_ => TriggerUpdates(), null, TimeSpan.Zero, updateInterval);
        }

        private void TriggerUpdates()
        {
            var currencyPairs = new List<(string,string)> { ("USD","ILS"), ("EUR","USD"), ("EUR","JPY"), ("GBP","EUR") };
            foreach (var pair in currencyPairs)

            {
                _processCurrencyPairBlock.Post(pair);
            }
        }

        public void Stop()
        {
            _timer?.Change(Timeout.Infinite, Timeout.Infinite);
            _processCurrencyPairBlock.Complete();
            _processCurrencyPairBlock.Completion.Wait();
        }

    }
}
