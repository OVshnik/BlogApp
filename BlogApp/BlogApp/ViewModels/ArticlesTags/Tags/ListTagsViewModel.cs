using BlogApp.Data.Models;

namespace BlogApp.ViewModels.ArticlesTags.Tags
{
    public class ListTagsViewModel
    {
        public List<TagViewModel> Tags { get; set; } = [];
        public List<string> TagsBox { get; set; }
    }
}
