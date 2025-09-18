using System;
using System.Collections.Generic;
using System.Text;
using HyBrCRM.Domain.Exchange.Entities;

namespace HyBrCRM.Domain.Exchange.DTOs
{
    public class LeadContactDto
    {
        public LeadContactDto()
        {
        }

        public LeadContactDto(LeadContact leadContact)
        {
            Id  = leadContact.Id;
            FirstName = leadContact.FirstName;
            LastName = leadContact.LastName;
            PhoneNumber1 = leadContact.PhoneNumber1;
            PhoneNumber2 = leadContact.PhoneNumber2;
            WhatsAppNumber = leadContact.WhatsAppNumber;
            Email = leadContact.Email;
            GenderId = leadContact.GenderId;
            GenderName = leadContact?.Gender?.CommonName;
            ParentsName = leadContact?.ParentsName;
            ParentsPhoneNumber = leadContact?.ParentsPhoneNumber;
            CountryId = leadContact?.CountryId;
            CountryName = leadContact?.Country?.CountryName;
            StateId = leadContact?.StateId;
            StateName = leadContact?.State?.StateName;
            District = leadContact?.District;
            City = leadContact?.City;
            Locality = leadContact?.Locality;
            VerticalId = leadContact?.VerticalId;
            Status = leadContact?.Status;

        }
        public string ? Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? PhoneNumber1 { get; set; }
        public string? PhoneNumber2 { get; set; }
        public string? WhatsAppNumber { get; set; }
        public string? Email { get; set; }
        public string? GenderId { get; set; }
        public string GenderName { get; set; }
        public string? ParentsName { get; set; }
        public string? ParentsPhoneNumber { get; set; }
        public string? CountryId { get; set; }
        public string ? CountryName { get; set; }
        public string? StateId { get; set; }
        public string ? StateOutsideIndia { get; set; }
        public string ? StateName { get; set; }
        public string? District { get; set; }
        public string? City { get; set; }
        public string? Locality { get; set; }
        public string? VerticalId { get; set; }
        public string ? VerticalName { get; set; }
        public int ? Status { get; set; }

    }
}
