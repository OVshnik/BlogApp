using BlogApp.Data.Models;

namespace BlogApp.ViewModels.Tags
{
	public class TagViewModel
	{
		public Tag Tag { get; set; }
		public TagViewModel(Tag tag)
		{
			Tag = tag;
		}
	}
}
