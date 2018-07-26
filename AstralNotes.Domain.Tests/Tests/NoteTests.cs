using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AstralNotes.Domain.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using AstralNotes.Domain.Tests.Extensions;
using AstralNotes.Database;
using Microsoft.EntityFrameworkCore;
using AstralNotes.Domain.Entities;

namespace AstralNotes.Domain.Tests.Tests
{
    class NoteTests
    {
        //Для arrange и assert
        private const string _theme1 = "theme1";
        private const string _text1 = "text1";
        private const string _theme2 = "theme2";
        private const string _text2 = "text2";

        //Тестируемый сервис
        private readonly INoteService _noteService;

        private readonly DatabaseContext _databaseContext;
        private readonly NoteDataFactory _noteDataFactory;

        /// <summary />
        public NoteTests()
        {
            _noteService = TestInitializer.Provider.GetService<INoteService>();
            _databaseContext = TestInitializer.Provider.GetService<DatabaseContext>();
            _noteDataFactory = TestInitializer.Provider.GetService<NoteDataFactory>();
        }

        /// <summary>
        /// Освобождение ресурсов
        /// </summary>
        /// <returns cref="Task"/>
        [TearDown]
        public async Task Cleanup()
        {
            await TestInitializer.Provider.GetService<NoteDataFactory>().Dispose();
            await TestInitializer.Provider.GetService<DatabaseContext>().SaveChangesAsync();
        }

        [Test]
        public async Task Create_WithRightInputs_ShouldSuccess()
        {
            Assert.DoesNotThrowAsync(async () => await _noteService.CreateAsync(_theme1, _text1));
            var resultNote = await TestInitializer.Provider.GetService<DatabaseContext>().Notes.FirstAsync(n => n.Theme == _theme1);

            Assert.NotNull(resultNote);
            Assert.NotNull(resultNote.Text);
            Assert.AreEqual(_text1, resultNote.Text);
        }

        [Test]
        public async Task Delete_WithExistingGuid_ShouldSuccess()
        {
            //arrange
            Note note = _noteDataFactory.Create(_theme1, _text1);
            await _databaseContext.SaveChangesAsync();
            //act
            //assert
            Assert.DoesNotThrowAsync(async () => await _noteService.DeleteAsync(note.NoteGuid));

            note = await TestInitializer.Provider.GetService<DatabaseContext>().Notes.FirstOrDefaultAsync(n => n.Theme == _theme1);
            Assert.Null(note);
        }

        [Test]
        public async Task Delete_WithNotExistingGuid_ShouldSuccess()
        {
            Assert.DoesNotThrowAsync(async () => await _noteService.DeleteAsync(new Guid()));
        }

        [Test]
        public async Task Edit_WithExistingGuid_ShouldSuccess()
        {
            //arrange
            Note note = _noteDataFactory.Create(_theme1, _text1);
            await _databaseContext.SaveChangesAsync();
            //act
            //assert
            Assert.DoesNotThrowAsync(async () => await _noteService.EditAsync(_theme2, _text2, note.NoteGuid));

            var resultNote = await TestInitializer.Provider.GetService<DatabaseContext>().Notes.FirstOrDefaultAsync(n => n.NoteGuid == note.NoteGuid);
            Assert.NotNull(resultNote);
            Assert.AreEqual(_theme2, resultNote.Theme);
            Assert.AreEqual(_text2, resultNote.Text);
        }

        [Test]
        public async Task Edit_WithNotExistingGuid_ShouldSuccess()
        {
            Assert.DoesNotThrowAsync(async () => await _noteService.EditAsync(_theme2, _text2, new Guid()));
        }

        [Test]
        public async Task GetAll_TwoNotes_ShouldSuccess()
        {
            //arrange
            _noteDataFactory.Create(_theme1, _text1);
            _noteDataFactory.Create(_theme2, _text2);
            await _databaseContext.SaveChangesAsync();
            //act
            //assert
            var notes = await _noteService.GetAllAsync();

            Assert.AreEqual(2, notes.Count);
        }

