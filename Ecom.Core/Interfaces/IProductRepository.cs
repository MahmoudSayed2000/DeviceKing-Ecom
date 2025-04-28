using Ecom.Core.DTO;
using Ecom.Core.Entites.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecom.Core.Interfaces
{
    public interface IProductRepository : IGenericRepository<Product>
    {
        // Add any additional methods specific to Product repository here
        // For example:
        // Task<Product> GetProductWithDetailsAsync(int productId);
        Task<bool> AddAsync(AddProductDTO productDTO);
        Task<bool> UpdateAsync(UpdateProductDTO updateproductDTO);
    }
}
