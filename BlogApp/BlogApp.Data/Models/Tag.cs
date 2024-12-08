using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Data.Models
{
	public class Tag
	{
		public Guid Id { get; set; }= Guid.NewGuid();
		public string Name { get; set; }=String.Empty;
		public string Type { get; set; } = String.Empty;
		public List<Article> ?Articles{ get; set; }	= new List<Article>();

	}
}
