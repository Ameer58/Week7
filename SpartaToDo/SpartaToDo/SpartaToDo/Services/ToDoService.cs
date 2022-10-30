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
        public async Task<List<ToDo>> GetToDoItems()
        {
            return await _context.ToDos.ToListAsync();
        }
        public async Task<ToDo?> GetToDoItemByIdAsync(int? id)
        {
            return await _context.ToDos.FindAsync(id);
        }
        public async Task RemoveToDoItemAsync(ToDo todo)
        {
            _context.Remove(todo);
            await _context.SaveChangesAsync();
        }
        public async Task SaveChanges()
        {
            await _context.SaveChangesAsync();
        }
        public bool ToDoItemExists(int id)
        {
            return (_context.ToDos?.Any(e => e.Id == id)).GetValueOrDefault();
        }
        public async Task CreateToDoItemAsync(ToDo ToDo)
        {
            await _context.AddAsync(ToDo);
            await _context.SaveChangesAsync();
        }

        public async Task<List<ToDo>> GetToDoItemsByUserIdAsync(string id)
        {
            return await _context.ToDos.Where(u => u.SpartanId == id).ToListAsync();
        }
    }
}
