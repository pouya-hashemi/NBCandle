using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NB.Candle.WebRzorPage.Common
{
    public class PageinateDetail
    {
        public PageinateDetail(
            int currentPageIndex,
            int currentPageSize,
            int totalRows)
        {
            CurrentPageIndex = currentPageIndex;
            CurrentPageSize = currentPageSize;
            TotalPages = totalRows % currentPageSize != 0 ? ((totalRows / currentPageSize) + 1) : (totalRows / currentPageSize);
            TotalRows = totalRows;
        }
        public int CurrentPageIndex { get; private set; }
        public int CurrentPageSize { get; private set; }
        public int TotalPages { get; private set; }
        public int TotalRows { get; private set; }
        public bool HasNextPage => CurrentPageIndex < TotalPages;
        public bool HasPreviousPage => CurrentPageIndex > 1;

    }
}
