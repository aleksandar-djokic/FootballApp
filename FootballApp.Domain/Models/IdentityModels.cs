using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.Infrastructure.Annotations;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace FootballApp.Domain.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        [Display(Name = "ProfilePicture")]
        public byte[] ProfilePicture { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<PrivateMessage> PrivateMessages { get; set; }
        public DbSet<Conversation> Conversations { get; set; }
        public DbSet<TeamChatMessage> TeamMessages { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<TeamMembers> TeamMembers { get; set; }
        public DbSet<TeamRole> TeamRoles { get; set; }
        public DbSet<TeamInvite> TeamInvites { get; set; }
        public DbSet<Friendship> Friendships { get; set; }
        public DbSet<FriendshipRequest> FriendRequests { get; set; }
        public DbSet<TeamJoinRequests> TeamJoinRequests { get; set; }
        public DbSet<FreeAgentProfile> FreeAgents { get; set; }
        public DbSet<Match> Matches { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder
                .Entity<Team>()
                .Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnAnnotation(
                    IndexAnnotation.AnnotationName,
                    new IndexAnnotation(
                        new IndexAttribute("TeamName") { IsUnique=true}
                        ));
            modelBuilder
                .Entity<TeamRole>()
                .Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(60)
                .HasColumnAnnotation(
                    IndexAnnotation.AnnotationName,
                    new IndexAnnotation(
                        new IndexAttribute("RoleName_TeamId", 1) { IsUnique = true }));
            modelBuilder
                .Entity<TeamRole>()
                .Property(t => t.TeamId)
                .IsRequired()
                .HasColumnAnnotation(
                    IndexAnnotation.AnnotationName,
                    new IndexAnnotation(
                        new IndexAttribute("RoleName_TeamId", 2) { IsUnique = true }));
            modelBuilder
                .Entity<Friendship>().HasKey(f => new { f.User1Id, f.User2Id });
            modelBuilder
                .Entity<Friendship>()
                .HasRequired(f => f.User1)
                .WithMany()
                .HasForeignKey(f => f.User1Id);
            modelBuilder
                .Entity<Friendship>()
                .HasRequired(f => f.User2)
                .WithMany()
                .HasForeignKey(f => f.User2Id)
                .WillCascadeOnDelete(false);
           
        }


    }
}