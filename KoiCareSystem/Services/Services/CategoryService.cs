using Repositories.Entities;
using Repositories.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services
{
    public class CategoryService
    {
        private CategoryRepository _repo = new();

        public List<Category> GetAllCategories()
        {
            return _repo.GetAllCategories();
        }

        public Category? GetCategoryById(int categoryId)
        {
            return _repo.GetByCategoryId(categoryId);
        }

        public void AddCategory(Category category)
        {
            _repo.Add(category);
        }

        public void UpdateCategory(Category category)
        {
            _repo.Update(category);
        }

        public void DeleteCategory(Category category)
        {
            _repo.Delete(category);
        }
    }
}
