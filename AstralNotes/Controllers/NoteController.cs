using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AstralNotes.ViewModels;
using System.Net.Http;
using Microsoft.AspNetCore.Authorization;
using AstralNotes.DAL;
using AstralNotes.Services;
using AstralNotes.DAL.Models;
using Microsoft.AspNetCore.Identity;

namespace AstralNotes.Controllers
{
    [Authorize]
    public class NoteController : Controller
    {
        private DataBaseContext _dbContext;
        private UserManager<IdentityUser> _userManager;
        public NoteController(DataBaseContext dbContext, UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
            _dbContext = dbContext;
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(NoteViewModel model, UniqueImageService imageService)
        {
            if (ModelState.IsValid)
            {
                IdentityUser user = await _userManager.FindByNameAsync(User.Identity.Name);

                Note note = new Note
                {
                    User = user,
                    Theme = model.Theme,
                    Text = model.Text,
                    Image = imageService.Get(model.Theme + model.Text)
                };

                await _dbContext.Notes.AddAsync(note);
                await _dbContext.SaveChangesAsync();
                return RedirectToAction("Index", "Home");                 
            }

            return View(model);
        }

        public async Task<IActionResult> Show([FromQuery] int? Id)
        {
            if (Id != null)
            {
                IdentityUser user = await _userManager.FindByNameAsync(User.Identity.Name);
                Note note = _dbContext.Notes.FirstOrDefault(n => n.Id.Equals(Id) && n.User.Id.Equals(user.Id));
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
                IdentityUser user = await _userManager.FindByNameAsync(User.Identity.Name);
                Note note = _dbContext.Notes.FirstOrDefault(n => n.Id.Equals(Id) && n.User.Id.Equals(user.Id));
                if (note != null)
                {
                    _dbContext.Notes.Remove(note);
                    await _dbContext.SaveChangesAsync();
                }
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> Search(string searchString)
        {
            if (!String.IsNullOrEmpty(searchString))
            {
                IdentityUser user = await _userManager.FindByNameAsync(User.Identity.Name);
                List<Note> notes = _dbContext.Notes.Where(n => (n.Text.Contains(searchString) || n.Theme.Contains(searchString)) && n.User.Id.Equals(user.Id)).ToList();
                return View("Search", notes);
            }

            return RedirectToAction("Index", "Home");
        }
    }
}
