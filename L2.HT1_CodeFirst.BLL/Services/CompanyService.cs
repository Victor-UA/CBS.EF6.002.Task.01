using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using HT1_CodeFirst.Context;
using HT1_CodeFirst.Models;

namespace L2.HT1_CodeFirst.BLL.Services
{
    public class CompanyService : IDisposable
    {
        private readonly CompanyContext db = new CompanyContext();

        public IQueryable<Specialty> GetSpecialties(string nameMask)
        {
            return db.Specialties.Where((s) => s.Name.Contains(nameMask) || nameMask == "*")
                .Include(s => s.Group)
                .Include(s => s.Group.Students)
                .Include(s => s.Audience)
                .Include(s => s.Schedule)
                .Include(s => s.Trainer)
                .Include(s => s.Trainer.Contragent)
                ;
        }

        public IQueryable<Contragent> GetContragents(ICollection<Student> students)
        {            
            return db.Students.Where(s => students.Any(ss => ss.Id == s.Id)).Select(s => s.Contragent);
        }

        public IQueryable<Contragent> GetContragents(Student student)
        {
            return db.Students.Where(s => s.Id == student.Id).Select(s => s.Contragent);
        }


        public void Dispose()
        {
            db.Dispose();
        }

        public CompanyService()
        {
            Init();
        }

        private void Init()
        {
            Database.SetInitializer(new DropCreateDatabaseAlways<CompanyContext>());
            //Database.SetInitializer(new DropCreateDatabaseIfModelChanges<OneToOneDbEntity>());
            string name;

            name = typeof(Group).Name;
            for (int i = 0; i < 2; i++)
            {
                db.Groups.Add(new Group() { Name = $"{name} {i}" });
            }
            db.SaveChanges();

            name = typeof(Student).Name;
            for (int i = 0; i < 3; i++)
            {
                var contragent = new Contragent { Name = $"{name} {i}", PhoneNumber = $"{i}{i}{i}", Address = $"{name} {i} Address" };
                db.Contragents.Add(contragent);
                var student = new Student { Contragent = contragent };
                db.Students.Add(student);
                var group = db.Groups.Find(i % 2 == 0 ? 1 : 2);
                group.Students.Add(student);
            }

            name = typeof(Trainer).Name;
            for (int i = 0; i < 2; i++)
            {
                var contragent = new Contragent { Name = $"{name} {i}", PhoneNumber = $"{i}{i}{i}", Address = $"{name} {i} Address" };
                db.Contragents.Add(contragent);
                db.Trainers.Add(new Trainer { Contragent = contragent });
            }

            name = typeof(Audience).Name;
            for (int i = 0; i < db.Groups.Count(); i++)
            {
                db.Audiences.Add(new Audience() { Name = $"{name} {i}" });
            }

            name = typeof(Schedule).Name;
            for (int i = 0; i < db.Groups.Count(); i++)
            {
                db.Schedules.Add(new Schedule() { Name = $"{name} {i}", WeekDaysTimes = $"WeekDaysTimes {i}" });
            }

            db.SaveChanges();

            name = typeof(Specialty).Name;
            for (int i = 0; i < db.Audiences.Count(); i++)
            {
                var trainer = db.Trainers.Find(i + 1);
                var group = db.Groups.Find(i + 1);
                var audience = db.Audiences.Find(i + 1);
                var schedule = db.Schedules.Find(i + 1);
                var specialty = new Specialty() { Name = $"{name} {i}", Trainer = trainer, Group = group, Audience = audience, Schedule = schedule };
                db.Specialties.Add(specialty);
            }

            db.SaveChanges();
        }
    }
}
