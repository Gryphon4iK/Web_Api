using AspNet_Programm_1.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
string connection = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<HotelsContext>(options => options.UseSqlServer(connection));

var app = builder.Build();
app.UseDefaultFiles();
app.UseStaticFiles();

app.MapGet("/api/hotels", async (HotelsContext db) => await db.Hotel.ToListAsync());
app.MapGet("/api/hotels/{id:int}", async (int id, HotelsContext db) =>
{
    Hotel? hotel = await db.Hotel.FirstOrDefaultAsync(u => u.ID == id);

    if (hotel == null) return Results.NotFound(new { message = "Îòåëü íå íàéäåí" });

    return Results.Json(hotel);
});

app.MapDelete("/api/hotels/{id:int}", async (int id, HotelsContext db) =>
{
    Hotel? hotel = await db.Hotel.FirstOrDefaultAsync(u => u.ID == id);

    if (hotel == null) return Results.NotFound(new { message = "Îòåëü íå íàéäåí" });

    db.Hotel.Remove(hotel);
    await db.SaveChangesAsync();
    return Results.Json(hotel);
});

app.MapPost("/api/hotels", async (Hotel hotel, HotelsContext db) =>
{
    await db.Hotel.AddAsync(hotel);
    await db.SaveChangesAsync();
    return hotel;
});

app.MapPut("/api/hotels", async (Hotel HotelData, HotelsContext db) =>
{
    var hotel = await db.Hotel.FirstOrDefaultAsync(u => u.ID == HotelData.ID);

    if (hotel == null) return Results.NotFound(new { message = "Îòåëü íå íàéäåí" });

    hotel.Name = HotelData.Name;
    hotel.CountOfStars = HotelData.CountOfStars;
    hotel.CountryCode = HotelData.CountryCode;
    await db.SaveChangesAsync();
    return Results.Json(hotel);
});



app.Run();
