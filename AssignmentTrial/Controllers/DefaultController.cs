using AssignmentTrial.Models;
using Newtonsoft.Json;
using System.IO;
using System.Net;
using System.Web.Mvc;

namespace AssignmentTrial.Controllers
{
    public class DefaultController : Controller
    {
        // GET: Default
        public ActionResult Index()
        {
            WebClient WC = new WebClient();
            var JSON = WC.DownloadString(Path.Combine(Server.MapPath("~/bin"), "raw_data.json"));
            TestInputCollection testInputCollection = JsonConvert.DeserializeObject<TestInputCollection>(JSON);

            var validationResult = testInputCollection.ValidateCollection();


            return View(validationResult);
        }
    }
}