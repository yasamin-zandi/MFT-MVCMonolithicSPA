using Microsoft.AspNetCore.Mvc;
using MonolithicSPA.ApplicationService.Contracts;
using MonolithicSPA.ApplicationService.Dtos;

namespace MonolithicSPA.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : Controller
    {

        private readonly IPersonApplicationService _personApplicationService;

        public PersonController(IPersonApplicationService personApplicationService)
        {
            _personApplicationService = personApplicationService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var list = await _personApplicationService.GetAllPeople();
            return Ok(list);
        }

        // گرفتن یک شخص با ID
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var person = await _personApplicationService.GetbyId(id);
            if (person == null) return NotFound();
            return Ok(person);
        }

        // ثبت شخص جدید
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] PostPersonDto dto)
        {
            await _personApplicationService.PostPerson(dto);
            return Ok(new { message =" با موفقیت ثبت شد "});
        }

        // ویرایش شخص
        [HttpPut]
        public async Task<IActionResult> Put([FromBody] EditPersonDto dto)
        {
            await _personApplicationService.Update(dto);
            return Ok(new { message =" با موفقیت ویرایش شد "});
        }

        // حذف شخص
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _personApplicationService.RemovePerson(id);
            return Ok(new { message = "با موفقیت حذف شد" });
        }
    }
}
