using SpartaToDo.Models;
using SpartaToDo.Models.ViewModels;

namespace SpartaToDo.Controllers
{
    public static class Utils
    {
        public static ToDoViewModel ToDoToToDoViewModel(Todo todo) =>
            new ToDoViewModel
            {
                Id = todo.Id,
                Title = todo.Title,
                Description = todo.Description,
                Complete = todo.Complete
            };
    }
}
