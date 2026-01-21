using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebProjectOOP.Business.Abstract;

namespace WebProjectOOP.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        private readonly ITaskService _taskService;

        public TasksController(ITaskService taskService)
        {
            _taskService = taskService;
        }
        [HttpPost]
        public async Task<IActionResult> Create(string title, string description)
        {
            await _taskService.Create(title, description);
            return Ok("Kayıt Başarılı.");
        }
        [HttpGet("{id}")]

        public async Task<IActionResult> Get(int id)
        {
            var result = await _taskService.Get(id);

            if (result == null)
            {
                return NotFound("Böyle bir görev bulunamadı.");
            }
            return Ok(result);
        }
        [HttpPut]

        public async Task<IActionResult> Put(int id, string title, string description) 
        {
            await _taskService.Update(id, title, description);
            return Ok("Başarıyla GÜncellendi");
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        { 
            var result = await _taskService.Get(id);
            if (result==null)
            {
                return NotFound("Böyle bir Kayıt Bulunamadı.");
            }
            await _taskService.Delete(id);
            
            return Ok("Başarıyla Silindi");
        
        }
    }
}


/*
CRUD İşlemi,HTTP Metodu,Controller'daki Etiket (Attribute)
Create,POST,[HttpPost]
Read,GET,[HttpGet]
Update,PUT / PATCH,[HttpPut]
Delete,DELETE,[HttpDelete]
*/