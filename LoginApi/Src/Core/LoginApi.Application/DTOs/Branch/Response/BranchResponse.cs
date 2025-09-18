using System.Collections.Generic;
using HyBrForex.Application.DTOs.Branch.Data;

namespace HyBrForex.Application.DTOs.Branch.Response;

public class BranchResponse
{
    public List<BranchData> branches { get; set; }
}