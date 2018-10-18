using System.Linq;

namespace Mood.Services
{
    public class MoodService : IMoodService
    {
        private readonly IMoodData _moodData;

        public MoodService(IMoodData moodData)
        {
            _moodData = moodData;
        }

        public string CountMessage()
        {
            return "";   //"Antal restauranger är " + _moodData.GetAll().Count();
        }
    }
}
