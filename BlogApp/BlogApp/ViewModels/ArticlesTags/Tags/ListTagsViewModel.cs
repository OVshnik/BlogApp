using BlogApp.Data.Models;

namespace BlogApp.ViewModels.ArticlesTags.Tags
{
    public class ListTagsViewModel
    {
        public List<TagViewModel> Tags { get; set; } = new List<TagViewModel>();
        public List<string> TagsBox { get; set; } = [];
    }
}
