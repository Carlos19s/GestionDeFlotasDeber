using GestionDeFlotas.Modelos;
using Proyecto.API.Consumer;
using QuestPDF.Infrastructure;
using Sensores.Modelos;

// Valores por defecto
Crud<Sensor>.EndPoint = "https://localhost:7238/api/Sensors";
Crud<Alerta>.EndPoint = "https://localhost:7238/api/Alertas";
Crud<Camion>.EndPoint = "https://localhost:7259/api/Camions";
Crud<Conductor>.EndPoint = "https://localhost:7259/api/Conductors";
Crud<Taller>.EndPoint = "https://localhost:7259/api/Tallers";
Crud<Mantenimiento>.EndPoint = "https://localhost:7259/api/Mantenimientoes";
Crud<LoginDto>.EndPoint = "https://localhost:7259/api/LoginDtoes";
Crud<Usuario>.EndPoint = "https://localhost:7259/api/Usuarios";

var builder = WebApplication.CreateBuilder(args);

// Cargar variables de entorno además de appsettings.json
builder.Configuration.AddEnvironmentVariables();

// Si las variables están definidas, sobrescribir los EndPoint
var apiSensoresBaseUrl = builder.Configuration["ApiSensoresBaseUrl"];
if (!string.IsNullOrEmpty(apiSensoresBaseUrl))
{
    Crud<Sensor>.EndPoint = apiSensoresBaseUrl + "/api/Sensors";
    Crud<Alerta>.EndPoint = apiSensoresBaseUrl + "/api/Alertas";
}


var apiGestionBaseUrl = builder.Configuration["ApiGestionBaseUrl"];
if (!string.IsNullOrEmpty(apiGestionBaseUrl))
{
    Crud<Camion>.EndPoint = apiGestionBaseUrl + "/api/Camions";
    Crud<Conductor>.EndPoint = apiGestionBaseUrl + "/api/Conductors";
    Crud<Taller>.EndPoint = apiGestionBaseUrl + "/api/Tallers";
    Crud<Mantenimiento>.EndPoint = apiGestionBaseUrl + "/api/Mantenimientoes";
    Crud<LoginDto>.EndPoint = apiGestionBaseUrl + "/api/LoginDtoes";
    Crud<Usuario>.EndPoint = apiGestionBaseUrl + "/api/Usuarios";
}

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Tiempo de expiración de la sesión
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});
builder.Services.AddHttpContextAccessor();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseSession();
app.UseAuthorization();
QuestPDF.Settings.License = LicenseType.Community;

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
