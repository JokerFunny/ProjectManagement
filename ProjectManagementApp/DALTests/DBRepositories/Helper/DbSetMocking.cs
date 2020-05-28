using Moq;
using Moq.Language;
using Moq.Language.Flow;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace DALTests.DBRepositories
{
	/// <summary>
	/// Helper for moq <see cref="DbContext"/>
	/// </summary>
	public static class DbSetMocking
	{
		/// <summary>
		/// Creates new <see cref="Mock{DbSet{T}}"/> by <paramref name="data"/>
		/// </summary>
		/// <typeparam name="T">Target type</typeparam>
		/// <param name="data">Target data</param>
		private static Mock<DbSet<T>> CreateMockSet<T>(IQueryable<T> data)
				where T : class
		{
			var queryableData = data.AsQueryable();
			var mockSet = new Mock<DbSet<T>>();
			mockSet.As<IQueryable<T>>().Setup(m => m.Provider)
					.Returns(queryableData.Provider);
			mockSet.As<IQueryable<T>>().Setup(m => m.Expression)
					.Returns(queryableData.Expression);
			mockSet.As<IQueryable<T>>().Setup(m => m.ElementType)
					.Returns(queryableData.ElementType);
			mockSet.As<IQueryable<T>>().Setup(m => m.GetEnumerator())
					.Returns(queryableData.GetEnumerator());
			return mockSet;
		}

		/// <summary>
		/// Returns DbSet
		/// </summary>
		/// <typeparam name="TEntity">Target entity type</typeparam>
		/// <typeparam name="TContext">Target context</typeparam>
		/// <param name="setup">Target dbset setup</param>
		/// <param name="entities">Target entities</param>
		public static IReturnsResult<TContext> ReturnsDbSet<TEntity, TContext>(
				this IReturns<TContext, DbSet<TEntity>> setup,
				TEntity[] entities)
			where TEntity : class
			where TContext : DbContext
		{
			var mockSet = CreateMockSet(entities.AsQueryable());
			return setup.Returns(mockSet.Object);
		}

		/// <summary>
		/// Returns DbSet
		/// </summary>
		/// <typeparam name="TEntity">Target entity type</typeparam>
		/// <typeparam name="TContext">Target context</typeparam>
		/// <param name="setup">Target dbset setup</param>
		/// <param name="entities">Target entities</param>
		public static IReturnsResult<TContext> ReturnsDbSet<TEntity, TContext>(
				this IReturns<TContext, DbSet<TEntity>> setup,
				IQueryable<TEntity> entities)
			where TEntity : class
			where TContext : DbContext
		{
			var mockSet = CreateMockSet(entities);
			return setup.Returns(mockSet.Object);
		}

		/// <summary>
		/// Returns DbSet
		/// </summary>
		/// <typeparam name="TEntity">Target entity type</typeparam>
		/// <typeparam name="TContext">Target context</typeparam>
		/// <param name="setup">Target dbset setup</param>
		/// <param name="entities">Target entities</param>
		public static IReturnsResult<TContext> ReturnsDbSet<TEntity, TContext>(
				this IReturns<TContext, DbSet<TEntity>> setup,
				IEnumerable<TEntity> entities)
			where TEntity : class
			where TContext : DbContext
		{
			var mockSet = CreateMockSet(entities.AsQueryable());
			return setup.Returns(mockSet.Object);
		}
	}
}
