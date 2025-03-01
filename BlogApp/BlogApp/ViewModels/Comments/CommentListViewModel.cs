using BlogApp.Data.Models;

namespace BlogApp.ViewModels.Comments;

public class CommentListViewModel
{
	public List<Comment> Comments { get; set; } = [];
	public List<Article> Articles { get; set; } = [];
}
