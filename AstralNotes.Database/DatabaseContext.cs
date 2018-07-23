using AstralNotes.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AstralNotes.Database
{
    /// <summary>
    /// EF Core контекст базы данных менеджмента
    /// </summary>
    public class DatabaseContext : IdentityDbContext<IdentityUser>
    {
        /// <summary />
        public DbSet<Note> Notes { get; set; }

        /// <summary />
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
    }
}