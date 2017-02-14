using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(UploadFileToImgurMVC.Startup))]
namespace UploadFileToImgurMVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            
        }
    }
}
