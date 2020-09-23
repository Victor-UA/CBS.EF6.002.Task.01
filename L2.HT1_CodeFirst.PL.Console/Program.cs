using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using L2.HT1_CodeFirst.BLL.Services;

namespace L2.HT1_CodeFirst.PL.AppConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            using (CompanyService company = new CompanyService())
            {
                var specialties = company.GetSpecialties("*");
                foreach (var specialty in specialties)
                {
                    Console.WriteLine("________________________________________________");
                    Console.WriteLine(specialty.Name);
                    Console.WriteLine($"{specialty.Name} {specialty.Trainer.Contragent.Name}");
                    Console.WriteLine($"{specialty.Name} {specialty.Group.Name}");
                    if (specialty.Group.Students.Count > 0)
                    {

                        #region Why does not it work ?
                        //var contragents = company.GetContragents(specialty.Group.Students);
                        //foreach (var contragent in contragents)
                        //{
                        //    Console.WriteLine($"{specialty.Name} {specialty.Group.Name} {contragent.Name}");
                        //}
                        #endregion

                        foreach (var student in specialty.Group.Students)
                        {
                            var contragent = company.GetContragents(student).First();
                            Console.WriteLine($"{specialty.Name} {specialty.Group.Name} {contragent.Name}");

                        }
                    }
                    Console.WriteLine($"{specialty.Name} {specialty.Audience.Name}");
                    Console.WriteLine($"{specialty.Name} {specialty.Schedule.WeekDaysTimes}");
                }
            }
            Console.ReadKey();
        }
    }
}
