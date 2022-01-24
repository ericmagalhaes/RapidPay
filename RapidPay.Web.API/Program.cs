using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using RapidPay.Domain.Adapters;
using RapidPay.Domain.Adapters.Interfaces;
using RapidPay.Repository;
using RapidPay.Repository.Repositories;
using RapidPay.Repository.Repositories.Interfaces;
using RapidPay.Shared;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;
// Add services to the container.
builder.Services.AddDbContext<RapidPayDbContext>(options =>
    options.UseSqlServer(configuration.GetConnectionString(nameof(RapidPayDbContext))));
builder.Services.AddScoped<IRapidPayDbContext, RapidPayDbContext>();
builder.Services.AddScoped<IApplicationUser>((provider)=> new ApplicationUser());
builder.Services.AddScoped((provider) => new UniversalFeesExchange());
builder.Services.AddScoped<IBalanceRepository, BalanceRepository>();
builder.Services.AddScoped<IRedisCacheLock, RedisCacheLock>();
builder.Services.AddScoped<ICardRepository, CardRepository>();
builder.Services.AddScoped<ICardAdapter, CardAdapter>();
builder.Services.AddScoped<IBalanceAdapter, BalanceAdapter>();


builder.Services.AddControllers();
var key = Encoding.ASCII.GetBytes("fedaf7d8863b48e197b9287d492b708e");
builder.Services.AddAuthentication(x =>
    {
        x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(x =>
    {
        x.RequireHttpsMetadata = false;
        x.SaveToken = true;
        x.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();