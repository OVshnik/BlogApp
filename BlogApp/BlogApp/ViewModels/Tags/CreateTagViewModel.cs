using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace BlogApp.ViewModels.Tags;

public class CreateTagViewModel
{
    [Remote(action: "CheckTagName", controller: "Tag", ErrorMessage = "Тег с таким именем уже существует в базе")]
    [Required(ErrorMessage = "Поле обязательно для заполнения")]
    [DataType(DataType.Text)]
    [Display(Name = "Тэг", Prompt = "Укажите тэг")]
    public string Name { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty;
    public Guid ArticleId { get; set; }
}