        [Test]
        public async Task GetAll_ZeroNotes_ShouldSuccess()
        {
            //act
            var notes = await _noteService.GetAllAsync();
            //assert
            Assert.IsEmpty(notes);
        }

        [Test]
        public async Task Get_WithExistingGuid_NotNull()
        {
            //arrange
            Note note = _noteDataFactory.Create(_theme1, _text1);
            await _databaseContext.SaveChangesAsync();
            //act
            var resultNote = await _noteService.GetAsync(note.NoteGuid);
            //assert
            Assert.NotNull(resultNote);
            Assert.AreEqual(_theme1, resultNote.Theme);
            Assert.AreEqual(_text1, resultNote.Text);
        }

        [Test]
        public async Task Get_WithNotExistingGuid_Null()
        {
            //act
            var resultNote = await _noteService.GetAsync(new Guid());
            //assert
            Assert.Null(resultNote);
        }

        [Test]
        public async Task Search_ByText_OneNote()
        {
            //arrange
            _noteDataFactory.Create(_theme1, _text1);
            _noteDataFactory.Create(_theme2, _text2);
            await _databaseContext.SaveChangesAsync();
            //act
            var resultNotes = await _noteService.SearchAsync(_text1);
            //assert
            Assert.NotNull(resultNotes);
            Assert.AreEqual(1, resultNotes.Count);
            Assert.AreEqual(_theme1, resultNotes[0].Theme);
            Assert.AreEqual(_text1, resultNotes[0].Text);
        }

        [Test]
        public async Task Search_ByText_TwoNote()
        {
            //arrange
            _noteDataFactory.Create(_theme1, _text1);
            _noteDataFactory.Create(_theme2, _text2);
            await _databaseContext.SaveChangesAsync();
            //act
            var resultNotes = await _noteService.SearchAsync("text");
            //assert

            Assert.NotNull(resultNotes);
            Assert.AreEqual(2, resultNotes.Count);
        }

        [Test]
        public async Task Search_ByText_NotFound()
        {
            //arrange
            _noteDataFactory.Create(_theme1, _text1);
            _noteDataFactory.Create(_theme2, _text2);
            await _databaseContext.SaveChangesAsync();
            //act
            var resultNotes = await _noteService.SearchAsync("WrongText");
            //assert
            Assert.IsEmpty(resultNotes);
        }

        [Test]
        public async Task Search_ByTheme_OneNote()
        {
            //arrange
            _noteDataFactory.Create(_theme1, _text1);
            _noteDataFactory.Create(_theme2, _text2);
            await _databaseContext.SaveChangesAsync();
            //act
            var resultNotes = await _noteService.SearchAsync(_theme1);
            //assert
            Assert.NotNull(resultNotes);
            Assert.AreEqual(1, resultNotes.Count);
            Assert.AreEqual(_theme1, resultNotes[0].Theme);
            Assert.AreEqual(_text1, resultNotes[0].Text);
        }

        [Test]
        public async Task Search_ByTheme_TwoNote()
        {
            //arrange
            _noteDataFactory.Create(_theme1, _text1);
            _noteDataFactory.Create(_theme2, _text2);
            await _databaseContext.SaveChangesAsync();
            //act
            var resultNotes = await _noteService.SearchAsync("Theme");
            //assert
            Assert.NotNull(resultNotes);
            Assert.AreEqual(2, resultNotes.Count);
        }

        [Test]
        public async Task Search_ByTheme_NotFound()
        {
            //arrange
            _noteDataFactory.Create(_theme1, _text1);
            _noteDataFactory.Create(_theme2, _text2);
            await _databaseContext.SaveChangesAsync();
            //act
            var resultNotes = await _noteService.SearchAsync("WrongTheme");
            //assert
            Assert.IsEmpty(resultNotes);
        }

    }
}
