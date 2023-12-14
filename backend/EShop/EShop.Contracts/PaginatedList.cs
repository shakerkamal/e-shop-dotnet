using System.Text.Json;

namespace EShop.Contracts;

public class PaginatedList<T>
{
    public long TotalItems { get; private set; }
    public int PageNumber { get; private set; }
    public int PageSize { get; private set; }
    public List<T> List { get; private set; }

    public PaginatedList(long totalItems, int pageNumber, int pageSize, List<T> list)
    {
        this.TotalItems = totalItems;
        this.PageNumber = pageNumber;
        this.PageSize = pageSize;
        this.List = list;
    }


    public int TotalPages => (int)Math.Ceiling(this.TotalItems / (double)this.PageSize);
    public bool HasPreviousPage => this.PageNumber > 1;
    public bool HasNextPage => this.PageNumber < this.TotalPages;
    public int NextPageNumber => this.HasNextPage ? this.PageNumber + 1 : this.TotalPages;
    public int PreviousPageNumber => this.HasPreviousPage ? this.PageNumber - 1 : 1;
    public PagingHeader GetHeader()
    {
        return new PagingHeader(this.TotalItems, this.PageNumber, this.PageSize, this.TotalPages);
    }

    public class PagingHeader
    {
        public long TotalItems { get; }
        public int PageNumber { get; }
        public int PageSize { get; }
        public int TotalPages { get; }

        public PagingHeader(long totalItems, int pageNumber, int pageSize, int totalPages)
        {
            this.TotalItems = totalItems;
            this.PageNumber = pageNumber;
            this.PageSize = pageSize;
            this.TotalPages = totalPages;
        }

        public string ToJson() => JsonSerializer.Serialize(this);
    }
}
