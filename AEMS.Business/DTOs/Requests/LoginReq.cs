using System.ComponentModel.DataAnnotations;

namespace IMS.Business.DTOs.Requests;


public class LoginReq
{
    /// <summary>
    /// Gets or sets the username of the user trying to log in.
    /// </summary>
    public string Username { get; set; } = null!;

    /// <summary>
    /// Gets or sets the password of the user trying to log in.
    /// </summary>
    /// <remarks>
    /// The password needs to be at least 8 characters long.
    /// </remarks>
    [Required]
    [MinLength(6, ErrorMessage = "Password needs to be at-least 8 Characters long")]
    public string Password { get; set; } = null!;

    /// <summary>
    /// Gets or sets a value indicating whether the system should remember the user for future sessions.
    /// </summary>
    public bool? RememberMe { get; set; } = false;
}