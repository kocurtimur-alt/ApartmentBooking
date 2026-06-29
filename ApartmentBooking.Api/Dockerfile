FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["ApartmentBooking.Api/ApartmentBooking.Api.csproj", "ApartmentBooking.Api/"]
RUN dotnet restore "ApartmentBooking.Api/ApartmentBooking.Api.csproj"
COPY . .
WORKDIR "/src/ApartmentBooking.Api"
RUN dotnet build "ApartmentBooking.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ApartmentBooking.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ApartmentBooking.Api.dll"]