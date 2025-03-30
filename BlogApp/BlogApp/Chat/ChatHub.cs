using Microsoft.AspNetCore.SignalR;
using BlogApp.Data.Repository;
using Microsoft.EntityFrameworkCore;
using BlogApp.Data.Models;

namespace BlogApp.Chat
{
	public class ChatHub:Hub
	{
		private readonly IMessageRepository _messageRepository;
		public ChatHub(IMessageRepository messageRepository)
		{
			_messageRepository = messageRepository;
 		}
		public async Task SendMessage(string senderUserId, string senderName, string receiverUserId, string message)
		{
			var newMessage = new Message()
			{
				Content = message,
				RecipientId=receiverUserId,
				SenderId=senderUserId,
			};
			await _messageRepository.AddNewMessageAsync(newMessage);
			await this.Clients.All.SendAsync("ReceiveMessage", senderUserId, senderName, message);
		}
	}
}
