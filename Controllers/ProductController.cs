using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProductAPI.Models;
using ProductAPI.Services;

namespace ProductAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        // Lấy tất cả sản phẩm
        [HttpGet]
        public IActionResult GetAllProducts()
        {
            var products = _productService.GetAllProducts();
            return Ok(products);
        }

        // Lấy thông tin sản phẩm theo ID
        [HttpGet("{id}")]
        public IActionResult GetProductById(int id)
        {
            var product = _productService.GetProductById(id);
            if (product == null)
                return NotFound(new { message = "Product not found" });

            return Ok(product);
        }

        // Thêm sản phẩm mới (yêu cầu xác thực)
        [HttpPost]
        [Authorize]
        public IActionResult AddProduct([FromBody] Product product)
        {
            if (product == null)
                return BadRequest(new { message = "Invalid product data" });

            _productService.AddProduct(product);
            return CreatedAtAction(nameof(GetProductById), new { id = product.Id }, product);
        }

        // Cập nhật sản phẩm (yêu cầu xác thực)
        [HttpPut("{id}")]
        [Authorize]
        public IActionResult UpdateProduct(int id, [FromBody] Product product)
        {
            if (product == null || product.Id != id)
                return BadRequest(new { message = "Invalid product data" });

            var existingProduct = _productService.GetProductById(id);
            if (existingProduct == null)
                return NotFound(new { message = "Product not found" });

            _productService.UpdateProduct(product);
            return NoContent();
        }

        // Xóa sản phẩm (yêu cầu xác thực)
        [HttpDelete("{id}")]
        [Authorize]
        public IActionResult DeleteProduct(int id)
        {
            var product = _productService.GetProductById(id);
            if (product == null)
                return NotFound(new { message = "Product not found" });

            _productService.DeleteProduct(id);
            return NoContent();
        }
    }
}