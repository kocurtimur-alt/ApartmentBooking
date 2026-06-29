using ApartmentBooking.Models;
using System.Net.Http.Json;

namespace ApartmentBooking.Services;

public class ApiService
{
    private readonly HttpClient _http;
    public ApiService(HttpClient http) => _http = http;

    public async Task<List<ApartmentDto>> SearchApartmentsAsync(
        string? city = null, decimal? minPrice = null,
        decimal? maxPrice = null, int? rooms = null)
    {
        var url = "api/apartments?";
        if (!string.IsNullOrEmpty(city)) url += $"city={city}&";
        if (minPrice.HasValue) url += $"minPrice={minPrice}&";
        if (maxPrice.HasValue) url += $"maxPrice={maxPrice}&";
        if (rooms.HasValue) url += $"rooms={rooms}&";

        return await _http.GetFromJsonAsync<List<ApartmentDto>>(url)
               ?? new List<ApartmentDto>();
    }

    public async Task<ApartmentDto?> GetApartmentAsync(int id) =>
        await _http.GetFromJsonAsync<ApartmentDto>($"api/apartments/{id}");

    public async Task<List<ApartmentDto>> GetMyApartmentsAsync() =>
        await _http.GetFromJsonAsync<List<ApartmentDto>>("api/apartments/my")
        ?? new List<ApartmentDto>();

    public async Task<bool> CreateApartmentAsync(object dto)
    {
        var res = await _http.PostAsJsonAsync("api/apartments", dto);
        return res.IsSuccessStatusCode;
    }

    public async Task<bool> UpdateApartmentAsync(int id, object dto)
    {
        var res = await _http.PutAsJsonAsync($"api/apartments/{id}", dto);
        return res.IsSuccessStatusCode;
    }

    public async Task<bool> DeleteApartmentAsync(int id)
    {
        var res = await _http.DeleteAsync($"api/apartments/{id}");
        return res.IsSuccessStatusCode;
    }

    public async Task<List<BookingDto>> GetMyBookingsAsync() =>
        await _http.GetFromJsonAsync<List<BookingDto>>("api/bookings/my")
        ?? new List<BookingDto>();

    public async Task<List<BookingDto>> GetApartmentBookingsAsync(int apartmentId) =>
        await _http.GetFromJsonAsync<List<BookingDto>>(
            $"api/apartments/{apartmentId}/bookings")
        ?? new List<BookingDto>();

    public async Task<(bool Success, string Error)> CreateBookingAsync(
        int apartmentId, DateTime checkIn, DateTime checkOut)
    {
        var res = await _http.PostAsJsonAsync("api/bookings",
            new { apartmentId, checkIn, checkOut });
        if (res.IsSuccessStatusCode) return (true, "");
        var err = await res.Content.ReadAsStringAsync();
        return (false, err);
    }

    public async Task<bool> CancelBookingAsync(int id)
    {
        var res = await _http.DeleteAsync($"api/bookings/{id}");
        return res.IsSuccessStatusCode;
    }
}