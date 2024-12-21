using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace BlogApp.ViewModels.ArticlesTags.Tags
{
    public class CreateTagViewModel
    {
        [Remote(action: "CheckTagName", controller: "Tag", ErrorMessage = "Тег с таким именем уже существует в базе")]
        [DataType(DataType.Text)]
        [Display(Name = "Тэг", Prompt = "Укажите тэг")]
        public string Name { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public Guid ArticleId { get; set; }
    }
}
