using BlogApp.Data.Models;

namespace BlogApp.ViewModels.Chat
{
	public class ChatViewModel
	{
		public List<Message> ChatHistory { get; set; }=new List<Message>();
	}
}
