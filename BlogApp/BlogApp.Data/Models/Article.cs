using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Data.Models
{
	public class Article
	{
		public Guid Id { get; set; }= Guid.NewGuid();
		public string ?Title { get; set; }	
		public string ?Description { get; set; }
		public string ?Content { get; set; }
		public DateTime? Created { get; set; }= DateTime.Now;
		public string AuthorId { get; set; } = "";
		public User Author { get; set; }

		public List<Tag> ?Tags { get; set; }= new List<Tag>();
		public List<Comment> ?Comments { get; set; }=new List<Comment>();
	}
}
