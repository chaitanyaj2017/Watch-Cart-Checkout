//using BcgCodingChallenge.WatchRepository;

using Microsoft.EntityFrameworkCore;
using WatchCart.BL;
using WatchCart.Data.Watchcartchallenge;
using WatchCart.IBLL;

var builder = WebApplication.CreateBuilder(args);
var conn = builder.Configuration.GetConnectionString("WCDB");
builder.Services.AddDbContext<WatchcartchallengeContext>(options => options.UseMySql(conn, new MySqlServerVersion(new Version(8, 0, 22))));
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


//builder.Services.AddScoped<IWatchRepository, WatchRepository>();
builder.Services.AddScoped<IWatchCart, WatchCartRepository>();
builder.Services.AddDbContext<WatchcartchallengeContext>(options => options.UseMySql(conn, new MySqlServerVersion(new Version(8, 0, 22))));
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

