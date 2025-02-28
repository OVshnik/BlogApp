using BlogApp.Data.Models;

namespace BlogApp.ViewModels.Tags
{
    public class ListTagsViewModel
    {
        public List<TagViewModel> Tags { get; set; } = new List<TagViewModel>();
    }
}
