﻿using System.ComponentModel.DataAnnotations;

namespace SeuGilbertoBot.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required, MaxLength(50)]
        public string Username { get; set; }

        [MaxLength(100)]
        public string FirstName { get; set; }

        [MaxLength(100)]
        public string LastName { get; set; }

        public long TelegramUserId { get; set; }
        public ICollection<UserRoundScore> UserRoundScores { get; set; } = new List<UserRoundScore>();

    }
}
