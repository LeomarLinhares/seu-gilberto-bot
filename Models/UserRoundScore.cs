﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SeuGilbertoBot.Models
{
    public class UserRoundScore
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }

        [ForeignKey("Round")]
        public int RoundId { get; set; }

        public decimal Score { get; set; }

        public User User { get; set; }
        public Round Round { get; set; }
    }
}
