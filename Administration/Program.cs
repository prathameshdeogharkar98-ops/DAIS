using Administration.Interfaces;
using Administration.Services;
using Utilities;
using Utilities.Commons;
using Utilities.DataManagementService;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<ILoginRepo, LoginRepo>();
builder.Services.AddScoped<IDataManagementService, DataManagementService>();
builder.Services.AddScoped<PasswordHashingUtil>();
builder.Services.AddScoped<ConnectionStringBuilder>();

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
