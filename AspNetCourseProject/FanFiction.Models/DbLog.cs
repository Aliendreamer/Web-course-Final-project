namespace FanFiction.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.Data;

    public class DbLog
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string LogType { get; set; }

        [Required]
        public string Content { get; set; }

        public bool Handled { get; set; }
    }
}