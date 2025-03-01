using BlogApp.Data.Models;
using System.ComponentModel.DataAnnotations;

namespace BlogApp.ViewModels.Tags;

public class TagViewModel
{
    public Guid Id { get; set; }
    [Required(ErrorMessage = "Поле обязательно для заполнения")]
    [DataType(DataType.Text)]
    [Display(Name = "Название", Prompt = "Тег")]
    public string Name { get; set; } = string.Empty;
    public bool IsChecked { get; set; } = false;
    public List<Article> Articles { get; set; } = new List<Article>();
}
