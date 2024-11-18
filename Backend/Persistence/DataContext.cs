using Domain;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Persistence
{
    public class DataContext : IdentityDbContext<AppUser>
    {
        public DataContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Activity> Activities { get; set; }
        public DbSet<ActivityAttendee> ActivityAttendees { get; set; }
        public DbSet<Photo> Photos { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<UserFollowing> UserFollowings { get; set; }
        public DbSet<Configuration> Configurations { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Sale> Sales { get; set; }
        public DbSet<Seller> Sellers { get; set; }
        public DbSet<Events> Events { get; set; }
        public DbSet<Register> Registers { get; set; }
        public DbSet<Bookmaker> Bookmakers { get; set; }
        public DbSet<SalesPerformanceTeam> SalesPerformanceTeams { get; set; }
        public DbSet<ProjectWeight> ProjectWeights { get; set; }
        public DbSet<TivoGame> TivoGames { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<ActivityAttendee>(x => x.HasKey(aa => new { aa.AppUserId, aa.ActivityId }));

            builder.Entity<ActivityAttendee>()
                .HasOne(u => u.AppUser)
                .WithMany(u => u.Activities)
                .HasForeignKey(aa => aa.AppUserId);

            builder.Entity<ActivityAttendee>()
                .HasOne(u => u.Activity)
                .WithMany(u => u.Attendees)
                .HasForeignKey(aa => aa.ActivityId);

            builder.Entity<Seller>()
                .HasOne(p => p.Project)
                .WithMany(s => s.Sellers)
                .HasForeignKey(s => s.ProjectId)
                .OnDelete(DeleteBehavior.NoAction);
            
            // Relação de Seller com Sale, para converter SellerId em SellerName
            builder.Entity<Sale>()
                .HasOne(s => s.Seller)
                .WithMany(s => s.Sales)
                .HasForeignKey(s => s.SellerId)
                .OnDelete(DeleteBehavior.NoAction);

            // Relação de Product com Sale, para converter ProductId em ProductName
            builder.Entity<Sale>()
                .HasOne(p => p.Product)
                .WithMany(s => s.Sales)
                .HasForeignKey(p => p.ProductId)
                .OnDelete(DeleteBehavior.NoAction);
            
            // Relação de Project com Sale, para converter ProjectId em ProjectName
            builder.Entity<Sale>()
                .HasOne(p => p.Project)
                .WithMany(s => s.Sales)
                .HasForeignKey(p => p.ProjectId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<Comment>()
                .HasOne(a => a.Activity)
                .WithMany(c => c.Comments)
                .OnDelete(DeleteBehavior.Cascade);

            // Relação de Register com Events, de um para muitos
            builder.Entity<Register>()
                .HasOne(e => e.Events)
                .WithMany(r => r.Registers)
                .HasForeignKey(e => e.EventsId)
                .OnDelete(DeleteBehavior.NoAction);

            // Relação de Register com Seller, de um para muitos
            builder.Entity<Register>()
                .HasOne(s => s.Seller)
                .WithMany(r => r.Registers)
                .HasForeignKey(s => s.SellerId)
                .OnDelete(DeleteBehavior.NoAction);

            // Relação de Register com Bookmaker, de um para muitos
            builder.Entity<Register>()
                .HasOne(b => b.Bookmaker)
                .WithMany(r => r.Registers)
                .HasForeignKey(b => b.BookmakerId)
                .OnDelete(DeleteBehavior.NoAction);

            // Relação de Register com Project, de um para muitos
            builder.Entity<Register>()
                .HasOne(p => p.Project)
                .WithMany(r => r.Registers)
                .HasForeignKey(p => p.ProjectId)
                .OnDelete(DeleteBehavior.NoAction);

            // Relação SalesPerformanceTeam com Seller
            builder.Entity<SalesPerformanceTeam>()
                .HasOne(s => s.SPTSeller)
                .WithMany(s => s.SalesPerformanceTeams)
                .HasForeignKey(s => s.SPTSellerId)
                .OnDelete(DeleteBehavior.NoAction);
            
            // Relação SalesPerformanceTeam com Project
            builder.Entity<SalesPerformanceTeam>()
                .HasOne(p => p.SPTProject)
                .WithMany(s => s.SalesPerformanceTeams)
                .HasForeignKey(p => p.SPTProjectId)
                .OnDelete(DeleteBehavior.NoAction);
            
            // Relação SalesPerformanceTeam com Events
            builder.Entity<SalesPerformanceTeam>()
                .HasOne(e => e.SPTEvent)
                .WithMany(s => s.SalesPerformanceTeams)
                .HasForeignKey(e => e.SPTEventId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<UserFollowing>(b =>
            {
                b.HasKey(k => new { k.ObserverId, k.TargetId });

                b.HasOne(o => o.Observer)
                    .WithMany(f => f.Followings)
                    .HasForeignKey(o => o.ObserverId)
                    .OnDelete(DeleteBehavior.NoAction);
                b.HasOne(t => t.Target)
                    .WithMany(f => f.Followers)
                    .HasForeignKey(t => t.TargetId)
                    .OnDelete(DeleteBehavior.NoAction);
            });

            builder.Entity<Project>()
                .HasMany(p => p.ProjectWeights)
                .WithOne(pw => pw.Project)
                .HasForeignKey(pw => pw.ProjectId);
        }
    }
}