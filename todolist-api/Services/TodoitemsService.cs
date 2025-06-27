using Microsoft.EntityFrameworkCore;
using todolist_api.Data;
using todolist_api.Dto;
using todolist_api.Interfaces;
using todolist_api.Model;

namespace todolist_api.Services
{
    public class TodoitemsService(DataContext context) : ITodoitemsService
    {
        private readonly DataContext _context = context;

        private ToDoItemDto MapToDto(ToDoItem toDoItem)
        {
            return new ToDoItemDto
            {
                Id = toDoItem.Id,
                Title = toDoItem.Title,
                Description = toDoItem.Description,
                Date = toDoItem.Date,
                UserId = toDoItem.User.Id
            };
        }
        private async Task<ToDoItem> MapToModelAsync(ToDoItemDto toDoItemDto, int userId)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId)
                ?? throw new ArgumentException("Nie znaleziono użytkownika");
            return new ToDoItem
            {
                Id = toDoItemDto.Id,
                Title = toDoItemDto.Title,
                Description = toDoItemDto.Description,
                Date = toDoItemDto.Date,
                User = user
            };
        }
        public async Task<ToDoItemDto> AddToDoItemAsync(ToDoItemDto toDoItemDto, int userId)
        {
            var toDoItem = await MapToModelAsync(toDoItemDto, userId);
            await _context.ToDoItems.AddAsync(toDoItem);
            await _context.SaveChangesAsync();

            return MapToDto(toDoItem);   
        }
        public async Task<ToDoItemDto> GetToDoItemByIdAsync(int userId, int toDoItemId)
        {
            var toDoItem = await _context.ToDoItems
                .Include(t => t.User)
                .FirstOrDefaultAsync(t => t.Id == toDoItemId && t.User.Id == userId) ??
                throw new ArgumentException("Nie znaleziono zadania do wykonania");
            return MapToDto(toDoItem);
        }
        public async Task<ICollection<ToDoItemDto>> GetToDoItemsByUserIdAsync(int userId)
        {
            var toDoItems = await _context.ToDoItems
                .Include(t => t.User)
                .Where(t => t.User.Id == userId)
                .ToListAsync();

            return [.. toDoItems.Select(MapToDto)];
        }
        public async Task<ICollection<ToDoItemDto>> GetToDoItemsByDateAsync(int userId, DateOnly date)
        {
            var toDoItems = await _context.ToDoItems
                .Include(t => t.User)
                .Where(t => t.User.Id == userId && t.Date == date)
                .ToListAsync();

            return [.. toDoItems.Select(MapToDto)];
        }

       public async Task<ToDoItemDto> UpdateToDoItemAsync(int userId, ToDoItemDto toDoItemDto)
        {
            var existingToDoItem = await _context.ToDoItems
                .Include(t => t.User)
                .FirstOrDefaultAsync(t => t.Id == toDoItemDto.Id && t.User.Id == userId)
                ?? throw new ArgumentException("Nie znaleziono zadania do wykonania");

            existingToDoItem.Title = toDoItemDto.Title;
            existingToDoItem.Description = toDoItemDto.Description;
            existingToDoItem.Date = toDoItemDto.Date;

            await _context.SaveChangesAsync();

            return MapToDto(existingToDoItem);
        }

        public async Task<bool> DeleteToDoItemAsync(int userId, int toDoItemId)
        {
            var toDoItem = await _context.ToDoItems
                .Include(t => t.User)
                .FirstOrDefaultAsync(t => t.Id == toDoItemId && t.User.Id == userId)
                ?? throw new ArgumentException("Nie znaleziono zadania do wykonania");
            _context.ToDoItems.Remove(toDoItem);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}