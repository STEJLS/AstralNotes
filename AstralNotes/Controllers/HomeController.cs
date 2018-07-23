using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using AstralNotes.Database;
using AstralNotes.Domain.Abstractions;

namespace AstralNotes.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly DatabaseContext _dbContext;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly INoteService _noteService;

        public HomeController(DatabaseContext dbContext, UserManager<IdentityUser> userManager, INoteService noteService)
        {
            _dbContext = dbContext;
            _userManager = userManager;
            _noteService = noteService;
        }
        
        public async Task<IActionResult> Index()
        {
            var notes = await _noteService.GetAllAsync(User);
            return View(notes);
        }
    }
}
