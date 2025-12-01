using Microsoft.EntityFrameworkCore;
using ProcureRiskAnalyzer.Web.Data;
using ProcureRiskAnalyzer.Web.Models;
using BCrypt.Net;

namespace ProcureRiskAnalyzer.Web.Services;

public class UserInitializationService
{
    private readonly AppDbContext _context;

    public UserInitializationService(AppDbContext context)
    {
        _context = context;
    }

    public async Task InitializeUsersAsync()
    {
        if (!await _context.Users.AnyAsync(u => u.Username == "olena_skakunn"))
        {
            var user = new User
            {
                Username = "olena_skakunn",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("A0982935531a"),
                Email = "olena_skakunn@example.com"
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();
        }
    }
}

