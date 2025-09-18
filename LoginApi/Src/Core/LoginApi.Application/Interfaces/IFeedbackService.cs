using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HyBrForex.Application.DTOs.Feedback.Request;
using HyBrForex.Application.DTOs.Feedback.Response;
using HyBrForex.Application.Wrappers;

namespace HyBrForex.Application.Interfaces.UserInterfaces
{
    public interface IFeedbackService
    {
        Task<BaseResult<FeedbackResponse>> SubmitFeedbackAsync(FeedbackRequest request);
        Task<IEnumerable<FeedbackResponse>> GetAllFeedbackAsync();
        Task<BaseResult<FeedbackResponse?>> GetFeedbackByIdAsync(string id);
        Task<(byte[] FileData, string ContentType, string FileName)?> DownloadFileAsync(string filePath);

    }
}
