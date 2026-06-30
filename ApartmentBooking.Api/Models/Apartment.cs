namespace ApartmentBooking.Api.Models;

public class Apartment
{
    public string PhoneNumber { get; set; } = "";
    public int Id { get; set; }
    public string Title { get; set; } = "";
    public string Description { get; set; } = "";
    public string Address { get; set; } = "";
    public string City { get; set; } = "";
    public decimal PricePerNight { get; set; }
    public int Rooms { get; set; }
    public int MaxGuests { get; set; }
    public string Images { get; set; } = ""; // JSON-строка со списком URL
    public bool IsAvailable { get; set; } = true;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public int OwnerId { get; set; }
    public User? Owner { get; set; }
    public List<Booking> Bookings { get; set; } = new();
}