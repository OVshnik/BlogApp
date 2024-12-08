using AutoMapper;
using BlogApp.Data.Models;
using BlogApp.ViewModels.Articles;
using BlogApp.ViewModels.Comments;
using BlogApp.ViewModels.Tags;
using BlogApp.ViewModels.Users;

namespace BlogApp
{
    public class MappingProfile : Profile
	{
		public MappingProfile()
		{
			CreateMap<RegisterViewModel, User>()
				.ForMember(x => x.BirthDate, opt => opt.MapFrom(c => new DateTime((int)c.Year, (int)c.Month, (int)c.Date)))
				.ForMember(x => x.Email, opt => opt.MapFrom(c => c.EmailReg))
				.ForMember(x => x.UserName, opt => opt.MapFrom(c => c.Login));
			CreateMap<CreateArticleViewModel, Article>();
			CreateMap<EditArticleViewModel, Article>();
			CreateMap<CreateCommentViewModel, Comment>();
			CreateMap<Comment, CreateCommentViewModel>();
			CreateMap<EditCommentViewModel, Comment>();
			CreateMap<Comment, EditCommentViewModel>();
			CreateMap<EditTagViewModel, Tag>();
			CreateMap<Tag, EditTagViewModel>();
			CreateMap<CreateTagViewModel, Tag>()
				.ForMember(x=>x.Name,opt=>opt.MapFrom(c=>c.Name))
				.ForMember(x=>x.Type,opt=>opt.MapFrom(c=>c.Type));
		}
	}
}
