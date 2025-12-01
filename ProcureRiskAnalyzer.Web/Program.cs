using Microsoft.AspNetCore.Authentication.Cookies;
using Okta.AspNetCore;
using Microsoft.EntityFrameworkCore;
using ProcureRiskAnalyzer.Web.Data;
using System.Linq;
using System.IO;
using System.Data.Common;


var builder = WebApplication.CreateBuilder(args);

// Додаємо MVC
builder.Services.AddControllersWithViews();

builder.Services.AddControllers();
builder.Services.AddHttpClient();

// Add CORS for MAUI client
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowMauiClient", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

// Налаштовуємо авторизацію через Okta OAuth2 та JWT Bearer
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = OktaDefaults.MvcAuthenticationScheme;
})
.AddCookie()
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(
            System.Text.Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"] ?? "YourSuperSecretKeyThatIsAtLeast32CharactersLongForJWTTokenSecurity!"))
    };
})
.AddOktaMvc(new OktaMvcOptions
{
    OktaDomain = builder.Configuration["Okta:Domain"],
    ClientId = builder.Configuration["Okta:ClientId"],
    ClientSecret = builder.Configuration["Okta:ClientSecret"]
});
var dbProvider = builder.Configuration["DatabaseProvider"];

switch (dbProvider)
{
    case "SqlServer":
        builder.Services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer")));
        break;
    case "Postgres":
        builder.Services.AddDbContext<AppDbContext>(options =>
            options.UseNpgsql(builder.Configuration.GetConnectionString("Postgres")));
        break;
    case "Sqlite":
        builder.Services.AddDbContext<AppDbContext>(options =>
            options.UseSqlite(builder.Configuration.GetConnectionString("Sqlite")));
        break;
    default:
        builder.Services.AddDbContext<AppDbContext>(options =>
            options.UseInMemoryDatabase("ProcureDB"));
        break;

}

var app = builder.Build();

// Ensure database is created with all tables and columns
// For development: Always recreate database to ensure correct structure
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
    try
    {
        var dbPath = Path.Combine(Directory.GetCurrentDirectory(), "procure.db");
        var needsRecreate = false;
        
        if (File.Exists(dbPath))
        {
            // Check if Tenders table exists and has CategoryId column
            try
            {
                // Ensure connection is closed before checking
                var connection = dbContext.Database.GetDbConnection();
                if (connection.State == System.Data.ConnectionState.Open)
                {
                    connection.Close();
                }
                
                // Wait a bit to ensure file is released
                System.Threading.Thread.Sleep(100);
                
                connection.Open();
                using var command = connection.CreateCommand();
                command.CommandText = "PRAGMA table_info(Tenders);";
                using var reader = command.ExecuteReader();
                var hasCategoryId = false;
                var tableExists = false;
                
                while (reader.Read())
                {
                    tableExists = true;
                    var columnName = reader.GetString(1);
                    if (columnName == "CategoryId")
                    {
                        hasCategoryId = true;
                        break;
                    }
                }
                
                connection.Close();
                
                if (tableExists && !hasCategoryId)
                {
                    logger.LogWarning("Database exists but Tenders table is missing CategoryId column. Recreating database...");
                    needsRecreate = true;
                }
                else if (!tableExists)
                {
                    logger.LogWarning("Tenders table does not exist. Recreating database...");
                    needsRecreate = true;
                }
            }
            catch (Exception checkEx)
            {
                logger.LogWarning(checkEx, "Could not check database structure. Recreating database...");
                needsRecreate = true;
            }
        }
        
        if (needsRecreate || !File.Exists(dbPath))
        {
            // Ensure connection is closed
            try
            {
                var connection = dbContext.Database.GetDbConnection();
                if (connection.State == System.Data.ConnectionState.Open)
                {
                    connection.Close();
                }
            }
            catch { }
            
            // Delete database using EF Core method
            try
            {
                logger.LogInformation("Deleting existing database...");
                dbContext.Database.EnsureDeleted();
                logger.LogInformation("Database deleted successfully.");
            }
            catch (Exception deleteEx)
            {
                logger.LogWarning(deleteEx, "Could not delete database using EnsureDeleted. Trying file deletion...");
                // Fallback to file deletion
                try
                {
                    if (File.Exists(dbPath))
                    {
                        File.Delete(dbPath);
                        var shmPath = dbPath + "-shm";
                        var walPath = dbPath + "-wal";
                        if (File.Exists(shmPath)) File.Delete(shmPath);
                        if (File.Exists(walPath)) File.Delete(walPath);
                        logger.LogInformation("Deleted database files manually.");
                    }
                }
                catch (Exception fileDeleteEx)
                {
                    logger.LogError(fileDeleteEx, "Could not delete database files. Application may fail.");
                }
            }
            
            logger.LogInformation("Creating database with current model...");
            dbContext.Database.EnsureCreated();
            logger.LogInformation("Database created successfully with all tables and columns.");
        }
        else
        {
            logger.LogInformation("Database already exists with correct structure.");
        }
    }
    catch (Exception ex)
    {
        logger.LogError(ex, "Error during database initialization.");
        // Try to continue anyway - might work if database already exists
    }
}

app.UseStaticFiles();
app.UseCors("AllowMauiClient");
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

// Маршрутизація
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
