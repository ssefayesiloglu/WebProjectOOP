using System;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using WebProjectOOP.Entities;

namespace WebProjectOOP.DataAccess

{
    public class ToDoContext : DbContext
    {
        public ToDoContext(DbContextOptions<ToDoContext> options) : base(options) {}

        public DbSet<Entities.Task> Tasks { get; set; }
    }
}
