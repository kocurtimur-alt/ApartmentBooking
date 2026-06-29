using ApartmentBooking.Api.Data;
using ApartmentBooking.Api.DTOs;
using ApartmentBooking.Api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Text.Json;

namespace ApartmentBooking.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ApartmentsController : ControllerBase
{
    private readonly AppDbContext _db;
    public ApartmentsController(AppDbContext db) => _db = db;

    private int GetUserId() =>
        int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

    private static List<string> ParseImages(string json)
    {
        if (string.IsNullOrEmpty(json)) return new();
        try { return JsonSerializer.Deserialize<List<string>>(json) ?? new(); }
        catch { return new(); }
    }

    private static string SerializeImages(List<string> images) =>
        JsonSerializer.Serialize(images);

    [HttpGet]
    public async Task<ActionResult<List<ApartmentDto>>> GetAll(
    [FromQuery] string? city,
    [FromQuery] decimal? minPrice,
    [FromQuery] decimal? maxPrice,
    [FromQuery] int? rooms)
    {
        // Загружаем всё в память, потом фильтруем на стороне C#
        var query = _db.Apartments
            .Include(a => a.Owner)
            .Where(a => a.IsAvailable);

        if (minPrice.HasValue)
            query = query.Where(a => a.PricePerNight >= minPrice);
        if (maxPrice.HasValue)
            query = query.Where(a => a.PricePerNight <= maxPrice);
        if (rooms.HasValue)
            query = query.Where(a => a.Rooms == rooms);

        var apartments = await query.ToListAsync();

        // Фильтрация по городу на стороне C# — корректно работает с кириллицей
        if (!string.IsNullOrEmpty(city))
            apartments = apartments.Where(a =>
                a.City.Contains(city, StringComparison.OrdinalIgnoreCase)).ToList();

        return Ok(apartments.Select(a => new ApartmentDto(
            a.Id, a.Title, a.Description, a.Address, a.City,
            a.PricePerNight, a.Rooms, a.MaxGuests,
            ParseImages(a.Images), a.IsAvailable, a.Owner!.Name, a.OwnerId)));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ApartmentDto>> GetById(int id)
    {
        var a = await _db.Apartments.Include(a => a.Owner)
            .FirstOrDefaultAsync(a => a.Id == id);
        if (a == null) return NotFound();

        return Ok(new ApartmentDto(a.Id, a.Title, a.Description, a.Address,
            a.City, a.PricePerNight, a.Rooms, a.MaxGuests,
            ParseImages(a.Images), a.IsAvailable, a.Owner!.Name, a.OwnerId));
    }

    [Authorize]
    [HttpGet("my")]
    public async Task<ActionResult<List<ApartmentDto>>> GetMine()
    {
        var userId = GetUserId();
        var apartments = await _db.Apartments
            .Include(a => a.Owner)
            .Where(a => a.OwnerId == userId)
            .ToListAsync();

        return Ok(apartments.Select(a => new ApartmentDto(
            a.Id, a.Title, a.Description, a.Address, a.City,
            a.PricePerNight, a.Rooms, a.MaxGuests,
            ParseImages(a.Images), a.IsAvailable, a.Owner!.Name, a.OwnerId)));
    }

    [Authorize]
    [HttpPost]
    public async Task<ActionResult<ApartmentDto>> Create(CreateApartmentDto dto)
    {
        var userId = GetUserId();
        var apartment = new Apartment
        {
            Title = dto.Title,
            Description = dto.Description,
            Address = dto.Address,
            City = dto.City,
            PricePerNight = dto.PricePerNight,
            Rooms = dto.Rooms,
            MaxGuests = dto.MaxGuests,
            Images = SerializeImages(dto.Images),
            OwnerId = userId
        };

        _db.Apartments.Add(apartment);
        await _db.SaveChangesAsync();

        var owner = await _db.Users.FindAsync(userId);
        return CreatedAtAction(nameof(GetById), new { id = apartment.Id },
            new ApartmentDto(apartment.Id, apartment.Title, apartment.Description,
                apartment.Address, apartment.City, apartment.PricePerNight,
                apartment.Rooms, apartment.MaxGuests, dto.Images,
                apartment.IsAvailable, owner!.Name, userId));
    }

    [Authorize]
    [HttpPut("{id}")]
    public async Task<ActionResult<ApartmentDto>> Update(int id, UpdateApartmentDto dto)
    {
        var apartment = await _db.Apartments.Include(a => a.Owner)
            .FirstOrDefaultAsync(a => a.Id == id);
        if (apartment == null) return NotFound();
        if (apartment.OwnerId != GetUserId()) return Forbid();

        apartment.Title = dto.Title;
        apartment.Description = dto.Description;
        apartment.Address = dto.Address;
        apartment.City = dto.City;
        apartment.PricePerNight = dto.PricePerNight;
        apartment.Rooms = dto.Rooms;
        apartment.MaxGuests = dto.MaxGuests;
        apartment.Images = SerializeImages(dto.Images);

        await _db.SaveChangesAsync();

        return Ok(new ApartmentDto(apartment.Id, apartment.Title, apartment.Description,
            apartment.Address, apartment.City, apartment.PricePerNight,
            apartment.Rooms, apartment.MaxGuests, dto.Images,
            apartment.IsAvailable, apartment.Owner!.Name, apartment.OwnerId));
    }

    [Authorize]
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var apartment = await _db.Apartments.FindAsync(id);
        if (apartment == null) return NotFound();
        if (apartment.OwnerId != GetUserId()) return Forbid();

        _db.Apartments.Remove(apartment);
        await _db.SaveChangesAsync();
        return NoContent();
    }

    [Authorize]
    [HttpGet("{id}/bookings")]
    public async Task<ActionResult<List<BookingDto>>> GetApartmentBookings(int id)
    {
        var apartment = await _db.Apartments.FindAsync(id);
        if (apartment == null) return NotFound();
        if (apartment.OwnerId != GetUserId()) return Forbid();

        var bookings = await _db.Bookings
            .Include(b => b.Tenant)
            .Include(b => b.Apartment)
            .Where(b => b.ApartmentId == id)
            .Select(b => new BookingDto(b.Id, b.ApartmentId, b.Apartment!.Title,
                b.Apartment.Address, b.Tenant!.Name, b.CheckIn, b.CheckOut,
                b.TotalPrice, b.Status))
            .ToListAsync();

        return Ok(bookings);
    }
}