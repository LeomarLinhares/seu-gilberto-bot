using System;
using System.ComponentModel.DataAnnotations;

namespace SeuGilbertoBot.Models
{
    public class Season
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int Year { get; set; }

        [Required]
        public DateTime StartDate { get; set; }
        public ICollection<Round> Rounds { get; set; } = new List<Round>();
    }
}
