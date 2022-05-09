using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using moveis_api.Models;
using moveis_api.Services;

namespace moveis_api.Controllers;

[ApiController]
[Route("product")]
public class ProductController : ControllerBase
{
    private readonly Services.ProductManager _productManager;

    private readonly ILogger<ProductController> _logger;

    public ProductController(ILogger<ProductController> logger, Context.Context context)
    {
        _logger = logger;
        _productManager = new ProductManager(context);
    }

    [HttpGet]
    public string findAll()
    {
        return _productManager.search();
    }

    [HttpGet("{code}")]
    public async Task<ActionResult<ProductModel>> findOne(int code)
    {
        return await _productManager.findOne(code);
    }

    [HttpPost]
    public async Task<ActionResult<string>> save(ProductModel product)
    {
        try
        {
            var productSavedModel = await _productManager.save(product);
            var result = CreatedAtAction("getProductModel", new { data = product }, product);
            return JsonConvert.SerializeObject(result.RouteValues);
        }
        catch (System.Exception)
        {
            return BadRequest();
        }     
        
    }

    [HttpPut("{code}")]
    public async Task<ActionResult<string>> update(int code, ProductModel product)
    {
        var productUpdateModel = await _productManager.update(code, product);
        var result = CreatedAtAction("getProduct", new { data = product }, product);

        return JsonConvert.SerializeObject(result.RouteValues);
    }

    [HttpDelete("{code}")]
    public async Task<IActionResult> delete(int code)
    {
        return await _productManager.delete(code);
    }


}
