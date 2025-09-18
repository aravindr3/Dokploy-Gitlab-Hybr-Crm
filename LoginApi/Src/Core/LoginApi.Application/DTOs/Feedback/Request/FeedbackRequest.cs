using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace HyBrForex.Application.DTOs.Feedback.Request
{
    public class FeedbackRequest
    {
        public string PageTitle { get; set; }
        public string FeedbackType { get; set; }
        public string Email { get; set; }
        public string Comments { get; set; }
        public IFormFile? File { get; set; }
    }
}
