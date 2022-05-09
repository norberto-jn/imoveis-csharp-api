using Microsoft.EntityFrameworkCore;
using moveis_api.Models;

namespace moveis_api.Context
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options)
            : base(options) => Database.EnsureCreated();

        public DbSet<CategoryModel> categoryModel { get; set; }
        public DbSet<ProductModel> productModel { get; set; }
        public DbSet<AddressModel> addressModel { get; set; }

    }
}