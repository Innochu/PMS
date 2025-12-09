using Microsoft.EntityFrameworkCore;
using PMS.Api.Models;

namespace PMS.Api.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // DbSets
        public DbSet<User> Users { get; set; } = null!;
        public DbSet<Portfolio> Portfolios { get; set; } = null!;
        public DbSet<Project> Projects { get; set; } = null!;
        public DbSet<PinForm> PinForms { get; set; } = null!;
        public DbSet<OapForm> OapForms { get; set; } = null!;
        public DbSet<PcapForm> PcapForms { get; set; } = null!;
        public DbSet<Document> Documents { get; set; } = null!;
        public DbSet<ActionItem> ActionItems { get; set; } = null!;
        public DbSet<WorkflowInstance> WorkflowInstances { get; set; } = null!;
        public DbSet<WorkflowStep> WorkflowSteps { get; set; } = null!;
        public DbSet<DrbDecision> DrbDecisions { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // 1. Project (1) ←→ (1) PinForm
            modelBuilder.Entity<Project>()
                .HasOne(p => p.PinForm)
                .WithOne(pin => pin.Project)
                .HasForeignKey<PinForm>(pin => pin.ProjectId)
                .OnDelete(DeleteBehavior.Cascade);

            // 2. Project → Portfolio
            modelBuilder.Entity<Project>()
                .HasOne(p => p.Portfolio)
                .WithMany(port => port.Projects)
                .HasForeignKey(p => p.PortfolioId);

            // 3. Project → User (CreatedBy) – string Id
            modelBuilder.Entity<Project>()
                .HasOne(p => p.CreatedBy)
                .WithMany()
                .HasForeignKey(p => p.CreatedById)
                .OnDelete(DeleteBehavior.Restrict);

            // 4. WorkflowInstance → WorkflowStep
            modelBuilder.Entity<WorkflowInstance>()
                .HasMany(w => w.Steps)
                .WithOne(s => s.WorkflowInstance)
                .HasForeignKey(s => s.WorkflowInstanceId)
                .OnDelete(DeleteBehavior.Cascade);

            // 5. Indexes
            modelBuilder.Entity<Project>()
                .HasIndex(p => p.ProjectNumber)
                .IsUnique();

            modelBuilder.Entity<Project>()
                .HasIndex(p => p.CurrentPhase);

            // 6. JSON columns (SQL Server & PostgreSQL both support text/jsonb)
            modelBuilder.Entity<PinForm>()
                .Property(p => p.Section1Data)
                .HasColumnType("jsonb");

            modelBuilder.Entity<PinForm>()
                .Property(p => p.Section2Data)
                .HasColumnType("jsonb");

            modelBuilder.Entity<OapForm>()
                .Property(o => o.FormData)
                .HasColumnType("jsonb");

            modelBuilder.Entity<PcapForm>()
                .Property(p => p.FormData)
                .HasColumnType("jsonb");

            modelBuilder.Entity<PcapForm>()
                .Property(p => p.GlobalDamControls)
                .HasColumnType("jsonb");

            // 7. CRITICAL FIX: Tell EF Core that User.Id is NOT an identity column
            modelBuilder.Entity<User>()
                .Property(u => u.Id)
                .ValueGeneratedNever();   // ← This stops PostgreSQL identity error

            // Also prevent any other string FK from being treated as identity
            modelBuilder.Entity<Project>()
                .Property(p => p.CreatedById)
                .ValueGeneratedNever();
        }
    }
}