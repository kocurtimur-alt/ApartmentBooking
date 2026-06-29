using ApartmentBooking.Api.Data;
using ApartmentBooking.Api.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(opt =>
    opt.UseSqlite("Data Source=apartments.db"));

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(opt =>
    {
        opt.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]
                    ?? "supersecretkey1234567890abcdef")),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });

builder.Services.AddAuthorization();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(opt =>
    opt.AddDefaultPolicy(p =>
        p.AllowAnyOrigin()
         .AllowAnyHeader()
         .AllowAnyMethod()));

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.EnsureCreated();
    if (!db.Users.Any())
    {
        await SeedData(db);
        Console.WriteLine("=== SEED DATA ДОБАВЛЕН УСПЕШНО ===");
    }
    else
    {
        Console.WriteLine($"=== В БД уже есть {db.Users.Count()} пользователей и {db.Apartments.Count()} квартир ===");
    }
}

app.UseCors();
app.UseSwagger();
app.UseSwaggerUI();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
static async Task SeedData(AppDbContext db)
{
    if (db.Users.Any()) return; // Если данные уже есть — не добавлять

    // Создаём пользователей-арендодателей
    var users = new[]
    {
        new User { Name = "Александр Иванов", Email = "alex@test.by",
            PasswordHash = BCrypt.Net.BCrypt.HashPassword("123456") },
        new User { Name = "Мария Петрова", Email = "maria@test.by",
            PasswordHash = BCrypt.Net.BCrypt.HashPassword("123456") },
        new User { Name = "Дмитрий Сидоров", Email = "dmitry@test.by",
            PasswordHash = BCrypt.Net.BCrypt.HashPassword("123456") },
        new User { Name = "Анна Козлова", Email = "anna@test.by",
            PasswordHash = BCrypt.Net.BCrypt.HashPassword("123456") },
        new User { Name = "Сергей Новиков", Email = "sergey@test.by",
            PasswordHash = BCrypt.Net.BCrypt.HashPassword("123456") },
    };

    db.Users.AddRange(users);
    await db.SaveChangesAsync();

    // Минск
    var imgMinsk1 = System.Text.Json.JsonSerializer.Serialize(new[] {
    "https://images.unsplash.com/photo-1560448204-e02f11c3d0e2?w=800",
    "https://images.unsplash.com/photo-1484154218962-a197022b5858?w=800",
    "https://images.unsplash.com/photo-1502672260266-1c1ef2d93688?w=800" });

    var imgMinsk2 = System.Text.Json.JsonSerializer.Serialize(new[] {
    "https://images.unsplash.com/photo-1493809842364-78817add7ffb?w=800",
    "https://images.unsplash.com/photo-1522708323590-d24dbb6b0267?w=800",
    "https://images.unsplash.com/photo-1556909114-f6e7ad7d3136?w=800" });

    var imgMinsk3 = System.Text.Json.JsonSerializer.Serialize(new[] {
    "https://images.unsplash.com/photo-1512917774080-9991f1c4c750?w=800",
    "https://images.unsplash.com/photo-1554995207-c18c203602cb?w=800",
    "https://images.unsplash.com/photo-1571508601891-ca5e7a713859?w=800" });

    var imgMinsk4 = System.Text.Json.JsonSerializer.Serialize(new[] {
    "https://images.unsplash.com/photo-1505691938895-1758d7feb511?w=800",
    "https://images.unsplash.com/photo-1536376072261-38c75010e6c9?w=800",
    "https://images.unsplash.com/photo-1550581190-9c1c48d21d6c?w=800" });

    var imgMinsk5 = System.Text.Json.JsonSerializer.Serialize(new[] {
    "https://images.unsplash.com/photo-1558618666-fcd25c85cd64?w=800",
    "https://images.unsplash.com/photo-1598928506311-c55ded91a20c?w=800",
    "https://images.unsplash.com/photo-1567767292278-a4f21aa2d36e?w=800" });

    var imgMinsk6 = System.Text.Json.JsonSerializer.Serialize(new[] {
    "https://images.unsplash.com/photo-1616594039964-ae9021a400a0?w=800",
    "https://images.unsplash.com/photo-1631049307264-da0ec9d70304?w=800",
    "https://images.unsplash.com/photo-1615529328331-f8917597711f?w=800" });

    var imgMinsk7 = System.Text.Json.JsonSerializer.Serialize(new[] {
    "https://images.unsplash.com/photo-1595526114035-0d45ed16cfbf?w=800",
    "https://images.unsplash.com/photo-1585412727339-54e4bae3bbf9?w=800",
    "https://images.unsplash.com/photo-1600210492493-0946911123ea?w=800" });

    var imgMinsk8 = System.Text.Json.JsonSerializer.Serialize(new[] {
    "https://images.unsplash.com/photo-1600585154340-be6161a56a0c?w=800",
    "https://images.unsplash.com/photo-1600047509807-ba8f99d2cdde?w=800",
    "https://images.unsplash.com/photo-1600566753190-17f0baa2a6c3?w=800" });

    var imgMinsk9 = System.Text.Json.JsonSerializer.Serialize(new[] {
    "https://images.unsplash.com/photo-1600607687939-ce8a6c25118c?w=800",
    "https://images.unsplash.com/photo-1600607687644-c7171b42498b?w=800",
    "https://images.unsplash.com/photo-1600607688969-a5bfcd646154?w=800" });

    var imgMinsk10 = System.Text.Json.JsonSerializer.Serialize(new[] {
    "https://images.unsplash.com/photo-1613490493576-7fde63acd811?w=800",
    "https://images.unsplash.com/photo-1613977257363-707ba9348227?w=800",
    "https://images.unsplash.com/photo-1613977257592-4871e5fcd7c4?w=800" });

    var imgMinsk11 = System.Text.Json.JsonSerializer.Serialize(new[] {
    "https://images.unsplash.com/photo-1582268611958-ebfd161ef9cf?w=800",
    "https://images.unsplash.com/photo-1598928636135-d146006ff4be?w=800",
    "https://images.unsplash.com/photo-1598928506311-c55ded91a20c?w=800" });

    var imgMinsk12 = System.Text.Json.JsonSerializer.Serialize(new[] {
    "https://images.unsplash.com/photo-1630699144867-37acec97df5a?w=800",
    "https://images.unsplash.com/photo-1630699144591-5a0644af2d3b?w=800",
    "https://images.unsplash.com/photo-1560185007-cde436f6a4d0?w=800" });

    var imgMinsk13 = System.Text.Json.JsonSerializer.Serialize(new[] {
    "https://images.unsplash.com/photo-1574362848149-11496d93a7c7?w=800",
    "https://images.unsplash.com/photo-1533090481720-856c6e3c1fdc?w=800",
    "https://images.unsplash.com/photo-1507089947368-19c1da9775ae?w=800" });

    var imgMinsk14 = System.Text.Json.JsonSerializer.Serialize(new[] {
    "https://images.unsplash.com/photo-1551361415-69c87624334f?w=800",
    "https://images.unsplash.com/photo-1556020685-ae41abfc9365?w=800",
    "https://images.unsplash.com/photo-1556020685-0e8af1ee7fce?w=800" });

    var imgMinsk15 = System.Text.Json.JsonSerializer.Serialize(new[] {
    "https://images.unsplash.com/photo-1464082354059-27db6ce50048?w=800",
    "https://images.unsplash.com/photo-1549488344-cbb6c34cf08b?w=800",
    "https://images.unsplash.com/photo-1494526585095-c41746248156?w=800" });

    // Гомель
    var imgGomel1 = System.Text.Json.JsonSerializer.Serialize(new[] {
    "https://images.unsplash.com/photo-1489171078254-c3365d6e359f?w=800",
    "https://images.unsplash.com/photo-1598928636135-d146006ff4be?w=800",
    "https://images.unsplash.com/photo-1484154218962-a197022b5858?w=800" });

    var imgGomel2 = System.Text.Json.JsonSerializer.Serialize(new[] {
    "https://images.unsplash.com/photo-1502672023488-70e25813eb80?w=800",
    "https://images.unsplash.com/photo-1522771739844-6a9f6d5f14af?w=800",
    "https://images.unsplash.com/photo-1560185127-6ed189bf02f4?w=800" });

    var imgGomel3 = System.Text.Json.JsonSerializer.Serialize(new[] {
    "https://images.unsplash.com/photo-1554995207-c18c203602cb?w=800",
    "https://images.unsplash.com/photo-1513694203232-719a280e022f?w=800",
    "https://images.unsplash.com/photo-1493663284031-b7e3aefcae8e?w=800" });

    var imgGomel4 = System.Text.Json.JsonSerializer.Serialize(new[] {
    "https://images.unsplash.com/photo-1600121848594-d8644e57abab?w=800",
    "https://images.unsplash.com/photo-1600121848782-b77fa40c7b63?w=800",
    "https://images.unsplash.com/photo-1600121848918-bcb48e3b5b09?w=800" });

    var imgGomel5 = System.Text.Json.JsonSerializer.Serialize(new[] {
    "https://images.unsplash.com/photo-1583847268964-b28dc8f51f92?w=800",
    "https://images.unsplash.com/photo-1583847268964-b28dc8f51f92?w=800",
    "https://images.unsplash.com/photo-1555041469-a586c61ea9bc?w=800" });

    var imgGomel6 = System.Text.Json.JsonSerializer.Serialize(new[] {
    "https://images.unsplash.com/photo-1586023492125-27b2c045efd7?w=800",
    "https://images.unsplash.com/photo-1587825140708-dfaf72ae4b04?w=800",
    "https://images.unsplash.com/photo-1588854337236-6889d631faa8?w=800" });

    var imgGomel7 = System.Text.Json.JsonSerializer.Serialize(new[] {
    "https://images.unsplash.com/photo-1560185008-b033106af5c3?w=800",
    "https://images.unsplash.com/photo-1560185007-5f0bb1866cab?w=800",
    "https://images.unsplash.com/photo-1560185127-6ed189bf02f4?w=800" });

    var imgGomel8 = System.Text.Json.JsonSerializer.Serialize(new[] {
    "https://images.unsplash.com/photo-1599619351208-3e6c839d6828?w=800",
    "https://images.unsplash.com/photo-1599619351208-3e6c839d6828?w=800",
    "https://images.unsplash.com/photo-1598928506311-c55ded91a20c?w=800" });

    var imgGomel9 = System.Text.Json.JsonSerializer.Serialize(new[] {
    "https://images.unsplash.com/photo-1605276374104-dee2a0ed3cd6?w=800",
    "https://images.unsplash.com/photo-1605276373954-0c4a0dac5b12?w=800",
    "https://images.unsplash.com/photo-1605276374344-5f9e798e6ea2?w=800" });

    var imgGomel10 = System.Text.Json.JsonSerializer.Serialize(new[] {
    "https://images.unsplash.com/photo-1571003123894-1f0594d2b5d9?w=800",
    "https://images.unsplash.com/photo-1571003123771-bd6f04c0b394?w=800",
    "https://images.unsplash.com/photo-1566073771259-6a8506099945?w=800" });

    // Орша
    var imgOrsha1 = System.Text.Json.JsonSerializer.Serialize(new[] {
    "https://images.unsplash.com/photo-1522771739844-6a9f6d5f14af?w=800",
    "https://images.unsplash.com/photo-1502005097973-6a7082348e28?w=800",
    "https://images.unsplash.com/photo-1484101403633-562f891dc89a?w=800" });

    var imgOrsha2 = System.Text.Json.JsonSerializer.Serialize(new[] {
    "https://images.unsplash.com/photo-1598928506311-c55ded91a20c?w=800",
    "https://images.unsplash.com/photo-1574362848149-11496d93a7c7?w=800",
    "https://images.unsplash.com/photo-1560185127-6ed189bf02f4?w=800" });

    var imgOrsha3 = System.Text.Json.JsonSerializer.Serialize(new[] {
    "https://images.unsplash.com/photo-1536376072261-38c75010e6c9?w=800",
    "https://images.unsplash.com/photo-1505691938895-1758d7feb511?w=800",
    "https://images.unsplash.com/photo-1493809842364-78817add7ffb?w=800" });

    var imgOrsha4 = System.Text.Json.JsonSerializer.Serialize(new[] {
    "https://images.unsplash.com/photo-1618221195710-dd6b41faaea6?w=800",
    "https://images.unsplash.com/photo-1618221118493-9cfa1a1193f5?w=800",
    "https://images.unsplash.com/photo-1618221381711-42ca8ab6e908?w=800" });

    var imgOrsha5 = System.Text.Json.JsonSerializer.Serialize(new[] {
    "https://images.unsplash.com/photo-1631049552057-403cdb8f0658?w=800",
    "https://images.unsplash.com/photo-1631049421450-348ccd7f8949?w=800",
    "https://images.unsplash.com/photo-1631049307264-da0ec9d70304?w=800" });

    var imgOrsha6 = System.Text.Json.JsonSerializer.Serialize(new[] {
    "https://images.unsplash.com/photo-1580041065738-e72023775cdc?w=800",
    "https://images.unsplash.com/photo-1580041065165-dc9470dc3031?w=800",
    "https://images.unsplash.com/photo-1580041064824-a99f4ca5b53f?w=800" });

    var imgOrsha7 = System.Text.Json.JsonSerializer.Serialize(new[] {
    "https://images.unsplash.com/photo-1565183997392-2f6f122e5912?w=800",
    "https://images.unsplash.com/photo-1565182999561-18d7dc61c393?w=800",
    "https://images.unsplash.com/photo-1560184897-ae75f418493e?w=800" });

    var imgOrsha8 = System.Text.Json.JsonSerializer.Serialize(new[] {
    "https://images.unsplash.com/photo-1524758631624-e2822e304c36?w=800",
    "https://images.unsplash.com/photo-1524758870432-af57e54afa26?w=800",
    "https://images.unsplash.com/photo-1524758631624-e2822e304c36?w=800" });

    // Жлобин
    var imgZhlobin1 = System.Text.Json.JsonSerializer.Serialize(new[] {
    "https://images.unsplash.com/photo-1556909172-54557c7e4fb7?w=800",
    "https://images.unsplash.com/photo-1556909114-f6e7ad7d3136?w=800",
    "https://images.unsplash.com/photo-1556909153-f5e5b9e96e04?w=800" });

    var imgZhlobin2 = System.Text.Json.JsonSerializer.Serialize(new[] {
    "https://images.unsplash.com/photo-1502672260266-1c1ef2d93688?w=800",
    "https://images.unsplash.com/photo-1502672023488-70e25813eb80?w=800",
    "https://images.unsplash.com/photo-1502005097973-6a7082348e28?w=800" });

    var imgZhlobin3 = System.Text.Json.JsonSerializer.Serialize(new[] {
    "https://images.unsplash.com/photo-1558618666-fcd25c85cd64?w=800",
    "https://images.unsplash.com/photo-1558618047-3c8c76ca7d13?w=800",
    "https://images.unsplash.com/photo-1558618048-fbd3f21a58f5?w=800" });

    var imgZhlobin4 = System.Text.Json.JsonSerializer.Serialize(new[] {
    "https://images.unsplash.com/photo-1598928506311-c55ded91a20c?w=800",
    "https://images.unsplash.com/photo-1598928636135-d146006ff4be?w=800",
    "https://images.unsplash.com/photo-1598928510144-700903a7484e?w=800" });

    var imgZhlobin5 = System.Text.Json.JsonSerializer.Serialize(new[] {
    "https://images.unsplash.com/photo-1560448075-bb485b1f1e37?w=800",
    "https://images.unsplash.com/photo-1560448204-61dc36dc98c8?w=800",
    "https://images.unsplash.com/photo-1560448204-e02f11c3d0e2?w=800" });

    var imgZhlobin6 = System.Text.Json.JsonSerializer.Serialize(new[] {
    "https://images.unsplash.com/photo-1564078516393-cf04bd966897?w=800",
    "https://images.unsplash.com/photo-1564078516399-cc08b5d9b0cc?w=800",
    "https://images.unsplash.com/photo-1564078516423-7c5c1d9e1ee7?w=800" });

    var imgZhlobin7 = System.Text.Json.JsonSerializer.Serialize(new[] {
    "https://images.unsplash.com/photo-1576941089067-2de3c901e126?w=800",
    "https://images.unsplash.com/photo-1576941089165-0e4a0f3b7e8c?w=800",
    "https://images.unsplash.com/photo-1576941342433-8c2b3b8b5c8f?w=800" });

    var imgZhlobin8 = System.Text.Json.JsonSerializer.Serialize(new[] {
    "https://images.unsplash.com/photo-1582037928769-181f2644ecb7?w=800",
    "https://images.unsplash.com/photo-1582037928880-de4e8beab3b8?w=800",
    "https://images.unsplash.com/photo-1582037929561-b7234be6e1fb?w=800" });

    var imgZhlobin9 = System.Text.Json.JsonSerializer.Serialize(new[] {
    "https://images.unsplash.com/photo-1590490360182-c33d57733427?w=800",
    "https://images.unsplash.com/photo-1590490359683-658d3d23f972?w=800",
    "https://images.unsplash.com/photo-1590490360182-c33d57733427?w=800" });

    var imgZhlobin10 = System.Text.Json.JsonSerializer.Serialize(new[] {
    "https://images.unsplash.com/photo-1600596542815-ffad4c1539a9?w=800",
    "https://images.unsplash.com/photo-1600596542815-ffad4c1539a9?w=800",
    "https://images.unsplash.com/photo-1600585154526-990dced4db0d?w=800" });

    var apartments = new List<Apartment>
{
    // ===== МИНСК =====
    new Apartment { Title = "Уютная студия в центре Минска", City = "Минск",
        Address = "пр. Независимости, 15", Images = imgMinsk1,
        Description = "Современная студия в самом центре города. Свежий ремонт, вся необходимая техника, скоростной Wi-Fi. Рядом метро Октябрьская, рестораны и магазины.",
        PricePerNight = 80, Rooms = 1, MaxGuests = 2, OwnerId = users[0].Id },

    new Apartment { Title = "Просторная 2-комнатная на Немиге", City = "Минск",
        Address = "ул. Немига, 3", Images = imgMinsk2,
        Description = "Светлая квартира с современным ремонтом. Две раздельные комнаты, большая кухня. Шаговая доступность метро Немига, Верхний город рядом.",
        PricePerNight = 120, Rooms = 2, MaxGuests = 4, OwnerId = users[1].Id },

    new Apartment { Title = "Апартаменты класса люкс на Проспекте", City = "Минск",
        Address = "пр. Победителей, 7", Images = imgMinsk3,
        Description = "Элитные апартаменты с панорамным видом на город. Дизайнерский ремонт, джакузи, подземный паркинг. Рядом Национальная библиотека и Парк Победы.",
        PricePerNight = 250, Rooms = 3, MaxGuests = 6, OwnerId = users[2].Id },

    new Apartment { Title = "Квартира у Комаровского рынка", City = "Минск",
        Address = "ул. Веры Хоружей, 12", Images = imgMinsk4,
        Description = "Уютная квартира в спальном районе. Тихий двор, свежий косметический ремонт, вся бытовая техника. До метро Площадь Якуба Коласа 5 минут пешком.",
        PricePerNight = 70, Rooms = 1, MaxGuests = 2, OwnerId = users[0].Id },

    new Apartment { Title = "3-комнатная на Немиге для семьи", City = "Минск",
        Address = "ул. Революционная, 8", Images = imgMinsk5,
        Description = "Большая квартира для семейного отдыха. Три отдельные спальни, просторная гостиная, детская кроватка по запросу. В 10 минутах от исторического центра.",
        PricePerNight = 180, Rooms = 3, MaxGuests = 6, OwnerId = users[1].Id },

    new Apartment { Title = "Студия возле ТЦ Galleria", City = "Минск",
        Address = "пр. Дзержинского, 22", Images = imgMinsk6,
        Description = "Компактная студия для деловых поездок. Всё необходимое есть, рядом крупный торговый центр и остановки транспорта.",
        PricePerNight = 60, Rooms = 1, MaxGuests = 2, OwnerId = users[2].Id },

    new Apartment { Title = "Квартира в Серебрянке", City = "Минск",
        Address = "ул. Якубовского, 34", Images = imgMinsk7,
        Description = "Тихий спальный район, новый дом. Два санузла, большой балкон, парковочное место во дворе. Удобная транспортная развязка.",
        PricePerNight = 100, Rooms = 2, MaxGuests = 4, OwnerId = users[3].Id },

    new Apartment { Title = "Однокомнатная у ж/д вокзала", City = "Минск",
        Address = "ул. Бобруйская, 6", Images = imgMinsk8,
        Description = "Отличный вариант для транзитных гостей. Квартира в 7 минутах ходьбы от железнодорожного вокзала. Чистая, уютная, со всем необходимым.",
        PricePerNight = 65, Rooms = 1, MaxGuests = 2, OwnerId = users[4].Id },

    new Apartment { Title = "VIP-апартаменты в Троицком предместье", City = "Минск",
        Address = "ул. Старовиленская, 2", Images = imgMinsk9,
        Description = "Эксклюзивные апартаменты в историческом квартале Минска. Кирпичные стены, деревянные балки, вид на реку Свислочь. Уникальная атмосфера старого города.",
        PricePerNight = 300, Rooms = 2, MaxGuests = 4, OwnerId = users[0].Id },

    new Apartment { Title = "Квартира в Малиновке", City = "Минск",
        Address = "ул. Есенина, 113", Images = imgMinsk10,
        Description = "Уютная квартира в новом доме. Свежий ремонт, встроенная кухня, кондиционер. Рядом крупный торговый центр Тивали.",
        PricePerNight = 75, Rooms = 2, MaxGuests = 3, OwnerId = users[1].Id },

    new Apartment { Title = "Квартира на Площади Победы", City = "Минск",
        Address = "ул. Захарова, 18", Images = imgMinsk11,
        Description = "Просторная квартира в сталинском доме. Высокие потолки, паркет, историческая атмосфера. 3 минуты до метро Площадь Победы.",
        PricePerNight = 110, Rooms = 2, MaxGuests = 4, OwnerId = users[2].Id },

    new Apartment { Title = "Студия на Партизанском проспекте", City = "Минск",
        Address = "пр. Партизанский, 45", Images = imgMinsk12,
        Description = "Современная студия после капитального ремонта. Смарт-телевизор, посудомоечная машина, быстрый интернет. Метро Партизанская в 5 минутах.",
        PricePerNight = 55, Rooms = 1, MaxGuests = 2, OwnerId = users[3].Id },

    new Apartment { Title = "4-комнатная для большой компании", City = "Минск",
        Address = "ул. Притыцкого, 62", Images = imgMinsk13,
        Description = "Большая квартира для групповых поездок. Четыре комнаты, две ванные, вместительная кухня-гостиная. Рядом ТЦ Тележка и МКАД.",
        PricePerNight = 220, Rooms = 4, MaxGuests = 8, OwnerId = users[4].Id },

    new Apartment { Title = "Квартира у Ботанического сада", City = "Минск",
        Address = "ул. Сурганова, 27", Images = imgMinsk14,
        Description = "Тихая квартира рядом с Ботаническим садом. Зелёный район, чистый воздух, парковое пространство рядом. Отлично подходит для отдыха.",
        PricePerNight = 90, Rooms = 2, MaxGuests = 3, OwnerId = users[0].Id },

    new Apartment { Title = "Апартаменты на Октябрьской площади", City = "Минск",
        Address = "пр. Независимости, 38", Images = imgMinsk15,
        Description = "Самый центр Минска — Октябрьская площадь. Квартира с панорамными окнами, видом на Дворец Республики. Всё для комфортного проживания.",
        PricePerNight = 160, Rooms = 2, MaxGuests = 4, OwnerId = users[1].Id },

    // ===== ГОМЕЛЬ =====
    new Apartment { Title = "Уютная студия в центре Гомеля", City = "Гомель",
        Address = "ул. Советская, 10", Images = imgGomel1,
        Description = "Светлая студия в историческом центре Гомеля. Рядом Дворец Румянцевых-Паскевичей и набережная реки Сож. Свежий ремонт, всё необходимое.",
        PricePerNight = 50, Rooms = 1, MaxGuests = 2, OwnerId = users[2].Id },

    new Apartment { Title = "2-комнатная у парка Румянцевых", City = "Гомель",
        Address = "пр. Ленина, 25", Images = imgGomel2,
        Description = "Просторная квартира в двух шагах от знаменитого Гомельского парка. Две комнаты, большой балкон, вид на парковую зону.",
        PricePerNight = 80, Rooms = 2, MaxGuests = 4, OwnerId = users[3].Id },

    new Apartment { Title = "Квартира у набережной Сожа", City = "Гомель",
        Address = "ул. Набережная, 3", Images = imgGomel3,
        Description = "Квартира с видом на реку Сож. Тихое место, рядом лесопарк и набережная для прогулок. Отличное место для спокойного отдыха.",
        PricePerNight = 70, Rooms = 1, MaxGuests = 2, OwnerId = users[4].Id },

    new Apartment { Title = "Апартаменты в новом доме Гомеля", City = "Гомель",
        Address = "ул. Барыкина, 49", Images = imgGomel4,
        Description = "Квартира в новострое с современным ремонтом. Закрытый двор, видеонаблюдение, подземный паркинг. Тихий спальный район.",
        PricePerNight = 90, Rooms = 2, MaxGuests = 4, OwnerId = users[0].Id },

    new Apartment { Title = "3-комнатная на Привокзальной площади", City = "Гомель",
        Address = "пл. Привокзальная, 5", Images = imgGomel5,
        Description = "Большая квартира в 5 минутах от железнодорожного вокзала. Три комнаты, просторная кухня. Удобно для транзитных гостей и командировочных.",
        PricePerNight = 110, Rooms = 3, MaxGuests = 5, OwnerId = users[1].Id },

    new Apartment { Title = "Студия у торгового центра Секрет", City = "Гомель",
        Address = "ул. Мазурова, 16", Images = imgGomel6,
        Description = "Компактная студия рядом с крупным торговым центром. Всё необходимое для краткосрочного проживания. Хорошая транспортная доступность.",
        PricePerNight = 45, Rooms = 1, MaxGuests = 2, OwnerId = users[2].Id },

    new Apartment { Title = "Квартира в тихом районе Гомеля", City = "Гомель",
        Address = "ул. Головацкого, 34", Images = imgGomel7,
        Description = "Уютная двухкомнатная квартира в спальном районе. Рядом школа, детский сад, магазины. Тихо, зелено, чисто. Хороший вариант для семьи.",
        PricePerNight = 65, Rooms = 2, MaxGuests = 4, OwnerId = users[3].Id },

    new Apartment { Title = "Люкс-апартаменты в центре Гомеля", City = "Гомель",
        Address = "ул. Крестьянская, 7", Images = imgGomel8,
        Description = "Дизайнерские апартаменты с авторским ремонтом. Смарт-дом, кофемашина, умное освещение. Лучший вариант для тех, кто ценит комфорт.",
        PricePerNight = 150, Rooms = 2, MaxGuests = 4, OwnerId = users[4].Id },

    new Apartment { Title = "Однокомнатная на Кирова", City = "Гомель",
        Address = "ул. Кирова, 52", Images = imgGomel9,
        Description = "Классическая однокомнатная квартира в центральном районе. Хороший ремонт, вся необходимая бытовая техника, холодильник с продуктами для первого дня.",
        PricePerNight = 55, Rooms = 1, MaxGuests = 2, OwnerId = users[0].Id },

    new Apartment { Title = "Квартира у железнодорожного вокзала", City = "Гомель",
        Address = "ул. Привокзальная, 12", Images = imgGomel10,
        Description = "Идеальный вариант для приезжающих на поезде. Квартира в 3 минутах ходьбы от вокзала. Ранний заезд и поздний выезд по согласованию.",
        PricePerNight = 60, Rooms = 1, MaxGuests = 2, OwnerId = users[1].Id },

    // ===== ОРША =====
    new Apartment { Title = "Уютная квартира в центре Орши", City = "Орша",
        Address = "ул. Ленина, 8", Images = imgOrsha1,
        Description = "Светлая квартира в самом центре Орши. Рядом исторический центр, рынок, магазины. Свежий ремонт, кондиционер, скоростной интернет.",
        PricePerNight = 40, Rooms = 1, MaxGuests = 2, OwnerId = users[2].Id },

    new Apartment { Title = "2-комнатная у Оршицы", City = "Орша",
        Address = "пр. Текстильщиков, 15", Images = imgOrsha2,
        Description = "Просторная квартира у реки Оршица. Вид на воду, тихий двор, бесплатная парковка. Две раздельные комнаты, современная кухня.",
        PricePerNight = 60, Rooms = 2, MaxGuests = 4, OwnerId = users[3].Id },

    new Apartment { Title = "Квартира у ж/д вокзала Орши", City = "Орша",
        Address = "ул. Привокзальная, 4", Images = imgOrsha3,
        Description = "Удобная квартира для транзитных гостей. До железнодорожного вокзала Орши 4 минуты пешком. Орша — крупный железнодорожный узел.",
        PricePerNight = 35, Rooms = 1, MaxGuests = 2, OwnerId = users[4].Id },

    new Apartment { Title = "Апартаменты с новым ремонтом в Орше", City = "Орша",
        Address = "ул. Молокова, 27", Images = imgOrsha4,
        Description = "Квартира после капитального ремонта. Новая мебель, встроенная кухня, смарт-телевизор. Закрытый двор, домофон.",
        PricePerNight = 55, Rooms = 2, MaxGuests = 3, OwnerId = users[0].Id },

    new Apartment { Title = "Студия для командировки в Орше", City = "Орша",
        Address = "ул. Карла Маркса, 11", Images = imgOrsha5,
        Description = "Компактная студия для деловых поездок. Рабочее место, быстрый Wi-Fi, принтер по запросу. Всё что нужно для продуктивной работы.",
        PricePerNight = 30, Rooms = 1, MaxGuests = 1, OwnerId = users[1].Id },

    new Apartment { Title = "3-комнатная для большой семьи в Орше", City = "Орша",
        Address = "ул. Советская, 33", Images = imgOrsha6,
        Description = "Большая квартира для семейного проживания. Три комнаты, две ванные комнаты, большая кухня-гостиная. Тихий двор с детской площадкой.",
        PricePerNight = 85, Rooms = 3, MaxGuests = 6, OwnerId = users[2].Id },

    new Apartment { Title = "Квартира в новом доме в Орше", City = "Орша",
        Address = "ул. Пионерская, 56", Images = imgOrsha7,
        Description = "Квартира в новострое 2022 года. Панельный дом повышенной комфортности, лифт, чистый подъезд. Вся техника новая, мебель IKEA.",
        PricePerNight = 50, Rooms = 2, MaxGuests = 4, OwnerId = users[3].Id },

    new Apartment { Title = "Однокомнатная у парка Орши", City = "Орша",
        Address = "ул. Парковая, 9", Images = imgOrsha8,
        Description = "Тихая квартира рядом с городским парком. Утренние пробежки в парке, свежий воздух, зелёный район. Хороший вариант для отдыха от городской суеты.",
        PricePerNight = 38, Rooms = 1, MaxGuests = 2, OwnerId = users[4].Id },

    // ===== ЖЛОБИН =====
    new Apartment { Title = "Уютная студия в центре Жлобина", City = "Жлобин",
        Address = "пр. Металлургов, 5", Images = imgZhlobin1,
        Description = "Современная студия в центре Жлобина. Рядом главная улица города, магазины, кафе. Свежий ремонт, всё необходимое для комфортного проживания.",
        PricePerNight = 35, Rooms = 1, MaxGuests = 2, OwnerId = users[0].Id },

    new Apartment { Title = "Квартира у БМЗ в Жлобине", City = "Жлобин",
        Address = "ул. Заводская, 18", Images = imgZhlobin2,
        Description = "Квартира для командировочных на БМЗ. Быстрый Wi-Fi, рабочее место, кухня. 10 минут до проходной Белорусского металлургического завода.",
        PricePerNight = 45, Rooms = 1, MaxGuests = 2, OwnerId = users[1].Id },

    new Apartment { Title = "2-комнатная у реки Днепр", City = "Жлобин",
        Address = "ул. Набережная, 7", Images = imgZhlobin3,
        Description = "Просторная квартира с видом на реку Днепр. Две комнаты, большой балкон, красивый вид. Отличное место для спокойного отдыха в Жлобине.",
        PricePerNight = 65, Rooms = 2, MaxGuests = 4, OwnerId = users[2].Id },

    new Apartment { Title = "Апартаменты с новым ремонтом в Жлобине", City = "Жлобин",
        Address = "ул. Первомайская, 23", Images = imgZhlobin4,
        Description = "Квартира после дизайнерского ремонта. Стильный интерьер, умное освещение, кофемашина. Лучший вариант жилья в Жлобине.",
        PricePerNight = 80, Rooms = 2, MaxGuests = 3, OwnerId = users[3].Id },

    new Apartment { Title = "3-комнатная для семьи в Жлобине", City = "Жлобин",
        Address = "ул. Советская, 41", Images = imgZhlobin5,
        Description = "Большая семейная квартира. Три комнаты, кухня-столовая, детская кроватка в наличии. Тихий зелёный двор, детская площадка рядом.",
        PricePerNight = 95, Rooms = 3, MaxGuests = 6, OwnerId = users[4].Id },

    new Apartment { Title = "Студия у торгового центра Корона", City = "Жлобин",
        Address = "ул. Мира, 15", Images = imgZhlobin6,
        Description = "Компактная студия рядом с ТЦ Корона. Всё для краткосрочного проживания. Остановка автобуса у подъезда, удобная парковка во дворе.",
        PricePerNight = 30, Rooms = 1, MaxGuests = 2, OwnerId = users[0].Id },

    new Apartment { Title = "Квартира в новом районе Жлобина", City = "Жлобин",
        Address = "ул. Молодёжная, 67", Images = imgZhlobin7,
        Description = "Квартира в современном жилом комплексе. Новый дом, чистый подъезд, консьерж, подземный паркинг. Всё для комфортного проживания.",
        PricePerNight = 55, Rooms = 2, MaxGuests = 4, OwnerId = users[1].Id },

    new Apartment { Title = "Однокомнатная у вокзала Жлобина", City = "Жлобин",
        Address = "пл. Вокзальная, 2", Images = imgZhlobin8,
        Description = "Удобная квартира для приезжих. До железнодорожного вокзала Жлобина 5 минут пешком. Ранний заезд от 8:00, поздний выезд до 15:00.",
        PricePerNight = 32, Rooms = 1, MaxGuests = 2, OwnerId = users[2].Id },

    new Apartment { Title = "Квартира у Дворца культуры металлургов", City = "Жлобин",
        Address = "пр. Металлургов, 34", Images = imgZhlobin9,
        Description = "Уютная квартира в центральном районе Жлобина. Рядом Дворец культуры металлургов, городской парк и фонтан. Хорошая инфраструктура.",
        PricePerNight = 48, Rooms = 1, MaxGuests = 2, OwnerId = users[3].Id },

    new Apartment { Title = "Люкс для корпоративных гостей в Жлобине", City = "Жлобин",
        Address = "ул. Заводская, 5", Images = imgZhlobin10,
        Description = "Представительские апартаменты для корпоративных гостей БМЗ. Переговорный уголок, кофемашина, быстрый интернет. Счёт для юридических лиц.",
        PricePerNight = 100, Rooms = 2, MaxGuests = 3, OwnerId = users[4].Id },
};

    db.Apartments.AddRange(apartments);
    await db.SaveChangesAsync();
}
var port = Environment.GetEnvironmentVariable("PORT") ?? "5001";
app.Run($"http://0.0.0.0:{port}");

