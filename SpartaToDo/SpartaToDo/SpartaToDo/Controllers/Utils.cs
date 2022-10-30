using SpartaToDo.Models.ViewModels;
using SpartaToDo.Models;

namespace SpartaToDo.Controllers
{
    public static class Utils
    {
        public static ToDoViewModel ToDoModelToToDoViewModel(ToDo todo) =>
            new ToDoViewModel
            {
                Id = todo.Id,
                Title = todo.Title,
                Description = todo.Description,
                Complete = todo.Complete,
                SpartanId = todo.SpartanId
            };
    }
}
