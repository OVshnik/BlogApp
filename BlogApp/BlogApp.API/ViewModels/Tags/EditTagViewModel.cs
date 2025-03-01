using System.ComponentModel.DataAnnotations;

namespace BlogApp.API.ViewModels.Tags;

public class EditTagViewModel
{
    public Guid Id { get; set; }
    [Required(ErrorMessage = "Поле обязательно для заполнения")]
    [DataType(DataType.Text)]
    [Display(Name = "Тэг", Prompt = "Поменять тэг")]
    public string Name { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty;
}
