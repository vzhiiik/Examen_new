using MooD.Models;
using System.Collections.Generic;

namespace MooD.Services
{
    public interface IMoodData
    {
        IEnumerable<OdeToMood> GetAll();
        OdeToMood Get(int id);
        OdeToMood Add(OdeToMood mood);
        OdeToMood Update(OdeToMood mood);
        void Remove(int id);
    }
}
