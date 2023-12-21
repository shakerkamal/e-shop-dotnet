using EShop.Entities;
using System.Linq.Expressions;

namespace EShop.Contracts;

public interface IRepositoryBase<TDocument> where TDocument : CoreBaseEntity
{
    TDocument Get(string id);
    ValueTask<TDocument> GetAsync(string id, CancellationToken cancellationToken = default);
    TDocument Get(Expression<Func<TDocument, bool>> expression);
    Task<TDocument> GetAsync(Expression<Func<TDocument, bool>> expression);
    IEnumerable<TDocument> GetAll();
    Task<IEnumerable<TDocument>> GetAllAsync(CancellationToken cancellationToken = default);
    PaginatedList<TDocument> GetAll(Expression<Func<TDocument, bool>> expression, Expression<Func<TDocument, string>> order, int pageNumber, int pageSize, bool isAscending = true);
    Task<PaginatedList<TProjected>> GetAllAsync<TProjected>(Expression<Func<TDocument, bool>> expression, Expression<Func<TDocument, TProjected>> projectionExpression, Expression<Func<TDocument, string>> order, int pageNumber, int pageSize, bool isAscending = true);
    Task<PaginatedList<TProjected>> GetAllAsync<TProjected>(Expression<Func<TDocument, TProjected>> projectionExpression, int pageNumber, int pageSize, bool isAscending = true);
    IEnumerable<TDocument> FindByCondition(Expression<Func<TDocument, bool>> expression);
    //Task<IEnumerable<TProjected>> FindByConditionAsync<TProjected>(Expression<Func<TDocument, bool>> expression, Expression<Func<TDocument, TProjected>> projectionExpression, CancellationToken cancellationToken = default);
    void Add(TDocument entity);
    Task AddAsync(TDocument entity, CancellationToken cancellationToken = default);
    void AddRange(IEnumerable<TDocument> entities);
    Task AddRangeAsync(IEnumerable<TDocument> entities, CancellationToken cancellationToken = default);
    void Update(TDocument entity);
    void Delete(Expression<Func<TDocument, bool>> expression);
    Task DeleteAsync(Expression<Func<TDocument, bool>> expression);
    bool DoesExist(Expression<Func<TDocument, bool>> expression);
    Task<bool> DoesExistAsync(Expression<Func<TDocument, bool>> expression, CancellationToken cancellationToken = default);
}
