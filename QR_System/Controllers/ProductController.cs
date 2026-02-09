using Application.DTOs.ProductFolder;
using Application.IServices;
using Microsoft.AspNetCore.Mvc;

namespace QR_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }


        [HttpGet("GetAllProducts")] 
        public async Task<IActionResult> GetAllProducts()
        {
            var products = await _productService.GetAllProductsAsync();
            return Ok(products);
        }

        [HttpGet("GetActiveProducts")]
        public async Task<IActionResult> GetActiveProducts()
        {
            var products = await _productService.GetActiveProductsAsync();
            return Ok(products);
        }

        [HttpGet("Search/{searchTerm}")]
        [ProducesResponseType(typeof(ProductRequest), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> SearchProducts(string searchTerm)
        {
            var products = await _productService.SearchProductAsync(searchTerm);
            return Ok(products);
        }

        [HttpPost("CreateProduct")]
        [ProducesResponseType(typeof(ProductRequest), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ProductRequest>> CreateProduct([FromBody] ProductRequest newProduct)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var createdProduct = await _productService.CreateProductAsync(newProduct);
            return CreatedAtAction(nameof(CreateProduct), new { id = createdProduct.Id }, createdProduct);
        }

        [HttpDelete("DeleteProduct/{id}")]
        [ProducesResponseType(typeof(ProductRequest), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            await _productService.DeleteProductAsync(id);
            return Ok("Product successfully removed.");
        }

        [HttpPut("UpdateProduct/{id}")]
        [ProducesResponseType(typeof(ProductRequest), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateProduct(int id, [FromBody] ProductRequest updatedProduct)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _productService.UpdateProductAsync(id, updatedProduct);
            return Ok($"Product successfully updated:\n{updatedProduct}");
        }
    }
}
