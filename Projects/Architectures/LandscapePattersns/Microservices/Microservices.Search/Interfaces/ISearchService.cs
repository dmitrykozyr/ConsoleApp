﻿namespace Microservices.Search.Interfaces;

public interface ISearchService
{
    Task<(bool IsSuccess, dynamic SearchResults)> SearchAsync(int customerId);
}
