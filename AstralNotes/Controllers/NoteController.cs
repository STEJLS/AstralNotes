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
        private readonly INoteService _noteService;

        public NoteController(INoteService noteService)
        {
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
                await _noteService.CreateAsync(model.Theme, model.Text);
                return RedirectToAction("Index", "Home");
            }

            return View(model);
        }

        public async Task<IActionResult> Show([FromQuery] Guid? NoteGuid)
        {
            if (NoteGuid != null)
            {
                var note = await _noteService.GetAsync((Guid) NoteGuid);
                if (note != null)
                {
                    return View(note);
                }
            }

            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> Delete([FromQuery] Guid? NoteGuid)
        {
            if (NoteGuid != null)
            {
                await _noteService.DeleteAsync((Guid) NoteGuid);
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> Search(string searchString)
        {
            if (!String.IsNullOrEmpty(searchString))
            {
                var notes = await _noteService.SearchAsync(searchString);
                return View("Search", notes);
            }

            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> Edit([FromQuery] Guid? NoteGuid)
        {
            if (NoteGuid != null)
            {
                Note note = await _noteService.GetAsync((Guid) NoteGuid);
                if (note != null)
                {
                    return View(new NoteViewModel {NoteGuid = note.NoteGuid, Text = note.Text, Theme = note.Theme});
                }
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> Edit(NoteViewModel model, [FromQuery] Guid? noteGuid)
        {
            if (ModelState.IsValid && noteGuid != null)
            {
                await _noteService.EditAsync(model.Theme, model.Text, (Guid) noteGuid);
                return RedirectToAction("Index", "Home");
            }

            return View(model);
        }
    }
}