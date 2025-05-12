using App.BookNotice;
using App.Domain.Infrastructure;
using App.Framework;
using App.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.ClearProviders();
builder.Logging.AddConsole();

builder.Services.Configure<MongoDbSettings>(
    builder.Configuration.GetSection(nameof(MongoDbSettings)));
builder.Services.AddSingleton<IMongoClient>(s =>
{
    var settings = s.GetRequiredService<IOptions<MongoDbSettings>>().Value;
    return new MongoClient(settings.ConnectionString);
});
builder.Services.AddSingleton<IMongoDbUnitOfWork, MongoDbUnitOfWorks>();

builder.Services.AddDbContext<MembershipDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("MembershipDb")));
builder.Services.AddScoped<IPostgresUnitOfWork, PostgresUnitOfWork>();

builder.Services.AddSingleton<ICurrencyCodeLookup, CurrencyCodeLookup>();
builder.Services.AddSingleton<IBookRepository, BookMongoRepository>();
builder.Services.AddSingleton<BooksOnNoticeApplicationService>();
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