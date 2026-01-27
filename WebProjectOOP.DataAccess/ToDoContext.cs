using System;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using WebProjectOOP.Entities;

namespace WebProjectOOP.DataAccess

{
    public class ToDoContext : DbContext
    {
        public ToDoContext(DbContextOptions<ToDoContext> options) : base(options) {}

        public DbSet<ToDoTask> Tasks { get; set; }

        public DbSet<UserTask> Users { get; set; }
    }
}
