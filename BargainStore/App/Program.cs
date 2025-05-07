using App.Api;
using App.Domain.Services;
using App.Providers;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.ClearProviders();
builder.Logging.AddConsole();

builder.Services.AddSingleton<BooksOnNoticeApplicationService>();
builder.Services.AddSingleton<ICurrencyCodeLookup, CurrencyCodeLookup>();
builder.Services.AddScoped<IBookRepository, BookRepository>();
builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();

app.Run();