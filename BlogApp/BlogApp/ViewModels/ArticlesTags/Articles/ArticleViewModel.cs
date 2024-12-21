using BlogApp.Data.Models;
using System.ComponentModel.DataAnnotations;

namespace BlogApp.ViewModels.ArticlesTags.Articles
{
    public class ArticleViewModel
    {
        public Article Article { get; set; }
        public ArticleViewModel(Article article)
        {
            Article = article;
        }
    }
}
