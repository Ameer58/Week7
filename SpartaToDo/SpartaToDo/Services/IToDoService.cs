using SpartaToDo.Models;

namespace SpartaToDo.Services
{
    public interface IToDoService
    {
        Task<List<Todo>> GetTodosAsync();
        Task<Todo> GetTodoByIdAsync(int? id);
        Task CreateTodoAsync(Todo todo);
        Task RemoveTodoAsync(Todo todo);
        void UpdateTodo(Todo todo);
        Task SaveTodoChangesAsync();

        bool ToDoExist(int id); 
    }
}
