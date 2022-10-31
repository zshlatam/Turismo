using Microsoft.AspNetCore.Identity;
using Turismo.Models;
using Turismo.Servicios;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddTransient<IRepositorioCiudad, RepositorioCiudad>();
builder.Services.AddTransient<IRepositorioEstadoDepartamento, RepositorioEstadoDepartamento>();
builder.Services.AddTransient<IRepositorioServiciosDepartamento, RepositorioServiciosDepartamento>();
builder.Services.AddTransient<IRepositorioDepartamento, RepositorioDepartamento>();
builder.Services.AddTransient<IRepositorioUsuario, RepositorioUsuario>();
builder.Services.AddTransient<IRepositorioReserva, RepositorioReserva>();
builder.Services.AddTransient<IRepositorioCheckIn, RepositorioCheckIn>();
builder.Services.AddTransient<IRepositorioEstadoReserva, RepositorioEstadoReserva>();
builder.Services.AddTransient<IRepositorioMantenimiento, RepositorioMantenimiento>();
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddTransient<IUserStore<Usuario>, UsuarioStore>();
builder.Services.AddTransient<SignInManager<Usuario>>();
builder.Services.AddIdentityCore<Usuario>(opciones =>
{
    opciones.Password.RequireDigit = false;
    opciones.Password.RequireLowercase = false;
    opciones.Password.RequireUppercase = false;
    opciones.Password.RequireNonAlphanumeric = false;
}).AddErrorDescriber<MensajesDeErrorIdentity>();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = IdentityConstants.ApplicationScheme;
    options.DefaultChallengeScheme = IdentityConstants.ApplicationScheme;
    options.DefaultSignOutScheme = IdentityConstants.ApplicationScheme;
}).AddCookie(IdentityConstants.ApplicationScheme, opciones =>
{
    opciones.LoginPath = "/usuario/login";
});
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
//el codigo de arriba singleton es dios

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();


app.UseAuthentication();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
