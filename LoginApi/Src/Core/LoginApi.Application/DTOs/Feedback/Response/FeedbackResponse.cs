using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HyBrForex.Application.DTOs.Feedback.Response
{
    public class FeedbackResponse
    {
        public string Id { get; set; }
        public string PageTitle { get; set; }
        public string FeedbackType { get; set; }
        public string Email { get; set; }
        public string Comments { get; set; }
        public string? FilePath { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
