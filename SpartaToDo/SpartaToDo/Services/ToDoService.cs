using Microsoft.EntityFrameworkCore;
using SpartaToDo.Data;
using SpartaToDo.Models;

namespace SpartaToDo.Services
{
    public class ToDoService : IToDoService
    {
        private readonly SpartaToDoContext _context;

        public ToDoService(SpartaToDoContext context)
        {
            _context = context;
        }

        public async Task CreateTodoAsync(Todo todo)
        {
            await _context.AddAsync(todo);
        }

        public async Task<Todo> GetTodoByIdAsync(int? id)
        {
            return await _context.ToDos
                .FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task<List<Todo>> GetTodosAsync()
        {
            return await _context.ToDos.ToListAsync();
        }

        public async Task RemoveTodoAsync(Todo toDo)
        {
           _context.ToDos.Remove(toDo);
            await _context.SaveChangesAsync();
        }

        public async Task SaveTodoChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void UpdateTodo(Todo todo)
        {
             _context.Update(todo);
        }

        public bool ToDoExist(int id)
        {
            return _context.ToDos.Any(e => e.Id == id);
        }
    }
}
