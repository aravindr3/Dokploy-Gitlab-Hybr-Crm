using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HyBrCRM.Application.DTOs.Domain.Data;
using HyBrCRM.Application.DTOs.Verticals.Data;

namespace HyBrCRM.Application.DTOs.Domain.Response
{
    public class DomainResponse
    {
        public List<DomainData> domains { get; set; }

    }
}
