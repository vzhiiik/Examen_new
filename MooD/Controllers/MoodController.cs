using Microsoft.AspNetCore.Mvc;
using MooD.Models;
using MooD.Services;
using MooD.ViewModels;

namespace MooD.Controllers
{
    //[Authorize]
    public class MoodController : Controller
    {
        private IMoodData _moodData;
        private IGreeter _greeter;
        private readonly IMoodService _moodService;
        private readonly IHttpService _httpService;

        public MoodController(IMoodData moodData, IGreeter greeter, IMoodService moodService, IHttpService httpService)
        {
            _moodData = moodData;
            _greeter = greeter;
            _moodService = moodService;
            _httpService = httpService;
        }

        //[AllowAnonymous]
        public IActionResult Index()
        {
            var model = new IndexViewModel();
            model.Mood = _moodData.GetAll();
            model.Hello = _greeter.GetTime();
            model.SomeCountMessage = _moodService.CountMessage();
            model.SomethingFromGoogle =  _httpService.Get("http://www.google.com").Result.Substring(0,100);
            return View(model);
        }

        public IActionResult Details(int id)
        {
            var model = _moodData.Get(id);
            if(model == null)
            {
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        [HttpGet]   
        public IActionResult Create()
        {            
            return View();
        }


        public IActionResult Edit(int id)
        {
            var model = _moodData.Get(id);
            if (model == null)
            {
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(OdeToMood mood)
        {
            if (ModelState.IsValid)
            {
                _moodData.Update(mood);
                return RedirectToAction("Details", new { id = mood.Id });
            }
            else
            {
                return View();
            }
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(EditViewModel model)
        {
            if (ModelState.IsValid)
            {
                var newMood = new Models.OdeToMood();
                newMood.Name = model.Name;

                newMood = _moodData.Add(newMood);

                return RedirectToAction(nameof(Details), new { id = newMood.Id });
            }
            else
            {
                return View();
            }
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            if (ModelState.IsValid)
            {
                _moodData.Remove(id);
                return RedirectToAction("Index");

            }

            return RedirectToAction("Edit");
        }

    }
}
