using System.Collections.Generic;
using System.Linq;
using BddSample.Model;
using BddSample.Service;
using Microsoft.AspNetCore.Mvc;

namespace BddSample.Controllers
{

    [ApiController]
    [Route("produtos")]
    public class ProductController : ControllerBase
    {
        private readonly ProductService _productService;

        public ProductController(ProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public ActionResult<List<Product>> Get([FromQuery] string[] filtro, 
        [FromQuery] string? sort = null, 
        [FromQuery] int? page = 0, 
        [FromQuery] int? size = 10)
        {
            var query = Request.Query;

            var products = _productService.GetProducts(filtro, page, size, sort);
            if (products == null || !products.Any())
            {
                return Ok(products);
            }
            // Calculate pagination metadata
            int totalCount = _productService.GetProductCount(filtro);
            int totalPages = (int)Math.Ceiling((double)totalCount / (double)size);

            // Set pagination headers
            Response.Headers.Add("X-Page", page.Value.ToString());
            Response.Headers.Add("X-PageSize", size.Value.ToString());
            Response.Headers.Add("X-Total-Count", totalCount.ToString());

            return Ok(products);
        }
    }
}