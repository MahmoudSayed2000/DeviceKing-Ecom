using AutoMapper;
using Ecom.API.Helper;
using Ecom.Core.DTO;
using Ecom.Core.Entites.Product;
using Ecom.Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ecom.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : BaseController
    {
        public CategoriesController(IUnitOfWork work, IMapper mapper) : base(work, mapper)
        {
        }

        [HttpGet("get-all")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var categories = await work.categoryRepository.GetAllAsync();
                if (categories == null)
                {
                    return BadRequest(new ResponseAPI(400 , "No categories found"));
                }
                return Ok(categories);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("get-by-id/{id}")]
        public async Task<IActionResult>GetById(int id)
        {
            try
            {
                var category = await work.categoryRepository.GetByIdAsync(id);
                if (category == null)
                {
                    return BadRequest(new ResponseAPI(400, $"No Category found by this id {id}"));
                }
                return Ok(category);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost("add-category")]
        public async Task<IActionResult> AddCategory(CategoryDTO categoryDTO)
        {
            try
            {
                var category = mapper.Map<Category>(categoryDTO);
                 await work.categoryRepository.AddAsync(category);
                return Ok(new ResponseAPI(200, "Category Added Successfully"));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut("update-category")]
        public async Task<IActionResult> UpdateCategory(UpdateCategoryDTO categoryDTO)
        {
            try
            {
                var category = mapper.Map<Category>(categoryDTO);
                await work.categoryRepository.UpdateAsync(category);

                return Ok(new ResponseAPI(200, "Category updated successfully"));

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        [HttpDelete("delete-category/{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            try
            {
                var category = await work.categoryRepository.GetByIdAsync(id);
                if (category == null)
                {
                    return BadRequest("Category not found");
                } //remove if and var above in needed
                await work.categoryRepository.DeleteAsync(id);
                return Ok(new ResponseAPI(200, "Category deleted successfully"));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
