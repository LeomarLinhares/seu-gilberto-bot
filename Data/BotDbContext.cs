using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SeuGilbertoBot.Models;
using System;

namespace SeuGilbertoBot.Data
{
    public class BotDbContext : DbContext
    {
        public BotDbContext(DbContextOptions<BotDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Season> Seasons { get; set; }
        public DbSet<Round> Rounds { get; set; }
        public DbSet<UserRoundScore> UserRoundScores { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlite("Data Source=CartolaDosBro.db");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // 🔹 Configuração para converter a data automaticamente
            var dateConverter = new ValueConverter<DateTime, string>(
                v => v.ToString("yyyyMMdd"),  // 🔹 Salvar no banco como string "yyyyMMdd"
                v => DateTime.ParseExact(v, "yyyyMMdd", null)  // 🔹 Converter string para DateTime ao carregar
            );

            modelBuilder.Entity<Season>()
                .Property(s => s.StartDate)
                .HasConversion(dateConverter);

            // Relacionamento entre UserRoundScore e User
            modelBuilder.Entity<UserRoundScore>()
                .HasOne(urs => urs.User)
                .WithMany(u => u.UserRoundScores) // User tem vários UserRoundScores
                .HasForeignKey(urs => urs.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // Relacionamento entre UserRoundScore e Round
            modelBuilder.Entity<UserRoundScore>()
                .HasOne(urs => urs.Round)
                .WithMany(r => r.UserRoundScores) // Round tem vários UserRoundScores
                .HasForeignKey(urs => urs.RoundId)
                .OnDelete(DeleteBehavior.Cascade);

            // Relacionamento entre Round e Season
            modelBuilder.Entity<Round>()
                .HasOne(r => r.Season)
                .WithMany(s => s.Rounds) // Season tem vários Rounds
                .HasForeignKey(r => r.SeasonId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
