using AllHandsMaio2024;
using AllHandsMaio2024.Repositories;
using AllHandsMaio2024.Repositories.Dapper;
using AllHandsMaio2024.Repositories.Dapper.Context;
using AllHandsMaio2024.Repositories.EF;
using AllHandsMaio2024.Repositories.EF.Context;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var config = builder.Configuration;

// Add EF
builder.Services.AddDbContextPool<ApplicationDbContext>(
    options => options.UseSqlServer(config.GetConnectionString("Default")));

// Add Dapper
builder.Services.AddSingleton<IDapperContext>(
    s => new DapperContext(builder.Configuration));

// Repositories
builder.Services.AddTransient<IProductRepository, EfProductRepository>();
//builder.Services.AddTransient<IProductRepository, DapperProductRepository>();

var app = builder.Build();

app.UseHttpsRedirection();

app.MapGet("/", () => "Hello World");
app.MapProductEndpoints();

app.Run();