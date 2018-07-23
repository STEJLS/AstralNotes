using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using AstralNotes.DAL;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using System.Threading.Tasks;

namespace AstralNotes.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly DataBaseContext _dbContext;
        private readonly UserManager<IdentityUser> _userManager;

        public HomeController(DataBaseContext dbContext, UserManager<IdentityUser> userManager)
        {
            _dbContext = dbContext;
            _userManager = userManager;
        }
        
        public async Task<IActionResult> Index()
        {
            string id = (await _userManager.GetUserAsync(User)).Id;
            var notes = _dbContext.Notes.Where(n => n.User.Id.Equals(id)).ToList();
            return View(notes);
        }
    }
}
