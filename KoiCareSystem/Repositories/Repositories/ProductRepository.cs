using Repositories.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Repositories
{
    public class ProductRepository
    {
        private KoiCareSystemContext? _context;


        public List<ExternalProduct> GetAll()
        {
            _context = new();
            return _context.ExternalProducts.ToList();
        }

        public ExternalProduct? GetById(int id)
        {
            _context = new();
            return _context.ExternalProducts.FirstOrDefault(p => p.ProductId == id);
        }

        public void Add(ExternalProduct product)
        {
            _context = new();
            _context.ExternalProducts.Add(product);
            _context.SaveChanges();
        }

        public void Update(ExternalProduct product)
        {
            _context = new();
            _context.ExternalProducts.Update(product);
            _context.SaveChanges();
        }

        public void Delete(ExternalProduct product)
        {
            _context = new();
            _context.ExternalProducts.Remove(product);
            _context.SaveChanges();
        }

        public List<ExternalProduct> GetProductsByCategory(int categoryId)
        {
            _context = new();
            return _context.ExternalProducts.Where(p => p.CategoryId == categoryId).ToList();
        }
    }
}
