using Repositories.Entities;
using Repositories.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services
{
    public class ProductService
    {
        private ProductRepository _repo = new();

        public List<ExternalProduct> GetAllProducts()
        {
            return _repo.GetAll();
        }

        public ExternalProduct? GetProductById(int productId)
        {
            return _repo.GetById(productId);
        }

        public void AddProduct(ExternalProduct product)
        {
            _repo.Add(product);
        }

        public void UpdateProduct(ExternalProduct product)
        {
            _repo.Update(product);
        }

        public void DeleteProduct(ExternalProduct product)
        {
            _repo.Delete(product);
        }

        public List<ExternalProduct> GetProductsByCategory(int categoryId)
        {
            return _repo.GetProductsByCategory(categoryId);
        }
    }
}
