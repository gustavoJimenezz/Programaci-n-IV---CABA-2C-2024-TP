using BlockBuster.manager.Conexion;
using BlockBuster.manager.Manager;
using BlockBuster.manager.Repositorios;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication.Cookies;
using BlockBuster.manager.Entidades;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddAuthentication(Options =>
{
    Options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    Options.DefaultChallengeScheme = GoogleDefaults.AuthenticationScheme;
}).AddCookie().AddGoogle(GoogleDefaults.AuthenticationScheme, Options =>
{
    Options.ClientId = builder.Configuration.GetSection("GoogleKeys:ClientId").Value;
    Options.ClientSecret = builder.Configuration.GetSection("GoogleKeys:ClientSecret").Value;

    Options.Events.OnCreatingTicket = ctx =>
    {
        var usuarioServicio = ctx.HttpContext.RequestServices.GetRequiredService<IUsuarioRepository>();
        string googleNameIdentifier = ctx.Identity.Claims.First(x => x.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier").Value.ToString(); ;
        var usuario = usuarioServicio.GetUsuarioPorGoogleSubject(googleNameIdentifier);
        
        int idUsuario = 0;
        //string nombre = "";

        if (usuario == null)
        {
            Usuario usuarioNuevo = new Usuario();


            usuarioNuevo.Nombre = ctx.Identity.Claims.First(x => x.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/givenname").Value.ToString();
            usuarioNuevo.GoogleIdentificador = googleNameIdentifier;
            usuarioNuevo.Activo = true;
            usuarioNuevo.FechaAlta = DateTime.Now;
            usuarioNuevo.IdentificacionId = null;

            idUsuario = usuarioServicio.CrearUsuario(usuarioNuevo);

            string rolUsuario = usuarioServicio.ObtenerRol(idUsuario);

        }
        else
        {
            idUsuario = usuario.UsuarioId;
        }
        if (usuario.Nombre == "elKeko" && usuario.GoogleIdentificador == "109537588717020104832")
        {
            ctx.Identity.AddClaim(new System.Security.Claims.Claim("Rol", "Administrador"));
        }
        else
        {
            idUsuario = usuario.UsuarioId;
        }
        ctx.Identity.AddClaim(new System.Security.Claims.Claim("idUsuario", idUsuario.ToString()));
        ctx.Identity.AddClaim(new System.Security.Claims.Claim("googleNameIdentifier", googleNameIdentifier.ToString()));
        var accessToken = ctx.AccessToken;
        ctx.Identity.AddClaim(new System.Security.Claims.Claim("accessToken", accessToken));

        return Task.CompletedTask;
    };


});
    
var connectionString = builder.Configuration.GetConnectionString("ConexionDefault");



//HAY QUE BORRAR, ConexionDB YA QUE SE HACE LA CONEXION EN REPOSITORIOS
ConexionDB miConexion = new ConexionDB(connectionString);

builder.Services.AddControllersWithViews();


builder.Services.AddScoped<IUsuarioManager, UsuarioManager>();
builder.Services.AddScoped<IPeliculasManager, PeliculasManager>();
builder.Services.AddScoped<IRegistroActividadManager, RegistroActividadManager>();
builder.Services.AddScoped<IUsuarioRepository>(provider => new UsuarioRepository(connectionString));
builder.Services.AddScoped<IPeliculaRepository>(provider => new PeliculasRepository(connectionString));
builder.Services.AddScoped<IRegistroActividadRepository>(provider => new RegistroActividadRepository(connectionString));


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
