using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreSystem.Shared.DTOs
{
    public class ResponsiblePersonGPSRDto
    {
        public int ResponsiblePersonGPSRId { get; set; }
        public string? CompanyName { get; set; }
        public string? CompanyNameAddition { get; set; }
        public string? Salutation { get; set; }
        public string? Title { get; set; }
        public string? Forename { get; set; }
        public string? Surename { get; set; }
        public string? Street { get; set; }
        public string? Housenumber { get; set; }
        public string? AdressAddition { get; set; }
        public string? ZipCode { get; set; }
        public string? City { get; set; }
        public string? Country { get; set; }
        public string? State { get; set; }
        public string? Phonenumber { get; set; }
        public string? Mobilephonenumber { get; set; }
        public string? Faxnumber { get; set; }
        public string? EmailAddress { get; set; }
        public string? Website { get; set; }
    }
}
