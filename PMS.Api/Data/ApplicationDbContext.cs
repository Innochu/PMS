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

        // PIN Form child tables
        public DbSet<ProjectPinValueDriver> ProjectPinValueDrivers => Set<ProjectPinValueDriver>();
        public DbSet<ProjectPinRamAssessment> ProjectPinRamAssessments => Set<ProjectPinRamAssessment>();
        public DbSet<ProjectPinCo2Screening> ProjectPinCo2Screenings => Set<ProjectPinCo2Screening>();
        public DbSet<ProjectPinMtoScore> ProjectPinMtoScores => Set<ProjectPinMtoScore>();
        public DbSet<ProjectPinStakeholder> ProjectPinStakeholders => Set<ProjectPinStakeholder>();
        public DbSet<ProjectPinAttachment> ProjectPinAttachments => Set<ProjectPinAttachment>();
        public DbSet<ProjectPinInterfaceSignOff> ProjectPinInterfaceSignOffs => Set<ProjectPinInterfaceSignOff>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // ===================================================================
            // 1. Project (1) ←→ (1) PinForm  (optional: a project may not have PIN yet)
            // ===================================================================
            modelBuilder.Entity<Project>()
                .HasOne(p => p.PinForm)
                .WithOne(pin => pin.Project)
                .HasForeignKey<PinForm>(pin => pin.ProjectId)
                .OnDelete(DeleteBehavior.SetNull); // Allow project to exist without PIN

            // ===================================================================
            // 2. Project → Portfolio
            // ===================================================================
            modelBuilder.Entity<Project>()
                .HasOne(p => p.Portfolio)
                .WithMany(port => port.Projects)
                .HasForeignKey(p => p.PortfolioId)
                .OnDelete(DeleteBehavior.Restrict);

            // ===================================================================
            // 3. Project → CreatedBy User
            // ===================================================================
            modelBuilder.Entity<Project>()
                .HasOne(p => p.CreatedBy)
                .WithMany()
                .HasForeignKey(p => p.CreatedById)
                .OnDelete(DeleteBehavior.Restrict);

            // ===================================================================
            // 4. CRITICAL: 1:1 Relationships for PIN Form Specialist Inputs
            // ===================================================================
            // RAM Assessment
            modelBuilder.Entity<PinForm>()
                .HasOne(pf => pf.RamAssessment)
                .WithOne(r => r.Pin)
                .HasForeignKey<ProjectPinRamAssessment>(r => r.PinId)
                .OnDelete(DeleteBehavior.Cascade);

            // CO2 Screening ← THIS WAS THE ERROR
            modelBuilder.Entity<PinForm>()
                .HasOne(pf => pf.Co2Screening)
                .WithOne(c => c.Pin)
                .HasForeignKey<ProjectPinCo2Screening>(c => c.PinId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<WorkflowInstance>()
    .HasOne(w => w.Project)
    .WithMany(p => p.Workflows)
    .HasForeignKey(w => w.ProjectId)
    .OnDelete(DeleteBehavior.Cascade);


            // MTO Score
            modelBuilder.Entity<PinForm>()
                .HasOne(pf => pf.MtoScore)
                .WithOne(m => m.Pin)
                .HasForeignKey<ProjectPinMtoScore>(m => m.PinId)
                .OnDelete(DeleteBehavior.Cascade);

            // Interface Sign-Off
            modelBuilder.Entity<PinForm>()
                .HasOne(pf => pf.InterfaceSignOff)
                .WithOne(i => i.Pin)
                .HasForeignKey<ProjectPinInterfaceSignOff>(i => i.PinId)
                .OnDelete(DeleteBehavior.Cascade);

            // ===================================================================
            // 5. Value Drivers (Many-to-Many)
            // ===================================================================
            modelBuilder.Entity<ProjectPinValueDriver>()
                .HasKey(vd => new { vd.PinId, vd.ValueDriverCode });

            // ===================================================================
            // 6. Indexes for performance
            // ===================================================================
            modelBuilder.Entity<Project>()
                .HasIndex(p => p.ProjectNumber)
                .IsUnique();

            modelBuilder.Entity<Project>()
                .HasIndex(p => p.CurrentPhase);

            modelBuilder.Entity<PinForm>()
                .HasIndex(pf => pf.ProjectId);

            // Unique indexes on child 1:1 tables
            modelBuilder.Entity<ProjectPinRamAssessment>()
                .HasIndex(r => r.PinId)
                .IsUnique();

            modelBuilder.Entity<ProjectPinCo2Screening>()
                .HasIndex(c => c.PinId)
                .IsUnique();

            modelBuilder.Entity<ProjectPinMtoScore>()
                .HasIndex(m => m.PinId)
                .IsUnique();

            modelBuilder.Entity<ProjectPinInterfaceSignOff>()
                .HasIndex(i => i.PinId)
                .IsUnique();

            // ===================================================================
            // 7. JSONB columns (for OAP/PCAP if needed)
            // ===================================================================
            modelBuilder.Entity<OapForm>()
                .Property(o => o.FormData)
                .HasColumnType("jsonb");

            modelBuilder.Entity<PcapForm>()
                .Property(p => p.FormData)
                .HasColumnType("jsonb");

            modelBuilder.Entity<PcapForm>()
                .Property(p => p.GlobalDamControls)
                .HasColumnType("jsonb");

            // ===================================================================
            // 8. User.Id is string (not identity)
            // ===================================================================
            modelBuilder.Entity<User>()
                .Property(u => u.Id)
                .ValueGeneratedNever();

            modelBuilder.Entity<Project>()
                .Property(p => p.CreatedById)
                .ValueGeneratedNever();
        }
    }
}