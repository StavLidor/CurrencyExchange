namespace ReadCurrency
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly ICurrencyRateReadService _currencyRateReadService;
        private const int ONE_MINUTE_IN_MILLISECONDS = 60000;

        public Worker(ILogger<Worker> logger, ICurrencyRateReadService currencyRateReadService)
        {
            _logger = logger;
            _currencyRateReadService = currencyRateReadService;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                if (_logger.IsEnabled(LogLevel.Information))
                {
                    _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                }
                try
                {
                    await _currencyRateReadService.PrintCurrencyRates();
                }
                catch (Exception ex)
                {
                    _logger.LogInformation("An error occurred while reading currency rates" );
                }
                
                
                Thread.Sleep(ONE_MINUTE_IN_MILLISECONDS);

                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}
