using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using HyBrCRM.Application.Interfaces.Repositories;
using HyBrForex.Application.Interfaces;
using HyBrForex.Application.Wrappers;
using MediatR;

namespace HyBrCRM.Application.Features.LeadDocument.Command.Update
{
    public class UpdateLeadDocumentCommandHandler(
    ILeadDocumentServices leadDocumentServices,
  
    IUnitOfWork unitOfWork) : IRequestHandler<UpdateLeadDocumentCommand, BaseResult>
    {


        public async Task<BaseResult> Handle(UpdateLeadDocumentCommand request, CancellationToken cancellationToken)
        {
            var profileids = await leadDocumentServices.GetByIdChildAsync(a => a.Id == request.Id);
            var pid = request.Id;
            var taskId = profileids.FirstOrDefault()?.TaskId;

            var profileid = profileids.FirstOrDefault();
           
            if (profileid is null || profileid.Status == 0)
                return new HyBrForex.Application.Wrappers.Error(ErrorCode.NotFound, " Lead document Id is not found", nameof(request.Id));

            string filename = profileid.FileName;
            string fileType = profileid.FileType;
            string fileSizeStr = profileid.FileSize;

            var _uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "LeadDocument");

            if (request.File != null)
            {
                if (!Directory.Exists(_uploadPath))
                    Directory.CreateDirectory(_uploadPath);

                if (!string.IsNullOrEmpty(profileid.FileName) && !string.IsNullOrEmpty(profileid.FileType))
                {
                    var oldFilePath = Path.Combine(_uploadPath, $"{profileid.FileName}.{profileid.FileType}");

                    if (File.Exists(oldFilePath))
                    {
                        try
                        {
                            File.Delete(oldFilePath);
                        }
                        catch (Exception ex)
                        {
                            return new HyBrForex.Application.Wrappers.Error(ErrorCode.NotFound, "Lead Document is not found", nameof(request.Id));


                        }
                    }
                }

                var _file = request.File.FileName.Split('.');
                filename = Ulid.NewUlid().ToString();
                fileSizeStr = request.File.Length.ToString();
                fileType = _file.Length > 1 ? _file[^1] : string.Empty;

                var filePath = Path.Combine(_uploadPath, $"{filename}.{fileType}");
                using (var stream = new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.None))
                {
                    await request.File.CopyToAsync(stream);
                }
            }

            profileid.Update(
                request.Remark,
               
                filename,
                fileType,
                fileSizeStr,
                taskId
            );


            await unitOfWork.SaveChangesAsync();
           
            var jsonObject = new
            {
                Id = profileid.Id,
                FileName = profileid.FileName,
            };

            string jsonString = JsonSerializer.Serialize(jsonObject, new JsonSerializerOptions { WriteIndented = true });
            return BaseResult<string>.Ok(jsonString);
        }

    
    }
}
