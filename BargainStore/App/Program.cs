using App.BookNotice;
using App.Domain.Infrastructure;
using App.Domain.Shared;
using App.Infrastructure;
using App.MemberProfile;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.ClearProviders();
builder.Logging.AddConsole();

builder.Services.Configure<MongoDbSettings>(
    builder.Configuration.GetSection(nameof(MongoDbSettings)));
builder.Services.AddScoped<IMongoClient>(s =>
{
    var settings = s.GetRequiredService<IOptions<MongoDbSettings>>().Value;
    return new MongoClient(settings.ConnectionString);
});
builder.Services.AddScoped<IMongoDbUnitOfWork, MongoDbUnitOfWorks>();

builder.Services.AddDbContext<MembershipDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("MembershipDb")));
builder.Services.AddScoped<IPostgresUnitOfWork, PostgresUnitOfWork>();


builder.Services.AddSingleton<ICurrencyCodeLookup, CurrencyCodeLookup>();
//todo@buraksenyurt Metin içeriği ve e-mail validasyonu farklı delegate türleri kullanabilir. Bunu nasıl çözeriz?
builder.Services.AddSingleton<TextValidator>(text =>
{
    var illegalWords = new[] { ";", "1=1", "select", "where" };
    foreach (var word in illegalWords)
    {
        if (text.Contains(word, StringComparison.OrdinalIgnoreCase))
        {
            return false;
        }
    }
    return !string.IsNullOrWhiteSpace(text) && text.Length <= 100;
});


builder.Services.AddScoped<IBookRepository, BookMongoRepository>();
builder.Services.AddScoped<IMemberProfileRepository, MemberProfileRepository>();
builder.Services.AddScoped<BooksOnNoticeApplicationService>();
builder.Services.AddScoped<MemberProfileApplicationService>();

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