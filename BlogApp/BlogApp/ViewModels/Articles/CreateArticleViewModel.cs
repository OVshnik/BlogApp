using BlogApp.ViewModels.Tags;
using System.ComponentModel.DataAnnotations;

namespace BlogApp.ViewModels.Articles;

public class CreateArticleViewModel
{
    public Guid Id { get; set; } = Guid.NewGuid();
    [Required(ErrorMessage = "Поле обязательно для заполнения")]
    [DataType(DataType.Text)]
    [Display(Name = "Название", Prompt = "Введите название")]
    public string? Title { get; set; }
    [Required(ErrorMessage = "Поле обязательно для заполнения")]
    [DataType(DataType.Text)]
    [Display(Name = "Описание", Prompt = "Введите описание")]
    public string? Description { get; set; }
    [Required(ErrorMessage = "Поле обязательно для заполнения")]
    [DataType(DataType.Text)]
    [Display(Name = "Статья:", Prompt = "...")]
    public string Content { get; set; } = string.Empty;
    public string AuthorId { get; set; } = string.Empty;
    public List<TagViewModel> Tags { get; set; } = new List<TagViewModel>();
}
