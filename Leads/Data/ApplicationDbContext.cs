using Leads.Models;
using Microsoft.EntityFrameworkCore;

namespace Leads.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<Lead> Leads { get; set; }
    public DbSet<Agent> Agents { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Lead>()
            .HasOne(l => l.Agent)
            .WithMany(a => a.Leads)
            .HasForeignKey(l => l.AgentId)
            .OnDelete(DeleteBehavior.Cascade); 

        base.OnModelCreating(modelBuilder);
    }
    
}