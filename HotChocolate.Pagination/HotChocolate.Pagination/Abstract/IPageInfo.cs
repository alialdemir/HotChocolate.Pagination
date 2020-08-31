﻿namespace HotChocolate.Pagination.Abstract
{
    public interface IPageInfo
    {
        long? TotalCount { get; set; }
        int? PageNumber { get; set; }
        int? Limit { get; set; }
    }
}