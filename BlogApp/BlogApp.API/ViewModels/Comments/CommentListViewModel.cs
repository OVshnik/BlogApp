using BlogApp.Data.Models;

namespace BlogApp.API.ViewModels.Comments;

public class CommentListViewModel
{
	public List<Comment> Comments { get; set; } = [];
}
