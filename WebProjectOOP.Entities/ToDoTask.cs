using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using WebProjectOOP.Core.Enums;

namespace WebProjectOOP.Entities
{
    public class ToDoTask
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public TaskState State { get; set; }
        public DateTime CreatingTime { get; set; }

    }
    }

       /* public string Summery { get; set; }

        public int UserId { get; set; }

        [JsonIgnore]

        public UserTask? user { get; set; }?/
    }
}

/*
 
 sınıfların içindeki ögelere erişim sağlamak için,
 sınıftan bir nsene türetilir.

    SınıfAdı nesneadı = new SınıfAdı();

    DraftClass ==> Sınıf Class
    draftClass ==> Nesne Object
 */
