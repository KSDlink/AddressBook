using ContactList.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

//Enable CORS
builder.Services.AddCors(c =>
{
    c.AddPolicy("AllowOrigin", options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()); //Посмотри что это
});

builder.Services.AddDbContext<ContactsContext>(options => options.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=contactlistdb;Trusted_Connection=True;"));

var app = builder.Build();

// Configure the HTTP request pipeline.

//Enable CORS
app.UseCors(options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
