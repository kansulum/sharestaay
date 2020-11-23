
using Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class RoomContext : DbContext
    {
        public RoomContext(DbContextOptions<RoomContext> options) : base(options)
        {
        }

        public DbSet<Room> Rooms { get; set; }
        public DbSet<Rule> Rules { get; set; }
        public DbSet<Amenity> Amenities { get; set; }
        public DbSet<AgeBracket> AgeBrackets { get; set; }
        public DbSet<Gender> Genders { get; set; }
        public DbSet<ChatMessage> ChatMessages { get; set; }
        public DbSet<FriendMapping> FriendMappings { get; set; }
        public DbSet<OnlineUser> OnlineUsers { get; set; }
        public DbSet<Favourite> Favourites { get; set; }

        public DbSet<UserNotification> UserNotifications { get; set; }

        public DbSet<Profile> Profiles { get; set; }
        
         protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<RoomAmenities>().HasKey(ra => new { ra.RoomId, ra.AmenitiesId });
            modelBuilder.Entity<RoomRule>().HasKey(rr => new { rr.RoomId, rr.RuleId });
            modelBuilder.Entity<RoommateAgeBracket>().HasKey(rpa => new { rpa.PreferedAgeId, rpa.RoomId });
            modelBuilder.Entity<RoomGender>().HasKey(rpa => new { rpa.PreferedGenderId, rpa.RoomId });
            modelBuilder.Entity<Favourite>().HasKey(fa => new { fa.AppUserId, fa.RoomId });
            base.OnModelCreating(modelBuilder);
        }
    }
}
    
