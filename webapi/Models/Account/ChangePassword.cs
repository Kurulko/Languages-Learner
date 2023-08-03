﻿using System.ComponentModel.DataAnnotations;

namespace WebApi.Models.Account;

public class ChangePassword
{
    [Display(Name = "Old password")]
    [DataType(DataType.Password)]
    [MinLength(8, ErrorMessage = "Password must be at least {1} characters long")]
    public string? OldPassword { get; set; } = null!;

    [Display(Name = "New password")]
    [Required(ErrorMessage = "Enter new password")]
    [DataType(DataType.Password)]
    [MinLength(8, ErrorMessage = "Password must be at least {1} characters long")]
    public string NewPassword { get; set; } = null!;

    [Display(Name = "Repeat new password")]
    [Required(ErrorMessage = "Repeat new password")]
    [DataType(DataType.Password)]
    [MinLength(8, ErrorMessage = "Password must be at least {1} characters long")]
    [Compare("NewPassword", ErrorMessage = "Passwords don't match")]
    public string ConfirmNewPassword { get; set; } = null!;
}