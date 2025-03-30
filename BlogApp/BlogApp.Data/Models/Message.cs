using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Data.Models
{
	public class Message
	{
		public Guid Id{ get; set; }= Guid.NewGuid();
		public string Content { get; set; }=String.Empty;
		public string SenderId { get; set; } = String.Empty;
		public User ?Sender { get; set; }
		public string RecipientId { get; set; } = String.Empty;
		public User ?Recipient { get; set; }
		public DateTime CreateTime { get; set; }=DateTime.Now;


	}
}
