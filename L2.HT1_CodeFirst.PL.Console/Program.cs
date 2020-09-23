using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using L2.HT1_CodeFirst.BLL.Services;

#region Task
//Задание по связям в EF6, подход Code First.
//Основные учасники: 
//Group, Trainer, Student, Speciality(Cources), Audience.

//Group – группа студентов.Net-Dev.
//Trainer – тренер, который преподает у группы .Net-Dev.
//Specialty – специальность, по которой учится группа .Net-Dev, а также курс.
//Audience – аудитория, в которой проводится занятие.

//Необходимо описать и настроить связи в сущности Group, помимо вышеуказанных данная сущность может иметь дополнительные свойства.
//Также необходимо описать каждую из сущностей на которые ссылается Group, при необходимости установить и в них связи.

//* Не забывайте о форме обучения и времени.
//**Хорошо продумайте каждую из сущностей, проводите ассоциации в реальный мир.
#endregion

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
