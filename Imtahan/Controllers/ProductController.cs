using AutoMapper;
using Imtahan.Data;
using Imtahan.Helper.Dto;
using Imtahan.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Imtahan.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        public ProductController(AppDbContext context,
                                IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(_mapper.Map<IEnumerable<ProductDto>>(await _context.Products.ToListAsync()));
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var product = _mapper.Map<ProductDto>(await _context.Products.FirstOrDefaultAsync(x => x.ProductId == id));
            if (product == null)
            {
                return NotFound();
            }
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ProductCreateDto model)
        {
            await _context.AddAsync(_mapper.Map<Product>(model));
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(Create), "Product successfully created!");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute]int id)
        {
            var product = await _context.Products.FirstOrDefaultAsync(x=>x.ProductId == id);
            if (product == null)
            {
                return NotFound();
            }
            _context.Remove(product);
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit([FromRoute] int id, [FromBody] ProductEditDto model)
        {
            var product = await _context.Products.FirstOrDefaultAsync(x => x.ProductId == id);
            if (product == null)
            {
                return NotFound();
            }
            _mapper.Map(model, product);
            _context.Update(product);
            await _context.SaveChangesAsync();


            return Ok();
        }
    }
}
