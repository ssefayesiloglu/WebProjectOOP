using WebProjectOOP.Core.Enums;
using WebProjectOOP.Entities;
using WebProjectOOP.Entities.Dtos;

namespace WebProjectOOP.Business.Abstract
{
    public interface ITaskService
    {
        // Artık string, string, int değil; tek bir DTO bekliyoruz kanka!
        Task Create(TaskCreateDto dto);

        // GetAll metoduna da userId filtresini mühürlemeyi unutma
        Task<List<ToDoTask>> GetAll(int userId);

        Task<ToDoTask> Get(int id);
        Task Update(int id, string title, string description, int state);
        Task Delete(int id);
    }
}