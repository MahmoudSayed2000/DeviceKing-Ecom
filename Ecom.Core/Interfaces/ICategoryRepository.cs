using Ecom.Core.Entites.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecom.Core.Interfaces
{
    public interface ICategoryRepository : IGenericRepository<Category>
    {
        // Add any additional methods specific to Category repository here
        // For example:
        // Task<Category> GetCategoryWithProductsAsync(int categoryId);
    }
}
