using BlogApp.ViewModels.Tags;
using System.ComponentModel.DataAnnotations;

namespace BlogApp.ViewModels.Articles
{
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
        public List<TagViewModel> Tags { get; set; } = new List<TagViewModel>();
    }
}
