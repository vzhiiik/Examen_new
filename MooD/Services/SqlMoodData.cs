using System.Collections.Generic;
using System.Linq;
using Mood.Models;
using Mood.Data;
using Microsoft.EntityFrameworkCore;

namespace Mood.Services
{
    public class SqlMoodData : IMoodData
    {
        private MoodDbContext _context;

        public SqlMoodData(MoodDbContext context)
        {
            _context = context;
        }

        public OdeToMood Add(OdeToMood mood)
        {
            _context.Mood.Add(mood);
            _context.SaveChanges();
            return mood;
        }

        public OdeToMood Get(int id)
        {
            return _context.Mood.FirstOrDefault(r => r.Id == id);
        } 

        public IEnumerable<OdeToMood> GetAll()
        {
            return _context.Mood.OrderBy(r => r.Name);
        }

        public OdeToMood Update(OdeToMood mood)
        {
            _context.Attach(mood).State = 
                EntityState.Modified;
            _context.SaveChanges();
            return mood;
        }

        public void Remove(int id)
        {
            var r = _context.Mood.First(x => x.Id == id);
            _context.Mood.Remove(r);
            _context.SaveChanges();
        }
    }
}
