using Lab1.Models;
using Microsoft.EntityFrameworkCore;

namespace Lab1.Data
{
    public class appDB : DbContext
    {
        public appDB()
        {

        }
        public appDB(DbContextOptions options) : base(options)
        {

        }

        public virtual DbSet<Student> Students { get; set; }
        public virtual DbSet<Department> Departments { get; set; }
        public virtual DbSet<Course> Courses { get; set; }
        public virtual DbSet<StudentCourse> StudentCourse { get; set; }

        //Lab5
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer("Server=Randa;Database=AspCourse;Trusted_Connection=True;");

        //    base.OnConfiguring(optionsBuilder);
        //}
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<StudentCourse>().HasKey(a => new {a.StdId,a.CourseId});

            modelBuilder.Entity<Course>().HasKey(a => a.id);
            modelBuilder.Entity<Course>().Property(a => a.Name).HasMaxLength(20).IsRequired();

            base.OnModelCreating(modelBuilder);
        }
    }
}
