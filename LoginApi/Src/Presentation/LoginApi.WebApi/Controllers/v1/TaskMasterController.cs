using HyBrCRM.Application.Features.Lead.Command.Create;
using HyBrForex.Application.Wrappers;
using System.Threading.Tasks;
using HyBrForex.WebApi.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using HyBrCRM.Application.Features.TaskMaster.Command.Create;
using HyBrCRM.Application.Features.Lead.Command.Delete;
using HyBrCRM.Application.Features.TaskMaster.Command.Delete;
using HyBrCRM.Application.Features.Lead.Command.Update;
using HyBrCRM.Application.Features.TaskMaster.Command.Update;
using HyBrCRM.Application.Features.Lead.Query.GetById;
using HyBrCRM.Application.Features.TaskMaster.Queries.GetById;
using HyBrCRM.Application.Features.Lead.Query.GetAll;
using HyBrCRM.Application.Features.TaskMaster.Queries.GetAll;

namespace HyBrCRM.WebApi.Controllers.v1
{
    [ApiVersion ("1")]
    public class TaskMasterController : BaseApiController
    {
        [HttpPost]
        public async Task<BaseResult> CreateTask([FromBody] CreateTaskMasterCommand model)
        {
            return await Mediator.Send(model);
        }
        [HttpDelete]
        public async Task<BaseResult> DeleteTask([FromQuery] DeleteTaskMasterCommand model)
        {
            return await Mediator.Send(model);
        }
        [HttpPost]
        public async Task<BaseResult> UpdateTask([FromBody] UpdateTaskMasterCommad model)
        {
            return await Mediator.Send(model);
        }

        [HttpGet]
        public async Task<BaseResult> GetTaskById([FromQuery] GetTaskByIdQuery model)
        {
            return await Mediator.Send(model);
        }
        [HttpGet]
        public async Task<BaseResult> GetAllTask([FromQuery] GetAllTaskMasterQuery model)
        {
            return await Mediator.Send(model);
        }
    }
}
