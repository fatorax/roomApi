using Microsoft.EntityFrameworkCore;
using RoomsApi.DataAccess;
using RoomsApi.Services;

var builder = WebApplication.CreateBuilder(args);

// =====================================================
// 1. SERVICES : tous les builder.Services ICI
// =====================================================
builder.Services.AddControllers();
builder.Services.AddOpenApi();

var connectionString = builder.Configuration.GetConnectionString("Room");

builder.Services.AddDbContext<RoomDbContext>(options =>
    options.UseMySql(connectionString, new MySqlServerVersion(new Version(8, 4, 0))));

builder.Services.AddScoped<UsedRoomRepository>();
builder.Services.AddScoped<RoomRepository>();
builder.Services.AddScoped<UsedRoomService>();
builder.Services.AddScoped<RoomService>();
builder.Services.AddScoped<IRoomRepository, RoomRepository>();
builder.Services.AddScoped<IUsedRoomRepository, UsedRoomRepository>();
builder.Services.AddScoped<IRoomService, RoomService>();
builder.Services.AddScoped<IUsedRoomService, UsedRoomService>();
var app = builder.Build();

// Creer les tables si elle n'y sont pas dnas la BDD
using (var scope = app.Services.CreateScope())
{
    scope.ServiceProvider.GetRequiredService<RoomDbContext>().Database.EnsureCreated();
}

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();