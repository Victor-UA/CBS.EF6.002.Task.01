using HT1_CodeFirst.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HT1_CodeFirst.Context
{
    class CompanyContextInitializer : DropCreateDatabaseAlways<CompanyContext>
    {
        protected override void Seed(CompanyContext context)
        {
            string name;

            name = typeof(Group).Name;
            for (int i = 0; i < 2; i++)
            {
                context.Groups.Add(new Group() { Name = $"{name} {i}" });
            }
            context.SaveChanges();

            name = typeof(Student).Name;
            for (int i = 0; i < 3; i++)
            {
                var contragent = new Contragent { Name = $"{name} {i}", PhoneNumber = $"{i}{i}{i}", Address = $"{name} {i} Address" };
                context.Contragents.Add(contragent);
                var student = new Student { Contragent = contragent };
                context.Students.Add(student);
                var group = context.Groups.Find(i % 2 == 0 ? 1 : 2);
                group.Students.Add(student);
            }

            name = typeof(Trainer).Name;
            for (int i = 0; i < 2; i++)
            {
                var contragent = new Contragent { Name = $"{name} {i}", PhoneNumber = $"{i}{i}{i}", Address = $"{name} {i} Address" };
                context.Contragents.Add(contragent);
                context.Trainers.Add(new Trainer { Contragent = contragent });
            }

            name = typeof(Audience).Name;
            for (int i = 0; i < context.Groups.Count(); i++)
            {
                context.Audiences.Add(new Audience() { Name = $"{name} {i}" });
            }

            name = typeof(Schedule).Name;
            for (int i = 0; i < context.Groups.Count(); i++)
            {
                context.Schedules.Add(new Schedule() { Name = $"{name} {i}", WeekDaysTimes = $"WeekDaysTimes {i}" });
            }

            context.SaveChanges();

            name = typeof(Specialty).Name;
            for (int i = 0; i < context.Audiences.Count(); i++)
            {
                var trainer = context.Trainers.Find(i + 1);
                var group = context.Groups.Find(i + 1);
                var audience = context.Audiences.Find(i + 1);
                var schedule = context.Schedules.Find(i + 1);
                var specialty = new Specialty() { Name = $"{name} {i}", Trainer = trainer, Group = group, Audience = audience, Schedule = schedule };
                context.Specialties.Add(specialty);
            }

            context.SaveChanges();
        }
    }
}
