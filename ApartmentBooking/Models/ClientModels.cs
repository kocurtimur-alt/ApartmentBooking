namespace ApartmentBooking.Models;

public class ApartmentDto
{
    public int Id { get; set; }
    public string Title { get; set; } = "";
    public string Description { get; set; } = "";
    public string Address { get; set; } = "";
    public string City { get; set; } = "";
    public decimal PricePerNight { get; set; }
    public int Rooms { get; set; }
    public int MaxGuests { get; set; }
    public string PhoneNumber { get; set; } = "";
    public List<string> Images { get; set; } = new();
    public bool IsAvailable { get; set; }
    public string OwnerName { get; set; } = "";
    public int OwnerId { get; set; }
}

public class BookingDto
{
    public int Id { get; set; }
    public int ApartmentId { get; set; }
    public string ApartmentTitle { get; set; } = "";
    public string ApartmentAddress { get; set; } = "";
    public string TenantName { get; set; } = "";
    public DateTime CheckIn { get; set; }
    public DateTime CheckOut { get; set; }
    public decimal TotalPrice { get; set; }
    public string Status { get; set; } = "";
}

public class AuthResponse
{
    public string Token { get; set; } = "";
    public int UserId { get; set; }
    public string Name { get; set; } = "";
}