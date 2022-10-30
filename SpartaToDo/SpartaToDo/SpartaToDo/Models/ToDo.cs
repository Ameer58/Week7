using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SpartaToDo.Models
{
    public class ToDo
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string Title { get; set; } = null!;
        public string? Description { get; set; }
        [Display(Name = "Complete ?")]
        public bool Complete { get; set; }
        [DataType(DataType.Date)]
        [Display(Name = "Created")]
        public DateTime DateCreated { get; init; } = DateTime.Now;
        [MaxLength(450)]
        [ForeignKey("Spartan")]
        public string SpartanId { get; set; }
        public Spartan Spartan { get; set; }
    }
}
