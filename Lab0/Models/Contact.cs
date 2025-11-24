using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace Lab0.Models;

public class Contact
{
    [HiddenInput]
    public int Id { get; set; }

    [Required]
    [StringLength(maximumLength: 100, MinimumLength = 2)]
    [Display(Name = "Imię i nazwisko")]
    public string? Name { get; set; }
    
    [EmailAddress]
    [Display(Name = "Adres email")]
    public string? Email { get; set; }

    // --- Add this ---
    public int? OrganizationId { get; set; }
    public Organization? Organization { get; set; }
}