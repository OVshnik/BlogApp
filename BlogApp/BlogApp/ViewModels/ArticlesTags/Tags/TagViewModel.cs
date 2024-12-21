using BlogApp.Data.Models;

namespace BlogApp.ViewModels.ArticlesTags.Tags
{
    public class TagViewModel
    {
        public Tag Tag { get; set; }
        public bool IsChecked { get; set; }=false;
        public TagViewModel(Tag tag)
        {
            Tag = tag;
        }
        
    }
}
