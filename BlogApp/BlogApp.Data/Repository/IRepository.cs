using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Data.Repository
{
	public interface IRepository<T> where T:class
	{
		IEnumerable<T> GetAll();
		Task<T> Get(Guid id);
		Task Create(T item);
		Task Update(T item);
		Task Delete(T item);
	}
}
