using System.Collections.Generic;
using System.Threading.Tasks;
using AstralNotes.Domain.Abstractions;
using AstralNotes.Database;
using AstralNotes.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace AstralNotes.Domain.Services
{
    /// <inheritdoc />
    public class NoteService : INoteService
    {
        private readonly DatabaseContext _dbContext;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IUniqueImageService _imageService;

        /// <summary>
        /// Конструктор с тремя параметрами: DatabaseContext, UserManager, IUniqueImageService
        /// </summary>
        /// <param name="dbContext"> Контекст базы данных  </param>
        /// <param name="userManager"> Менеджер пользователя </param>
        /// <param name="imageService"> Сервис уникальных картинок </param>
        public NoteService(DatabaseContext dbContext, UserManager<IdentityUser> userManager, IUniqueImageService imageService)
        {
            _userManager = userManager;
            _dbContext = dbContext;
            _imageService = imageService;
        }

        /// <inheritdoc />
        public async Task CreateAsync(string theme, string text, ClaimsPrincipal claims)
        {
            IdentityUser user = await _userManager.FindByNameAsync(claims.Identity.Name);

            Note note = new Note
            {
                User = user,
                Theme = theme,
                Text = text,
                Image = _imageService.Get(theme + text)
            };

            await _dbContext.Notes.AddAsync(note);
            await _dbContext.SaveChangesAsync();
        }

        /// <inheritdoc />
        public async Task DeleteAsync(int id, ClaimsPrincipal claims)
        {
            IdentityUser user = await _userManager.FindByNameAsync(claims.Identity.Name);
            Note note = await _dbContext.Notes.FirstOrDefaultAsync(n => n.Id.Equals(id) && n.User.Id.Equals(user.Id));
            if (note != null)
            {
                _dbContext.Notes.Remove(note);
                await _dbContext.SaveChangesAsync();
            }
        }

        /// <inheritdoc />
        public async Task EditAsync(string theme, string text, int id, ClaimsPrincipal claims)
        {
            IdentityUser user = await _userManager.FindByNameAsync(claims.Identity.Name);
            Note note = await _dbContext.Notes.FirstOrDefaultAsync(n => n.Id.Equals(id) && n.User.Id.Equals(user.Id));
            if (note != null)
            {
                note.Theme = theme;
                note.Text = text;
                note.Image = _imageService.Get(theme + text);
                _dbContext.Update(note);
                await _dbContext.SaveChangesAsync();
            }
        }

        /// <inheritdoc />
        public async Task<List<Note>> GetAllAsync(ClaimsPrincipal claims)
        {
            string id = (await _userManager.GetUserAsync(claims)).Id;
            var notes = await _dbContext.Notes.Where(n => n.User.Id.Equals(id)).ToListAsync();

            return notes;
        }

        /// <inheritdoc />
        public async Task<Note> GetAsync(int id, ClaimsPrincipal claims)
        {
            IdentityUser user = await _userManager.FindByNameAsync(claims.Identity.Name);
            Note note = await _dbContext.Notes.FirstOrDefaultAsync(n => n.Id.Equals(id) && n.User.Id.Equals(user.Id));

            return note;
        }

        /// <inheritdoc />
        public async Task<List<Note>> SearchAsync(string searchString, ClaimsPrincipal claims)
        {
            IdentityUser user = await _userManager.FindByNameAsync(claims.Identity.Name);
            List<Note> notes = await _dbContext.Notes.Where(n =>
                (n.Text.ToLower().Contains(searchString.ToLower()) ||
                 n.Theme.ToLower().Contains(searchString.ToLower())) && n.User.Id.Equals(user.Id)).ToListAsync();

            return notes;
        }
    }
}
