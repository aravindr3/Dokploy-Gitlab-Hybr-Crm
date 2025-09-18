using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using HyBrCRM.Application.Interfaces.Repositories;
using HyBrForex.Application.Interfaces;
using HyBrForex.Application.Wrappers;
using MediatR;

namespace HyBrCRM.Application.Features.LeadDocument.Command.Delete
{
    public class DeleteLeadDocumentCommandHandler(ILeadDocumentServices leadDocumentServices,
    IUnitOfWork unitOfWork) : IRequestHandler<DeleteLeadDocumentCommand, BaseResult>
    {
        public async Task<BaseResult> Handle(DeleteLeadDocumentCommand request, CancellationToken cancellationToken)
        {
            var upload = await leadDocumentServices.GetByIdAsync(request.Id);

            if (upload is null)
                return new Error(ErrorCode.NotFound, "Lead Document Upload is not found", nameof(request.Id));


            leadDocumentServices.Delete(upload);

            var _uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "LeadDocument");

            if (!string.IsNullOrEmpty(request.Id))
            {
                var matchingFiles = Directory.EnumerateFiles(_uploadPath)
                    .Where(file => Path.GetFileNameWithoutExtension(file)
                    .Equals(upload.FileName, StringComparison.OrdinalIgnoreCase));

                var filePath = matchingFiles.FirstOrDefault();

                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                }


            }

            await unitOfWork.SaveChangesAsync();
            return BaseResult.Ok();
        }

    }
    
    
}
