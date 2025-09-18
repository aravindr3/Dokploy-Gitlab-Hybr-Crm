using HyBrForex.Application.Wrappers;
using MediatR;

namespace HyBrForex.Application.Features.CommonTypeMasters.Commands.DeleteCommonTypeMsater;

public class DeleteCommonTypeMsater : IRequest<BaseResult>
{
    public string Id { get; set; }
}