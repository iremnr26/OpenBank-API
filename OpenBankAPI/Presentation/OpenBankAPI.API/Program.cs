using System.Security.Claims;
using System.Text;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using OpenBankAPI.Application;
using OpenBankAPI.Application.Abstraction;
using OpenBankAPI.Application.Repositories;
using OpenBankAPI.Application.Validators.Accounts;
using OpenBankAPI.Infrastructure;
using OpenBankAPI.Infrastructure.Filter;
using OpenBankAPI.Infrastructure.Services.Exchange;
using OpenBankAPI.Persistence;
using OpenBankAPI.Persistence.Repositories;
using OpenBankAPI.Persistence.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddPersistenceServices(builder.Configuration);
builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices();
builder.Services.AddHttpClient<ExchangeRateService>();


builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<ITransactionWriteRepository, TransactionWriteRepository>();

builder.Services.AddHttpContextAccessor();

// ✅ CORS Politikasını Doğru Tanımla
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularApp", policy =>
    {
        policy.WithOrigins("http://localhost:4200", "https://localhost:4200")
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials();
    });
});


// ✅ FluentValidation ve ValidationFilter Ekleyelim
builder.Services.AddScoped<ValidationFilter>(); // ValidationFilter Dependency Injection ile ekleniyor
/*
//JWT
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Token:Issuer"],
        ValidAudience = builder.Configuration["Token:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Token:SecurityKey"])),
        NameClaimType = ClaimTypes.NameIdentifier,
        RoleClaimType = ClaimTypes.Role
    };
});*/


builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidIssuer = builder.Configuration["Token:Issuer"],
        ValidAudience = builder.Configuration["Token:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Token:SecurityKey"])),

        NameClaimType = "id", // 🔥 BU ÇOK KRİTİK
        RoleClaimType = ClaimTypes.Role
    };
});




builder.Services.AddControllers(options =>
{
    options.Filters.Add<ValidationFilter>(); // ValidationFilter burada ekleniyor
})
    .AddFluentValidation(fv =>
    {
        fv.RegisterValidatorsFromAssemblyContaining<CreateProductValidator>(); // FluentValidation Validatorları ekleniyor
    })
    .ConfigureApiBehaviorOptions(options => options.SuppressModelStateInvalidFilter = true); // ModelState hatalarını suppress et

// ✅ FluentValidation'ı Manuel Ekleyelim (Bazı versiyonlarda çalışmıyor olabilir)
builder.Services.AddValidatorsFromAssemblyContaining<CreateProductValidator>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// ✅ CORS Kullanımı (Middleware DOĞRU YERDE!)
app.UseCors("AllowAngularApp");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication(); // 🔥 Mutlaka Authorization'dan önce olmalı
app.UseAuthorization();

app.MapControllers();

app.Run();
