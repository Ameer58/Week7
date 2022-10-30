using System.ComponentModel.DataAnnotations;

namespace SpartaToDo.Models.ViewModels
{
    public class ToDoViewModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Title { get; set; } = null!;
        public string? Description { get; set; }
        [Display(Name = "Complete ?")]
        public bool Complete { get; set; }
        [DataType(DataType.Date)]
        [Display(Name = "Created")]
        public DateTime DateCreated { get; } = DateTime.Now;
        public string SpartanId { get; set; } = "";
    }
}
