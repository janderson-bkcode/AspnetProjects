using AllHandsMaio2024.Entities;
using AllHandsMaio2024.Repositories.Dapper.Context;
using Dapper;

namespace AllHandsMaio2024.Repositories.Dapper;

public class DapperProductRepository : IProductRepository
{
    private readonly IDapperContext dapperContext;

    public DapperProductRepository(IDapperContext dapperContext)
    {
        this.dapperContext = dapperContext;
    }

    public ICollection<Product> GetAll()
    {
        using var context = dapperContext.GetConnection();

        return context.Query<Product>(
            sql: "SELECT Id, Name, Price, BrandId FROM Products").ToList();
    }

    public Product? GetById(int id, bool includeBrand = false)
    {
        using var context = dapperContext.GetConnection();

        if (includeBrand)
        {
            return context.Query<Product, Brand, Product>(
                sql: $"SELECT P.Id, P.Name, P.Price, B.Id, B.Name " +
                     $"FROM Products P " +
                     $"LEFT JOIN Brands B ON B.Id = P.BrandId " +
                     $"WHERE P.Id = @ProductId",
                param: new { ProductId = id },
                map: (product, brand) => 
                {
                    product.Brand = brand;
                    return product;
                })
                .FirstOrDefault();
        }

        return context.QueryFirstOrDefault<Product>(
            sql: $"SELECT Id, Name, Price, BrandId FROM Products WHERE Id = @ProductId",
            param: new { ProductId = id });
    }

    public Product Add(Product newProduct)
    {
        using var context = dapperContext.GetConnection();

        newProduct.Id = context.ExecuteScalar<int>(
            sql: "INSERT INTO Products (Name, Price, BrandId) OUTPUT INSERTED.Id VALUES (@Name, @Price, @BrandId)",
            param: new 
            { 
                Name = newProduct.Name, 
                Price = newProduct.Price,
                BrandId = newProduct.BrandId
            });

        return newProduct;
    }

    public void Update(Product product)
    {
        using var context = dapperContext.GetConnection();

        context.Execute(
            sql: "UPDATE Products SET Name = @Name, Price = @Price, BrandId = @BrandId WHERE Id = @ProductId",
            param: new { 
                ProductId = product.Id,
                Name = product.Name, 
                Price = product.Price,
                BrandId = product.BrandId
            });
    }

    public bool Delete(int id)
    {
        using var context = dapperContext.GetConnection();

        var rows = context.Execute(
            sql: "DELETE FROM Products WHERE Id = @ProductId",
            param: new { ProductId = id });

        return rows > 0;
    }
}
