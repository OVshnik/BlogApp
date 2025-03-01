using BlogApp.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Identity.Client;
using System.ComponentModel.DataAnnotations;

namespace BlogApp.ViewModels.Roles;

public class RoleViewModel
{
	public Guid Id { get; set; }
	public string Name { get; set; } = string.Empty;
	public string Description { get; set; } = string.Empty;
	public bool IsChecked { get; set; } = false;
}
