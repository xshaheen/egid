using EGID.Core.Common.Result;

namespace EGID.Core.Common.Pages
{
    public class Page<T> : IDataResult<T>
    {
        public Page(T data, PagingInfo pagingInfo)
        {
            Data = data;
            PagingInfo = pagingInfo;
        }

        public T Data { get; protected set; }
        public PagingInfo PagingInfo { get; protected set; }
    }

    public static class Page
    {
        public static Page<T> Create<T>(T data, int totalItems, int currentPage, int pageSize)
        {
            return new Page<T>(data, new PagingInfo
            {
                TotalItems = totalItems,
                CurrentPage = currentPage,
                ItemsPerPage = pageSize
            });
        }
    }
}
