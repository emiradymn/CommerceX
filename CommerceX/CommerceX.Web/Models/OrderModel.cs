using System.ComponentModel.DataAnnotations;
using CommerceX.Web.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding;

public class OrderModel
{
    public int Id { get; set; }
    public DateTime OrderData { get; set; }
    public string Name { get; set; } = null!;
    public string City { get; set; } = null!;
    public string Phone { get; set; } = null!;
    [EmailAddress]
    public string Email { get; set; } = null!;
    public string AdressLine { get; set; } = null!;
    [BindNever]
    public Cart? Cart { get; set; } = new();

    public string? CartName { get; set; }
    public string? CartNumber { get; set; }
    public string? ExpirationMonth { get; set; }
    public string? ExpirationYear { get; set; }
    public string? Cvc { get; set; }

}