using RealmApp.Models.Common;
using Realms;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace RealmApp.Infrastructure.Services
{
	public interface IBaseServices<TEntity, TViewModel>
		where TEntity : RealmObject, IRealmEntity
		where TViewModel : class
	{
		TViewModel Add(TViewModel viewModel);
		void AddRange(IEnumerable<TViewModel> viewModels);
		TViewModel Update(TViewModel viewModel);
		void Delete(long id);

		bool Any();

		TViewModel Get(long id);
		TViewModel Get(Expression<Func<TEntity, bool>> predicate);
		long GetLastId();

		IEnumerable<TViewModel> All();
		IEnumerable<TViewModel> Find(Expression<Func<TEntity, bool>> predicate);
	}
}