using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Data.Models
{
	public class User:IdentityUser
	{
		public string FirstName { get; set; } = String.Empty;
		public string LastName { get; set; } = String.Empty;
		public string MiddleName { get; set; } = String.Empty;
		public DateTime BirthDate { get; set; }
		public List<Article> Articles { get; set; }= new List<Article>();
		public List<Comment> Comments { get; set; } = new List<Comment>();
		public string GetFullName()
		{
			return FirstName + " " + MiddleName + " " + LastName;
		}
	}
}
