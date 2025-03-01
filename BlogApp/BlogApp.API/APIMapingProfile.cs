using AutoMapper;
using BlogApp.Data.Models;
using BlogApp.API.ViewModels;
using BlogApp.API.ViewModels.Users;
using BlogApp.API.ViewModels.Articles;
using BlogApp.API.ViewModels.Comments;
using BlogApp.API.ViewModels.Tags;
using BlogApp.API.ViewModels.Roles;

namespace BlogApp.API;

    public class APIMappingProfile : Profile
{
	public APIMappingProfile()
	{
		CreateMap<RegisterViewModel, User>()
			.ForMember(x => x.Email, opt => opt.MapFrom(c => c.EmailReg))
			.ForMember(x => x.UserName, opt => opt.MapFrom(c => c.Login));
		CreateMap<LoginViewModel, User>();
		CreateMap<UserEditViewModel, User>();
		CreateMap<CreateArticleViewModel, Article>()
			.ForMember(x => x.Tags, opt => opt.Ignore()); 
		CreateMap<EditArticleViewModel, Article>()
			.ForMember(x => x.Tags, opt => opt.Ignore()); 
		CreateMap<Article, ArticleViewModel>();
		CreateMap<CreateCommentViewModel, Comment>();
		CreateMap<EditCommentViewModel, Comment>();
		CreateMap<Comment, CommentViewModel>();
		CreateMap<EditTagViewModel, Tag>();
		CreateMap<Tag, TagViewModel>();
		CreateMap<TagForArticleViewModel, Tag>();
		CreateMap<Tag, TagForArticleViewModel>();
		CreateMap<CreateTagViewModel, Tag>();
		CreateMap<Role, RoleViewModel>();
		CreateMap<CreateRoleViewModel, Role>();
		CreateMap<EditRoleViewModel, Role>();
	}
}
