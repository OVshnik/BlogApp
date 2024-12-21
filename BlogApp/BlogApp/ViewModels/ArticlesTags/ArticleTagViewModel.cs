using BlogApp.Data.Models;
using BlogApp.ViewModels.ArticlesTags.Articles;
using BlogApp.ViewModels.ArticlesTags.Tags;

namespace BlogApp.ViewModels.ArticlesTags
{
    public class ArticleTagViewModel
    {
        public ListTagsViewModel ListTags { get; set; }
        public CreateArticleViewModel CreateArticle { get; set; }
        public ArticleTagViewModel()
        {
            ListTags=new ListTagsViewModel();
            CreateArticle=new CreateArticleViewModel();
        }
    }
}
