using Leads.Models;
using Microsoft.EntityFrameworkCore;

namespace Leads.Data;

public class ApplicationDbContext : Microsoft.EntityFrameworkCore.DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<Lead> Leads { get; set; }
    public DbSet<Agent> Agents { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        
        modelBuilder.Entity<User>()
            .HasOne(u => u.Agent)
            .WithOne(a => a.User)
            .HasForeignKey<User>(u => u.AgentId)
            .OnDelete(DeleteBehavior.SetNull); 
        
        modelBuilder.Entity<Lead>()
            .HasOne(l => l.Agent)
            .WithMany(a => a.Leads)
            .HasForeignKey(l => l.AgentId)
            .OnDelete(DeleteBehavior.Cascade); 

        base.OnModelCreating(modelBuilder);
    }
    
}