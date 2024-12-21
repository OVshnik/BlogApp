using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;

namespace BlogApp.ViewModels.Users
{
	public class LoginViewModel
	{
		[Required]
		[EmailAddress]
		[Display(Name = "Email", Prompt = "Введите email")]
		public string Email { get; set; }=String.Empty;

		[Required]
		[DataType(DataType.Password)]
		[Display(Name = "Пароль", Prompt = "Введите пароль")]
		public string Password { get; set; } = String.Empty;

		[Display(Name = "Запомнить?")]
		public bool RememberMe { get; set; }

		public string ReturnUrl { get; set; } = "";
	}
}
