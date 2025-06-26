using todolist_api.Dto;

namespace todolist_api.Interfaces
{
    public interface ITodoitemsService
    {
        Task<ToDoItemDto> AddToDoItemAsync(ToDoItemDto toDoItemDto, int userId);
        Task<ICollection<ToDoItemDto>> GetToDoItemsByUserIdAsync(int userId);
        Task<ICollection<ToDoItemDto>> GetToDoItemsByDateAsync(int userId, DateOnly date);
        Task<ToDoItemDto> GetToDoItemByIdAsync(int userId, int toDoItemId);
        Task<bool> DeleteToDoItemAsync(int userId, int toDoItemId);

    }
}