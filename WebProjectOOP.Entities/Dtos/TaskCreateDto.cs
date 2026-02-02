using System;
using System.Collections.Generic;
using System.Text;
using WebProjectOOP.Entities;

namespace WebProjectOOP.Entities.Dtos
{
    public class TaskCreateDto
    {
        public int Id { get; set; }

        public string? Title { get; set; }

        public string? Description { get; set; }

        public int UserId { get; set; }


    }
}
