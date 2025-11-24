using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using workhub_api.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = "Server=localhost;Port=3306;Database=workhub;User=root;Password=;";
builder.Services.AddDbContext<WorkHubContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString))
);
//interfaces goes here
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

builder.Services.AddControllers();
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

app.UseAuthorization();

app.MapControllers();

app.Run();
