using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Mood.Models;
using Mood.Models.FaceApiData;
using Mood.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace Mood.Controllers
{
    //[Authorize]
    public class HomeController : Controller
    {
        const string subscriptionKey = "0f02fdf50aa34b43a890cc185515e46f";
        const string uriBase = "https://westcentralus.api.cognitive.microsoft.com/face/v1.0/detect";

        public IActionResult Index()
        {
            return RedirectToAction("Index", "Mood");
        }

        public IActionResult Kalle(string emotion)
        {
            PlaylistViewModel playlist = LinkPlaylistDependingOnEmotion(emotion);
            return View(emotion, playlist);
        }

        static async Task<string> MakeAnalysisRequest(string pic)
        {
            HttpClient client = new HttpClient();

            client.DefaultRequestHeaders.Add(
                "Ocp-Apim-Subscription-Key", subscriptionKey);

            string requestParameters = "returnFaceId=true&returnFaceLandmarks=false" +
                "&returnFaceAttributes=age,gender,headPose,smile,facialHair,glasses," +
                "emotion,hair,makeup,occlusion,accessories,blur,exposure,noise";

            string uri = uriBase + "?" + requestParameters;

            HttpResponseMessage response;

            byte[] byteData = GetImageAsByteArray(pic);

            using (ByteArrayContent content = new ByteArrayContent(byteData))
            {
                content.Headers.ContentType =
                    new MediaTypeHeaderValue("application/octet-stream");

                response = await client.PostAsync(uri, content);

                string contentString = await response.Content.ReadAsStringAsync();

                 return contentString;
            }
        }

        static byte[] GetImageAsByteArray(string pic)
        {
            using (FileStream fileStream =
                new FileStream(pic, FileMode.Open, FileAccess.Read))
            {
                BinaryReader binaryReader = new BinaryReader(fileStream);
                return binaryReader.ReadBytes((int)fileStream.Length);
            }
        }

        //gör metod som visar historik för inloggade användare
        [HttpGet]        
        public IActionResult ShowHistory ()
        {
            return View();

        }

        [HttpPost("UploadPic")]
        public async Task<IActionResult> PostPic(string pic)
        {
            string replaced = pic.Substring(22);
            string filePath = Path.GetTempFileName();
            byte[] bytes = Convert.FromBase64String(replaced);
            using (FileStream fs = new FileStream(filePath, FileMode.Create))

            {
                using (BinaryWriter bw = new BinaryWriter(fs))
                {
                    byte[] data = Convert.FromBase64String(replaced);

                    bw.Write(data);

                    bw.Close();
                }

            }
            var print = await MakeAnalysisRequest(filePath);
            string emotionResult = ConvertToEmotion(print);
            PlaylistViewModel playlist = LinkPlaylistDependingOnEmotion(emotionResult);
            return View(emotionResult, playlist);
        } 

        [AllowAnonymous]
        [HttpPost("UploadFiles")]
        public async Task<IActionResult> Post(IFormFile file)
        {
            var filePath = Path.GetTempFileName();

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            var print = await MakeAnalysisRequest(filePath);

            string emotionResult = ConvertToEmotion(print);
            PlaylistViewModel playlist = LinkPlaylistDependingOnEmotion(emotionResult);
            return View(emotionResult, playlist);

        }

        [AllowAnonymous]
        public string ConvertToEmotion(string print)
        {
            var emotions = JsonConvert.DeserializeObject<List<Class1>>(print);

            Emotion AllEmotion = emotions[0].faceAttributes.emotion;

            var list = new List<Em>
            {
                new Em
                {
                    EmotionName = "anger",
                    Value = AllEmotion.anger
                },
                new Em
                {
                    EmotionName = "contempt",
                    Value = AllEmotion.contempt
                },
                new Em
                {
                    EmotionName = "happiness",
                    Value = AllEmotion.happiness
                },
                new Em
                {
                    EmotionName = "fear",
                    Value = AllEmotion.fear
                },
                new Em
                {
                    EmotionName = "sadness",
                    Value = AllEmotion.sadness
                },
                new Em
                {
                    EmotionName = "surprise",
                    Value = AllEmotion.surprise
                },
                new Em
                {
                    EmotionName = "neutral",
                    Value = AllEmotion.neutral
                },
                new Em
                {
                    EmotionName = "disgust",
                    Value = AllEmotion.disgust
                },
            };

            var primaryEmotion = list.OrderByDescending(x => x.Value).FirstOrDefault();
            string emotionResult = primaryEmotion.EmotionName;
            return emotionResult;
            

        }

        [AllowAnonymous]
        public PlaylistViewModel LinkPlaylistDependingOnEmotion(string emotionResult)
        {

            PlaylistViewModel result = new PlaylistViewModel();

            if (emotionResult == "anger" || emotionResult == "contempt" || emotionResult == "disgust")
            {
                result.Playlist1 = $"https://open.spotify.com/embed/user/artkul/playlist/0ybhZEyc8RrHsVDFt9x5CI";
                result.Playlist2 = $"https://open.spotify.com/embed/user/1239561108/playlist/29EOIjr2saw00KxpYdYSQM";
                result.Playlist3 = $"https://open.spotify.com/embed/user/dvaughan2021/playlist/5Am1VHtu0oJAc5omSVHvat";
                return result;
            }
            else if (emotionResult == "happiness")
            {
                result.Playlist1 = $"https://open.spotify.com/embed/user/spotify/playlist/37i9dQZF1DXdPec7aLTmlC";
                result.Playlist2 = $"https://open.spotify.com/embed/user/spotify/playlist/37i9dQZF1DXdPec7aLTmlC";
                result.Playlist3 = $"https://open.spotify.com/embed/user/spotify/playlist/37i9dQZF1DXdPec7aLTmlC";
            }
            else if (emotionResult == "fear")
            {
                result.Playlist1 = $"https://open.spotify.com/embed/show/5XhS5WBxLYgN3S9KhEyrrF";
                result.Playlist2 = $"https://open.spotify.com/embed/user/spotify/playlist/37i9dQZF1DX6SpcerLn1dx";
                result.Playlist3 = $"https://open.spotify.com/embed/user/warnermusicus/playlist/59njg5yJwvLH2vAuaZdAZD";
                return result;


            }
            else if (emotionResult == "sadness")
            {
                result.Playlist1 = $"https://open.spotify.com/embed/user/spotify/playlist/37i9dQZF1DX3YSRoSdA634";
                result.Playlist2 = $"https://open.spotify.com/embed/user/spotify/playlist/37i9dQZF1DX7qK8ma5wgG1";
                result.Playlist3 = $"https://open.spotify.com/embed/user/funnybunny000000/playlist/4EoPt05ztUjVaujcWbUL2Z";
                return result;


            }
            else if (emotionResult == "surprise")
            {
                result.Playlist1 = $"https://open.spotify.com/embed/user/ofinns/playlist/61CPcnHmTVMloD399c76et";
                result.Playlist2 = $"https://open.spotify.com/embed/user/juandurfelworld/playlist/1SUu5S4mKpyOEeuImxGM64";
                result.Playlist3 = $"https://open.spotify.com/embed/user/spotify/playlist/37i9dQZF1DWSEMER0I7qzl";
                return result;


            }
            else if (emotionResult == "neutral")
            {
                result.Playlist1 = $"https://open.spotify.com/embed/user/spotify/playlist/37i9dQZF1DXbITWG1ZJKYt";
                result.Playlist2 = $"https://open.spotify.com/embed/user/spotify/playlist/37i9dQZF1DWTkxQvqMy4WW";
                result.Playlist3 = $"https://open.spotify.com/embed/user/foilism/playlist/37qanRa2o6oa2l0TkMNdnD";
                return result;


            }
            
            
                return result;

            

        }
    }
}

