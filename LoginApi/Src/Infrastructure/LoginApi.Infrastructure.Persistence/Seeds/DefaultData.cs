using System.Threading.Tasks;
using HyBrForex.Infrastructure.Persistence.Contexts;

namespace HyBrForex.Infrastructure.Persistence.Seeds;

public static class DefaultData
{
    public static async Task SeedAsync(ApplicationDbContext applicationDbContext)
    {
        //if (!await applicationDbContext.ProductDetails.AnyAsync())
        //{
        //    List<Product> defaultProducts = [
        //        //new Product("Product1",100000,"111111111111"),
        //        //new Product("Product2",150000,"222222222222"),
        //        //new Product("Product3",200000,"333333333333"),
        //        //new Product("Product4",105000,"444444444444"),
        //        //new Product("Product5",200000,"555555555555")
        //    ];

        //    foreach (var product in defaultProducts)
        //    {
        //        var existingProduct = await applicationDbContext.ProductDetails.AsNoTracking().FirstOrDefaultAsync(p => p.Id == product.Id);
        //        if (existingProduct == null)
        //        {
        //            applicationDbContext.ProductDetails.Add(product);
        //        }
        //    }
        //    await applicationDbContext.SaveChangesAsync();

        //}
    }
}