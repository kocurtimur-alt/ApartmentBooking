using ApartmentBooking.Api.Data;
using ApartmentBooking.Api.DTOs;
using ApartmentBooking.Api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace ApartmentBooking.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class BookingsController : ControllerBase
{
    private readonly AppDbContext _db;
    public BookingsController(AppDbContext db) => _db = db;

    private int GetUserId() =>
        int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

    [HttpGet("my")]
    public async Task<ActionResult<List<BookingDto>>> GetMyBookings()
    {
        var userId = GetUserId();
        var bookings = await _db.Bookings
            .Include(b => b.Apartment)
            .Include(b => b.Tenant)
            .Where(b => b.TenantId == userId)
            .Select(b => new BookingDto(b.Id, b.ApartmentId, b.Apartment!.Title,
                b.Apartment.Address, b.Tenant!.Name, b.CheckIn, b.CheckOut,
                b.TotalPrice, b.Status))
            .ToListAsync();

        return Ok(bookings);
    }

    [HttpPost]
    public async Task<ActionResult<BookingDto>> Create(CreateBookingDto dto)
    {
        var userId = GetUserId();
        var apartment = await _db.Apartments.FindAsync(dto.ApartmentId);
        if (apartment == null) return NotFound("Квартира не найдена");
        if (apartment.OwnerId == userId)
            return BadRequest("Нельзя забронировать собственную квартиру");
        if (dto.CheckIn >= dto.CheckOut)
            return BadRequest("Дата заезда должна быть раньше даты выезда");

        // Проверка пересечений дат
        var conflict = await _db.Bookings.AnyAsync(b =>
            b.ApartmentId == dto.ApartmentId &&
            b.Status == "Confirmed" &&
            b.CheckIn < dto.CheckOut &&
            b.CheckOut > dto.CheckIn);

        if (conflict) return BadRequest("Квартира уже занята на эти даты");

        var nights = (dto.CheckOut - dto.CheckIn).Days;
        var booking = new Booking
        {
            ApartmentId = dto.ApartmentId,
            TenantId = userId,
            CheckIn = dto.CheckIn,
            CheckOut = dto.CheckOut,
            TotalPrice = nights * apartment.PricePerNight
        };

        _db.Bookings.Add(booking);
        await _db.SaveChangesAsync();

        var tenant = await _db.Users.FindAsync(userId);
        return CreatedAtAction(nameof(GetMyBookings), new BookingDto(
            booking.Id, booking.ApartmentId, apartment.Title,
            apartment.Address, tenant!.Name, booking.CheckIn,
            booking.CheckOut, booking.TotalPrice, booking.Status));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Cancel(int id)
    {
        var booking = await _db.Bookings.FindAsync(id);
        if (booking == null) return NotFound();
        if (booking.TenantId != GetUserId()) return Forbid();

        booking.Status = "Cancelled";
        await _db.SaveChangesAsync();
        return NoContent();
    }
}