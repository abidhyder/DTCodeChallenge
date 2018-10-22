using System;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using NUnit.Framework;
using Web.Business.Features;
using Web.UI.Controllers;
using System.Web.Mvc;
using System.Web;
using System.Linq;
using System.Web.Routing;
using System.IO;
using System.Text;
using Web.Business.Model;

namespace UnitTests
{
    [TestFixture]
    public class UploadFileTests
    {
        private FileUploadRepository repository;
        private HomeController homeController;
        private HttpContextBase context;
        private HttpRequestBase mockRequest;
        private HttpFileCollectionBase mockCollection;
        private HttpPostedFileBase mockFile;

        [SetUp]
        public void Setup()
        {
            repository = new FileUploadRepository();
            homeController = new HomeController(repository);
            context = Substitute.For<HttpContextBase>();
            mockRequest = Substitute.For<HttpRequestBase>();
            mockCollection = Substitute.For<HttpFileCollectionBase>();
            mockFile = Substitute.For<HttpPostedFileBase>();

            UTF8Encoding enc = new UTF8Encoding();
            var array = new[] { mockFile };
            mockFile.FileName.Returns("test.csv");
            mockFile.ContentLength.Returns(1000);
            mockFile.InputStream.Returns(new MemoryStream(enc.GetBytes(MockCSVFile())));
            mockCollection.GetEnumerator().Returns(array.GetEnumerator());
            mockCollection.Count.Returns(array.Count());

            // setup the context and request
            mockRequest.Files.Returns(mockCollection);
            context.Request.Returns(mockRequest);
            context.Request.Files[0].Returns(mockFile);
        }

        [Test]
        public void when_upload_invoked_check_all_rows_are_uploaded_with_info_of_mostsoldvehicle()
        {
            //Act
            homeController.ControllerContext = new ControllerContext(context, new RouteData(), homeController);
            ActionResult result = homeController.UploadFile();

            //Assert
            var model = ((result as ViewResult).Model as FileUploadModel);
            Assert.AreEqual(4, model.DealerList.Count);
            Assert.AreEqual("2017 Ferrari 488 Spider", model.MostSoldVehicle.Trim());
        }

        public string MockCSVFile()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("DealNumber,CustomerName,DealershipName,Vehicle,Price,Date\n");
            builder.Append("5469,Milli Fulton,Sun of Saskatoon,2017 Ferrari 488 Spider,\"429,987\",6/19/2018\n");
            builder.Append("5132,Rahima Skinner, Milton Jeep Limited,2009 Lamborghini Gallardo Carbon Fiber LP-560,\"169,900\",1/14/2018\n");
            builder.Append("1234,Aroush Knapp,Maxwell & Junior,2016 Porsche 911 2dr Cpe GT3 RS,\"289,900\",6/7/2018\n");
            builder.Append("5359,Maxine Daniels,Saskatoon Ferrari,2017 Ferrari 488 Spider,\"419,955\",7/15/2018\n");
            return builder.ToString();
        }
    }
}
