using System.Collections.Generic;
using HyBrForex.Application.DTOs.AuthorizedDealer.Data;

namespace HyBrForex.Application.DTOs.AuthorizedDealer.Response;

public class DealerResponse
{
    public List<DealerData> Data { get; set; }
}