using BlogApp.Data.Models;
using BlogApp.API.ViewModels.Comments;
using System.ComponentModel.DataAnnotations;

namespace BlogApp.API.ViewModels.Articles
{
    public class ArticleViewModel
    {
		public Guid Id { get; set; }
		public string Title { get; set; } = "";
		public string Description { get; set; } = "";
		public string Content { get; set; } = "";
		public DateTime Created { get; set; }
		public string AuthorId { get; set; } = "";
		public List<Comment> Comments { get; set; }=new List<Comment>();
		public List<Tag> Tags { get; set; }=new List<Tag>();
    }
}
