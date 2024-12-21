using System.ComponentModel.DataAnnotations;

namespace BlogApp.ViewModels.ArticlesTags.Tags
{
    public class EditTagViewModel
    {
        public Guid Id { get; set; }
        [DataType(DataType.Text)]
        [Display(Name = "Тэг", Prompt = "Поменять тэг")]
        public string Name { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
    }
}
