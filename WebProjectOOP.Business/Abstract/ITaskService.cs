using System;
using System.Collections.Generic;
using System.Text;

namespace WebProjectOOP.Business.Abstract
{
    public interface ITaskService
    {
        Task<Entities.ToDoTask> Get(int id); //
        Task Create(string title, string description);
        Task Update(int id, string title, string description);

        Task Delete(int id);
    }
}
//abstact(soyut): arayüzümüz (interface). bu, yapılacak işlerin listesidir. 
//-görev eklenebilir, silinebilr vb.