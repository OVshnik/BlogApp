using System.ComponentModel.DataAnnotations;

namespace BlogApp.ViewModels.ArticlesTags.Articles
{
    public class CreateArticleViewModel
    {
        [DataType(DataType.Text)]
        [Display(Name = "Название", Prompt = "Введите название")]
        public string? Title { get; set; }
        [DataType(DataType.Text)]
        [Display(Name = "Описание", Prompt = "Введите описание")]
        public string? Description { get; set; }
        [DataType(DataType.Text)]
        [Display(Name = "Статья:")]
        public string? Content { get; set; }
        public string AuthorId { get; set; } = string.Empty;
    }
}
