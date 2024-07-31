using ProjectStartupClean.Data;

namespace ProjectStartupClean.services
{
    public class ProductService
    {
        private readonly IRepository<Product> _productRepository;

        public ProductService(IRepository<Product> productRepository)
        {
            _productRepository = productRepository;
        }

        // Métodos de serviço aqui
    }
}