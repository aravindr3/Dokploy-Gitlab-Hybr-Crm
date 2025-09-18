using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using HyBrCRM.Application.Features.Lead.Command.Create;
using HyBrCRM.Application.Interfaces.Repositories;
using HyBrCRM.Application.Interfaces.UserInterfaces;
using HyBrCRM.Domain.Bonvoice.DTO.Request;
using HyBrCRM.Domain.Exchange.Entities;
using HyBrForex.Application.Interfaces;
using HyBrForex.Application.Interfaces.UserInterfaces;
using HyBrForex.Application.Wrappers;
using MediatR;
using Microsoft.Extensions.Options;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Security.Cryptography;

namespace HyBrCRM.Application.Features.TaskMaster.Command.Create
{
    public class CreateTaskMasterCommandHandler(
        ITaskMasterServices taskMasterServices,
        IUserService userService,
        IDomainRepository domainRepository,
        IActivityLogServices activityLogServices,
        ILeadPropertiesValueServices leadPropertiesValueServices,
        IleadPropertyDefinitionServices leadPropertyDefinitionServices,
        IStagesServices stagesServices, IDomainStagesServices domainStagesServices,
        ILeadDocumentServices leadDocumentServices,
    ILeadRepository leadRepository,
    ILeadContactRepository leadContactRepository,
    IHolidaysLeadServices holidaysLeadServices,
    IBonVoiceCallServices bonVoiceCallServices,
 IBonvoiceSettings bonvoiceSettings,
 IAutoCallSettings autoCallSettings,
    IUnitOfWork unitOfWork) : IRequestHandler<CreateTaskMasterCommand, BaseResult<string>>
    {



        
        public async Task<BaseResult<string>> Handle(CreateTaskMasterCommand request, CancellationToken cancellationToken)
        {
            var userResult = await userService?.GetUserByIdAsync(request.OwnerId);
            if (userResult == null)
                return new HyBrForex.Application.Wrappers.Error(ErrorCode.BadRequest, "Owner not found");
            var stages = stagesServices.GetByIdAsync(domainStagesServices?.GetByIdAsync(request?.DomainStagesId)?.Result?.StagesId)?.Result?.Id;
            var ownerId = userResult?.Data?.Id;
            DateTime date = DateTime.UtcNow;
            var existingTasks = await taskMasterServices?.GetByIdChildAsync(x => x.LeadId == request.LeadId); 
            var isInitialTask = !existingTasks.Any();
            var lastTask = existingTasks?.OrderByDescending(t => t.Created)?.FirstOrDefault()?.Id;

            // If NOT initial task  and task is call 

            if (!isInitialTask && request?.DomainStagesId == "01JYJY1J0T6247H5ZCXCESYSX211")
            {
                var leadContact = "";
                var leadContactNumber = "";
                var ownerContactNumber = userResult?.Data?.PhoneNumber;
               
                if (request.DomainId == "01JXF2E06WSBC19T902TN878TH")
                {
                    leadContact = holidaysLeadServices.GetByIdAsync(request?.LeadId)?.Result?.LeadContactId;
                    leadContactNumber = leadContactRepository.GetByIdAsync(leadContact)?.Result?.PhoneNumber1;
                }
                else if (request.DomainId == "01JZT3J2CSEKGNJPZ748WNW7J3")
                {
                    leadContact = leadRepository.GetByIdAsync(request?.LeadId)?.Result?.LeadContactId;
                    leadContactNumber = leadContactRepository.GetByIdAsync(leadContact)?.Result?.PhoneNumber1;
                }
                if (ownerContactNumber == "")
                {
                    return new HyBrForex.Application.Wrappers.Error(ErrorCode.NotFound, "User Phomne Number not found");
                }
                if (leadContactNumber == "")
                {
                    return new HyBrForex.Application.Wrappers.Error(ErrorCode.NotFound, "Lead Phomne Number not found");
                }

                var authRequest = new AuthRequest
                {
                    Username = bonvoiceSettings.Username,
                    Password = bonvoiceSettings.Password,
                    LeadId = request?.LeadId,
                    TaskId = lastTask,
                };

                var callRequest = new AutoCallRequestDto
                {
                    AutocallType = autoCallSettings.AutocallType,
                    Destination = ownerContactNumber,
                    RingStrategy = autoCallSettings?.RingStrategy,
                    LegACallerID = autoCallSettings?.LegACallerID,
                    LegAChannelID = autoCallSettings?.LegAChannelID,
                    LegADialAttempts = autoCallSettings?.LegADialAttempts,
                    LegBDestination = leadContactNumber,
                    LegBCallerID = autoCallSettings?.LegBCallerID,
                    LegBChannelID = autoCallSettings?.LegBChannelID,
                    LegBDialAttempts = autoCallSettings?.LegBDialAttempts,
                    EventID = GenerateRandomString(13)
                };

                var result = await bonVoiceCallServices.AutoCallBridgeAsync(authRequest, callRequest);

                // Create activity log for the call
                var activity1 = new Domain.Exchange.Entities.ActivityLog(
                    request.LeadId,
                    ownerId,
                    request.DomainStagesId,
                    request.TaskNote,
                    DateTime.UtcNow,
                    $"Call initiated for task {lastTask}",
                    request?.CallType
                )
                {
                    Id = Ulid.NewUlid().ToString()
                };
                await activityLogServices.AddAsync(activity1);

                // Update lead properties
                var leadPropDefs1 = (await leadPropertyDefinitionServices.GetAllAsync())
                    .Where(d => d.Domain == request.DomainId && d.Status == 1)
                    .ToList();

                var existingValues1 = (await leadPropertiesValueServices.GetAllAsync())
                    .Where(v => v.LeadId == request.LeadId && v.Status == 1)
                    .ToList();

                // Update owner for existing values
                foreach (var value in existingValues1)
                {
                    if (value.OwnerId != request.OwnerId)
                    {
                        value.OwnerId = request.OwnerId;
                        leadPropertiesValueServices.Update(value);
                    }
                }

                // Domain-specific property updates
                if (request.DomainId == "01JXF2E06WSBC19T902TN878TH")
                {
                    foreach (var def in leadPropDefs1)
                    {
                        var stagesName = stagesServices.GetByIdAsync(domainStagesServices.GetByIdAsync(request?.DomainStagesId)?.Result?.StagesId).Result?.Name;
                        var subDescriptionName = stagesServices.GetByIdAsync(domainStagesServices.GetByIdAsync(request?.SubDescription)?.Result?.StagesId)?.Result?.Name;

                        string newValue = def.Id switch
                        {
                            "01JZT20S7BZKW3YGM05S5GVFY2" => string.Join(",", request.CountriesInterestedIn ?? new List<string>()),
                            "01JZT23EGX1EVH403ZNDQHY3ZF" => request.UniversityPreferred,
                            "DepositPaidUniversity" => request.DepositPaidUniversity,
                            "CallType" => request.CallType,
                            "TaskNote" => request.TaskNote,
                            "01JZT223RGMGW12HZH9P4VFBJK" => stagesName?.ToLower() == "document upload" ? subDescriptionName : "",
                            "01JZT23EGX1EVH403ZNDQHY3ZF12" => stagesName?.ToLower() == "offerletter " ? subDescriptionName : "",
                            "01JZT23EGX1EVH403ZNDQHY3ZF13" => stagesName?.ToLower() == "visa status" ? subDescriptionName : "",
                            _ => null
                        };

                        if (!string.IsNullOrWhiteSpace(newValue))
                        {
                            var existing = existingValues1.FirstOrDefault(x => x.PropertyDefinitionId == def.Id);
                            if (existing != null)
                            {
                                leadPropertiesValueServices.Delete(existing);
                            }

                            var newProp = new LeadProperyValue(request.LeadId, ownerId, def.Id, newValue)
                            {
                                Id = Ulid.NewUlid().ToString()
                            };
                            await leadPropertiesValueServices.AddAsync(newProp);
                        }
                    }
                }
                else if (request.DomainId == "01JZT3J2CSEKGNJPZ748WNW7J3")
                {
                    foreach (var def in leadPropDefs1)
                    {
                        var stagesName = stagesServices.GetByIdAsync(domainStagesServices.GetByIdAsync(request?.DomainStagesId)?.Result?.StagesId).Result?.Name;
                        var subDescriptionName = stagesServices.GetByIdAsync(domainStagesServices.GetByIdAsync(request?.SubDescription)?.Result?.StagesId)?.Result?.Name;
                        string newValue = def.Id switch
                        {
                            "01JZT223RGMGW12HZH9P4VFBJK1" => string.Join(",", request.CountriesInterestedIn ?? new List<string>()),
                            
                            "01JZT20S7BZKW3YGM05S5GVFY21" => stagesName?.ToLower() == "requirement captured" ? "True" : "",
                            "01JZT3J2CSEKGNJPZ748WNW7J31" => stagesName?.ToLower()== "package shared" ?"True" : "",
                            "01JZT20S7BZKW3YGM05S5GVFY212" => stagesName?.ToLower()== "payment initiated" ? "True" : "",
                            _=>null,
                        };

                        if (!string.IsNullOrWhiteSpace(newValue))
                        {
                            var existing = existingValues1.FirstOrDefault(x => x.PropertyDefinitionId == def.Id);
                            if (existing != null)
                            {
                                leadPropertiesValueServices.Delete(existing);
                            }

                            var newProp = new LeadProperyValue(request.LeadId, ownerId, def.Id, newValue)
                            {
                                Id = Ulid.NewUlid().ToString()
                            };
                            await leadPropertiesValueServices.AddAsync(newProp);
                        }
                    }
                }

                // Update task master status
                var taskToUpdate = await taskMasterServices.GetByIdAsync(lastTask);
                if (taskToUpdate != null)
                {
                    taskToUpdate.TaskStatus = "Completed";
                    taskMasterServices.Update(taskToUpdate);
                }

                await unitOfWork.SaveChangesAsync();

                return new BaseResult<string>
                {
                    Success = result.Success,
                    Data = lastTask
                };
            }

            // Proceed with task creation (only for initial task)

            var domainStagesId = isInitialTask && request?.DomainStagesId== "01JYJY1J0T6247H5ZCXCESYSX211" ? "01JYJY1J0T6247H5ZCXCESYSX2" : request?.DomainStagesId;
            var taskNote = isInitialTask && request?.DomainStagesId == "01JYJY1J0T6247H5ZCXCESYSX211" ? "initial task" : request?.TaskNote;

            var master = new Domain.Exchange.Entities.TaskMaster(
                request.LeadId,
                ownerId,
                domainStagesId,
                request.TaskDate,
                taskNote,
                request?.TaskStatus,
                string.Join(",", request.CountriesInterestedIn ?? new List<string>()),
                request?.UniversityPreferred,
                request?.SubDescription,
                request?.DepositPaidUniversity,
                request?.CallType
            )
            {
                Id = Ulid.NewUlid().ToString()
            };

            await taskMasterServices.AddAsync(master);
            var ststus = await unitOfWork.SaveChangesAsync();

            // If applicable, trigger call after task creation

            if (ststus && request?.DomainStagesId == "01JYJY1J0T6247H5ZCXCESYSX211")
            {
                var leadContact = "";
                var leadContactNumber = "";
                var ownerContactNumber = userResult?.Data?.PhoneNumber;

                if (request.DomainId == "01JXF2E06WSBC19T902TN878TH")
                {
                    leadContact = holidaysLeadServices.GetByIdAsync(request?.LeadId)?.Result?.LeadContactId;
                    leadContactNumber = leadContactRepository.GetByIdAsync(leadContact)?.Result?.PhoneNumber1;
                }
                else if (request.DomainId == "01JZT3J2CSEKGNJPZ748WNW7J3")
                {
                    leadContact = leadRepository.GetByIdAsync(request?.LeadId)?.Result?.LeadContactId;
                    leadContactNumber = leadContactRepository.GetByIdAsync(leadContact)?.Result?.PhoneNumber1;
                }
                if (ownerContactNumber == "")
                {
                    return new HyBrForex.Application.Wrappers.Error(ErrorCode.NotFound, "User Phomne Number not found");
                }
                if (leadContactNumber == "")
                {
                    return new HyBrForex.Application.Wrappers.Error(ErrorCode.NotFound, "Lead Phomne Number not found");
                }
                var authRequest = new AuthRequest
                {
                    Username = bonvoiceSettings.Username,
                    Password = bonvoiceSettings.Password,
                    LeadId = request?.LeadId,
                    TaskId = master?.Id,
                };

                var callRequest = new AutoCallRequestDto
                {
                    AutocallType = autoCallSettings.AutocallType,
                    Destination = ownerContactNumber,
                    RingStrategy = autoCallSettings?.RingStrategy,
                    LegACallerID = autoCallSettings?.LegACallerID,
                    LegAChannelID = autoCallSettings?.LegAChannelID,
                    LegADialAttempts = autoCallSettings?.LegADialAttempts,
                    LegBDestination = leadContactNumber,
                    LegBCallerID = autoCallSettings?.LegBCallerID,
                    LegBChannelID = autoCallSettings?.LegBChannelID,
                    LegBDialAttempts = autoCallSettings?.LegBDialAttempts,
                    EventID = GenerateRandomString(13)
                };

                var result = await bonVoiceCallServices.AutoCallBridgeAsync(authRequest, callRequest);

                if (!result.Success)
                {
                  return result.Errors;
                }
            }

            // Document binding
            if (request.IdTypes.Any())
            {
                foreach (var item in request.IdTypes)
                {
                    var doc = await leadDocumentServices.GetByIdAsync(item);
                    if (doc != null)
                    {
                        doc.LeadId = request.LeadId;
                        doc.TaskId = master.Id;
                        leadDocumentServices.Update(doc);
                        await unitOfWork.SaveChangesAsync();
                    }
                }
            }

            // Log Activity
            var activity = new Domain.Exchange.Entities.ActivityLog(
                master.LeadId,
                master.OwnerId,
                master.DomainStagesId,
                master.TaskNote,
                master.Created.Value,
                $"Created on {(master.Created ?? date):yyyy-MM-dd HH:mm:ss}",
                master?.CallType
            )
            {
                Id = Ulid.NewUlid().ToString()
            };

            await activityLogServices.AddAsync(activity);
            await unitOfWork.SaveChangesAsync();

            // Update existing lead properties
            var leadPropDefs = (await leadPropertyDefinitionServices.GetAllAsync())
                .Where(d => d.Domain == request.DomainId && d.Status == 1)
                .ToList();

            var existingValues = (await leadPropertiesValueServices.GetAllAsync())
                .Where(v => v.LeadId == request.LeadId && v.Status == 1)
                .ToList();

            foreach (var value in existingValues)
            {
                if (value.OwnerId != request.OwnerId)
                {
                    value.OwnerId = request.OwnerId;
                    leadPropertiesValueServices.Update(value);
                }
            }

            if (request.DomainId == "01JXF2E06WSBC19T902TN878TH")
            {
                foreach (var def in leadPropDefs)
                {
                    var stagesName = stagesServices.GetByIdAsync(domainStagesServices.GetByIdAsync(request?.DomainStagesId)?.Result?.StagesId).Result?.Name;
                    var subDescriptionName = stagesServices.GetByIdAsync(domainStagesServices.GetByIdAsync(request?.SubDescription)?.Result?.StagesId)?.Result?.Name;

                    string newValue = def.Id switch
                    {
                        "01JZT20S7BZKW3YGM05S5GVFY2" => string.Join(",", request.CountriesInterestedIn ?? new List<string>()),
                        "01JZT23EGX1EVH403ZNDQHY3ZF" => request.UniversityPreferred,
                        "DepositPaidUniversity" => request.DepositPaidUniversity,
                        "CallType" => request.CallType,
                        "TaskNote" => request.TaskNote,
                        "01JZT223RGMGW12HZH9P4VFBJK" => stagesName.ToLower() == "document upload" ? subDescriptionName : "",
                        "01JZT23EGX1EVH403ZNDQHY3ZF12" => stagesName.ToLower() == "offerletter " ? subDescriptionName : "",
                        "01JZT23EGX1EVH403ZNDQHY3ZF13" => stagesName.ToLower() == "visa status" ? subDescriptionName : "",
                        _ => null
                    };

                    if (!string.IsNullOrWhiteSpace(newValue))
                    {
                        var existing = existingValues.FirstOrDefault(x => x.PropertyDefinitionId == def.Id);
                        if (existing != null)
                        {
                            leadPropertiesValueServices.Delete(existing);
                        }

                        var newProp = new LeadProperyValue(request.LeadId, ownerId, def.Id, newValue)
                        {
                            Id = Ulid.NewUlid().ToString()
                        };

                        await leadPropertiesValueServices.AddAsync(newProp);
                    }
                }
            }

            if (request.DomainId == "01JZT3J2CSEKGNJPZ748WNW7J3")
            {
                foreach (var def in leadPropDefs)
                {
                    var stagesName = stagesServices.GetByIdAsync(domainStagesServices.GetByIdAsync(request?.DomainStagesId)?.Result?.StagesId).Result?.Name;

                    string newValue = def.Id switch
                    {
                        "01JZT223RGMGW12HZH9P4VFBJK1" => string.Join(",", request.CountriesInterestedIn ?? new List<string>()),

                        "01JZT20S7BZKW3YGM05S5GVFY21" => stagesName?.ToLower() == "requirement captured" ? "True" : "",
                        "01JZT3J2CSEKGNJPZ748WNW7J31" => stagesName?.ToLower() == "package shared" ? "True" : "",
                        "01JZT20S7BZKW3YGM05S5GVFY212" => stagesName?.ToLower() == "payment initiated" ? "True" : "",
                        _ => null,
                    };

                    if (!string.IsNullOrWhiteSpace(newValue))
                    {
                        var existing = existingValues.FirstOrDefault(x => x.PropertyDefinitionId == def.Id);
                        if (existing != null)
                        {
                            leadPropertiesValueServices.Delete(existing);
                        }

                        var newProp = new LeadProperyValue(request.LeadId, ownerId, def.Id, newValue)
                        {
                            Id = Ulid.NewUlid().ToString()
                        };

                        await leadPropertiesValueServices.AddAsync(newProp);
                    }
                }
            }

            await unitOfWork.SaveChangesAsync();


            //  to mark the just-created master as "Completed":

            var masterupdate = await taskMasterServices.GetByIdAsync(request.Id);
            var masterComplete = await taskMasterServices.GetByIdAsync(master.Id);
            if (stages == "01JZW1ZKZW2QR6GAXSQQDBGAF4" || stages == "01JZW1ZE1N07568Y7XQTY9ZKQZ" || stages == "01JZW5612T7SP0F0N0HW4WQS0D")

                if (masterComplete != null)
                {
                    masterComplete.TaskStatus = "Completed";
                    taskMasterServices.Update(masterComplete);
                    await unitOfWork.SaveChangesAsync();
                }


            if (masterupdate != null)
            {
                masterupdate.TaskStatus = "Completed";
                taskMasterServices.Update(masterupdate);
                await unitOfWork.SaveChangesAsync();
            }

            return master.Id;
        }



        public string GenerateRandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var data = new byte[length];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(data);
            }

            var result = new char[length];
            for (int i = 0; i < length; i++)
            {
                result[i] = chars[data[i] % chars.Length];
            }

            return new string(result);
        }

    }
}



