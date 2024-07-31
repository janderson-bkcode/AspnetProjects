using AllHandsMaio2024.Entities;

namespace AllHandsMaio2024.Repositories;

public interface IProductRepository
{
    ICollection<Product> GetAll();
    Product GetById(int id, bool includeBrand = false);
    Product Add(Product newProduct);
    void Update(Product product);
    bool Delete(int id);
}
