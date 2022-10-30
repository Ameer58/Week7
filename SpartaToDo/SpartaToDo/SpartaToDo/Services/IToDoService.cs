using SpartaToDo.Models;

namespace SpartaToDo.Services
{
    public interface IToDoService
    {
        public Task<List<ToDo>> GetToDoItems();
        public Task<ToDo> GetToDoItemByIdAsync(int? id);
        public Task RemoveToDoItemAsync(ToDo todo);
        public Task SaveChanges();
        public bool ToDoItemExists(int id);
        public Task CreateToDoItemAsync(ToDo ToDos);
        public Task<List<ToDo>> GetToDoItemsByUserIdAsync(string id);
    }
}
