using RealmApp.Models.Common;
using Realms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace RealmApp.Infrastructure.Repository
{
	public class BaseRepository : IBaseRepository
	{
		readonly IRealmConnection _realmConnection;
		readonly object _lock = new object();
		Realm _connection;
		Realm Connection
		{
			get
			{
				if (_connection?.IsClosed ?? true)
				{
					_connection = _realmConnection.Connection;
				}
				return _connection;
			}
		}

		public BaseRepository(IRealmConnection realmConnection)
		{
			_realmConnection = realmConnection;
			_connection = realmConnection.Connection;
		}

		/// <summary>
		/// Add the specified TEntity.
		/// </summary>
		/// <returns>The add.</returns>
		/// <param name="TEntity">TE ntity.</param>
		public TEntity Add<TEntity>(TEntity entity) where TEntity : RealmObject, IRealmEntity
		{
			lock (_lock)
			{
				using (var transaction = Connection.BeginWrite())
				{
					entity.Id = GetLastId<TEntity>() + 1;
					var result = Connection.Add<TEntity>(entity);
					transaction.Commit();
					return result;
				}
			}
		}

		public void AddRange<TEntity>(IEnumerable<TEntity> entities) where TEntity : RealmObject, IRealmEntity
		{
			lock (_lock)
			{
				using (var transaction = Connection.BeginWrite())
				{
					foreach (var entity in entities)
					{
						entity.Id = GetLastId<TEntity>() + 1;
						Connection.Add<TEntity>(entity);
					}
					transaction.Commit();
				}
			}
		}

		/// <summary>
		/// Update the specified TEntity.
		/// </summary>
		/// <returns>The update.</returns>
		/// <param name="entity">TE ntity.</param>
		public TEntity Update<TEntity>(TEntity entity) where TEntity : RealmObject, IRealmEntity
		{
			lock (_lock)
			{
				using (var transation = Connection.BeginWrite())
				{
					var result = Connection.Add<TEntity>(entity, true);
					transation.Commit();
					return result;
				}
			}
		}

		/// <summary>
		/// Delete the specified TEntity.
		/// </summary>
		/// <returns>The delete.</returns>
		/// <param name="entity">TE ntity.</param>
		public void Delete<TEntity>(long id) where TEntity : RealmObject, IRealmEntity
		{
			lock (_lock)
			{
				using (var transation = Connection.BeginWrite())
				{
					var entity = Get<TEntity>(id);
					if (entity != null)
						Connection.Remove(entity);
					transation.Commit();
				}
			}
		}

		/// <summary>
		/// Any this instance.
		/// </summary>
		/// <returns>The any.</returns>
		public bool Any<TEntity>() where TEntity : RealmObject, IRealmEntity
		{
			lock (_lock)
				return Connection.All<TEntity>()?.Any() ?? false;
		}

		/// <summary>
		/// Get this instance.
		/// </summary>
		/// <returns>The get.</returns>
		public TEntity Get<TEntity>(long id) where TEntity : RealmObject, IRealmEntity
		{
			lock (_lock)
				return Connection.Find<TEntity>(id);
		}

		/// <summary>
		/// Gets the with predicate.
		/// </summary>
		/// <returns>The with predicate.</returns>
		/// <param name="predicate">Predicate.</param>
		public TEntity Get<TEntity>(Expression<Func<TEntity, bool>> predicate) where TEntity : RealmObject, IRealmEntity
		{
			lock (_lock)
				return Connection.All<TEntity>()?.FirstOrDefault(predicate);
		}

		public long GetLastId<TEntity>() where TEntity : RealmObject, IRealmEntity
		{
			lock (_lock)
				return Connection.All<TEntity>()?.LastOrDefault()?.Id ?? 0;
		}

		/// <summary>
		/// Gets all.
		/// </summary>
		/// <returns>The all.</returns>
		public IEnumerable<TEntity> All<TEntity>() where TEntity : RealmObject, IRealmEntity
		{
			lock (_lock)
				return Connection.All<TEntity>()?.ToList();
		}

		/// <summary>
		/// Gets all with predicate.
		/// </summary>
		/// <returns>The all with predicate.</returns>
		/// <param name="predicate">Predicate.</param>
		public IEnumerable<TEntity> Find<TEntity>(Expression<Func<TEntity, bool>> predicate) where TEntity : RealmObject, IRealmEntity
		{
			lock (_lock)
				return Connection.All<TEntity>().Where(predicate)?.ToList();
		}
	}
}