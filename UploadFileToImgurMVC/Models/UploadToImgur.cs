using System.Collections.Generic;
using System.Web;

namespace UploadFileToImgurMVC.Models
{
    public class UploadToImgur
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public IEnumerable<HttpPostedFileBase> ImageFile { get; set; }
    }
}