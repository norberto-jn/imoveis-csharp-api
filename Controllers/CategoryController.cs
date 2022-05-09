using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using moveis_api.Models;
using moveis_api.Services;

namespace moveis_api.Controllers;

[ApiController]
[Route("category")]
public class CategoryController : ControllerBase
{
    private readonly Services.CategoryManager _categoryManager;

    private readonly ILogger<CategoryController> _logger;

    public CategoryController(ILogger<CategoryController> logger, Context.Context context)
    {
        _logger = logger;
        _categoryManager = new CategoryManager(context);
    }

    [HttpGet("search")]
    public string search()
    {
        return _categoryManager.search();
    }

    [HttpGet]
    public string findAll()
    {
        return _categoryManager.findAll();
    }

    [HttpGet("{code}")]
    public async Task<ActionResult<CategoryModel>> findOne(int code)
    {
        return await _categoryManager.findOne(code);
    }

    [HttpPost]
    public async Task<ActionResult<string>> save(CategoryModel category)
    {
        var categorySavedModel = await _categoryManager.save(category);
        var result = CreatedAtAction("getCategory", new { data = category }, category);

        return JsonConvert.SerializeObject(result.RouteValues);
    }

    [HttpPut("{code}")]
    public async Task<ActionResult<string>> update(int code, CategoryModel category)
    {
        var categoryUpdateModel = await _categoryManager.update(code, category);
        var result = CreatedAtAction("getCategory", new { data = category }, category);

        return JsonConvert.SerializeObject(result.RouteValues);
    }

    [HttpDelete("{code}")]
    public async Task<IActionResult> delete(int code)
    {
        return await _categoryManager.delete(code);
    }


}
