using AutoMapper;
using RealmApp.Infrastructure.Repository;
using RealmApp.Models.Common;
using Realms;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace RealmApp.Infrastructure.Services
{
	public class BaseServices<TEntity, TViewModel> : IBaseServices<TEntity, TViewModel>
	 where TEntity : RealmObject, IRealmEntity
	 where TViewModel : class
	{
		readonly IBaseRepository _baseRepository;

		public BaseServices(IBaseRepository baseRepository)
		{
			_baseRepository = baseRepository;
		}

		public TViewModel Add(TViewModel viewModel)
		{
			try
			{
				var entity = Mapper.Map<TEntity>(viewModel);
				var result = _baseRepository.Add(entity);
				return Mapper.Map<TViewModel>(result);
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public void AddRange(IEnumerable<TViewModel> viewModels)
		{
			try
			{
				var entities = Mapper.Map<IEnumerable<TEntity>>(viewModels);
				_baseRepository.AddRange(entities);
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public TViewModel Update(TViewModel viewModel)
		{
			try
			{
				var entity = Mapper.Map<TEntity>(viewModel);
				entity = _baseRepository.Update(entity);
				return Mapper.Map<TViewModel>(entity);
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public void Delete(long id)
		{
			try
			{
				_baseRepository.Delete<TEntity>(id);
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public bool Any()
		{
			try
			{
				return _baseRepository.Any<TEntity>();
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public TViewModel Get(long id)
		{
			try
			{
				var entity = _baseRepository.Get<TEntity>(id);
				return Mapper.Map<TViewModel>(entity);
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public TViewModel Get(Expression<Func<TEntity, bool>> predicate)
		{
			try
			{
				var entity = _baseRepository.Get<TEntity>(predicate);
				return Mapper.Map<TViewModel>(entity);
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public long GetLastId()
		{
			try
			{
				return _baseRepository.GetLastId<TEntity>();
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public IEnumerable<TViewModel> All()
		{
			try
			{
				var entities = _baseRepository.All<TEntity>();
				return Mapper.Map<IEnumerable<TViewModel>>(entities);
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public IEnumerable<TViewModel> Find(Expression<Func<TEntity, bool>> predicate)
		{
			try
			{
				var entities = _baseRepository.Find(predicate);
				return Mapper.Map<IEnumerable<TViewModel>>(entities);
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
	}
}