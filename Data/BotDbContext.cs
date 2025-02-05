using Microsoft.EntityFrameworkCore;
using SeuGilbertoBot.Models;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace SeuGilbertoBot.Data
{
    public class BotDbContext : DbContext
    {
        public BotDbContext(DbContextOptions<BotDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Season> Seasons { get; set; }
        public DbSet<Round> Rounds { get; set; }
        public DbSet<UserRoundScore> UserRoundScores { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserRoundScore>()
                .HasOne(urs => urs.User)
                .WithMany()
                .HasForeignKey(urs => urs.UserId);

            modelBuilder.Entity<UserRoundScore>()
                .HasOne(urs => urs.Round)
                .WithMany()
                .HasForeignKey(urs => urs.RoundId);
        }
    }
}
