using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using Web.Business.Features;
using Web.Business.Model;

namespace Web.UI.Controllers
{
    public class HomeController : Controller
    {
        private readonly IFileUploadRepository fileUpload;

        public HomeController(IFileUploadRepository fileUpload)
        {
            this.fileUpload = fileUpload;
        }
        public ActionResult Index()
        {
            return View(new FileUploadModel());
        }
        
        [HttpPost]
        public ActionResult UploadFile()
        {
            var fileModel = new FileUploadModel();
            if (Request.Files.Count == 1)
            {
                var file = Request.Files[0];
                fileModel = fileUpload.UploadFile(file);
            }

            return View("Index", fileModel);
        }

        public ActionResult NotFound()
        {
            return View();
        }
    }
}