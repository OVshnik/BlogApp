using BlogApp.Data.Models;

namespace BlogApp.API.ViewModels.Tags;

public class ListTagsViewModel
{
    public List<Tag> Tags { get; set; } = new List<Tag>();
}
