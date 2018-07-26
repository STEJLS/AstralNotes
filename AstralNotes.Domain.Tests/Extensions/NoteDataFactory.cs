using AstralNotes.Database;
using AstralNotes.Domain.Entities;
using AstralNotes.Domain.Abstractions;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System;
using AstralNotes.Domain.Models;

namespace AstralNotes.Domain.Tests.Extensions
{
    /// <summary>
    /// Заполнение БД заметками
    /// </summary>
    class NoteDataFactory
    {
        private readonly DatabaseContext _databaseContext;
        private readonly IUniqueImageService _uniqueImageService;
        private readonly SessionContext _sessionContex;

        /// <summary />
        public NoteDataFactory(DatabaseContext databaseContext, SessionContext sessionContext,
            IUniqueImageService uniqueImageService)
        {
            _databaseContext = databaseContext;
            _uniqueImageService = uniqueImageService;
            _sessionContex = sessionContext;
        }

        /// <summary>
        /// Создние тестовой заметки
        /// </summary>
        public Note Create(string theme = "theme", string text = "text")
        {
            var note = new Note
            {
                NoteGuid = Guid.NewGuid(),
                Theme = theme,
                Text = text,
                UserGuid = _sessionContex.UserGuid,
                Image = _uniqueImageService.Get(theme + text)
            };

            _databaseContext.Notes.Add(note);

            return note;
        }

        /// <summary>
        /// Освобождение ресурсов
        /// </summary>
        /// <returns cref="Task" />
        public async Task Dispose()
        {
            var notes = await _databaseContext.Notes.ToListAsync();
            _databaseContext.Notes.RemoveRange(notes);
        }
    }
}
