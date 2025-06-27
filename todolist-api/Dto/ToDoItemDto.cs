namespace todolist_api.Dto
{
    public class ToDoItemDto
    {
        public int Id { get; set; }
        public required string Title { get; set; }
        public required string Description { get; set; }
        public DateOnly Date { get; set; }
        public int UserId { get; set; } 
    }
}