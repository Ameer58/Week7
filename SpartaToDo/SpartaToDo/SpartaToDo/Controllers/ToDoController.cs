using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SpartaToDo.Data;
using SpartaToDo.Models;
using SpartaToDo.Models.ViewModels;
using SpartaToDo.Services;

namespace SpartaToDo.Controllers
{
    [Authorize]
    public class ToDoController : Controller
    {
        private readonly ILogger<ToDoController> _logger;
        private readonly IToDoService _service;
        private UserManager<Spartan> _userManager;
        public ToDoController(IToDoService service,ILogger<ToDoController> logger, UserManager<Spartan> userManager)
        {
            _logger = logger;
            _service = service;
            _userManager = userManager;
        }

        // GET: ToDo
        [Authorize(Roles = "Trainee, Trainer")]
        public async Task<IActionResult> Index(string searchString)
        {
            var currentUser = await _userManager.GetUserAsync(HttpContext.User);
            var toDos = new List<ToDo>();
            if (HttpContext.User.IsInRole("Trainer"))
            {
                toDos = await _service.GetToDoItems();
            }
            else if (HttpContext.User.IsInRole("Trainee"))
            {
                toDos = await _service.GetToDoItemsByUserIdAsync(currentUser.Id);
            }
            var toDosViewModelList = new List<ToDoViewModel>();
            foreach (var toDo in toDos)
            {
                toDosViewModelList.Add(Utils.ToDoModelToToDoViewModel(toDo));

                if (!String.IsNullOrEmpty(searchString))
                {
                    toDosViewModelList = toDosViewModelList.Where(t => t.Title.ToLower().Contains(searchString.ToLower()) ||
                    t.Description.ToLower().Contains(searchString.ToLower())).ToList();
                }

            }
            return View(toDosViewModelList);
        }



        // GET: ToDo/Details/5
        [Authorize(Roles = "Trainee, Trainer")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var toDo = await _service.GetToDoItemByIdAsync(id);
            var currentUser = await _userManager.GetUserAsync(HttpContext.User);
            if (toDo == null)
            {
                return NotFound();
            }
            if (currentUser.Id == toDo.SpartanId)
            {
                var toDoViewModel = Utils.ToDoModelToToDoViewModel(toDo);
                return View(toDoViewModel);
            }
            return NotFound();
        }

        // GET: ToDo/Create
        [Authorize(Roles = "Trainee")]

        public IActionResult Create()
        {
            return View();
        }

        // POST: ToDo/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        //Removed "Id" from the Bind attribute parameter (will be created automatically for us)
        public async Task<IActionResult> Create([Bind("Title,Description,Complete")] ToDoViewModel toDoViewModel)
        {
            if (ModelState.IsValid)
            {
                var currentUser = await _userManager.GetUserAsync(HttpContext.User);
                var toDo = new ToDo
                {
                    Title = toDoViewModel.Title,
                    Description = toDoViewModel.Description,
                    Complete = toDoViewModel.Complete,
                    Spartan = currentUser,
                    SpartanId = currentUser.Id

                };

                await _service.CreateToDoItemAsync(toDo);
                return RedirectToAction(nameof(Index));
            }
            return View(toDoViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateCheckBox(int? id, [Bind("Complete")] ToDoViewModel toDoViewModel)
        {
            var toDo = await _service.GetToDoItemByIdAsync(id);
            if (toDo == null) return NotFound();
            toDo.Complete = toDoViewModel.Complete;
            await _service.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        // GET: ToDo/Edit/5
        // POST: ToDo/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Description,Complete,DateCreated")]ToDoViewModel toDoViewModel)
        {
            var toDo = await _service.GetToDoItemByIdAsync(id);

            if (id != toDo.Id || toDo == null)
            {
                return NotFound();
            }
            var x = ModelState.IsValid;
            if (ModelState.IsValid)
            {
                try
                {
                    toDo.Title = toDoViewModel.Title;
                    toDo.Description = toDoViewModel.Description;
                    toDo.Complete = toDoViewModel.Complete;
                    await _service.SaveChanges();
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
            return View(toDoViewModel);
        }

        // GET: ToDo/Edit/5


        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var toDo = await _service.GetToDoItemByIdAsync(id);
            if (toDo == null)
            {
                return NotFound();
            }
            return View(Utils.ToDoModelToToDoViewModel(toDo));
        }


        // GET: ToDo/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int? id)
        {
            var toDo = await _service.GetToDoItemByIdAsync(id);
            await _service.RemoveToDoItemAsync(toDo);
            return RedirectToAction(nameof(Index));
        }

        private bool ToDoExists(int id)
        {
          return _service.ToDoItemExists(id);
        }
    }
}
