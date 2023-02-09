﻿using TestTask.Data.DTO.Request;
using TestTask.Data.DTO.Response;

namespace TestTask.Services;

public interface ISearchService
{
    Task<SearchResponse> SearchAsync(SearchRequest request, CancellationToken cancellationToken);
    Task<bool> IsAvailableAsync(CancellationToken cancellationToken);
}
