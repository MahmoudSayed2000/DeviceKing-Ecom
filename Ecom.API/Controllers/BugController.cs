using AutoMapper;
using Ecom.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Ecom.API.Controllers;

public class BugController : BaseController
{
    // GET
    public BugController(IUnitOfWork work, IMapper mapper) : base(work, mapper)
    {
    }

    [HttpGet("not-found")]
    public async Task<IActionResult> GetNotFound()
    {
        var category = await work.categoryRepository.GetByIdAsync(100);
        if (category == null) return NotFound();
        return Ok();
    }
    [HttpGet("server-error")]
    public async Task<IActionResult> ServerError()
    {
        var category = await work.categoryRepository.GetByIdAsync(100);
        category.name = "";
        return Ok();
    }
    
    [HttpGet("bad-request/{id}")]
    public async Task<IActionResult> GetBadRequest(int id)
    {
        return Ok();
    }
    
    [HttpGet("bad-request/")]
    public async Task<IActionResult> GetBadRequest()
    {
        return BadRequest();
    }
}