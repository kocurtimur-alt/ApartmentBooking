namespace ApartmentBooking.Api.Models;

public class Booking
{
    public int Id { get; set; }
    public DateTime CheckIn { get; set; }
    public DateTime CheckOut { get; set; }
    public decimal TotalPrice { get; set; }
    public string Status { get; set; } = "Confirmed"; // Confirmed / Cancelled
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public int TenantId { get; set; }
    public User? Tenant { get; set; }

    public int ApartmentId { get; set; }
    public Apartment? Apartment { get; set; }
}