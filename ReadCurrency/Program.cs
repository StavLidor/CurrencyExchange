using Data;
using DBManger;
using Microsoft.EntityFrameworkCore;
using ReadCurrency;
using Shard;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddHostedService<Worker>();
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(Consts.CONNECTION_APP_DATA_BASE));
builder.Services.AddSingleton<IDataManagerRead, DataManagerRead>();
builder.Services.AddSingleton<ICurrencyRateReadService, CurrencyRateReadService>();
var host = builder.Build();
host.Run();
