using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace LoginRegisterFromScratch.Attributes
{
    public class LoginAttribute : AuthorizeAttribute
    {
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            var user = filterContext.HttpContext.GetOwinContext().Authentication.User.Identity.Name;
            if (!string.IsNullOrEmpty(user))
            {
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new
                {
                    action = "Index",
                    controller = "Home",
                    area = ""
                }));
            }
        }
    }
}