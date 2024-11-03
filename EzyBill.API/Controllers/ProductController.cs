using EzyBill.BLL.Interfaces;
using EzyBill.BLL.Models.DataTranserObjects;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace EzyBill.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController, Authorize]
    public class ProductController : ControllerBase
    {
        private readonly ILogger<ProductController> _logger;
        private readonly IProductService _productService;
        public ProductController(ILogger<ProductController> logger, IProductService productService) {
          _productService = productService;
          _logger = logger;
        }

        [HttpPost("[action]")]
        public IEnumerable<ProductDto> Add([FromBody] List<ProductDto> products) {
            try
            {
                return this._productService.Add(products);
            }
            catch (Exception ex)
            {
                _logger.LogError($"{ex.Message}\n Input {JsonSerializer.Serialize(products)}");
                return Enumerable.Empty<ProductDto>();
            }
        }
    }
}
