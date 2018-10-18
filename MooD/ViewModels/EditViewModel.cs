using Mood.Models;
using System.ComponentModel.DataAnnotations;

namespace Mood.ViewModels
{
    public class EditViewModel
    {
        [Required, MaxLength(80)]
        public string Name { get; set; }
    }
}
