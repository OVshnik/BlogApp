using BlogApp.Data.Repository;
using BlogApp.Data.UOW;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Data.Extensions
{
	public static class ServiceExtensions
	{
		public static IServiceCollection AddUnitOfWork(this IServiceCollection services)
		{
			services.AddTransient<IUnitOfWork, UnitOfWork>();

			return services;
		}

		public static IServiceCollection AddCustomRepository<TEntity, IRepository>(this IServiceCollection services)
				 where TEntity : class
				 where IRepository : class, IRepository<TEntity>
		{
			services.AddTransient<IRepository<TEntity>, IRepository>();

			return services;
		}
	}
}
