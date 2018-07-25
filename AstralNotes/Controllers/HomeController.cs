using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using AstralNotes.Domain.Abstractions;

namespace AstralNotes.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly INoteService _noteService;

        public HomeController(INoteService noteService)
        {
            _noteService = noteService;
        }
        
        public async Task<IActionResult> Index()
        {
            var notes = await _noteService.GetAllAsync();
            return View(notes);
        }
    }
}
