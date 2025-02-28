using AutoMapper;
using BlogApp.Data.Models;
using BlogApp.ViewModels.Articles;
using BlogApp.ViewModels.Comments;
using BlogApp.ViewModels.Roles;
using BlogApp.ViewModels.Tags;
using BlogApp.ViewModels.Users;

namespace BlogApp
{
    public class MappingProfile : Profile
	{
		public MappingProfile()
		{
			CreateMap<RegisterViewModel, User>()
				.ForMember(x => x.Email, opt => opt.MapFrom(c => c.EmailReg))
				.ForMember(x => x.UserName, opt => opt.MapFrom(c => c.Login));
			CreateMap<LoginViewModel, User>();
			CreateMap<UserEditViewModel, User>();
			CreateMap<User, UserEditViewModel>().ForMember(x=>x.UserId,opt=>opt.MapFrom(c=>c.Id));
			CreateMap<CreateArticleViewModel, Article>();
			CreateMap<EditArticleViewModel, Article>();
			CreateMap<Article, EditArticleViewModel>();
			CreateMap<Article, ArticleViewModel>();
			CreateMap<ArticleViewModel, Article>();
			CreateMap<CreateCommentViewModel, Comment>();
			CreateMap<Comment, CreateCommentViewModel>();
			CreateMap<EditCommentViewModel, Comment>();
			CreateMap<Comment, EditCommentViewModel>();
			CreateMap<Comment, CommentViewModel>();
			CreateMap<EditTagViewModel, Tag>();
			CreateMap<TagViewModel,Tag>();
			CreateMap<Tag, TagViewModel>();
			CreateMap<Tag, EditTagViewModel>();
			CreateMap<CreateTagViewModel, Tag>();
			CreateMap<RoleViewModel, Role>();
			CreateMap<Role, RoleViewModel>();
			CreateMap<Role, CreateRoleViewModel>();
			CreateMap<CreateRoleViewModel, Role>();
			CreateMap<EditRoleViewModel, Role>();
			CreateMap<Role, EditRoleViewModel>();
		}
	}
}
