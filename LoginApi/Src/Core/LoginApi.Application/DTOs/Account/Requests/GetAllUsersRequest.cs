using HyBrForex.Application.Parameters;

namespace HyBrForex.Application.DTOs.Account.Requests;

public class GetAllUsersRequest : PaginationRequestParameter
{
    public string Name { get; set; }
}