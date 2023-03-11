namespace backend.Models.IdentityModels;

using Microsoft.AspNetCore.Identity;

public class User : IdentityUser<string>
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
}