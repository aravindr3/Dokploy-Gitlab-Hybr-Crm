using System;
using System.Collections.Generic;
using System.Text;
using HyBrForex.Domain.Common;
using HyBrForex.Domain.Exchange.Entities;

namespace HyBrCRM.Domain.Exchange.Entities
{
    public class LeadContact : AuditableBaseEntity
    {
        public LeadContact()
        {
        }

        public LeadContact(string? firstName,
     string? lastName ,
        string? phoneNumber1,
            string? phoneNumber2,
            string? whatsAppNumber ,
            string? email,
            string? genderId,
            string? parentsName,
            string? parentsPhoneNumber,
            string? countryId,
            string? stateId,
            string? stateOutsideIndia,
            string? district,
            string? city,
            string? locality,
            string? verticalId



            )
        {
            FirstName = firstName;
            LastName = lastName;
            PhoneNumber1 = phoneNumber1;
            PhoneNumber2 = phoneNumber2;
            WhatsAppNumber = whatsAppNumber;
            Email = email;
            GenderId = genderId;
            ParentsName = parentsName;
            ParentsPhoneNumber = parentsPhoneNumber;
            CountryId = countryId;
            StateId = stateId;
            StateOutsideIndia = stateOutsideIndia;
            District = district;
            City = city;
            VerticalId = verticalId;

        }

        public string ? FirstName { get; set; }
        public string? LastName { get; set; }
        public string ? PhoneNumber1 { get; set; }
        public string ? PhoneNumber2 { get; set; }
        public string ? WhatsAppNumber {  get; set; }
        public string ? Email {  get; set; }
        public string ? GenderId { get; set; }
        public string ? ParentsName { get; set; }
        public string ? ParentsPhoneNumber { get; set; }
        public string ? CountryId { get; set; }
        public string ? StateId { get; set; }
        public string ? StateOutsideIndia { get; set; }
        public string ? District {  get; set; }
        public string ? City { get; set; }
        public string ? Locality { get; set; }
        public string ? VerticalId { get; set; }

        public virtual CommonMsater ? Gender { get; set; }
        public virtual CountryMaster ? Country {  get; set; }
        public virtual StateMaster? State {  get; set; }

        public void Update (string? firstName,
     string? lastName,
        string? phoneNumber1,
            string? phoneNumber2,
            string? whatsAppNumber,
            string? email,
            string? genderId,
            string? parentsName,
            string? parentsPhoneNumber,
            string? countryId,
            string? stateId,
            string? stateOutsideIndia,
            string? district,
            string? city,
            string? locality,
            string? verticalId
)
        {
            FirstName = firstName;
            LastName = lastName;
            PhoneNumber1 = phoneNumber1;
            PhoneNumber2 = phoneNumber2;
            WhatsAppNumber = whatsAppNumber;
            Email = email;
            GenderId = genderId;
            ParentsName = parentsName;
            ParentsPhoneNumber = parentsPhoneNumber;
            CountryId = countryId;
            StateId = stateId;
            StateOutsideIndia = stateOutsideIndia;
            District = district;
            City = city;
            Locality = locality;
            VerticalId = verticalId;
        }


    }
}
