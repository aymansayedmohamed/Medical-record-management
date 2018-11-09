using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MedicalRecordManagement.Startup))]
namespace MedicalRecordManagement
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
