using HyBrForex.Application.DTOs.Feedback.Request;
using System.Threading.Tasks;
using LoginApi.WebApi.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using HyBrForex.Application.Interfaces.UserInterfaces;

namespace HyBrForex.WebApi.Controllers.v1
{
    [ApiVersion("1")]
    public class FeedbackController(IFeedbackService feedbackService) : BaseApiController
    {
        [HttpPost("submit")]
        public async Task<IActionResult> SubmitFeedback([FromForm] FeedbackRequest request)
        {
            var response = await feedbackService.SubmitFeedbackAsync(request);
            if (response.Data == null) return BadRequest(response.Errors);
            return CreatedAtAction(nameof(GetFeedbackById), new { id = response.Data.Id }, response.Data);

        }

        [HttpGet]
        public async Task<IActionResult> GetAllFeedback()
        {
            var feedbackList = await feedbackService.GetAllFeedbackAsync();
            return Ok(feedbackList);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetFeedbackById(string id)
        {
            var feedback = await feedbackService.GetFeedbackByIdAsync(id);
            if (feedback == null) return NotFound();
            return Ok(feedback);
        }
        [HttpGet("download")]
        public async Task<IActionResult> DownloadFile([FromQuery] string filePath)
        {
            var result = await feedbackService.DownloadFileAsync(filePath);

            if (result == null)
                return NotFound("File not found.");

            return File(result.Value.FileData, result.Value.ContentType, result.Value.FileName);
        }
    }
}
