using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Sample.Api.Api.Attributes;
using Sample.Api.Api.Models.Request;
using Sample.Api.Api.Models.Response;
using Sample.Api.ApplicationServices;
using Sample.Api.Domain.Categories;

namespace Sample.Api.Api.Controllers;

[ApiController]
[Route("[controller]")]
[LogActionExecution]
public class CategoriesController : ApiController
{
    private readonly CategoriesService _categoriesService;

    public CategoriesController(CategoriesService categoriesService)
    {
        _categoriesService = categoriesService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAsync(CancellationToken cancellationToken)
    {
        var categories = await _categoriesService.GetAsync(cancellationToken);

        var categoriesResponse = Mapper.Map<IEnumerable<CategoryResponseItem>>(categories);

        return Ok(categoriesResponse);
    }

    [HttpGet("{id}", Name = "GetCategoryById")]
    public async Task<IActionResult> GetByIdAsync([FromRoute] int id, CancellationToken cancellationToken)
    {
        var category = await _categoriesService.GetByIdAsync(id, cancellationToken);

        var categoryResponse = Mapper.Map<CategoryResponseItem>(category);

        return Ok(categoryResponse);
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromBody] CreateCategoryRequest request, CancellationToken cancellationToken)
    {
        ValidateAndThrowIfNotValid(request);

        var newCategory = Mapper.Map<Category>(request);

        var createdCategory = await _categoriesService.CreateAsync(newCategory, cancellationToken);

        var response = Mapper.Map<CategoryResponseItem>(createdCategory);

        return CreatedAtRoute("GetCategoryById", new { id = createdCategory.Id }, response);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateAsync([FromRoute] int id, [FromBody] UpdateCategoryRequest request, CancellationToken cancellationToken)
    {
        ValidateAndThrowIfNotValid(request);

        var updatedCategory = Mapper.Map<Category>(request);

        var result = await _categoriesService.UpdateAsync(updatedCategory, cancellationToken);

        return Ok(result);
    }
}
