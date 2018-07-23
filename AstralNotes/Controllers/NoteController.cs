using System;
using System.Threading.Tasks;
using AstralNotes.Database;
using AstralNotes.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using AstralNotes.ViewModels;
using Microsoft.AspNetCore.Authorization;
using AstralNotes.Domain.Abstractions;
using Microsoft.AspNetCore.Identity;

namespace AstralNotes.Controllers
{
    [Authorize]
    public class NoteController : Controller
    {
        private DatabaseContext _dbContext;
        private UserManager<IdentityUser> _userManager;
        private readonly INoteService _noteService;

        public NoteController(DatabaseContext dbContext, UserManager<IdentityUser> userManager, INoteService noteService)
        {
            _userManager = userManager;
            _dbContext = dbContext;
            _noteService = noteService;
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(NoteViewModel model)
        {
            if (ModelState.IsValid)
            {
                await _noteService.CreateAsync(model.Theme, model.Text, User);
                return RedirectToAction("Index", "Home");  
            }

            return View(model);
        }

        public async Task<IActionResult> Show([FromQuery] int? Id)
        {
            if (Id != null)
            {
                var note = await _noteService.GetAsync((int)Id, User);
                if (note != null)
                {
                    return View(note);
                }
            }

            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> Delete([FromQuery] int? Id)
        {
            if (Id != null)
            {
                await _noteService.DeleteAsync((int)Id, User);
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> Search(string searchString)
        {
            if (!String.IsNullOrEmpty(searchString))
            {
                var notes = await _noteService.SearchAsync(searchString, User);
                return View("Search", notes);
            }

            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> Edit([FromQuery] int? Id)
        {
            if (Id != null)
            {
                Note note = await _noteService.GetAsync((int)Id, User);
                if (note != null)
                {
                    return View(new NoteViewModel {Id = note.Id, Text = note.Text, Theme = note.Theme});
                }
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> Edit(NoteViewModel model, [FromQuery] int? id)
        {
            if (ModelState.IsValid && id != null)
            {
                await _noteService.EditAsync(model.Theme, model.Text, (int)id, User);
                return RedirectToAction("Index", "Home");
            }

            return View(model);
        }
    }
}