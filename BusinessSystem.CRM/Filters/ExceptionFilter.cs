using BusinessSystem.CRM.Logger;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System;
using System.IO;

namespace BusinessSystem.CRM.Filters
{
    public class ExceptionFilter : Attribute, IExceptionFilter
    {
        private static readonly object _lock = new object();

        public void OnException(ExceptionContext context)
        {
            lock(_lock)
            {
                File.AppendAllText(Path.Combine(Directory.GetCurrentDirectory(), "logger.txt"), LoggerFormater.ErrorFormater(context, nameof(ExceptionFilter)) + Environment.NewLine);
            }
            context.ExceptionHandled = true;
        }
    }
}
