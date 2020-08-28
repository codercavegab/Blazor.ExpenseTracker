using BlazorExpenseTracker.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorExpenseTracker.UI.Interfaces
{
    public interface ICategoryService
    {
        Task<IEnumerable<Category>> GetAllCategories();
        Task<Category> GetCategoryDetails(int id);
        Task SaveCategory(Category category);
        Task DeleteCategory(int id);

    }
}
