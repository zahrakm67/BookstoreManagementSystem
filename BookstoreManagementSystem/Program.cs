using System.Text;
using BookstoreManagementSystem.Configs;
using Infrastructure.Contexts;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Domain.Entities.Users;
using Microsoft.AspNetCore.Identity;


var builder = WebApplication.CreateBuilder(args);


// Call your custom DI configuration.
DependencyInjectionStartupConfig.Setup(builder.Services, builder.Configuration);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// ◆ Identity
builder.Services
    .AddIdentity<ApplicationUser, IdentityRole>(opts => {
        opts.Password.RequiredLength       = 8;
        opts.Password.RequireDigit         = true;
        opts.Lockout.MaxFailedAccessAttempts = 5;
    })
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<CoreContext>()    
    .AddSignInManager()
    .AddDefaultTokenProviders();

// ◆ JWT Authentication
var jwtKey = builder.Configuration["Jwt:Key"];
var jwtIssuer= builder.Configuration["Jwt:Issuer"];

builder.Services.AddAuthentication(options => {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme    = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(opts => {
        opts.RequireHttpsMetadata = true;
        opts.SaveToken            = true;
        opts.TokenValidationParameters = new TokenValidationParameters {
            ValidateIssuer           = true,
            ValidateAudience         = true,
            ValidIssuer              = jwtIssuer,
            ValidAudience            = jwtIssuer,
            IssuerSigningKey         = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey))
        };
    });


builder.Services.AddAuthorization();
// Add services to the container.
builder.Services.AddControllers();

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