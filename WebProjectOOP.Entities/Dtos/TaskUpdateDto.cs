using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace WebProjectOOP.Entities.Dtos
{
    public class TaskUpdateDto
    {
     
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }

    }
}
