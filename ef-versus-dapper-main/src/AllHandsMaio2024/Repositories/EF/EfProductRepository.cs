using AllHandsMaio2024.Entities;
using AllHandsMaio2024.Repositories.EF.Context;
using Microsoft.EntityFrameworkCore;

namespace AllHandsMaio2024.Repositories.EF;

public class EfProductRepository : IProductRepository
{
    private readonly ApplicationDbContext context;

    public EfProductRepository(ApplicationDbContext context)
    {
        this.context = context;
    }

    public ICollection<Product> GetAll()
    {
        return context.Products.ToList();
    }

    public Product? GetById(int id, bool includeBrand = false)
    {
        var query = context.Products
            .AsQueryable();

        if (includeBrand)
        {
            query = query.Include(x => x.Brand);
        }

        return query.FirstOrDefault(x => x.Id == id);
    }

    public Product Add(Product newProduct)
    {
        if (newProduct.BrandId.HasValue)                                                    
        {
            newProduct.Brand = context.Brands
                .FirstOrDefault(x => x.Id == newProduct.BrandId.Value);
        }

        context.Products.Add(newProduct);
        context.SaveChanges();

        return newProduct;
    }

    public void Update(Product product)
    {
        if (product.BrandId.HasValue)
        {
            product.Brand = context.Brands
                .FirstOrDefault(x => x.Id == product.BrandId.Value);
        }

        context.Products.Attach(product);
        context.Products.Update(product);
        
        context.SaveChanges();
    }

    public bool Delete(int id)
    {
        var rows = context.Products
            .Where(x => x.Id == id)
            .ExecuteDelete();
        
        return rows > 0;
    }
}
