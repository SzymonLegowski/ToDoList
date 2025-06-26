using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace todolist_api.Model;

public class ToDoItem
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public required string Title { get; set; }
    public required string Description { get; set; }
    public DateOnly Date { get; set; }
    public User User { get; set; } = null!; 
}
