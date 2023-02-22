

global using API.Models;
global using Microsoft.EntityFrameworkCore;
global using API.Services;
using System.Security;
using API;

var builder = WebApplication.CreateBuilder(args);

//to add parameter at constructor of controller
builder.Services.AddTransient<ProductService>();
builder.Services.AddTransient<UserService>();

//Auto mapping 
builder.Services.AddAutoMapper(typeof(MappingConfig));//Auto mapping

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<B2bapiContext>();

//Acept for accessing API 
builder.Services.AddCors();

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
app.UseSwagger();
app.UseSwaggerUI();
//}


//Acept for accessing API 
app.UseCors(opt =>
{
    opt.AllowAnyHeader().AllowAnyMethod().WithOrigins("http://localhost:3000");
});



app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

