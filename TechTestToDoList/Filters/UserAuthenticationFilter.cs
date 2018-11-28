using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Filters;
using TechTestToDoList.Dal.ViewModels;

namespace TechTestToDoList.Filters
{
    public class UserAuthenticationFilter : ActionFilterAttribute, IAuthenticationFilter
    {
        public void OnAuthentication(AuthenticationContext filterContext)
        {
            //Check Session is Empty Then set as Result is HttpUnauthorizedResult 
            if (filterContext.HttpContext.Session["User"] == null)
            {
                filterContext.Result = new HttpUnauthorizedResult();
            }
            else
            {
                UserViewModel user = (UserViewModel)filterContext.HttpContext.Session["User"];
                if((user.sessionStartDate-DateTime.Now).Minutes > 20)
                {
                    filterContext.HttpContext.Session["User"] = null;
                    filterContext.Result = new RedirectToRouteResult("Default", null);
                }
                else
                {
                    user.sessionStartDate = DateTime.Now;
                    filterContext.HttpContext.Session["User"] = user;
                }                
            }
        }

        //Runs after the OnAuthentication method  
        //------------//
        //OnAuthenticationChallenge:- if Method gets called when Authentication or Authorization is 
        //failed and this method is called after
        //Execution of Action Method but before rendering of View
        //------------//
        public void OnAuthenticationChallenge(AuthenticationChallengeContext filterContext)
        {
            //We are checking Result is null or Result is HttpUnauthorizedResult 
            // if yes then we are Redirect to Error View
            if (filterContext.Result == null || filterContext.Result is HttpUnauthorizedResult)
            {
                filterContext.Result = new ViewResult
                {
                    ViewName = "Error"
                };
            }
        }
    }
}