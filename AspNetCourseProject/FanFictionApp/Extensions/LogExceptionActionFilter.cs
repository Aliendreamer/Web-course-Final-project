namespace FanFictionApp.Extensions
{
    using System;
    using FanFiction.Data;
    using FanFiction.Models;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Filters;
    using Microsoft.AspNetCore.Routing;

    public class LogExceptionActionFilter : ExceptionFilterAttribute
    {
        public LogExceptionActionFilter(FanFictionContext context)
        {
            this.Context = context;
        }

        public FanFictionContext Context { get; set; }

        public override void OnException(ExceptionContext context)
        {
            var user = context.HttpContext.User.Identity.Name ?? "anonymous";
            var exceptionMethod = context.Exception.TargetSite;
            var trace = context.Exception.StackTrace;
            var exception = context.Exception.GetType().Name;
            var time = DateTime.UtcNow.ToLongDateString();

            string message = $"Occurence: {time}  User: {user}  Path:{exceptionMethod}  Trace: {trace}";

            var log = new DbLog
            {
                Content = message,
                Handled = false,
                LogType = exception
            };
            this.Context.Logs.Add(log);
            this.Context.SaveChanges();

            context.ExceptionHandled = true;
            context.Result = new RedirectToRouteResult(new RouteValueDictionary
            {
                { "controller", "Home" },
                { "action", "Error" },
                {"area",""}
            });
        }
    }
}