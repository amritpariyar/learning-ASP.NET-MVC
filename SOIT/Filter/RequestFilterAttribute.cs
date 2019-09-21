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
            if (filterContext.Result == null)
                return;

            if (!filterContext.HttpContext.User.Identity.IsAuthenticated)
            {
                var vr = new ViewResult();
                vr.ViewName = View;
                ViewDataDictionary dict = new ViewDataDictionary();
                dict.Add("Message", "Sorry you are not Authorized to Perform this Action");
                vr.ViewData = dict;
                var result = vr;
                filterContext.Result = result;
            }

            //       if (filterContext.Result == null)
            //       {
            //           string username = filterContext.HttpContext.User.Identity.Name;
            //           string query = @"Declare @starttime nvarchar(20),@endtime nvarchar(20),@systemTimeNow nvarchar(20),@userid nvarchar(120);
            //               set @starttime=(select SystemStartTime from OrganizationBranch where ID=
            //               (select ui.OrganizationBranch from UserInfo ui where ui.UserId=(select id from AspNetUsers where UserName='"+username+@"')));
            //               set @endtime=(select SystemStopTime from OrganizationBranch where ID=
            //               (select ui.OrganizationBranch from UserInfo ui where ui.UserId=(select id from AspNetUsers where UserName='"+username+ @"')));
            //               set @systemTimeNow = (select CONVERT(nvarchar,convert(time,SYSDATETIME())))
            //               if ((@starttime is null or @starttime >= @systemTimeNow or @starttime = '') and(@endtime is null or @endtime >= @systemTimeNow or @endtime = ''))
            //               begin
            //                select 'true'
            //               end
            //               else if @starttime < @systemTimeNow and @endtime < @systemTimeNow
            //               begin
            //                select 'false'
            //               end
            //               else    
            //begin
            //	select 'invalid'
            //end";
            //           string CanSystemAccess = string.Empty;
            //           using(BanijyaEntities context = new BanijyaEntities())
            //           {
            //               CanSystemAccess = context.Database.SqlQuery<string>(query).First();
            //           }


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