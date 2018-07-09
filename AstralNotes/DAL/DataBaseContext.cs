using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using AstralNotes.DAL.Models;

namespace AstralNotes.DAL
{
    public class DataBaseContext : IdentityDbContext<IdentityUser>
    {
        public DbSet<Note> Notes { get; set; }

        public DataBaseContext(DbContextOptions<DataBaseContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
