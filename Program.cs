using backend_agua.Infraestructure.Database;
using backend_agua.Infraestructure.Seeders;
using backend_agua.Interfaces;
using backend_agua.Services;
using backend_agua.Settings;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

using backend_agua.BackgroundServices;
using backend_agua.Hubs;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.SetIsOriginAllowed(_ => true)
              .WithMethods("GET", "POST", "PUT", "DELETE", "PATCH", "OPTIONS")
              .WithHeaders("Authorization", "Content-Type", "Accept", "X-Requested-With", "X-SignalR-User-Agent")
              .AllowCredentials();
    });
});

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IUsuarioService, UsuarioService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IMunicipioService, MunicipioService>();
builder.Services.AddScoped<IParroquiaService, ParroquiaService>();
builder.Services.AddScoped<IComunaService, ComunaService>();
builder.Services.AddScoped<IComunidadService, ComunidadService>();
builder.Services.AddScoped<IReporteService, ReporteService>();

builder.Services.Configure<ReportSettings>(builder.Configuration.GetSection("ReportSettings"));

var jwtSettings = builder.Configuration.GetSection("Jwt");
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtSettings["Issuer"],
            ValidAudience = jwtSettings["Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["Key"]!))
        };
    });

builder.Services.AddControllers();
builder.Services.AddSignalR();
builder.Services.AddHostedService<ReportTimerWorker>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Backend Agua API", Version = "v1" });

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Introduce el token JWT JWT. Ejemplo: \"Bearer {token}\""
    });

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
            Array.Empty<string>()
        }
    });
});

var app = builder.Build();

// Middleware de diagnóstico de CORS y manejo de OPTIONS manual
app.Use(async (context, next) =>
{
    // Asegurar que las cabeceras CORS estén presentes en todas las respuestas
    context.Response.Headers.Append("Access-Control-Allow-Origin", context.Request.Headers.Origin.FirstOrDefault() ?? "*");
    context.Response.Headers.Append("Access-Control-Allow-Methods", "GET, POST, PUT, DELETE, PATCH, OPTIONS");
    context.Response.Headers.Append("Access-Control-Allow-Headers", "Authorization, Content-Type, Accept, X-Requested-With, X-SignalR-User-Agent");
    context.Response.Headers.Append("Access-Control-Allow-Credentials", "true");

    if (context.Request.Method == "OPTIONS")
    {
        context.Response.StatusCode = 204;
        await context.Response.CompleteAsync();
        return;
    }

    await next();
});

app.UseCors("AllowAll");

// Seed the database
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<ApplicationDbContext>();
    
    // Aplicar migraciones automáticamente al iniciar
    context.Database.Migrate();

    MunicipioSeeder.Initialize(services);
    ParroquiaSeeder.Initialize(services);
    ComunaSeeder.Initialize(services);
    ComunidadSeeder.Initialize(services);
    UsuarioSeeder.Initialize(services);
}

app.UseHttpsRedirection();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Backend Agua API v1"));

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapHub<ReportHub>("/hubs/reports");

app.Run();
