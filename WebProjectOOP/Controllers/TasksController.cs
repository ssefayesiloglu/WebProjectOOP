using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client.Extensibility;
using WebProjectOOP.Business;
using WebProjectOOP.Business.Abstract;
using WebProjectOOP.Entities.Dtos;

namespace WebProjectOOP.Controllers
{



    [Route("api/[controller]")]
    [ApiController]

    public class TasksController : ControllerBase 
        // ControllerBase sınıfından miras aldık. Hazır metodlara erişim ( ok(), notfound() gibi
    {
        private readonly ITaskService _taskService;
        private readonly IAiService _aiService;
        //sınıf içindeki özel alanlar alt çizgiyle başlar(_). readonly yazarak, bu değişkenin sadece inşa edici
        // yani constructor içinde tanımlanabileceğini garanti ediyoruz.

        public TasksController(ITaskService taskService, IAiService aiService) 
            // dışarıdan bana bir "ITaskService" ver, onu kullanıcam diyoruz. nesneleri "new" ile oluşturmuyoruz.
        {
            _taskService = taskService;
            _aiService = aiService;
        }


        [HttpGet("generate-description")]
        public async Task<IActionResult> GenerateAiDescription(string alldata)  
        {
            var result = await _aiService.GenerateDescriptionAsync(alldata);    // result değişkenini var ile tamınladık. değişken tipini otomatik oluiturucak. kodun okunulabilirliği artıyor.
            return Ok(new { description = result });                          // GenerateDescriptionAsync, OllamaAiService den AI mantığını çağırır. ordan soruyu ve cevabı çekiyoruz.
        }
        /*
        Talep (ITaskService taskService): Constructor parantezinde 
        "Bana bir ITaskService lazım, adı bu metodun içinde taskService olsun" dersin.

        Kabul: .NET sistemi (Dependency Injection konteyneri) sana o gerçek nesneyi getirip parantezin içine bırakır.

        Devir (_taskService = taskService;): Parantezin içindeki o "geçici" ismi 
        (çünkü o isim sadece o metodun içinde yaşar), 
        sınıfın her yerinden erişilebilen "kalıcı" _taskService rafına kopyalarsın.

         */

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] TaskCreateDto dto) // [FromBody] eklemezsen 400 hatası alırsın!
        {
            if (dto == null) return BadRequest("Veri boş geldi!");

            await _taskService.Create(dto);
            return Ok();
        }

        [HttpGet("{id}")] // belirli bir ID'ye göre veri çekmek için kullanılır. (id) değişkendir.

        public async Task<IActionResult> Get(int id)
        {
            var result = await _taskService.Get(id);

            if (result == null)
            {
                return NotFound("Böyle bir görev bulunamadı.");
            }
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(int userId)
        {
            var tasks = await _taskService.GetAll(userId);
            return Ok(tasks);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] TaskUpdateDto dto)
        {
            // dto içindeki State servise gider.
            await _taskService.Update(id, dto.Title, dto.Description, dto.State);
            return Ok("Başarıyla Güncellendi");
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