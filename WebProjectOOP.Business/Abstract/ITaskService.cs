using System;
using System.Collections.Generic;
using System.Text;
using WebProjectOOP.Entities;

namespace WebProjectOOP.Business.Abstract
{
    public interface ITaskService
    {
        Task<ToDoTask> Get(int id); // geriye değer döndürdüğü için todotask a gittik

        Task Create(string title, string description);// geri değer döndürmüyor
        Task Update(int id, string title, string description);

        Task Delete(int id);
        
        Task<List<ToDoTask>> GetAll();
    }

}
//abstact(soyut): arayüzümüz (interface). bu, yapılacak işlerin listesidir. 
//-görev eklenebilir, silinebilr vb.
//nesne oluşturulmaz. ama referans noktası oluşturulabilir.