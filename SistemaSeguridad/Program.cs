using Microsoft.AspNetCore.Identity;
using SistemaSeguridad.Models;
using SistemaSeguridad.Servicios;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddTransient<IRepositoryGenero, RepositoryGenero>();
builder.Services.AddTransient<IServicioUsuarios, ServicioUsuarios>();
builder.Services.AddTransient<IRepositoyEmpresa, RepositoryEmpresa>();
builder.Services.AddHttpContextAccessor();
builder.Services.AddTransient<IReposirorySucursal, RepositorySucursal>();
builder.Services.AddTransient<IRepositoryUsuarios, RepositoryUsuarios>();
builder.Services.AddTransient<IUserStore<UsuarioPrueba>, UsuarioStore>();
builder.Services.AddTransient<SignInManager<UsuarioPrueba>>();
builder.Services.AddIdentityCore<UsuarioPrueba>(opciones =>
{
	opciones.Password.RequireDigit = false;
	opciones.Password.RequireLowercase = false;
	opciones.Password.RequireUppercase = false;
	opciones.Password.RequireNonAlphanumeric = false;
});

builder.Services.AddAuthentication(options =>
{
	options.DefaultAuthenticateScheme = IdentityConstants.ApplicationScheme;
	options.DefaultChallengeScheme = IdentityConstants.ApplicationScheme;
	options.DefaultSignOutScheme = IdentityConstants.ApplicationScheme;
}).AddCookie(IdentityConstants.ApplicationScheme, opciones =>
{
	opciones.LoginPath = "/Usuario/Login";

});

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
