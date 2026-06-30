namespace ApartmentBooking.Api.DTOs;

public record RegisterDto(string Name, string Email, string Password);
public record LoginDto(string Email, string Password);
public record AuthResponseDto(string Token, int UserId, string Name);

public record CreateApartmentDto(
    string Title,
    string Description,
    string Address,
    string City,
    decimal PricePerNight,
    int Rooms,
    int MaxGuests,
    string PhoneNumber,
    List<string> Images
);

public record UpdateApartmentDto(
    string Title,
    string Description,
    string Address,
    string City,
    decimal PricePerNight,
    int Rooms,
    int MaxGuests,
    string PhoneNumber,
    List<string> Images
);

public record ApartmentDto(
    int Id,
    string Title,
    string Description,
    string Address,
    string City,
    decimal PricePerNight,
    int Rooms,
    int MaxGuests,
    string PhoneNumber,
    List<string> Images,
    bool IsAvailable,
    string OwnerName,
    int OwnerId
);

public record CreateBookingDto(
    int ApartmentId,
    DateTime CheckIn,
    DateTime CheckOut
);

public record BookingDto(
    int Id,
    int ApartmentId,
    string ApartmentTitle,
    string ApartmentAddress,
    string TenantName,
    DateTime CheckIn,
    DateTime CheckOut,
    decimal TotalPrice,
    string Status
);