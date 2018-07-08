using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

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
