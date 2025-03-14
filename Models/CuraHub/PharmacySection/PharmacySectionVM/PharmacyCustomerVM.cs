using System;
using System.ComponentModel.DataAnnotations;
using CMS.Models.CuraHub.IdentitySection;
using CMS.Models.CuraHub.IdentitySection.IdentitySectionVM;
using Microsoft.AspNetCore.Http;

namespace CMS.Models.CuraHub.PharmacySection.PharmacySectionVM;

public class PharmacyCustomerVM 
{
        public int Id { get; set; }
        [Required]
        public string FirstName { get; set; } = null!;
        [Required]
        public string LastName { get; set; } = null!;
        [EmailAddress]
        public string Email { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public string State { get; set; } = null!;
        public string City { get; set; } = null!;
        public string Region { get; set; } = null!;
        public string Street { get; set; } = null!;   
        public List<PharmacyOrderVM>? PharmacyOrders { get; set; }
}
