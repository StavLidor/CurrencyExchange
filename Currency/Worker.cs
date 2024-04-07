
using CurrencyExchange;

namespace Currency
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly ICurrencyRateUpdaterService _currencyRateUpdaterService;
        private const int ONE_MINUTE = 1;
        

        public Worker(ILogger<Worker> logger, ICurrencyRateUpdaterService currencyRateUpdaterService)
        {
            _logger = logger;
            _currencyRateUpdaterService = currencyRateUpdaterService;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _currencyRateUpdaterService.Start(TimeSpan.FromMinutes(ONE_MINUTE));

            stoppingToken.Register(() => _currencyRateUpdaterService.Stop());

            while (!stoppingToken.IsCancellationRequested)
            {
                if (_logger.IsEnabled(LogLevel.Information))
                {
                    _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                }
                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}
