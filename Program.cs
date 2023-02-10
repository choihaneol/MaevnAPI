global using API.Models;
global using Microsoft.EntityFrameworkCore;
global using API.Services;

var builder = WebApplication.CreateBuilder(args);

//to add parameter at constructor of controller
builder.Services.AddSingleton<ProductService>();

// Add services to the container.
//builder.Services.AddScoped<IService, Service>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<B2bapiContext>();



var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
app.UseSwagger();
app.UseSwaggerUI();
//}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
