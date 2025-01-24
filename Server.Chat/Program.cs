using Azure.Core;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Web;
using Server.Chat.Helper;
using Server.Chat.Repositories;
using Server.Chat.Services;
using Server.Chat.Services.Dto;

var options = new SecretClientOptions
{
    Retry =
    {
        Delay = TimeSpan.FromSeconds(2),
        MaxDelay = TimeSpan.FromSeconds(16),
        MaxRetries = 5,
        Mode = RetryMode.Exponential
    }
};


var builder = WebApplication.CreateBuilder(args);

var client = new SecretClient(
    new Uri(builder.Configuration.GetConnectionString("KeyVaultUri")!),
    new DefaultAzureCredential(),
    options
);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// SignalR
var signalRSecret = client.GetSecret("SignalRConnection").Value;
builder.Services.AddSignalR().AddAzureSignalR(signalRSecret.Value);

// DI
builder.Services.AddScoped<IChatDtoService, ChatDtoService>();
builder.Services.AddScoped<ChatService, ChatService>();
builder.Services.AddScoped<ChatHub, ChatHub>();

// DbContext
var databaseConnectionString = builder.Configuration.GetConnectionString("DefaultConnection");
if (databaseConnectionString is not null)
{
    builder.Services.AddDbContext<ChatDbContext>(options => options.UseNpgsql(databaseConnectionString));
}
else
{
    var databaseConnectionSecret = client.GetSecret("DatabaseConnection").Value;
    builder.Services.AddDbContext<ChatDbContext>(options => options.UseNpgsql(databaseConnectionSecret.Value));
}

builder.Services.AddControllers();

// exception handlers
builder.Services.AddExceptionHandler<BadRequestExceptionHandler>();
builder.Services.AddExceptionHandler<NotFoundExceptionHandler>();
builder.Services.AddExceptionHandler<ForbiddenExceptionHandler>();
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();

// Authentication
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddMicrosoftIdentityWebApi(options =>
    {
        builder.Configuration.Bind("AzureAd", options);
        options.TokenValidationParameters.NameClaimType = "name";
    }, options => { builder.Configuration.Bind("AzureAd", options); });

builder.Services.AddAuthorization(config =>
{
    config.AddPolicy("AuthZPolicy",
        policyBuilder =>
        {
            policyBuilder.Requirements.Add(new ScopeAuthorizationRequirement()
                { RequiredScopesConfigurationKey = $"AzureAd:Scopes" });
        });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(corsPolicyBuilder => corsPolicyBuilder
    .WithOrigins("https://green-flower-092d69f03.4.azurestaticapps.net", "http://localhost:4200")
    .AllowAnyMethod()
    .AllowAnyHeader()
    .AllowCredentials()
);

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();
app.UseExceptionHandler();
app.MapControllers();
app.MapHub<ChatHub>("/chat");

ChatDbContext context = app.Services.GetRequiredService<ChatDbContext>();
context.Database.Migrate();

app.Run();