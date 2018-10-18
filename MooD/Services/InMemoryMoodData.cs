using System.Collections.Generic;
using System.Linq;
using MooD.Models;

namespace MooD.Services
{
    public class InMemoryMoodData : IMoodData
    {
        static List<OdeToMood> _moods = new List<OdeToMood>();

        public IEnumerable<OdeToMood> GetAll()
        {
            return _moods.OrderBy(r => r.Name);
        }

        public OdeToMood Get(int id)
        {
            return _moods.FirstOrDefault(r => r.Id == id);
        }

        public OdeToMood Add(OdeToMood mood)
        {
            if (_moods.Any())
                mood.Id = _moods.Max(r => r.Id) + 1;
            else
                mood.Id = 1;

            _moods.Add(mood);
            return mood;
        }

        public OdeToMood Update(OdeToMood mood)
        {
            var r = _moods.Single(x => x.Id == mood.Id);
            r.Name = mood.Name;
            return r;
        }

        public void Remove(int id)
        {
            _moods = _moods.Where(x => x.Id != id).ToList();
        }


    }
}
