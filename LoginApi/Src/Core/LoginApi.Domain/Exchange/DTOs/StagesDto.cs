using System;
using System.Collections.Generic;
using System.Text;
using HyBrCRM.Domain.Exchange.Entities;

namespace HyBrCRM.Domain.Exchange.DTOs
{
    public class StagesDto
    {
        public StagesDto(Stages stages)
        {
            Id = stages.Id;
            Name = stages.Name;
            Status = stages.Status;
        }
        public string ? Id { get; set; }
        public string? Name { get; set; }
        public int Status { get; set; }
    }
}
