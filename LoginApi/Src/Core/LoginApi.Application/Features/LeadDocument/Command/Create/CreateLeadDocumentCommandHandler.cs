using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using HyBrCRM.Application.Interfaces.Repositories;
using HyBrCRM.Domain.Exchange.Entities;
using HyBrForex.Application.Interfaces;
using HyBrForex.Application.Wrappers;
using MediatR;

namespace HyBrCRM.Application.Features.LeadDocument.Command.Create
{
    public class CreateLeadDocumentCommandHandler(
        ILeadDocumentServices leadDocumentServices,
       
        IUnitOfWork unitOfWork,
        ITranslator translator
    ) : IRequestHandler<CreateLeadDocumentCommand, BaseResult<string>>
    {
        public async Task<BaseResult<string>> Handle(CreateLeadDocumentCommand request, CancellationToken cancellationToken)
        {
            var pId = request.LeadId;
            var _uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "LeadDocument");
            if (!Directory.Exists(_uploadPath)) Directory.CreateDirectory(_uploadPath);

          

            var _file = request.File.FileName.Split('.');
            var filename = Ulid.NewUlid().ToString();
            string fileSizeStr = request.File.Length.ToString();
            string fileType = request.File.ContentType;

            var filePath = Path.Combine(_uploadPath, $"{filename}.{_file[1]}");
            var lead = new HyBrCRM.Domain.Exchange.Entities.LeadDocument(request.LeadId, filename,request?.Remark, fileType, fileSizeStr , null)
            {
                Id = Ulid.NewUlid().ToString()
            };

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await request.File.CopyToAsync(stream);
            }

            await leadDocumentServices.AddAsync(lead);
            await unitOfWork.SaveChangesAsync();

           

            var jsonObject = new
            {
                lead.Id,
                FileName = filename,
            };

            string jsonString = JsonSerializer.Serialize(jsonObject, new JsonSerializerOptions { WriteIndented = true });
            return jsonString;
        }

       
    }
    
}
