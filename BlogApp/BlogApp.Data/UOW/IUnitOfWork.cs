using BlogApp.Data.Repository;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Data.UOW
{
	public interface IUnitOfWork:IDisposable
	{
		int SaveChanges(bool ensureAutoHistory = false);
		IRepository<TEntity> GetRepository<TEntity>(bool hasCustomRepository= true)where TEntity:class;
	}
}
