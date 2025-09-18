using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using HyBrCRM.Application.Features.Lead.Command.Update;
using HyBrCRM.Application.Interfaces.Repositories;
using HyBrCRM.Domain.Exchange.Entities;
using HyBrForex.Application.Interfaces;
using HyBrForex.Application.Interfaces.UserInterfaces;
using HyBrForex.Application.Wrappers;
using MediatR;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace HyBrCRM.Application.Features.TaskMaster.Command.Update
{
    public class UpdateTaskMasterCommandHandler(
        ITaskMasterServices taskMasterServices,
        IUserService userService,
    ILeadRepository leadRepository,
    IActivityLogServices activityLogServices,
    ILeadPropertiesValueServices leadPropertiesValueServices,
    IleadPropertyDefinitionServices leadPropertyDefinitionServices,
    IStagesServices stagesServices,
    IDomainStagesServices domainStagesServices,
    ILeadDocumentServices leadDocumentServices,
    IUnitOfWork unitOfWork) : IRequestHandler<UpdateTaskMasterCommad, BaseResult>
    {
        //public async Task<BaseResult> Handle(UpdateTaskMasterCommad request, CancellationToken cancellationToken)
        //{
        //    var master = await taskMasterServices.GetByIdAsync(request.Id);

        //    if (master is null) return new HyBrForex.Application.Wrappers.Error (ErrorCode.NotFound, "Task not found", nameof(request.Id));
        //    var userResult = await userService?.GetUserByIdAsync(request.OwnerId);
        //    if (userResult == null)
        //    {

        //        return new HyBrForex.Application.Wrappers.Error(ErrorCode.BadRequest, "Owner not found");
        //    }
        //    var ownerId = userResult?.Data?.Id;

        //    master.Update(
        //                     request.LeadId,
        //     ownerId,
        //    request.DomainStagesId,
        //    request.TaskDate,
        //    request.TaskNote,
        //    request?.TaskStatus,
        //    //request?.CountryInterestedIn ,
        //     string.Join(",", request.CountriesInterestedIn ?? new List<string>()),

        //    request?.UniversityPreferred ,
        //    request?.SubDescription,
        //    request?.DepositPaidUniversity,
        //    request?.CallType
        //        );
        //    await unitOfWork.SaveChangesAsync();
        //    DateTime date = DateTime.UtcNow;

        //    var activity = new Domain.Exchange.Entities.ActivityLog(
        //       master.LeadId,
        //       master.OwnerId,
        //       master.DomainStagesId,
        //    master.TaskNote,
        //    master.Created.Value,
        //     $"Updated on {(master.Created ?? date).ToString("yyyy-MM-dd HH:mm:ss")}",
        //     master?.CallType




        //      );
        //    activity.Id = Ulid.NewUlid().ToString();
        //    await activityLogServices.AddAsync(activity);
        //    await unitOfWork.SaveChangesAsync();

        //    return BaseResult.Ok();
        //}
        public async Task<BaseResult> Handle(UpdateTaskMasterCommad request, CancellationToken cancellationToken)
        {
            var master = await taskMasterServices.GetByIdAsync(request.Id);
            if (master is null)
                return new HyBrForex.Application.Wrappers.Error(ErrorCode.NotFound, "Task not found", nameof(request.Id));

            var userResult = await userService?.GetUserByIdAsync(request.OwnerId);
            if (userResult == null)
                return new HyBrForex.Application.Wrappers.Error(ErrorCode.BadRequest, "Owner not found");

            var ownerId = userResult?.Data?.Id;

            // ✅ Update task
            master.Update(
                request.LeadId,
                ownerId,
                request.DomainStagesId,
                request.TaskDate,
                request.TaskNote,
                request?.TaskStatus,
                string.Join(",", request.CountriesInterestedIn ?? new List<string>()),
                request?.UniversityPreferred,
                request?.SubDescription,
                request?.DepositPaidUniversity,
                request?.CallType
            );
            await unitOfWork.SaveChangesAsync();
            var ststus = await unitOfWork.SaveChangesAsync();
            //var ststus = await unitOfWork.SaveChangesAsync();

            if (ststus)
                if (request.IdTypes.Any())
                    foreach (var item in request.IdTypes)
                    {
                        var ProId = await leadDocumentServices.GetByIdAsync(item);
                        if (ProId != null)
                        {
                            ProId.LeadId = request.LeadId;
                            ProId.TaskId = master.Id;
                            leadDocumentServices.Update(ProId);
                            await unitOfWork.SaveChangesAsync();
                        }
                    }
            // ✅ Log activity
            DateTime date = DateTime.UtcNow;
            var activity = new Domain.Exchange.Entities.ActivityLog(
                master.LeadId,
                master.OwnerId,
                master.DomainStagesId,
                master.TaskNote,
                master.Created.Value,
                $"Updated on {(master.Created ?? date):yyyy-MM-dd HH:mm:ss}",
                master?.CallType
            );
            activity.Id = Ulid.NewUlid().ToString();
            await activityLogServices.AddAsync(activity);
            await unitOfWork.SaveChangesAsync();

            // ✅ Update LeadPropertyValues
            var leadPropDefs = (await leadPropertyDefinitionServices.GetAllAsync())
                .Where(d => d.Domain == request.DomainId && d.Status == 1)
                .ToList();

            if (leadPropDefs.Any())
            {
                var existingValues = (await leadPropertiesValueServices.GetAllAsync())
                    .Where(v => v.LeadId == request.LeadId && v.Status == 1)
                    .ToList();

                // ✅ Update OwnerId of all lead property values
                foreach (var value in existingValues)
                {
                    if (value.OwnerId != request.OwnerId)
                    {
                        value.OwnerId = request.OwnerId;
                        leadPropertiesValueServices.Update(value);
                    }
                }

                // ✅ Domain-specific value mapping
                foreach (var def in leadPropDefs)
                {
                    var stagesName = stagesServices.GetByIdAsync(domainStagesServices.GetByIdAsync(request?.DomainStagesId)?.Result?.StagesId).Result?.Name;
                    var subDescriptionName = stagesServices.GetByIdAsync(request?.SubDescription)?.Result?.Name;
                    string newValue = request.DomainId switch
                    {
                        // Domain 1: General Study

                        "01JXF2E06WSBC19T902TN878TH" => def.Id switch
                        {
                            "01JZT20S7BZKW3YGM05S5GVFY2" => string.Join(",", request.CountriesInterestedIn ?? new List<string>()),
                            "01JZT23EGX1EVH403ZNDQHY3ZF" => request.UniversityPreferred,
                            "DepositPaidUniversity" => request.DepositPaidUniversity,
                            "CallType" => request.CallType,
                            "TaskNote" => request.TaskNote,
                            "DocumentUpload" => stagesName.ToLower() == "documentupload"
                                     ? subDescriptionName
                                     : "",
                            "01JZT23EGX1EVH403ZNDQHY3ZF12" => stagesName.ToLower() == "offerletter "
                            ? subDescriptionName
                            : "",
                            "01JZT23EGX1EVH403ZNDQHY3ZF13" => stagesName.ToLower() == "visastatus"
                           ? subDescriptionName
                           : "",
                            _ => null
                        },

                        // Domain 2: Special Destination Lead
                        "01JZT3J2CSEKGNJPZ748WNW7J3" => def.Id switch
                        {
                            "01JZT223RGMGW12HZH9P4VFBJK1" => string.Join(",", request.CountriesInterestedIn ?? new List<string>()),

                            "01JZT20S7BZKW3YGM05S5GVFY21" => stagesName?.ToLower() == "requirement captured" ? "True" : "",
                            "01JZT3J2CSEKGNJPZ748WNW7J31" => stagesName?.ToLower() == "package shared" ? "True" : "",
                            "01JZT20S7BZKW3YGM05S5GVFY212" => stagesName?.ToLower() == "payment initiated" ? "True" : "",
                            _ => null,
                        },

                        // Add other domains if needed
                        _ => null
                    };

                    if (!string.IsNullOrWhiteSpace(newValue))
                    {
                        var existing = existingValues.FirstOrDefault(x => x.PropertyDefinitionId == def.Id);

                        if (existing != null)
                        {
                            if (existing.Value != newValue || existing.OwnerId != ownerId)
                            {
                                existing.Value = newValue;
                                existing.OwnerId = ownerId;
                                leadPropertiesValueServices.Update(existing);
                            }
                        }
                        else
                        {
                            var newProp = new LeadProperyValue(request.LeadId, ownerId, def.Id, newValue);
                            await leadPropertiesValueServices.AddAsync(newProp);
                        }
                    }
                }

                await unitOfWork.SaveChangesAsync();
            }

            return BaseResult.Ok();
        }


    }

}
