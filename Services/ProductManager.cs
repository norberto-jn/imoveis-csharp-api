using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using moveis_api.Models;
using moveis_api.Context;
using Newtonsoft.Json;

using moveis_api.Http;

namespace moveis_api.Services
{
    public class ProductManager
    {

        private readonly Context.Context _context;
        private readonly Http.HttpResponse _response = new Http.HttpResponse();

        public ProductManager(Context.Context context)
        {
            _context = context;
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
                            address = product.address,
                            categoryCode = product.categoryCode,
                            addressCode = product.addressCode,

                        }
                    ).ToList()
                }
            )
            .ToList();

            return JsonConvert.SerializeObject(data);
        }
        public async Task<ActionResult<ProductModel>> findOne(int code)
        {
            var productModel = await _context.productModel.FindAsync(code);

            if (productModel == null)
            {
                return _response.NotFound("teste");
            }

            return productModel;

        }

        public async Task<ActionResult<ProductModel>> save(ProductModel product)
        {
           var productModel = await _context.categoryModel.FindAsync(product.categoryCode);

            if (productModel is null)
            {
                return _response.NotFound("erro");
            }
            _context.productModel.Add(product);
            var productSavedModel = await _context.SaveChangesAsync();

            return product;
        }

        public async Task<ActionResult<ProductModel>> update(int code, ProductModel product)
        {

            var productModel = await _context.productModel.FindAsync(code);

            if (productModel is null)
            {
                return _response.NotFound("erro");
            }

            productModel.name = product.name;

            var productUpdateModel = await _context.SaveChangesAsync();

            return product;
        }

        public async Task<IActionResult> delete(int code)
        {
            var productModel = await _context.productModel.FindAsync(code);

            if (productModel is null)
            {
                return _response.NotFound("erro");
            }

            _context.productModel.Remove(productModel);
            await _context.SaveChangesAsync();

            return _response.NoContent();

        }


    }
}