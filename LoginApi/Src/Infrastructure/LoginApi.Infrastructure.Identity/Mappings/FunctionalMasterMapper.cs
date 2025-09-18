using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HyBrForex.Application.DTOs.FunctionalMaster.Requests;
using HyBrForex.Application.DTOs.FunctionalMaster.Responses;
using HyBrForex.Infrastructure.Identity.Models;
using LoginApi.Infrastructure.Identity.Models;

namespace HyBrForex.Infrastructure.Identity.Mappings
{
    public static class FunctionalMasterMapper
    {
        public static FunctionalMaster ToEntity(FunctionalMasterRequest request)
        {
            return new FunctionalMaster
            {
                Id = Ulid.NewUlid().ToString(),
                FunctionalName = request.FunctionalName,
                RouterName = request.RouterName,
                RouterDescription = request.RouterDescription,
                RouterLink = request.RouterLink,
                SequenceId = request.SequenceId,
                Icon = request.Icon,
                ParentId = request.ParentId,
                isHeader = request.isHeader,
                isSubHeader = request.isSubHeader,
                Orderby = request.Orderby,
                // Convert the list from the request to an array.
                ChildRouterLinks = request.ChildRouterLinks?.ToArray() ?? new string[0]
            };
        }

        public static FunctionalMasterResponse ToResponse(FunctionalMaster entity)
        {
            return new FunctionalMasterResponse
            {
                Id = entity.Id,
                FunctionalName = entity.FunctionalName,
                RouterName = entity.RouterName,
                RouterDescription = entity.RouterDescription,
                RouterLink = entity.RouterLink,
                SequenceId = entity.SequenceId,
                Icon = entity.Icon,
                ParentId = entity.ParentId,
                isHeader = entity.isHeader,
                isSubHeader = entity.isSubHeader,
                Orderby = entity.Orderby,
                // Convert the array to a list for the response.
                ChildRouterLinks = entity.ChildRouterLinks?.ToList() ?? new List<string>()
            };
        }

        // For updating an existing entity.
        public static void Map(FunctionalMasterRequest request, FunctionalMaster entity)
        {
            entity.FunctionalName = request.FunctionalName;
            entity.RouterName = request.RouterName;
            entity.RouterDescription = request.RouterDescription;
            entity.RouterLink = request.RouterLink;
            entity.SequenceId = request.SequenceId;
            entity.Icon = request.Icon;
            entity.ParentId = request.ParentId;
            entity.isHeader = request.isHeader;
            entity.isSubHeader = request.isSubHeader;
            // Optionally update Orderby, or leave it as determined by your business logic.
            entity.Orderby = request.Orderby;
            entity.ChildRouterLinks = request.ChildRouterLinks?.ToArray() ?? new string[0];
        }
    }

}
