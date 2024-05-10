using API.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

//connect the database with data entities and data in sql <3:
builder.Services.AddDbContext<DataContext>(options =>
{
    options.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=DatingApp;Integrated Security=True");
});
builder.Services.AddCors();

builder.Services.AddCors();

var app = builder.Build();

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseCors(builder => builder.AllowAnyHeader().AllowAnyMethod().WithOrigins("https://localhost:4200"));
app.MapControllers();


app.Run();
