using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(FBClone.WebUI.Startup))]

namespace FBClone.WebUI
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            //ConfigureAuth(app);
            app.MapSignalR();
        }
    }
}