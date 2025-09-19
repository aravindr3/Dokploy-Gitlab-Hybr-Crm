using HyBrForex.Application.Wrappers;
using System.Threading.Tasks;
using HyBrForex.WebApi.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using HyBrCRM.Application.Features.LeadDocument.Command.Create;
using System.IO;
using HyBrCRM.Application.Interfaces.Repositories;
using System.Linq;
using System;
using HyBrCRM.Application.Features.LeadDocument.Command.Update;
using HyBrCRM.Application.Features.LeadDocument.Command.Delete;
namespace HyBrCRM.WebApi.Controllers.v1
{
    [ApiVersion("1")]
    public class LeadDocumentController(ILeadDocumentServices leadDocumentServices) : BaseApiController
    {
        [HttpPost]
        public async Task<BaseResult> CreateLeadDocument([FromForm] CreateLeadDocumentCommand model)
        {
            return await Mediator.Send(model);
        }

        [HttpGet("Download/{LeadFileName}")]
        public async Task<IActionResult> Lead(string LeadFileName)
        {
            var upload = await leadDocumentServices.GetByIdChildAsync(a => a.FileName == LeadFileName);
            if (upload == null || upload.Count == 0)
            {
                return NotFound("Lead upload  is not found");
            }

            var _uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "LeadDocument");
            if (!Directory.Exists(_uploadPath))
            {
                Directory.CreateDirectory(_uploadPath);
            }

            if (string.IsNullOrEmpty(LeadFileName))
            {
                return BadRequest("LeadFileName cannot be null or empty");
            }

            var matchingFiles = Directory.EnumerateFiles(_uploadPath)
                .Where(file => Path.GetFileNameWithoutExtension(file)
                .Equals(LeadFileName, StringComparison.OrdinalIgnoreCase));

            var filePath = matchingFiles.FirstOrDefault();
            if (filePath == null)
            {
                return NotFound("File not found in PurchaseUpload directory");
            }

            var extension = Path.GetExtension(filePath).ToLowerInvariant();
            var contentType = extension switch
            {
                ".txt" => "text/plain",
                ".jpg" => "image/jpeg",
                ".jpeg" => "image/jpeg",
                ".png" => "image/png",
                ".pdf" => "application/pdf",
                ".doc" => "application/msword",
                ".docx" => "application/vnd.openxmlformats-officedocument.wordprocessingml.document",
                ".xlsx" => "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                ".csv" => "text/csv",
                _ => "application/octet-stream"
            };

            var fileBytes = await System.IO.File.ReadAllBytesAsync(filePath);

            return File(fileBytes, contentType, Path.GetFileName(filePath));
        }
        [HttpPost]
        public async Task<BaseResult> UpdateleadDocument([FromForm] UpdateLeadDocumentCommand model)
        {
            return await Mediator.Send(model);
        }
        [HttpDelete]
        public async Task<BaseResult> DeleteLeadDocument([FromQuery] DeleteLeadDocumentCommand model)
=> await Mediator.Send(model);

    }
}
