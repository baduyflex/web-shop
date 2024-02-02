using Mango.Web.Models;
using Mango.Web.Services.Interface;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Mango.Web.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        public async Task<IActionResult> ProductIndex()
        {
            List<ProductDto> products = new();

            var resp = await _productService.GetAllProductsAsync<ResponseDto>();
            if(resp != null & resp.IsSuccess)
            {
                products = JsonConvert.DeserializeObject<List<ProductDto>>(Convert.ToString(resp.Result));
            }

            return View(products);
        }

        public async Task<IActionResult> ProductCreate()
        {
            

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ProductCreate(ProductDto productDto)
        {   
            if(ModelState.IsValid)
            {
                var resp = await _productService.CreateProductAsync<ResponseDto>(productDto);

                if (resp != null && resp.IsSuccess)
                {
                    return RedirectToAction(nameof(ProductIndex));
                }
            }
            return View(productDto);
        }

        public async Task<IActionResult> ProductEdit(int productId)
        {
            var resp = await _productService.GetProductByIdAsync<ResponseDto>(productId);
            if (resp != null & resp.IsSuccess)
            {
                var model = JsonConvert.DeserializeObject<ProductDto>(Convert.ToString(resp.Result));
                return View(model); 
            }
            return NotFound();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ProductEdit(ProductDto productDto)
        {
            if (ModelState.IsValid)
            {
                var resp = await _productService.UpdateProductAsync<ResponseDto>(productDto);

                if (resp != null && resp.IsSuccess)
                {
                    return RedirectToAction(nameof(ProductIndex));
                }
            }
            return View(productDto);
        }

        public async Task<IActionResult> ProductDelete(int productId)
        {
            var resp = await _productService.GetProductByIdAsync<ResponseDto>(productId);
            if (resp != null & resp.IsSuccess)
            {
                var model = JsonConvert.DeserializeObject<ProductDto>(Convert.ToString(resp.Result));
                return View(model);
            }
            return NotFound();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ProductDelete(ProductDto productDto)
        {
            if (ModelState.IsValid)
            {
                var resp = await _productService.DeleteProductAsync<ResponseDto>(productDto.ProductId);

                if (resp != null && resp.IsSuccess)
                {
                    return RedirectToAction(nameof(ProductIndex));
                }
            }
            return View(productDto);
        }
    }
}
