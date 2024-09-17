using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace DataAccess.Model
{
    public class GOStudyContext : DbContext
    {
        private readonly IConfiguration _configuration;

        public GOStudyContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Attendance> Attendances { get; set; }
        public DbSet<Specialization> Specializations { get; set; }
        public DbSet<Semester> Semesters { get; set; }
        public DbSet<Classroom> Classrooms { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Reaction> Reactions { get; set; }
        public DbSet<BlogPost> BlogPosts { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Bookmark> Bookmarks { get; set; }
        public DbSet<Ranking> Rankings { get; set; }
        public DbSet<Tasks> Tasks { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<PrivacySetting> PrivacySettings { get; set; }
        public DbSet<FriendRequest> FriendRequests { get; set; }
        public DbSet<EncryptionKey> EncryptionKeys { get; set; }
        public DbSet<SupportTicket> SupportTickets { get; set; }
        public DbSet<FAQ> FAQs { get; set; }
        public DbSet<ContactInfo> ContactInfos { get; set; }
        public DbSet<Analytic> Analytics { get; set; }
        public DbSet<Package> Packages { get; set; }
        public DbSet<PaymentTransaction> PaymentTransactions { get; set; }
        public virtual DbSet<RefreshToken> RefreshTokens { get; set; } = null!;
        public DbSet<UserSpecialization> UserSpecializations { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionString = _configuration.GetConnectionString("DefaultConnection");
            optionsBuilder.UseSqlServer(connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure currency precision for packages, payment transactions, and rankings
            modelBuilder.Entity<Package>()
                .Property(p => p.Price)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<PaymentTransaction>()
          .HasOne(p => p.User)
          .WithMany(u => u.PaymentTransactions)
          .HasForeignKey(p => p.UserId);

            modelBuilder.Entity<PaymentTransaction>()
                .HasOne(p => p.Package)
                .WithMany(pkg => pkg.PaymentTransactions)
                .HasForeignKey(p => p.PackageId);

            modelBuilder.Entity<PaymentTransaction>()
                .Property(pt => pt.Amount)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<Ranking>()
                .Property(r => r.PerformanceScore)
                .HasColumnType("decimal(18,2)");

            // FriendRequest relationships
            modelBuilder.Entity<FriendRequest>()
                .HasOne(fr => fr.Requester)
                .WithMany(u => u.SentFriendRequests)
                .HasForeignKey(fr => fr.RequesterId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<FriendRequest>()
                .HasOne(fr => fr.Recipient)
                .WithMany(u => u.ReceivedFriendRequests)
                .HasForeignKey(fr => fr.RecipientId)
                .OnDelete(DeleteBehavior.Restrict);

            // Message relationships (User-to-User)
            modelBuilder.Entity<Message>()
                .HasOne(m => m.Sender)
                .WithMany(u => u.MessagesSent)
                .HasForeignKey(m => m.SenderId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Message>()
                .HasOne(m => m.Recipient)
                .WithMany(u => u.MessagesReceived)
                .HasForeignKey(m => m.RecipientId)
                .OnDelete(DeleteBehavior.Restrict);

            // Bookmark relationships
            modelBuilder.Entity<Bookmark>()
                .HasOne(b => b.User)
                .WithMany(u => u.Bookmarks)
                .HasForeignKey(b => b.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Bookmark>()
                .HasOne(b => b.Post)
                .WithMany(p => p.Bookmarks)
                .HasForeignKey(b => b.PostId)
                .OnDelete(DeleteBehavior.Restrict);

            // Comment relationships
            modelBuilder.Entity<Comment>()
                .HasOne(c => c.Post)
                .WithMany(bp => bp.Comments)
                .HasForeignKey(c => c.PostId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Comment>()
                .HasOne(c => c.User)
                .WithMany(u => u.Comments)
                .HasForeignKey(c => c.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            // Notification relationships
            modelBuilder.Entity<Notification>()
                .HasOne(n => n.Task)
                .WithMany(t => t.Notifications)
                .HasForeignKey(n => n.TaskId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Notification>()
                .HasOne(n => n.User)
                .WithMany(u => u.Notifications)
                .HasForeignKey(n => n.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            // Analytic relationships
            modelBuilder.Entity<Analytic>()
                .HasOne(a => a.User)
                .WithMany(u => u.Analytics)
                .HasForeignKey(a => a.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Analytic>()
                .HasOne(a => a.Task)
                .WithMany(t => t.Analytics)
                .HasForeignKey(a => a.TaskId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Analytic>()
               .HasOne(a => a.Classroom)
               .WithMany(c => c.Analytics)
               .HasForeignKey(a => a.ClassroomId)
               .OnDelete(DeleteBehavior.NoAction);

            // UserSpecialization relationships
            modelBuilder.Entity<UserSpecialization>()
               .HasKey(us => us.UserSpecializationId);

            modelBuilder.Entity<UserSpecialization>()
                .HasOne(us => us.User)
                .WithMany(u => u.UserSpecializations)
                .HasForeignKey(us => us.UserId);

            modelBuilder.Entity<UserSpecialization>()
                .HasOne(us => us.Specialization)
                .WithMany(s => s.UserSpecializations)
                .HasForeignKey(us => us.SpecializationId);

            // RefreshToken relationships
            modelBuilder.Entity<RefreshToken>(entity =>
            {
                entity.ToTable("RefreshToken");

                entity.HasIndex(e => e.UserId, "IX_RefreshToken_UserId");

                entity.Property(e => e.RefreshTokenId).ValueGeneratedNever();

                entity.Property(e => e.ExpriedAt).HasColumnType("datetime");

                entity.Property(e => e.IssuedAt).HasColumnType("datetime");

                entity.Property(e => e.JwtId)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Token)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.RefreshTokens)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_RefreshToken_Users");
            });
            modelBuilder.Entity<Specialization>().HasData(
          new Specialization { SpecializationId = -1, Name = "English" },
          new Specialization { SpecializationId = -2, Name = "Chinese" },
          new Specialization { SpecializationId = -3, Name = "Japanese" },
          new Specialization { SpecializationId = -4, Name = "Economics and Management" },
          new Specialization { SpecializationId = -5, Name = "Marketing" },
          new Specialization { SpecializationId = -6, Name = "Media and Journalism" },
          new Specialization { SpecializationId = -7, Name = "Design" },
          new Specialization { SpecializationId = -8, Name = "Science and Technology" },
          new Specialization { SpecializationId = -9, Name = "Industry and Construction" },
          new Specialization { SpecializationId = -10, Name = "General Subjects" }
      );

            // Seed data cho Classroom
            modelBuilder.Entity<Classroom>().HasData(
                new Classroom { ClassroomId = -1, Name = "Room 101", SpecializationId = -1, Nickname = "Eng101", CreatedAt = DateTime.UtcNow },
                new Classroom { ClassroomId = -2, Name = "Room 102", SpecializationId = -2, Nickname = "Chi102", CreatedAt = DateTime.UtcNow },
                new Classroom { ClassroomId = -3, Name = "Room 103", SpecializationId = -3, Nickname = "Jap103", CreatedAt = DateTime.UtcNow },
                new Classroom { ClassroomId = -4, Name = "Room 104", SpecializationId = -4, Nickname = "EconMgmt104", CreatedAt = DateTime.UtcNow },
                new Classroom { ClassroomId = -5, Name = "Room 105", SpecializationId = -5, Nickname = "Mkt105", CreatedAt = DateTime.UtcNow },
                new Classroom { ClassroomId = -6, Name = "Room 106", SpecializationId = -6, Nickname = "MediaJourn106", CreatedAt = DateTime.UtcNow },
                new Classroom { ClassroomId = -7, Name = "Room 107", SpecializationId = -7, Nickname = "Design107", CreatedAt = DateTime.UtcNow },
                new Classroom { ClassroomId = -8, Name = "Room 108", SpecializationId = -8, Nickname = "SciTech108", CreatedAt = DateTime.UtcNow },
                new Classroom { ClassroomId = -9, Name = "Room 109", SpecializationId = -9, Nickname = "IndConst109", CreatedAt = DateTime.UtcNow },
                new Classroom { ClassroomId = -10, Name = "Room 110", SpecializationId = -10, Nickname = "GenSub110", CreatedAt = DateTime.UtcNow }
            );
            base.OnModelCreating(modelBuilder);
        }
    }
}
