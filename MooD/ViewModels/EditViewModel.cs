using MooD.Models;
using System.ComponentModel.DataAnnotations;

namespace MooD.ViewModels
{
    public class EditViewModel
    {
        [Required, MaxLength(80)]
        public string Name { get; set; }
    }
}
