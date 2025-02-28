using BlogApp.Data.Models;
using System.ComponentModel.DataAnnotations;

namespace BlogApp.API.ViewModels.Tags
{
    public class TagViewModel
    {
        public Guid Id { get; set; }
        [Required(ErrorMessage = "Поле обязательно для заполнения")]
        [DataType(DataType.Text)]
        [Display(Name = "Название", Prompt = "Тег")]
        public string Name { get; set; } = string.Empty;
        public List<Article> Articles { get; set; } = new List<Article>();
    }
}
