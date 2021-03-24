using System;

namespace BusinessSystem.CRM.Models
{
    public class ErrorViewModel
    {
        public int StatusCode { get; set; }
        public string StatusMessage { get; set; }
        public string RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}
