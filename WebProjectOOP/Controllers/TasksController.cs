using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client.Extensibility;
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
        //sınıf içindeki özel alanlar alt çizgiyle başlar(_). readonly yazarak, bu değişkenin sadece inşa edici
        // yani constructor içinde tanımlanabileceğini garanti ediyoruz.

        public TasksController(ITaskService taskService) 
            // dışarıdan bana bir "ITaskService" ver, onu kullanıcam diyoruz. nesneleri "new" ile oluşturmuyoruz.
        {
            _taskService = taskService;
        }
        /*
        Talep (ITaskService taskService): Constructor parantezinde 
        "Bana bir ITaskService lazım, adı bu metodun içinde taskService olsun" dersin.

        Kabul: .NET sistemi (Dependency Injection konteyneri) sana o gerçek nesneyi getirip parantezin içine bırakır.

        Devir (_taskService = taskService;): Parantezin içindeki o "geçici" ismi 
        (çünkü o isim sadece o metodun içinde yaşar), 
        sınıfın her yerinden erişilebilen "kalıcı" _taskService rafına kopyalarsın.

         */

        [HttpPost] // veritabanına yeni veri eklerken kullanırız. HTTP Metodu.
        public async Task<IActionResult> Create([FromBody] TaskCreateDto dto) 
        // Task<IActionResult> işlemin asenkron olduğunu ve sonunda bir hhtp sonucu (başarılı, hata vb.) döneceğini beliritr
        {
            await _taskService.Create(dto.Title, dto.Description); // servis katmanına gönderiyoruz. 
            return Ok("Kayıt Başarılı.");
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
        public async Task<IActionResult> GetAll()
        {
            var result = await _taskService.GetAll();
            return Ok(result);
        }

        [HttpPut("{id}")]

        public async Task<IActionResult> Put(int id, [FromBody] TaskUpdateDto dto) 
        {
            await _taskService.Update(id, dto.Title, dto.Description);
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