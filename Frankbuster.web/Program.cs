using BlockBuster.manager.Conexion;
using BlockBuster.manager.Manager;
using BlockBuster.manager.Repositorios;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("ConexionDefault");

//HAY QUE BORRAR, ConexionDB YA QUE SE HACE LA CONEXION EN REPOSITORIOS
ConexionDB miConexion = new ConexionDB(connectionString);

builder.Services.AddControllersWithViews();


builder.Services.AddScoped<IUsuarioManager, UsuarioManager>();
builder.Services.AddScoped<IPeliculasManager, PeliculasManager>();
builder.Services.AddScoped<IUsuarioRepository>(provider => new UsuarioRepository(connectionString));
builder.Services.AddScoped<IPeliculaRepository>(provider => new ContainerRepository(connectionString));


builder.Services.AddScoped<IdentificacionManager>(provider => new IdentificacionManager(miConexion));

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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
