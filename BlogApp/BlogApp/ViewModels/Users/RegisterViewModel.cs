using System.ComponentModel.DataAnnotations;

namespace BlogApp.ViewModels.Users
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Поле Имя обязательно для заполнения")]
        [DataType(DataType.Text)]
        [Display(Name = "Имя", Prompt = "Введите имя")]
        public string FirstName { get; set; }= string.Empty;

        [Required(ErrorMessage = "Поле Фамилия обязательно для заполнения")]
        [DataType(DataType.Text)]
        [Display(Name = "Фамилия", Prompt = "Введите фамилию")]
        public string LastName { get; set; } = string.Empty;

		[Required(ErrorMessage = "Поле Email обязательно для заполнения")]
        [EmailAddress]
        [Display(Name = "Email")]
        public string EmailReg { get; set; } = string.Empty;

		[Required(ErrorMessage = "Поле Год обязательно для заполнения")]
        [Display(Name = "Год")]
        public int Year { get; set; }

        [Required(ErrorMessage = "Поле День обязательно для заполнения")]
        [Display(Name = "День")]
        public int Date { get; set; }

        [Required(ErrorMessage = "Поле Месяц обязательно для заполнения")]
        [Display(Name = "Месяц")]
        public int Month { get; set; }

        [Required(ErrorMessage = "Поле Пароль обязательно для заполнения")]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        [StringLength(100, ErrorMessage = "Поле {0} должно иметь минимум {2} и максимум {1} символов.", MinimumLength = 10)]
        public string PasswordReg { get; set; } = string.Empty;

		[Required(ErrorMessage = "Обязательно подтвердите пароль")]
        [Compare("PasswordReg", ErrorMessage = "Пароли не совпадают")]
        [DataType(DataType.Password)]
        [Display(Name = "Подтвердить пароль")]
        public string PasswordConfirm { get; set; } = string.Empty;

		public string Login => EmailReg;
    }
}
