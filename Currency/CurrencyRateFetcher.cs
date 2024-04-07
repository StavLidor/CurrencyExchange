using Data;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System.Globalization;

namespace CurrencyExchange
{
    public interface ICurrencyRateFetcher
    {
        CurrencyRate FetchCurrencyRateAsync(string fromCurrency, string toCurrency);
    }

    public class CurrencyRateFetcher: ICurrencyRateFetcher
    {
        private const string URL_TEMPLATE = "https://www.xe.com/currencyconverter/convert/?Amount=1&From={0}&To={1}";
        private const string RATE_X_PATH = "//*[@id='__next']/div[4]/div[2]/section/div[2]/div/main/div/div[2]/div[1]/div/p[2]";
        private const string DATE_X_PATH = "//*[@id='__next']/div[4]/div[2]/section/div[2]/div/main/div/div[2]/div[3]/div[2]/div[2]";
        private const int SEC_WAIT = 30;

        public CurrencyRate FetchCurrencyRateAsync(string fromCurrency, string toCurrency)
        {
            try
            {
                var url = string.Format(URL_TEMPLATE, fromCurrency, toCurrency);
                var options = new ChromeOptions();
                options.AddArgument("--headless");
                using (var driver = new ChromeDriver(options))
                {
                    driver.Navigate().GoToUrl(url);
                    var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(SEC_WAIT));


                    wait.Until(driver => driver.FindElement(By.XPath(RATE_X_PATH)));
                    wait.Until(driver => driver.FindElement(By.XPath(DATE_X_PATH)));

                    var rateText = driver.FindElement(By.XPath(RATE_X_PATH)).Text.Trim().Split(' ')[0];
                    var fullDateText = driver.FindElement(By.XPath(DATE_X_PATH)).Text.Trim().Split('—')[1].Trim().Replace("Last updated ", "");


                    DateTime.TryParseExact(fullDateText, "MMM d, yyyy, HH:mm 'UTC'", CultureInfo.InvariantCulture,
                        DateTimeStyles.AssumeUniversal | DateTimeStyles.AdjustToUniversal, out var date);

                    var currencyRate = new CurrencyRate
                    {
                        FromCurrency = fromCurrency,
                        ToCurrency = toCurrency,
                        UpdateTime = date == DateTime.MinValue ? DateTime.UtcNow : date,
                        Rate = double.TryParse(rateText, out var rate) ? rate : 0
                    };

                    return currencyRate;
                }
            }
            catch(Exception ex)
            {
                throw;
            }
            
        }
       
    }
}
