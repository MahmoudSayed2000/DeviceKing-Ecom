using AutoMapper;
using Ecom.API.Helper;
using Ecom.Core.DTO;
using Ecom.Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ecom.API.Controllers
{
    public class ProductsController : BaseController
    {
        public ProductsController(IUnitOfWork work, IMapper mapper) : base(work, mapper)
        {
        }
        [HttpGet("get-all")]
        public async Task<IActionResult> GetAll(string sort, int? CategoryId)
        {
            try
            {
                var products = await work.productRepository
                    .GetAllAsync(sort,CategoryId);
               
                return Ok(products);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("get-by-id/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var product = await work.productRepository
                    .GetByIdAsync(id, x => x.category, x => x.photos);
                if (product == null)
                {
                    return BadRequest(new ResponseAPI(400, "No product found"));
                }
                var result = mapper.Map<ProductDTO>(product);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost("add-product")]
        public async Task<IActionResult> AddProduct(AddProductDTO productDTO)
        {
            try
            {
                await work.productRepository.AddAsync(productDTO);
                return Ok(new ResponseAPI(200, "Product added successfully"));
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseAPI(400, ex.Message));
            }
        }
        [HttpPut("update-product")]
        public async Task<IActionResult> UpdateProduct(UpdateProductDTO updateproductDTO)
        {
            try
            {
                await work.productRepository.UpdateAsync(updateproductDTO);
                return Ok(new ResponseAPI(200, "Product updated successfully"));
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseAPI(400, ex.Message));
            }
        }
        [HttpDelete("delete-product/{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            try
            {
                var product = await work.productRepository.GetByIdAsync(id, x => x.photos, x => x.category);
                await work.productRepository.DeleteAsync(product);
                return Ok(new ResponseAPI(200, "Product deleted successfully"));
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseAPI(400, ex.Message));
            }
        }
    }
}
