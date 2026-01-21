using System;
using System.Collections.Generic;
using System.Text;
using WebProjectOOP.Business.Abstract;
using WebProjectOOP.DataAccess;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;


namespace WebProjectOOP.Business.Concrete
{
    public class TaskService(ToDoContext _context) : ITaskService //context in amacı veritabanı iletişimdenki köprüyü kurmak. servis başladığında otomatik ToDoContext buraya gelir.
    {
         public async Task Create(string title, string description)
        {
            var newTask = new Entities.ToDoTask()
            {
                Title = title,
                Description = description,
                State = Core.Enums.TaskState.Todo,
                CreatingTime = DateTime.Now,
            };
            await _context.Tasks.AddAsync(newTask);   //await kullanarak uyg. donmasını engelliyoruz.
            await _context.SaveChangesAsync();
        }
        public async Task<Entities.ToDoTask> Get(int id) //Task<Entities.Task> yazmamızın sebebi, bu metodun sonunda elmizde bir "Task" 
                                                                                   // verisi kalacak olmasıdır.
        {
            var task = await _context.Tasks.FindAsync(id);
            return task;
        }

        async Task ITaskService.Update(int id, string title, string description)
        {
            var existingTask = await _context.Tasks.FindAsync(id);  //FirstOrDefault fonk. - Select Kullanımlarını öğren.
            if (existingTask != null)                                 
            {                                                        
                existingTask.Title = title;                                  
                existingTask.Description = description;                    
                                                                       
                await _context.SaveChangesAsync();                            
            }                                                              
        }
        /*Güncelleme işlemi, veritabanındaki mevcut kaydı (existing)
        asenkron bir şekilde bularak başlar; burada existingTask
        isimlendirmesi verinin halihazırda orada olduğunu belirtmek için
        kullanılır. Yazdığımız null kontrolü, geçersiz bir ID
        gönderildiğinde uygulamanın çökmesini engelleyen kritik bir
        güvenlik kuralıdır. Nesne bulunduktan sonra sadece başlık ve
        açıklama alanlarını güncelliyoruz çünkü Entity Framework bu n
        esneyi izlemeye (tracking) devam eder; bu sayede
        SaveChangesAsync komutu verildiğinde sistem sadece değişen
        alanları algılar ve veritabanına otomatik olarak mühürler*/

        async Task ITaskService.Delete(int id) 
        {
            var deleteTask = await _context.Tasks.FindAsync(id);   //bool kontrolüne bak.

            if (deleteTask != null)               //if (task != null) kontrolleriyle programın "Nesne bulunamadı" hatasıyla kapanmasını önledik.
            {
                _context.Tasks.Remove(deleteTask);
                await _context.SaveChangesAsync();
            }
        }

    }
}
