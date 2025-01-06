using BerberWebSitesi.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Veritabanı bağlantısını yapılandırma
builder.Services.AddDbContext<BerberDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Servisleri ekleme
builder.Services.AddControllersWithViews();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Oturum süresi
    options.Cookie.HttpOnly = true; // Güvenlik için HTTPOnly
    options.Cookie.IsEssential = true; // Çerezlerin gerekli olduğuna işaret eder
});

// Uygulama oluşturma
var app = builder.Build();

// Middleware'leri yapılandırma
app.UseStaticFiles();
app.UseRouting();
app.UseSession(); // Session middleware'i burada kullanılmalı
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
