using System;
using System.Collections.Generic;
using System.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SpartaToDo.Data;
using SpartaToDo.Models;
using SpartaToDo.Models.ViewModels;
using SpartaToDo.Services;

namespace SpartaToDo.Controllers
{
    public class ToDoController : Controller
    {
        private IToDoService _service;

        public ToDoController(IToDoService service)
        {
            _service = service;
        }

        // GET: ToDo
        public async Task<IActionResult> Index()
        {
            var toDos = await _service.GetTodosAsync();
            var toDosViewModels = new List<ToDoViewModel>();
            foreach (var item in toDos)
            {
                toDosViewModels.Add(Utils.ToDoToToDoViewModel(item));
            }
            return View(toDosViewModels);
        }

        // GET: ToDo/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _service.GetTodosAsync() == null)
            {
                return NotFound();
            }

            var toDo = await _service.GetTodoByIdAsync(id);
            if (toDo == null)
            {
                return NotFound();
            }
            var toDoViewModel = Utils.ToDoToToDoViewModel(toDo);

            return View(toDoViewModel);
        }

        // GET: ToDo/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ToDo/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Description,Complete,Date")] Todo toDo)
        {
            if (ModelState.IsValid)
            {
                await _service.CreateTodoAsync(toDo);
                await _service.SaveTodoChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            var toDoViewModel = Utils.ToDoToToDoViewModel(toDo);

            return View(toDoViewModel);
        }

        // GET: ToDo/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _service.GetTodosAsync() == null)
            {
                return NotFound();
            }

            var toDo = await _service.GetTodoByIdAsync(id);
            if (toDo == null)
            {
                return NotFound();
            }
            var toDoViewModel = Utils.ToDoToToDoViewModel(toDo);

            return View(toDoViewModel);
        }

        // POST: ToDo/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Description,Complete,Date")] Todo toDo)
        {
            if (id != toDo.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _service.UpdateTodo(toDo);
                    await _service.SaveTodoChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ToDoExists(toDo.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            var toDoViewModel = Utils.ToDoToToDoViewModel(toDo);

            return View(toDoViewModel);
        }

        // GET: ToDo/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            var toDo =await _service.GetTodoByIdAsync(id);
            await _service.RemoveTodoAsync(toDo);
            return RedirectToAction(nameof(Index));
        }

        //// POST: ToDo/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(int id)
        //{
        //    if (_context.ToDos == null)
        //    {
        //        return Problem("Entity set 'SpartaToDoContext.ToDos'  is null.");
        //    }
        //    var toDo = await _context.ToDos.FindAsync(id);
        //    if (toDo != null)
        //    {
        //        _context.ToDos.Remove(toDo);
        //    }

        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}

        private bool ToDoExists(int id)
        {
            return _service.ToDoExist(id);
        }
    }
}