using BlogApp.Data.Repository;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Formats.Tar;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Data.UOW
{
	public class UnitOfWork:IUnitOfWork
	{
		private ApplicationDbContext _context;
		private Dictionary<Type, object> _repositories;
		public UnitOfWork(ApplicationDbContext context)
		{
			_context = context;
		}
		public void Dispose()
		{
		}
		public IRepository<TEntity> GetRepository<TEntity>(bool hasCustomRepository = true) where TEntity : class
		{
			if (_repositories == null)
			{
				_repositories = new Dictionary<Type, object>();
			}
			if (hasCustomRepository)
			{
				var customRepo = _context.GetService<IRepository<TEntity>>();
				if (customRepo != null)
				{
					return customRepo;
				}
			}
			var type = typeof(TEntity);
			if (!_repositories.ContainsKey(type))
			{
				_repositories[type] = new Repository<TEntity>(_context);
			}
			return (IRepository<TEntity>)_repositories[type];
		}
		public int SaveChanges(bool ensureAutoHistory=false)
		{
			throw new NotImplementedException();
		}
	}
}
