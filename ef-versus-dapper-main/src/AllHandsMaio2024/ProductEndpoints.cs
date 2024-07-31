using AllHandsMaio2024.Entities;
using AllHandsMaio2024.Repositories;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace AllHandsMaio2024;

public static class ProductEndpoints
{
    public static void MapProductEndpoints(this WebApplication app)
    {
        app.MapGet("/products", GetAllProducts);
        app.MapGet("/products/{id}", GetById);
        app.MapPost("/products", AddProduct);
        app.MapPut("/products/{id}", UpdateProduct);
        app.MapDelete("/products/{id}", DeleteProduct);
    }

    private static Ok<ICollection<Product>> GetAllProducts(
        IProductRepository productRepository)
    {
        return TypedResults.Ok(productRepository.GetAll());
    }

    private static Results<Ok<Product>, NotFound> GetById(
        IProductRepository productRepository, 
        [FromRoute] int id,
        [FromQuery] bool includeBrand = false)
    {
        var product = productRepository.GetById(id, includeBrand);

        return product is not null 
            ? TypedResults.Ok(product) 
            : TypedResults.NotFound();
    }

    private static Ok<Product> AddProduct(
        IProductRepository productRepository,
        [FromBody] Product newProduct)
    {
        newProduct = productRepository.Add(newProduct);
        return TypedResults.Ok(newProduct);
    }

    private static NoContent UpdateProduct(
        IProductRepository productRepository,
        [FromRoute] int id,
        [FromBody] Product product)
    {
        product.Id = id;
        productRepository.Update(product);
        return TypedResults.NoContent();
    }

    private static Results<NoContent, NotFound> DeleteProduct(
        IProductRepository productRepository,
        [FromRoute] int id)
    {
        var deleted = productRepository.Delete(id);
        
        return deleted 
            ? TypedResults.NoContent() 
            : TypedResults.NotFound();
    }
}
