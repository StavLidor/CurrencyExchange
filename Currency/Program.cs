using Currency;
using CurrencyExchange;
using Data;
using DBManger;
using Microsoft.EntityFrameworkCore;
using Shard;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddSingleton<IDataMangerWrite, DataMangerWrite>();
builder.Services.AddSingleton<ICurrencyRateUpdaterService, CurrencyRateUpdaterService>();
builder.Services.AddSingleton<ICurrencyRateFetcher, CurrencyRateFetcher>();
builder.Services.AddHostedService<Worker>();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(Consts.CONNECTION_APP_DATA_BASE));


var host = builder.Build();
host.Run();
