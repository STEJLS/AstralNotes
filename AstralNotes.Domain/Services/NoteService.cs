using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AstralNotes.Domain.Abstractions;
using AstralNotes.Database;
using AstralNotes.Domain.Entities;
using System.Linq;
using AstralNotes.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace AstralNotes.Domain.Services
{
    /// <inheritdoc />
    public class NoteService : INoteService
    {
        private readonly DatabaseContext _dbContext;
        private readonly IUniqueImageService _imageService;
        private readonly SessionContext _sessionContext;

        /// <summary>
        /// Конструктор с тремя параметрами: DatabaseContext, UserManager, IUniqueImageService
        /// </summary>
        /// <param name="dbContext"> Контекст базы данных  </param>
        /// <param name="imageService"> Сервис уникальных картинок </param>
        /// <param name="sessionContext"></param>
        public NoteService(DatabaseContext dbContext, IUniqueImageService imageService, SessionContext sessionContext)
        {
            _dbContext = dbContext;
            _imageService = imageService;
            _sessionContext = sessionContext;
        }

        /// <inheritdoc />
        public async Task CreateAsync(string theme, string text)
        {
            byte[] image = _imageService.Get(theme + text);
            Note note = new Note(_sessionContext.UserGuid, theme, text, image);
            await _dbContext.Notes.AddAsync(note);
            await _dbContext.SaveChangesAsync();
        }

        /// <inheritdoc />
        public async Task DeleteAsync(Guid noteGuid)
        {
            Note note = await _dbContext.Notes.FirstOrDefaultAsync(n => n.NoteGuid.Equals(noteGuid) && n.UserGuid.Equals(_sessionContext.UserGuid));
            if (note != null)
            {
                _dbContext.Notes.Remove(note);
                await _dbContext.SaveChangesAsync();
            }
        }

        /// <inheritdoc />
        public async Task EditAsync(string theme, string text, Guid noteGuid)
        {
            Note note = await _dbContext.Notes.FirstOrDefaultAsync(n => n.NoteGuid.Equals(noteGuid) && n.UserGuid.Equals(_sessionContext.UserGuid));
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
        public async Task<List<Note>> GetAllAsync()
        {
            return await _dbContext.Notes.Where(n => n.UserGuid.Equals(_sessionContext.UserGuid)).ToListAsync();
        }

        /// <inheritdoc />
        public async Task<Note> GetAsync(Guid noteGuid)
        {
            return await _dbContext.Notes.FirstOrDefaultAsync(n => n.NoteGuid.Equals(noteGuid) && n.UserGuid.Equals(_sessionContext.UserGuid));
        }

        /// <inheritdoc />
        public async Task<List<Note>> SearchAsync(string searchString)
        {
            List<Note> notes = await _dbContext.Notes.Where(n =>
                (n.Text.ToLower().Contains(searchString.ToLower()) ||
                 n.Theme.ToLower().Contains(searchString.ToLower())) && n.UserGuid.Equals(_sessionContext.UserGuid)).ToListAsync();

            return notes;
        }
    }
}
