using BlogApp.Data.Models;

namespace BlogApp.API.ViewModels.Comments;

public class CommentViewModel
{
	public Guid Id { get; set; } 
	public string Content { get; set; } = "";
	public string CommentMakerId { get; set; } = "";
	public DateTime Created { get; set; } 
	public User? CommentMaker { get; set; }
	public Guid ArticleId { get; set; }
	public Article? Article { get; set; }
}
