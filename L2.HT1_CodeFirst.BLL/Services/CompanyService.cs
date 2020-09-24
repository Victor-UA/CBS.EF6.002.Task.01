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
    }
}
