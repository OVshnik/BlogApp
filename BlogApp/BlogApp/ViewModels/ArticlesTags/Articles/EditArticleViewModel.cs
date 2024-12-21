using System.ComponentModel.DataAnnotations;

namespace BlogApp.ViewModels.ArticlesTags.Articles
{
    public class EditArticleViewModel
    {
        public Guid Id { get; set; }
        [DataType(DataType.Text)]
        [Display(Name = "Название", Prompt = "Поменять название")]
        public string? Title { get; set; }
        [DataType(DataType.Text)]
        [Display(Name = "Название", Prompt = "Поменять описание")]
        public string? Description { get; set; }
        [DataType(DataType.Text)]
        [Display(Name = "Изменить содержимое статьи")]
        public string? Content { get; set; }
        public string AuthorId { get; set; } = "";
    }
}
