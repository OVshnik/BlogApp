using BlogApp.Data.Models;

namespace BlogApp.ViewModels.Comments
{
	public class CommentViewModel
	{
		public Comment Comment;
		public CommentViewModel(Comment comment)
		{
			Comment=comment;
		}
	}
}
