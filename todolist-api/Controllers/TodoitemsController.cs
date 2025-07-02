using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using todolist_api.Dto;
using todolist_api.Interfaces;

namespace todolist_api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TodoitemsController(ITodoitemsService todoitemsService) : ControllerBase
    {
        private readonly ITodoitemsService _todoitemsService = todoitemsService;

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AddTodoitem([FromBody] ToDoItemDto toDoItemDto)
        {
            if (toDoItemDto == null)
            {
                return BadRequest("Nieprawidłowe dane wejściowe.");
            }

            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userIdClaim == null || !int.TryParse(userIdClaim, out int userId))
            {
                return Unauthorized("Brak identyfikatora użytkownika.");
            }
            var createdItem = await _todoitemsService.AddToDoItemAsync(toDoItemDto, userId);
            if (createdItem == null)
            {
                return StatusCode(500, "Błąd podczas tworzenia zadania.");
            }

            return Ok(createdItem);
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetTodoitemsByUserId()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out int userId))
            {
                return Unauthorized("Brak identyfikatora użytkownika.");
            }

            var todoitems = await _todoitemsService.GetToDoItemsByUserIdAsync(userId);
            return Ok(todoitems);
        }

        [Authorize]
        [HttpGet("by-date")]
        public async Task<IActionResult> GetTodoitemsByDate([FromQuery] string date)
        {
            if (!DateOnly.TryParse(date, out var parsedDate))
                return BadRequest("Niepoprawny format daty (użyj YYYY-MM-DD)");

            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out int userId))
            {
                return Unauthorized("Brak identyfikatora użytkownika.");
            }

            var todoitems = await _todoitemsService.GetToDoItemsByDateAsync(userId, parsedDate);
            return Ok(todoitems);
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTodoitemById(int id)
        {
            if (!int.TryParse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value, out int userId))
            {
                return Unauthorized("Brak identyfikatora użytkownika.");
            }
            try
            {
                var todoitem = await _todoitemsService.GetToDoItemByIdAsync(userId, id);
                return Ok(todoitem);
            }
            catch (ArgumentException)
            {
                return NotFound("Zadanie o podanym identyfikatorze nie zostało znalezione.");
            }

        }

        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTodoitem(int id, [FromBody] ToDoItemDto toDoItemDto)
        {
            if (toDoItemDto == null || toDoItemDto.Id != id)
            {
                return BadRequest("Nieprawidłowe dane wejściowe.");
            }

            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userIdClaim == null || !int.TryParse(userIdClaim, out int userId))
            {
                return Unauthorized("Brak identyfikatora użytkownika.");
            }

            var updatedItem = await _todoitemsService.UpdateToDoItemAsync(userId, toDoItemDto);
            if (updatedItem == null)
            {
                return StatusCode(500, "Błąd podczas aktualizacji zadania.");
            }

            return Ok(updatedItem);
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTodoitem(int id)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (!int.TryParse(userIdClaim, out int userId))
            {
                return Unauthorized("Brak identyfikatora użytkownika.");
            }
            try
            {
                var result = await _todoitemsService.DeleteToDoItemAsync(userId, id);
                return result ? NoContent() : NotFound("Zadanie o podanym identyfikatorze nie zostało znalezione.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Błąd serwera: {ex.Message}");
            }
        }
    }
}