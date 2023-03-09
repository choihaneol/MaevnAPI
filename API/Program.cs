
global using API.Models;
global using Microsoft.EntityFrameworkCore;
global using API.Services;
using System.Security;
using API;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.OpenApi.Models;
using System.Net;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

//Services
builder.Services.AddTransient<ProductService>();
builder.Services.AddTransient<UserService>();
builder.Services.AddTransient<OrderService>();

//Auto mapping 
builder.Services.AddAutoMapper(typeof(MappingConfig));//Auto mapping

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

//Token for swagger
builder.Services.AddSwaggerGen(c =>
{
    var jwtSecurityScheme = new OpenApiSecurityScheme
    {
        BearerFormat = "JWT",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = JwtBearerDefaults.AuthenticationScheme,
        Description = "Put Bearer + your token in the box below",
        Reference = new OpenApiReference
        {
            Id = JwtBearerDefaults.AuthenticationScheme,
            Type = ReferenceType.SecurityScheme
        }
    };

    c.AddSecurityDefinition(jwtSecurityScheme.Reference.Id, jwtSecurityScheme);

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            jwtSecurityScheme, Array.Empty<string>()
        }
    });
});


builder.Services.AddDbContext<B2bapiContext>();

//Validate token
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(opt =>
    {
        opt.TokenValidationParameters = new TokenValidationParameters
        {
            ClockSkew = TimeSpan.Zero,
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
            LifetimeValidator = (DateTime? notBefore, DateTime? expires, SecurityToken securityToken,
                             TokenValidationParameters validationParameters) =>
            {
                return notBefore <= DateTime.UtcNow &&
                       expires > DateTime.UtcNow;
            },

            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.
                GetBytes(builder.Configuration["ApiSettings:Secret"]))
        };

        opt.Events = new JwtBearerEvents
        {
            OnChallenge = context =>
            {
                context.HandleResponse();
                context.Response.StatusCode = 401;
                context.Response.ContentType = "application/json";
                context.Response.WriteAsync("{\"error\":\"token error\"}");
                return Task.CompletedTask;
            }
        };

    });







builder.Services.AddAuthorization();

//Acept for accessing API 
builder.Services.AddCors();

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
app.UseSwagger();
//app.UseSwaggerUI();
//}


//Persis logined status in swagger 
app.UseSwaggerUI(c =>
{
    c.ConfigObject.AdditionalItems.Add("persistAuthorization", "true");
});

//Acept for accessing API 
app.UseCors(opt =>
{
    opt.AllowAnyHeader().AllowAnyMethod().WithOrigins("http://localhost:3000", "http://localhost:3006").AllowCredentials();

});

app.UseHttpsRedirection();

//Authorization
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
