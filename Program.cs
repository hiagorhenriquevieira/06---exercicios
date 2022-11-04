using DependencyStore.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddService();
builder.Services.AddRepository();

builder.Services.AddControllers();

var app = builder.Build();

app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();

app.Run();
