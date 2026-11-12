using Microsoft.AspNetCore.Mvc;
using MonolithicSPA.ApplicationService.Contracts;
using MonolithicSPA.ApplicationService.Dtos;

namespace MonolithicSPA.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : Controller
    {
        private readonly IProductApplicationService _productApplicationService;
        private readonly ICategoryApplicationService _categoryApplicationService;

        public ProductController(
            IProductApplicationService productApplicationService,
            ICategoryApplicationService categoryApplicationService)
        {
            _productApplicationService = productApplicationService;
            _categoryApplicationService = categoryApplicationService;
        }

        // گرفتن لیست تمام محصولات
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var list = await _productApplicationService.GetAllProducts();
            return Ok(list);
        }

        // گرفتن محصول با ID
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var product = await _productApplicationService.GetProductbyId(id);
            if (product == null) return NotFound();
            return Ok(product);
        }

        // گرفتن لیست دسته‌بندی‌ها برای فرم‌ها (Select Option)
        [HttpGet("GetCategories")]
        public async Task<IActionResult> GetCategories()
        {
            var categories = await _categoryApplicationService.GetCategoriesSelectList();
            return Ok(categories);
        }

        // ثبت محصول جدید
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] PostProductDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            await _productApplicationService.PostProduct(dto);
            return Ok(new { message = "محصول با موفقیت ثبت شد" });
        }

        // ویرایش محصول
        [HttpPut]
        public async Task<IActionResult> Put([FromBody] EditProductDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            await _productApplicationService.EditProduct(dto);
            return Ok(new { message = "محصول با موفقیت ویرایش شد" });
        }

        // حذف محصول
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _productApplicationService.DeleteProduct(id);
            return Ok(new { message = "محصول با موفقیت حذف شد" });
        }
    }
}
