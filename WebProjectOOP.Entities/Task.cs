using System.ComponentModel.DataAnnotations;
using WebProjectOOP.Core;

namespace WebProjectOOP.Entities
{
    public class Task
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }

        public string Description { get; set; }
        public TaskState  State { get; set; }
        public DateTime CreatingTime { get; set; }

    }
}

/*
 
 sınıfların içindeki ögelere erişim sağlamak için,
 sınıftan bir nsene türetilir.

    SınıfAdı nesneadı = new SınıfAdı();

    DraftClass ==> Sınıf Class
    draftClass ==> Nesne Object
 */
