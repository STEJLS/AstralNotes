using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;

namespace AstralNotes.ViewModels
{
    public class NoteViewModel
    {
        [BindNever]
        public int Id { get; set; }

        [Required(ErrorMessage = "Укажите тему")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Длина строки должна быть от 3 до 50 символов")]
        [Display(Name = "Тема")]
        public string Theme { get; set; }

        [Required(ErrorMessage = "Введите текст заметки")]
        [Display(Name = "Текст")]
        public string Text { get; set; }

        [BindNever]
        public byte[] Image { get; set; }
    }
}
