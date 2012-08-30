using System;
using System.ServiceModel.Activation;
using System.Web;
using System.Web.Routing;
using Rest.Routes;

namespace Rest
{
    public class Global : HttpApplication
    {
        void Application_Start(object sender, EventArgs e)
        {
            RegisterRoutes();
        }

        private void RegisterRoutes()
        {
            RouteTable.Routes.Add(new ServiceRoute("Auth", new WebServiceHostFactory(), typeof(AuthService)));
            RouteTable.Routes.Add(new ServiceRoute("Member", new WebServiceHostFactory(), typeof(MemberService)));
            RouteTable.Routes.Add(new ServiceRoute("Family", new WebServiceHostFactory(), typeof(FamilyService)));
            RouteTable.Routes.Add(new ServiceRoute("Notification", new WebServiceHostFactory(), typeof(NotificationService)));
        }
    }
}
