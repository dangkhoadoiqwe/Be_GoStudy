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
        public DbSet<BlogImg> BlogImgs { get; set; }
        public DbSet<Feature> Features { get; set; }
        public DbSet<UserLike> UserLikes { get; set; }

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

            modelBuilder.Entity<UserLike>()
             .HasOne(ul => ul.User)
             .WithMany(u => u.UserLikes)
             .HasForeignKey(ul => ul.UserId)
             .OnDelete(DeleteBehavior.NoAction); // Keep data intact when user is deleted

            modelBuilder.Entity<Package>()
          .HasMany(p => p.Feature)
          .WithOne(f => f.Package)
          .HasForeignKey(f => f.PackageId)
          .OnDelete(DeleteBehavior.Cascade); // Remove features when package is removed

            // Foreign key for BlogId (cascade delete allowed)
            modelBuilder.Entity<UserLike>()
                .HasOne(ul => ul.BlogPost)
                .WithMany(bp => bp.UserLikes)
                .HasForeignKey(ul => ul.BlogId)
                .OnDelete(DeleteBehavior.Cascade); // Remove likes when blog post is removed

            modelBuilder.Entity<BlogImg>()
           .HasOne(bi => bi.BlogPost)
           .WithMany(bp => bp.BlogImgs)
           .HasForeignKey(bi => bi.BlogId)
           .OnDelete(DeleteBehavior.Cascade); // Remove images when blog post is removed

            // FriendRequest relationships
            modelBuilder.Entity<FriendRequest>()
                .HasOne(fr => fr.Requester)
                .WithMany(u => u.SentFriendRequests)
                .HasForeignKey(fr => fr.RequesterId)
                .OnDelete(DeleteBehavior.Restrict); // Keep requester information intact

            modelBuilder.Entity<FriendRequest>()
                .HasOne(fr => fr.Recipient)
                .WithMany(u => u.ReceivedFriendRequests)
                .HasForeignKey(fr => fr.RecipientId)
                .OnDelete(DeleteBehavior.Restrict); // Keep recipient information intact

            // Message relationships (User-to-User)
            modelBuilder.Entity<Message>()
                .HasOne(m => m.Sender)
                .WithMany(u => u.MessagesSent)
                .HasForeignKey(m => m.SenderId)
                .OnDelete(DeleteBehavior.Restrict); // Keep sender information intact

            modelBuilder.Entity<Message>()
                .HasOne(m => m.Recipient)
                .WithMany(u => u.MessagesReceived)
                .HasForeignKey(m => m.RecipientId)
                .OnDelete(DeleteBehavior.Restrict); // Keep recipient information intact

            // Bookmark relationships
            modelBuilder.Entity<Bookmark>()
                .HasOne(b => b.User)
                .WithMany(u => u.Bookmarks)
                .HasForeignKey(b => b.UserId)
                .OnDelete(DeleteBehavior.Restrict); // Keep user bookmarks intact

            modelBuilder.Entity<Bookmark>()
                .HasOne(b => b.Post)
                .WithMany(p => p.Bookmarks)
                .HasForeignKey(b => b.PostId)
                .OnDelete(DeleteBehavior.Restrict); // Keep post bookmarks intact

            // Comment relationships
            modelBuilder.Entity<Comment>()
                .HasOne(c => c.Post)
                .WithMany(bp => bp.Comments)
                .HasForeignKey(c => c.PostId)
                .OnDelete(DeleteBehavior.NoAction); // Keep comments intact

            modelBuilder.Entity<Comment>()
                .HasOne(c => c.User)
                .WithMany(u => u.Comments)
                .HasForeignKey(c => c.UserId)
                .OnDelete(DeleteBehavior.NoAction); // Keep user comments intact

            // Notification relationships
            modelBuilder.Entity<Notification>()
                .HasOne(n => n.Task)
                .WithMany(t => t.Notifications)
                .HasForeignKey(n => n.TaskId)
                .OnDelete(DeleteBehavior.Restrict); // Keep task notifications intact

            modelBuilder.Entity<Notification>()
                .HasOne(n => n.User)
                .WithMany(u => u.Notifications)
                .HasForeignKey(n => n.UserId)
                .OnDelete(DeleteBehavior.Restrict); // Keep user notifications intact

            // Analytic relationships
            modelBuilder.Entity<Analytic>()
                .HasOne(a => a.User)
                .WithMany(u => u.Analytics)
                .HasForeignKey(a => a.UserId)
                .OnDelete(DeleteBehavior.Cascade); // Remove analytics when user is removed

            modelBuilder.Entity<Analytic>()
                .HasOne(a => a.Task)
                .WithMany(t => t.Analytics)
                .HasForeignKey(a => a.TaskId)
                .OnDelete(DeleteBehavior.NoAction); // Keep task analytics intact

            modelBuilder.Entity<Analytic>()
               .HasOne(a => a.Classroom)
               .WithMany(c => c.Analytics)
               .HasForeignKey(a => a.ClassroomId)
               .OnDelete(DeleteBehavior.NoAction); // Keep classroom analytics intact

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

            // Seed data for Specialization
            modelBuilder.Entity<Specialization>().HasData(
                new Specialization { SpecializationId = 1, Name = "English" },
                new Specialization { SpecializationId = 2, Name = "Chinese" },
                new Specialization { SpecializationId = 3, Name = "Japanese" },
                new Specialization { SpecializationId = 4, Name = "Economics and Management" },
                new Specialization { SpecializationId = 5, Name = "Marketing" },
                new Specialization { SpecializationId = 6, Name = "Media and Journalism" },
                new Specialization { SpecializationId = 7, Name = "Design" },
                new Specialization { SpecializationId = 8, Name = "Science and Technology" },
                new Specialization { SpecializationId = 9, Name = "Industry and Construction" },
                new Specialization { SpecializationId = 10, Name = "General Subjects" }
            );

            // Seed data for Classroom
            modelBuilder.Entity<Classroom>().HasData(
                new Classroom { ClassroomId = 1, Name = "Room 101", SpecializationId = 1, Nickname = "Eng101", LinkUrl = "http://example.com/eng101", CreatedAt = DateTime.UtcNow, status = 1 },
                new Classroom { ClassroomId = 2, Name = "Room 102", SpecializationId = 2, Nickname = "Chi102", LinkUrl = "http://example.com/chi102", CreatedAt = DateTime.UtcNow, status = 1 },
                new Classroom { ClassroomId = 3, Name = "Room 103", SpecializationId = 3, Nickname = "Jap103", LinkUrl = "http://example.com/jap103", CreatedAt = DateTime.UtcNow, status = 1 },
                new Classroom { ClassroomId = 4, Name = "Room 104", SpecializationId = 4, Nickname = "EconMgmt104", LinkUrl = "http://example.com/econmgmt104", CreatedAt = DateTime.UtcNow, status = 1 },
                new Classroom { ClassroomId = 5, Name = "Room 105", SpecializationId = 5, Nickname = "Mkt105", LinkUrl = "http://example.com/mkt105", CreatedAt = DateTime.UtcNow, status = 1 },
                new Classroom { ClassroomId = 6, Name = "Room 106", SpecializationId = 6, Nickname = "MediaJourn106", LinkUrl = "http://example.com/mediajourn106", CreatedAt = DateTime.UtcNow, status = 1 },
                new Classroom { ClassroomId = 7, Name = "Room 107", SpecializationId = 7, Nickname = "Design107", LinkUrl = "http://example.com/design107", CreatedAt = DateTime.UtcNow, status = 1 },
                new Classroom { ClassroomId = 8, Name = "Room 108", SpecializationId = 8, Nickname = "SciTech108", LinkUrl = "http://example.com/scitech108", CreatedAt = DateTime.UtcNow, status = 1 },
                new Classroom { ClassroomId = 9, Name = "Room 109", SpecializationId = 9, Nickname = "IndConst109", LinkUrl = "http://example.com/indconst109", CreatedAt = DateTime.UtcNow, status = 1 },
                new Classroom { ClassroomId = 10, Name = "Room 110", SpecializationId = 10, Nickname = "GenSub110", LinkUrl = "http://example.com/gensub110", CreatedAt = DateTime.UtcNow, status = 1 }
            );

            // Seed data for PrivacySettings
            modelBuilder.Entity<PrivacySetting>().HasData(
                new PrivacySetting { PrivacySettingId = 1, Visibility = "Public" },
                new PrivacySetting { PrivacySettingId = 2, Visibility = "Friends Only" },
                new PrivacySetting { PrivacySettingId = 3, Visibility = "Private" }
            );

            // Seed data for Semesters
            modelBuilder.Entity<Semester>().HasData(
                new Semester { SemesterId = 1, Name = "Fall 2024" },
                new Semester { SemesterId = 2, Name = "Spring 2025" },
                new Semester { SemesterId = 3, Name = "Summer 2025" }
            );

            // Seed data for Packages
            modelBuilder.Entity<Package>().HasData(
                new Package { PackageId = 1, Name = "Free", Price = 0.00M },
                new Package { PackageId = 2, Name = "Plus", Price = 39000.00M },
                new Package { PackageId = 3, Name = "Premium", Price = 59000.00M }
            );

            // Seed data for Features
            modelBuilder.Entity<Feature>().HasData(
                new Feature { FeatureId = 4, Name = "Shows a total of 2 rooms corresponding to 2 subjects", PackageId = 1 },
                new Feature { FeatureId = 5, Name = "Join the room of 2 subjects, after 3 days of use, you have the right to reset the room ~ subject", PackageId = 1 },
                new Feature { FeatureId = 6, Name = "Provide symbolic times to be able to enhance the study schedule for subjects", PackageId = 1 },
                new Feature { FeatureId = 7, Name = "Posts in the community are archived, but there are limits", PackageId = 1 },
                new Feature { FeatureId = 8, Name = "Chat and exchange with friends in your community", PackageId = 1 },
                new Feature { FeatureId = 9, Name = "Graded learning ability by week and by semester", PackageId = 1 },
                new Feature { FeatureId = 10, Name = "Unlock 4 rooms corresponding to 4 subjects", PackageId = 2 },
                new Feature { FeatureId = 11, Name = "Join the room of 4 subjects, after 1 day of use, you have the right to reset the room ~ subject", PackageId = 2 },
                new Feature { FeatureId = 12, Name = "Provide a timetable to be able to schedule classes for subjects", PackageId = 2 },
                new Feature { FeatureId = 13, Name = "The calendar will pop up in the room to fill in the next lesson", PackageId = 2 },
                new Feature { FeatureId = 14, Name = "Store posts in a comfortable community", PackageId = 2 },
                new Feature { FeatureId = 15, Name = "Chat and exchange with friends in your community", PackageId = 2 },
                new Feature { FeatureId = 16, Name = "Evaluated and ranked learning productivity by day, week and semester", PackageId = 2 },
                new Feature { FeatureId = 17, Name = "Light/Dark interface of Study Room", PackageId = 2 },
                new Feature { FeatureId = 18, Name = "Unlock 6 rooms corresponding to 6 subjects", PackageId = 3 },
                new Feature { FeatureId = 19, Name = "Join the room of 6 subjects, after 2 hours of use, you have the right to reset the room ~ subject", PackageId = 3 },
                new Feature { FeatureId = 20, Name = "Provide a timetable to be able to schedule classes for subjects", PackageId = 3 },
                new Feature { FeatureId = 21, Name = "Take notes and save them during the learning process", PackageId = 3 },
                new Feature { FeatureId = 22, Name = "Store posts in a comfortable community", PackageId = 3 },
                new Feature { FeatureId = 23, Name = "The calendar will pop up in the room to fill in the next lesson", PackageId = 3 },
                new Feature { FeatureId = 24, Name = "Chat and exchange with friends in your community", PackageId = 3 },
                new Feature { FeatureId = 25, Name = "Evaluated and ranked learning productivity by day, week and semester", PackageId = 3 },
                new Feature { FeatureId = 26, Name = "Light/Dark interface of Study Room", PackageId = 3 },
                new Feature { FeatureId = 27, Name = "Exclusive 30-day Premium avatar frame interface helps you stand out", PackageId = 3 }
            );

            base.OnModelCreating(modelBuilder);
        }
    }
}
