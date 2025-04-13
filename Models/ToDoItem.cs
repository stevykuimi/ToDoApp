using System.ComponentModel.DataAnnotations;
using System;

namespace TodoApp.Models
{
    public class TodoItem
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        [MaxLength(100)]
        public required string Title { get; set; }

        [MaxLength(1000)]
        public string? Description { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Due Date")]
        public DateTime DueDate { get; set; }

        public bool IsCompleted { get; set; } = false;
    }
}
