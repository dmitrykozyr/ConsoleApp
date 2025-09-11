using Microservices.Search.Interfaces;
using Microservices.Search.Models;
using Microsoft.AspNetCore.Mvc;

namespace Microservices.Search.Controllers;

[ApiController]
[Route("api/search")]
public class SearchController
{
    private readonly ISearchService _searchService;

    public SearchController(ISearchService searchService)
    {
        _searchService = searchService;
    }

    [HttpPost]
    public async Task<IResult> SearchAsync(SearchTerm term)
    {
        var result = await _searchService.SearchAsync(term.CustomerId);

        if (result.IsSuccess)
        {
            return Results.Ok(result.SearchResults);
        }

        return Results.NotFound();
    }
}
