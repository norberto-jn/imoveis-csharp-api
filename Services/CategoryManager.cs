using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using moveis_api.Models;
using moveis_api.Context;
using Newtonsoft.Json;

using moveis_api.Http;

namespace moveis_api.Services
{
    public class CategoryManager
    {

        private readonly Context.Context _context;
        private readonly Http.HttpResponse _response = new Http.HttpResponse();

        public CategoryManager(Context.Context context)
        {
            _context = context;
            // _categoryManager = categoryManager;
        }

        public string findAll()
        {
            var data = (
                from category in _context.categoryModel

                select new
                {
                    code = category.code,
                    name = category.name
                }
            ).ToList();

            return JsonConvert.SerializeObject(data);
        }

        public string search()
        {
            var data = (

                from category in _context.categoryModel

                select new
                {
                    code = category.code,
                    categoria = category.name,
                    imoveis = (
                        from product in _context.productModel
                        where product.categoryCode == category.code
                        select new
                        {
                            code = product.code,
                            nome = product.name,
                            imagem = product.image,
                            whatsapp = product.whatsapp,
                            valor = product.value,
                            categoryCode = product.categoryCode,
                            addressCode = product.addressCode,
                            address = product.address,

                        }
                    ).ToList()
                }
            )
            .ToList();

            return JsonConvert.SerializeObject(data);
        }
        public async Task<ActionResult<CategoryModel>> findOne(int code)
        {
            var categoryModel = await _context.categoryModel.FindAsync(code);

            if (categoryModel == null)
            {
                return _response.NotFound("teste");
            }

            return categoryModel;

        }

        public async Task<ActionResult<CategoryModel>> save(CategoryModel category)
        {
            var categoryModel = await _context.categoryModel.FirstOrDefaultAsync(item => item.name == category.name);

            if (categoryModel != null)
            {
                return _response.BadRequest("erro");
            }

            _context.categoryModel.Add(category);
            var categorySavedModel = await _context.SaveChangesAsync();
            //var result = CreatedAtAction("getCategory", new { data = category }, category);


            return category;
        }

        public async Task<ActionResult<CategoryModel>> update(int code, CategoryModel category)
        {

            var categoryModel = await _context.categoryModel.FindAsync(code);

            if (categoryModel is null)
            {
                return _response.NotFound("erro");
            }

            categoryModel.name = category.name;

            var categoryUpdateModel = await _context.SaveChangesAsync();

            return category;
        }

        public async Task<IActionResult> delete(int code)
        {
            var categoryModel = await _context.categoryModel.FindAsync(code);

            if (categoryModel is null)
            {
                return _response.NotFound("erro");
            }

            var productModel = await _context.productModel.FirstOrDefaultAsync(item => item.categoryCode == code);

            if (productModel != null)
            {
                return _response.BadRequest("erro");
            }

            _context.categoryModel.Remove(categoryModel);
            await _context.SaveChangesAsync();

            return _response.NoContent();

        }


    }
}