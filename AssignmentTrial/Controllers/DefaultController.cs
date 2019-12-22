using AssignmentTrial.Models;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace AssignmentTrial.Controllers
{
    public class DefaultController : Controller
    {
        // GET: Default
        public ActionResult Index()
        {
            string path = Convert.ToString(TempData["PATH"]);
            WebClient WC = new WebClient();
            var JSON = WC.DownloadString(path);//Path.Combine(Server.MapPath("~/bin"), "raw_data.json"));
            TestInputCollection testInputCollection = JsonConvert.DeserializeObject<TestInputCollection>(JSON);

            var validationResult = testInputCollection.ValidateCollection();


            return View(validationResult);
        }

        public ActionResult UploadFile()
        {
            return View();
        }


        [HttpPost]
        public ActionResult UploadFile(HttpPostedFileBase file)
        {
            try
            {
                string _path = string.Empty;
                if (file.ContentLength > 0)
                {
                    string _FileName = Path.GetFileName(file.FileName);
                    _path = Path.Combine(Server.MapPath("~/UploadedFiles"), _FileName);
                    file.SaveAs(_path);
                }
                ViewBag.Message = "File Uploaded Successfully!!";
                TempData["PATH"] = _path;
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                ViewBag.Message = "File upload failed!!";
                return View();
            }
        }
    }
}