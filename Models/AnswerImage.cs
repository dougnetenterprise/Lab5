using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lab5.Models
{
    public enum Question
    {
        Earth, Computer
    }
    public class AnswerImage
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int AnswerImageId { get; set; }

        [Required]
        [StringLength(50)]
        [Column("FirstName")]
        [Display(Name = "File Name")]
        public string FileName { get; set; }

        [Required]
        [Display(Name = "URL")]
        [Column("URL")]
        public string Url { get; set; }

        [Required]
        [Column("Question")]
        public Question Question { get; set; }
    }
}
