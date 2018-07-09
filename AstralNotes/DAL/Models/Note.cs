using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace AstralNotes.DAL.Models
{
    public class Note
    {
        [Key] public int Id { get; set; }
        public IdentityUser User{ get; set; }
        public string Theme { get; set; }
        public string Text { get; set; }
        public byte[] Image { get; set; }
    }
}
