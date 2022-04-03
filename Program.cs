using Microsoft.EntityFrameworkCore;
using System.Configuration;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using AppBuild.Models;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpClient();
// add db context for mysql
builder.Services.AddDbContext<AppBuildContext>(options =>
    options.UseSqlServer("Server=localhost,1433;Initial Catalog=appBuild;Persist Security Info=False;User ID=SA;Password=NE>jt6wPs;MultipleActiveResultSets=False;TrustServerCertificate=True;Connection Timeout=30;"));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors(builder =>
    builder.AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader());

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();


