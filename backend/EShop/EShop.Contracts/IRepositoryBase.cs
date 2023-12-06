using System.Linq.Expressions;

namespace EShop.Contracts;

public interface IRepositoryBase<T> where T : class
{
    T Get(string id);
    T Get(Expression<Func<T, bool>> expression);
    Task<T> GetAsync(Expression<Func<T, bool>> expression);
    ValueTask<T> GetAsync(Guid id, CancellationToken cancellationToken = default);
    IQueryable<T> GetAll(bool trackChanges);
    Task<IQueryable<T>> GetAllAsync(bool trackChanges, CancellationToken cancellationToken = default);
    Task<PaginatedList<T>> GetAllAsync(Expression<Func<T, bool>> expression, Expression<Func<T, string>> order, int pageNumber, int pageSize, bool isAscending = true);
    PaginatedList<T> GetAll(Expression<Func<T, bool>> expression, Expression<Func<T, string>> order, int pageNumber, int pageSize, bool isAscending = true);
    IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression, bool trackChanges);
    Task<IQueryable<T>> FindByConditionAsync(Expression<Func<T, bool>> expression, bool trackChanges, CancellationToken cancellationToken = default);
    void Add(T entity);
    Task AddAsync(T entity);
    void AddRange(IEnumerable<T> entities);
    Task AddRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default);
    void Update(T entity);
    void Delete(T entity);
    bool DoesExist(Expression<Func<T, bool>> expression);
    Task<bool> DoesExistAsync(Expression<Func<T, bool>> expression, CancellationToken cancellationToken = default);
}
