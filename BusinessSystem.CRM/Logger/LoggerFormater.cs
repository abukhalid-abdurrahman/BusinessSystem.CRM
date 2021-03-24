using Microsoft.AspNetCore.Mvc.Filters;
using System;

namespace BusinessSystem.CRM.Logger
{
    public class LoggerFormater
    {
        public static string InformationFormater(string message, string userName)
        {
            return $"Time: {DateTime.Now}->User: {userName}->Information Message: {message}";
        }
        public static string ErrorFormater(ExceptionContext exception, string userName)
        {
            return $"Time: {DateTime.Now}->User: {userName}->Error Message: Action[{exception.ActionDescriptor.DisplayName}] Excepton[{exception.Exception.Message}]";
        }
        public static string ErrorFormater(Exception exception, string userName)
        {
            return $"Time: {DateTime.Now}->User: {userName}->Error Message: Excepton[{exception.Message}]";
        }
    }
}
