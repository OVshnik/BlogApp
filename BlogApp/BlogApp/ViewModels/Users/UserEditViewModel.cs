using System.ComponentModel.DataAnnotations;

namespace BlogApp.ViewModels.Users
{
	public class UserEditViewModel
	{
		[Required]
		[Display(Name = "Идентификатор пользователя")]
		public string UserId { get; set; } = String.Empty;

		[DataType(DataType.Text)]
		[Display(Name = "Имя", Prompt = "Введите имя")]
		public string FirstName { get; set; } = String.Empty;

		[DataType(DataType.Text)]
		[Display(Name = "Фамилия", Prompt = "Введите фамилию")]
		public string LastName { get; set; } = String.Empty;

		[EmailAddress]
		[Display(Name = "Email", Prompt = "example.com")]
		public string Email { get; set; } = String.Empty;

		[DataType(DataType.Date)]
		[Display(Name = "Дата рождения")]
		public DateTime BirthDate { get; set; }

		public string UserName => Email;

		[DataType(DataType.Text)]
		[Display(Name = "Отчество", Prompt = "Введите отчество")]
		public string MiddleName { get; set; } = String.Empty;

		[DataType(DataType.ImageUrl)]
		[Display(Name = "Фото", Prompt = "Укажите ссылку на картинку")]
		public string Image { get; set; } = String.Empty;

	}
}
