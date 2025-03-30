using BlogApp.Data.Models;
using BlogApp.Data.Repository;
using BlogApp.ViewModels.Chat;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BlogApp.Controllers
{
	public class ChatController : Controller
	{
		private readonly IMessageRepository _messageRepository;
		private readonly UserManager<User> _userManager;
		public ChatController(IMessageRepository messageRepository, UserManager<User> userManager)
		{
			_messageRepository = messageRepository;
			_userManager = userManager;
		}
		public async Task<IActionResult> Chat(string UserId)
		{
			var sender = await _userManager.GetUserAsync(User);
			var recipient = await _userManager.FindByIdAsync(UserId);

			var chatHistory = new ChatViewModel();

			if (sender != null && recipient != null)
			{
				chatHistory.ChatHistory.AddRange(await _messageRepository.GetMessagesAsync(sender, recipient));
			}
			return View(chatHistory);
		}
	}
}
