using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using WebProjectOOP.Business.Abstract;
using WebProjectOOP.Core.Enums;
using WebProjectOOP.DataAccess;
using WebProjectOOP.Entities;
using WebProjectOOP.Entities.Dtos;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;


namespace WebProjectOOP.Business.Concrete
{
    public class TaskService(ToDoContext _context) : ITaskService //context in amacı veritabanı iletişimdenki köprüyü kurmak. servis başladığında otomatik ToDoContext buraya gelir.
    {
        public async Task Create(TaskCreateDto dto) // Ayrı parametreler yerine DTO aldık
        {
        
            var newTask = new Entities.ToDoTask()
            {
                Title = dto.Title,
                Description = dto.Description,
                UserId = dto.UserId,
                State = Core.Enums.TaskState.Todo,
                CreatingTime = DateTime.Now
            };
            await _context.Tasks.AddAsync(newTask);
            await _context.SaveChangesAsync();
        }
        public async Task<ToDoTask> Get(int id) //Task<Entities.ToDoTask> yazmamızın sebebi, bu metodun sonunda elmizde bir "Task" 
                                                         // verisi kalacak olmasıdır.
        {
            var task = await _context.Tasks.FindAsync(id);
            //if (task != null)
            {
                return task;
            }
            
            
        }


        public async Task Update(int id, string title, string description, int state) // 'int state' eklendi!
        {
            var existingTask = await _context.Tasks.FindAsync(id);

            if (existingTask != null)
            {
                existingTask.Title = title;
                existingTask.Description = description;

                // SQL'de değişmeyen o '0' rakamını canlandıran satır:
                existingTask.State = (WebProjectOOP.Core.Enums.TaskState)state;

                await _context.SaveChangesAsync(); // Değişikliği veritabanına mühürler.
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

        public async Task Delete(int id) 
        {
            var deleteTask = await _context.Tasks.FindAsync(id);   //bool kontrolüne bak.

            if (deleteTask != null)               //if (task != null) kontrolleriyle programın "Nesne bulunamadı" hatasıyla kapanmasını önledik.
            {
                _context.Tasks.Remove(deleteTask);
                await _context.SaveChangesAsync();
            }
        }
        public async Task<List<ToDoTask>> GetAll(int userId)
        {
            return await _context.Tasks
                         .Where(x => x.UserId == userId)
                         .ToListAsync();
        }

    }
}
