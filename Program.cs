using System.Text;
using DotNetEnv;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Repaso.Config;
using Repaso.Data;
using Repaso.Repositories;
using Repaso.Services;

Env.Load();  // Carga las variables de entorno desde un archivo .env

// Obtiene las variables de entorno necesarias para conectarse a la base de datos
var host = Environment.GetEnvironmentVariable("DB_HOST");
var databaseName = Environment.GetEnvironmentVariable("DB_NAME");
var port = Environment.GetEnvironmentVariable("DB_PORT");
var username = Environment.GetEnvironmentVariable("DB_USERNAME");
var password = Environment.GetEnvironmentVariable("DB_PASSWORD");

// Crea la cadena de conexión utilizando las variables de entorno
var connectionString = $"server={host};port={port};database={databaseName};uid={username};password={password}";

var builder = WebApplication.CreateBuilder(args);

// Conexión con la base de datos usando MySQL y la versión especificada
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseMySql(connectionString, ServerVersion.Parse("8.0.20-mysql")));

// Registra los servicios para trabajar con Pets y Users
builder.Services.AddScoped<IPetRepository, PetServices>();
builder.Services.AddScoped<IUserRepository, UserServices>();

// Configura el servicio para el uso de JWT para la autenticación
builder.Services.AddSingleton<JWT>();
builder.Services.AddAuthentication(config =>
{
    config.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    config.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    config.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(config =>
{
    config.RequireHttpsMetadata = false;
    config.SaveToken = true;

    // Define los parámetros de validación del token JWT
    config.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true, // Valida el emisor del token
        ValidIssuer = Environment.GetEnvironmentVariable("JWT_ISSUER"),
        ValidateAudience = false, // No se valida la audiencia
        ValidAudience = Environment.GetEnvironmentVariable("JWT_AUDIENCE"),
        ValidateLifetime = true, // Valida el tiempo de vida del token
        ClockSkew = TimeSpan.Zero, // No permite margen de tiempo para la expiración
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("JWT_KEY")!))
    };
});

// Agrega los controladores al contenedor de servicios
builder.Services.AddControllers();

// Configura Swagger para la documentación de la API
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    // Define la documentación de versiones v1 y v2 para la API
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Repaso Filtro", Version = "v1" });
    c.SwaggerDoc("v2", new OpenApiInfo { Title = "Repaso Filtro", Version = "v2" });

    // Configura el esquema de autenticación JWT en Swagger
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme. Example: \"Bearer {token}\"",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer"
    });

    // Define los requisitos de seguridad para usar el token Bearer
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});

var app = builder.Build();

// Configura el pipeline de solicitudes HTTP
if (app.Environment.IsDevelopment())
{
    // Habilita Swagger en modo desarrollo
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "Repaso Filtro v1");
        options.SwaggerEndpoint("/swagger/v2/swagger.json", "Repaso Filtro v2");
    });
}

// Habilita la redirección HTTPS
app.UseHttpsRedirection();

// Configura la autenticación y autorización
app.UseAuthentication();
app.UseAuthorization();

// Habilita una página de bienvenida en la ruta "/"
app.UseWelcomePage(new WelcomePageOptions
{
    Path = "/"
});

// Mapea las rutas a los controladores
app.MapControllers();

// Inicia la aplicación
app.Run();
