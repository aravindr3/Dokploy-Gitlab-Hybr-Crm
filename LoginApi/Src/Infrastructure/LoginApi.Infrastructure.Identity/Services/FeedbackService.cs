using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using HyBrForex.Application.DTOs.Feedback.Request;
using HyBrForex.Application.DTOs.Feedback.Response;
using HyBrForex.Application.Interfaces.UserInterfaces;
using HyBrForex.Application.Wrappers;
using HyBrForex.Infrastructure.Identity.Models;
using LoginApi.Infrastructure.Identity.Contexts;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

namespace HyBrForex.Infrastructure.Identity.Services
{
    public class FeedbackService(IdentityContext identityContext, IWebHostEnvironment environment, IMapper mapper) : IFeedbackService
    {
        public async Task<BaseResult<FeedbackResponse>> SubmitFeedbackAsync(FeedbackRequest request)
        {
            string? filePath = null;

            // Handle file upload
            if (request.File != null)
            {
                var uploadDir = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "feedback");
                Directory.CreateDirectory(uploadDir);

                var fileName = $"{Guid.NewGuid()}_{request.File.FileName}";
                var fullPath = Path.Combine(uploadDir, fileName);

                using (var stream = new FileStream(fullPath, FileMode.Create))
                {
                    await request.File.CopyToAsync(stream);
                }

                filePath = $"/feedback/{fileName}"; // Relative path for serving
            }

            var feedback = mapper.Map<Feedback>(request);
            feedback.Id = Ulid.NewUlid().ToString(); // Assign Ulid ID
            feedback.FilePath = filePath;

            identityContext.feedbacks.Add(feedback);
            await identityContext.SaveChangesAsync();

            return mapper.Map<FeedbackResponse>(feedback);
        }

        public async Task<IEnumerable<FeedbackResponse>> GetAllFeedbackAsync()
        {
            var feedbackList = await identityContext.feedbacks
                .OrderByDescending(f => f.CreatedAt)
                .ToListAsync();

            return mapper.Map<IEnumerable<FeedbackResponse>>(feedbackList);
        }

        

        public async Task<BaseResult<FeedbackResponse?>> GetFeedbackByIdAsync(string id)
        {
            var feedback = await identityContext.feedbacks.FindAsync(id);
            return feedback == null ? null : mapper.Map<FeedbackResponse>(feedback);
        }
        public async Task<(byte[] FileData, string ContentType, string FileName)?> DownloadFileAsync(string filePath)
        {
            if (string.IsNullOrEmpty(filePath))
                return null;

            // Get the actual project root (move up from bin/Debug/net9.0 to the correct directory)
            var basePath = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "../../../../../Presentation/LoginApi.WebApi/"));

            // Combine the correct file path
            var fileFullPath = Path.Combine(basePath, filePath.TrimStart('/').Replace("/", Path.DirectorySeparatorChar.ToString()));

            if (!File.Exists(fileFullPath))
                return null;

            var provider = new FileExtensionContentTypeProvider();
            if (!provider.TryGetContentType(fileFullPath, out var contentType))
            {
                contentType = "application/octet-stream"; // Default MIME type if unknown
            }

            var fileBytes = await File.ReadAllBytesAsync(fileFullPath);
            var fileName = Path.GetFileName(fileFullPath);

            return (fileBytes, contentType, fileName);
        }
    }
}