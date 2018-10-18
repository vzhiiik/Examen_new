using Mood.Models;
using System.Collections.Generic;

namespace Mood.ViewModels
{
    public class IndexViewModel
    {
        public IEnumerable<OdeToMood> Mood { get; set; }
        public string Hello { get; set; }
        public string SomeCountMessage { get; set; }
        public string SomethingFromGoogle { get; set; }
    }
}
