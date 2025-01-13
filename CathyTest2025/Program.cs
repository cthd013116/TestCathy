using CathyTest2025.Data;
using CathyTest2025.Factory;
using CathyTest2025.Interface;
using CathyTest2025.Repository;
using Microsoft.EntityFrameworkCore;
using static CathyTest2025.Interface.IRepository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// µù¥U HttpClient
builder.Services.AddHttpClient();

builder.Services.AddControllers();


builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IRepositoryFactory, RepositoryFactory>();
builder.Services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));

//builder.Services.AddScoped<IRepository<T>, EfRepository<T>>();

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
