using SOIT.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SOIT
{
    public class RequestFilterAttribute : AuthorizeAttribute
    {
        public RequestFilterAttribute()
        {
            View = "AuthorizeFailed";
        }

        public string View { get; set; }



        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            base.OnAuthorization(filterContext);
            IsUserAuthorized(filterContext);
        }

        private void IsUserAuthorized(AuthorizationContext filterContext)
        {
            //if (filterContext.Result == null)
            //    return;

            //if (!filterContext.HttpContext.User.Identity.IsAuthenticated)
            //{
            //    var vr = new ViewResult();
            //    vr.ViewName = View;
            //    ViewDataDictionary dict = new ViewDataDictionary();
            //    dict.Add("Message", "Sorry you are not Authorized to Perform this Action");
            //    vr.ViewData = dict;
            //    var result = vr;
            //    filterContext.Result = result;
            //}
            //string username = filterContext.HttpContext.User.Identity.Name;
            //// get rolename
            //string actionname = filterContext.ActionDescriptor.ActionName;

            if (filterContext.Result == null)
            {
                string username = filterContext.HttpContext.User.Identity.Name;
                string query = $@"INSERT INTO [UserAccessLog]([UserName],[AccessTime])VALUES('{username}','{DateTime.Now}')";
                
                using (SOITEntities context = new SOITEntities())
                {
                    context.Database.ExecuteSqlCommand(query);
                }

            }
            //           if (CanSystemAccess != "true")
            //           {
            //               var vr = new ViewResult();
            //               vr.ViewName = "UnAuthorizeAccess";
            //               ViewDataDictionary dict = new ViewDataDictionary();
            //               dict.Add("Message", "System is not accessing yet. wait for some time");
            //               vr.ViewData = dict;
            //               var result = vr;
            //               filterContext.Result = result;
            //           }
            //           return;
            //       }

            //if (!filterContext.HttpContext.User.Identity.IsAuthenticated)
            //{
            //    var vr = new ViewResult();
            //    vr.ViewName = View;
            //    ViewDataDictionary dict = new ViewDataDictionary();
            //    dict.Add("Message", "Sorry you are not Authorized to Perform this Action");
            //    vr.ViewData = dict;
            //    var result = vr;
            //    filterContext.Result = result;
            //}
        }
    }
}