using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Net;
using System.Web.Mvc;
using System.Xml.Linq;
using UploadFileToImgurMVC.Models;

namespace UploadFileToImgurMVC.Controllers
{
    public class HomeController : Controller
    {
        // Update ClientId
        const string ClientId = "";

        public ActionResult Upload()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Upload(UploadToImgur model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            List<string> urls = new List<string>();

            foreach (var item in model.ImageFile)
            {
                byte[] file = new byte[item.InputStream.Length];
                item.InputStream.Read(file, 0, file.Length);

                var url = UploadOnImgur(file);

                model.Id = Guid.NewGuid().ToString();
                model.Name = item.FileName;
                model.Url = url;

                urls.Add(url);

                // TODO: save in database
            }

            return Content("Success. Urls: " + string.Join(",", urls));
        }

        public string UploadOnImgur(byte[] file)
        {
            string url = string.Empty;

            using (var w = new WebClient())
            {
                w.Headers.Add("Authorization", "Client-ID " + ClientId);
                var values = new NameValueCollection
                    {
                        { "image", Convert.ToBase64String(file) }
                    };

                byte[] response = w.UploadValues("https://api.imgur.com/3/upload.xml", values);

                var xml = XDocument.Load(new MemoryStream(response));

                url = xml.Root.Element("link").Value;
            }

            return url;
        }
    }
}