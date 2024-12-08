using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Data.Models
{
	public class Comment
	{
		public Guid Id { get; set; } = Guid.NewGuid();
		public string ?Content { get; set; }
		public string CommentMakerId { get; set; } = String.Empty;
		public DateTime? Created { get; set; }= DateTime.Now;
		public User ?CommentMaker { get; set; }
		public Guid ArticleId { get; set; }
		public Article ?Article { get; set; }
	}
}
