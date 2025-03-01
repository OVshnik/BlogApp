using BlogApp.Data.Models;
using BlogApp.API.ViewModels.Tags;
using System.ComponentModel.DataAnnotations;

namespace BlogApp.API.ViewModels.Articles;

public class EditArticleViewModel
{
    public Guid Id { get; set; }
    [Required(ErrorMessage = "Поле обязательно для заполнения")]
    [DataType(DataType.Text)]
    [Display(Name = "Название", Prompt = "Поменять название")]
    public string Title { get; set; } = "";
    [Required(ErrorMessage = "Поле обязательно для заполнения")]
    [DataType(DataType.Text)]
    [Display(Name = "Название", Prompt = "Поменять описание")]
    public string Description { get; set; } = "";
    [Required(ErrorMessage = "Поле обязательно для заполнения")]
    [DataType(DataType.Text)]
    [Display(Name = "Изменить содержимое статьи")]
    public string Content { get; set; } = "";
    public string AuthorId { get; set; } = "";
    public List<TagForArticleViewModel> Tags { get; set; } = new List<TagForArticleViewModel>();
}
