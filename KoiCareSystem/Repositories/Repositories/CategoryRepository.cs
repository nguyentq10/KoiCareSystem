using Repositories.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Repositories
{
    public class CategoryRepository
    {
        private KoiCareSystemContext? _context;

        public List<Category> GetAllCategories()
        {
            _context = new();
            return _context.Categories.ToList();
        }

        public Category? GetByCategoryId(int id)
        {
            _context = new();
            return _context.Categories.FirstOrDefault(c => c.CategoryId == id);
        }

        public void Add(Category category)
        {
            _context = new();
            _context.Categories.Add(category);
            _context.SaveChanges();
        }

        public void Update(Category category)
        {
            _context = new();
            _context.Categories.Update(category);
            _context.SaveChanges();
        }

        public void Delete(Category category)
        {
            _context = new();
            _context.Categories.Remove(category);
            _context.SaveChanges();
        }
    }
}
