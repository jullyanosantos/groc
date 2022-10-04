namespace Microservices.Application.Helpers.PaginationHelper
{
    public abstract class PagedResultBase
    {
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public int TotalItems { get; set; }

        public int FirstRowOnPage
        {
            get { return (CurrentPage - 1) * PageSize + 1; }
        }

        public int LastRowOnPage
        {
            get { return Math.Min(CurrentPage * PageSize, TotalItems); }
        }

        public int TotalPages => (int)Math.Ceiling(TotalItems / (double)PageSize);
        public bool HasPreviousPage => CurrentPage > 1;
        public bool HasNextPage => CurrentPage < TotalPages;
        public int NextPageNumber =>
               HasNextPage ? CurrentPage + 1 : TotalPages;
        public int PreviousPageNumber =>
               HasPreviousPage ? CurrentPage - 1 : 1;
    }
}