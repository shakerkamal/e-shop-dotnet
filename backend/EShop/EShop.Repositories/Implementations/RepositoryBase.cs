using EShop.Contracts;
using EShop.Entities;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Linq.Expressions;

namespace EShop.Repository.Implementations;

public abstract class RepositoryBase<TDocument> : IRepositoryBase<TDocument> where TDocument : CoreBaseEntity
{
    private readonly IMongoCollection<TDocument> _collection;

    protected RepositoryBase(IMongoDatabase mongoDatabase)
    {
        _collection = mongoDatabase.GetCollection<TDocument>(GetCollectionName(typeof(TDocument)));
    }

    private protected string GetCollectionName(Type documentType)
    {
        return ((BsonCollectionAttribute)documentType.GetCustomAttributes(
            typeof(BsonCollectionAttribute), true)
            .FirstOrDefault())?.CollectionName;
    }

    public void Add(TDocument entity)
    {
        _collection.InsertOne(entity);
    }

    public async Task AddAsync(TDocument entity, CancellationToken cancellationToken = default)
    {
        await _collection.InsertOneAsync(entity);
    }

    public void AddRange(IEnumerable<TDocument> entities)
    {
        _collection.InsertMany(entities);
    }

    public async Task AddRangeAsync(IEnumerable<TDocument> entities, CancellationToken cancellationToken = default)
    {
        await _collection.InsertManyAsync(entities);
    }

    public void Delete(Expression<Func<TDocument, bool>> expression)
    {
        _collection.FindOneAndDelete(expression);
    }

    public Task DeleteAsync(Expression<Func<TDocument, bool>> expression)
    {
        return Task.Run(() => _collection.FindOneAndDeleteAsync(expression));
    }

    public bool DoesExist(Expression<Func<TDocument, bool>> expression)
    {
        var res = _collection.Find(expression);
        return res is null ? false : true;
    }

    public async Task<bool> DoesExistAsync(Expression<Func<TDocument, bool>> expression, CancellationToken cancellationToken = default)
    {
        var res = await _collection.FindAsync(expression);
        return res is null ? false : true;
    }

    public IEnumerable<TDocument> FindByCondition(Expression<Func<TDocument, bool>> expression)
    {
        return _collection.Find(expression).ToEnumerable();
    }

    public TDocument Get(string id)
    {
        var objectId = new ObjectId(id);
        var filter = Builders<TDocument>.Filter.Eq(d => d.Id, objectId);
        return _collection.Find(filter).SingleOrDefault();
    }

    public TDocument Get(Expression<Func<TDocument, bool>> expression)
    {
        return _collection.Find(expression).FirstOrDefault();
    }

    public PaginatedList<TDocument> GetAll(Expression<Func<TDocument, bool>> expression, Expression<Func<TDocument, string>> order, int pageNumber, int pageSize, bool isAscending = true)
    {
        var totalItems = _collection.CountDocuments(expression);
        List<TDocument> list = new List<TDocument>();
        if (isAscending)
        {
            //sort by ascending order
        }
        else
        {
            //sort by descending order
        }
        var pagedResponse = _collection
            .Find(expression)
            .Skip((pageNumber - 1) * pageSize)
            .Limit(pageSize)
            //.SortBy(order) 
            .ToList();

        var paginatedList = new PaginatedList<TDocument>(totalItems, pageNumber, pageSize, pagedResponse);
        return paginatedList;
    }

    public async Task<PaginatedList<TProjected>> GetAllAsync<TProjected>(Expression<Func<TDocument, bool>> expression, Expression<Func<TDocument, TProjected>> projectionExpression, Expression<Func<TDocument, string>> order, int pageNumber, int pageSize, bool isAscending = true)
    {
        List<TDocument> list = new List<TDocument>();
        var totalItems = await _collection.CountDocumentsAsync(expression);
        if (isAscending)
        {
            //sort by ascending order
        }
        else
        {
            //sort by descending order
        }
        var pagedResponse = await _collection
            .Find(expression)
            .Project(projectionExpression)
            .Skip((pageNumber - 1) * pageSize)
            .Limit(pageSize)
            //.SortBy(order) 
            .ToListAsync();

        var paginatedList = new PaginatedList<TProjected>(totalItems, pageNumber, pageSize, pagedResponse);
        return paginatedList;
    }

    public async Task<PaginatedList<TProjected>> GetAllAsync<TProjected>(Expression<Func<TDocument, TProjected>> projectionExpression, int pageNumber, int pageSize, bool isAscending = true)
    {
        List<TDocument> list = new List<TDocument>();
        var totalItems = await _collection.CountDocumentsAsync(Builders<TDocument>.Filter.Empty);
        if (isAscending)
        {
            //sort by ascending order
        }
        else
        {
            //sort by descending order
        }
        var pagedResponse = await _collection
            .Find(Builders<TDocument>.Filter.Empty)
            .Project(projectionExpression)
            .Skip((pageNumber - 1) * pageSize)
            .Limit(pageSize)
            //.SortBy() 
            .ToListAsync();

        var paginatedList = new PaginatedList<TProjected>(totalItems, pageNumber, pageSize, pagedResponse);
        return paginatedList;
    }

    public async ValueTask<TDocument> GetAsync(string id, CancellationToken cancellationToken = default)
    {
        var objectId = new ObjectId(id);
        var filter = Builders<TDocument>.Filter.Eq(d => d.Id, objectId);
        return _collection.Find(filter).SingleOrDefault();
    }

    public Task<TDocument> GetAsync(Expression<Func<TDocument, bool>> expression)
    {
        return _collection.Find(expression).FirstOrDefaultAsync();
    }

    public void Update(TDocument entity)
    {
        _collection.ReplaceOne(doc => doc.Id == entity.Id, entity);
    }

    public IEnumerable<TDocument> GetAll()
    {
        return _collection.Find(_ => true).ToList();
    }

    public async Task<IEnumerable<TDocument>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _collection.Find(_ => true).ToListAsync();
    }
}
