using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PhoneNumberFrame.Models;
using PhoneNumberFrame.Repositories;

namespace PhoneNumberFrame
{
    class Program
    {
        static void Main(string[] args)
        {
            var peopleRepo = new PeopleRepo();
            bool isRunning = true;
            int choice = default;

            while(isRunning)
            {
                Console.WriteLine(" 1. Вывести всех сотрудников на экран");
                Console.WriteLine(" 2. Добавить нового сотрудника");
                Console.WriteLine(" 3. Поиск по Имени");
                Console.WriteLine(" 4. Поиск по Фамилии");
                Console.WriteLine(" 5. Поиск по ФИО");
                Console.WriteLine(" 6. Поиск по номеру рабочего телефона");
                Console.WriteLine(" 7. Поиск по ID");
                Console.WriteLine(" 8. Вывести всех сотрудников по выбранной позиции");
                Console.WriteLine(" 9. Удалить сотрудника по ID");
                Console.WriteLine("10. Удалить сотрудника по ФИО");
                Console.WriteLine("=================================================");
                ConsoleColor prevColor = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.Red; // изменяет цвет шрифта
                Console.WriteLine("Enter: Выход");

                if (int.TryParse(Console.ReadLine(), out int i))
                    choice = i;
                else
                {
                    break;
                }

                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Yellow;
                
                switch (choice)
                {
                    case 1:
                        printPeople(peopleRepo.GetPeople()); break;
                    case 2:
                        var person = MakePerson(peopleRepo);
                        peopleRepo.AddPerson(person);
                        break;
                    case 3:
                        Console.Write("Введите имя: ");
                        printPeople(peopleRepo.SearchByName(Console.ReadLine())); break;
                    case 4:
                        Console.Write("Введите фамилию: ");
                        printPeople(peopleRepo.SearchBySurname(Console.ReadLine())); break;
                    case 5:
                        Console.Write("Введите <имя> <фамилия>: ");
                        printPeople(peopleRepo.SearchByFullname(Console.ReadLine())); break;
                    case 6:
                        Console.Write("Введите рабочий телефон: ");
                        printPeople(peopleRepo.SearchByWorkPhoneNumber(Console.ReadLine())); break;
                    case 7:
                        Console.Write("Введите ID сотрудника (поиск): ");
                        printPeople(peopleRepo.SearchById(Console.ReadLine())); break;
                    case 8:
                        Console.WriteLine("Выберите Id позиции: ");
                        printPositions(peopleRepo.GetPositions()); // Выводит все позиции из БД
                        Console.Write(">>> "); // Выбираем нужную позицию
                        if(int.TryParse(Console.ReadLine(), out int pos))
                        {
                            printPeople(peopleRepo.SearchByPosition(pos));
                        }
                        break;
                    case 9:
                        Console.Write("Введите ID сотрудника (удаление из БД): ");
                        if(int.TryParse(Console.ReadLine(), out int id))
                        {
                            peopleRepo.DeletePerson(id);
                        } break;
                    case 10:
                        Console.Write("Введите <имя> <фамилия> (удаление из БД): ");
                        peopleRepo.DeletePerson(Console.ReadLine());
                        break;
                    default:
                        Console.WriteLine("Выберите пункт из списка.");
                        break;
                }

                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("\nНажмите любую клавишу чтобы очистить консоль...");
                Console.ReadKey();
                Console.Clear();
                Console.ForegroundColor = prevColor;
            }
            
        }

        /// <summary>
        /// Helper функция для форматированного вывода информации на консоль
        /// </summary>
        /// <param name="people"></param>
        private static void printPeople(List<MPerson> people)
        {
            Console.WriteLine();
            Console.WriteLine("{0, -3}| {1, -10}| {2, -10}| {3, -15}| {4, -11}| {5, -23}| {6, -23}", "ID", "Имя", "Фамилия", "Адрес", "Раб. тел.", "Дата Рождения", "Принят на работу");
            Console.WriteLine("================================================================================================================");
            foreach (var item in people)
            {
                Console.WriteLine("{0, 2} | {1, -10}| {2, -10}| {3, -15}| {4, -11}| {5, -23}| {6, -23}", item.id, item.Name, item.Surname, item.Address, item.WorkPhoneNum, item.Birthdate, item.WorkBegin);
                Console.WriteLine("================================================================================================================");
            }
        }

        private static void printPositions(List<MPosition> position)
        {
            foreach (var item in position)
            {
                Console.WriteLine("{0}: {1}", item.Id, item.Name);
            }
        }

        /// <summary>
        /// Helper функция, возвращает объект класса MPerson на основе введенной в консоль информации
        /// Принимает peopleRepo чтобы вывести всевозможные позиции
        /// </summary>
        /// <param name="peopleRepo"></param>
        /// <returns></returns>
        private static MPerson MakePerson(PeopleRepo peopleRepo)
        {
            MPerson person = new MPerson();

            Console.Write("Введите имя: ");
            person.Name = Console.ReadLine();

            Console.Write("Введите фамилию: ");
            person.Surname = Console.ReadLine();

            Console.Write("Введите дату рождения в формате гг.мм.дд.: ");
            if (DateTime.TryParse(Console.ReadLine(), out DateTime birthday))
                person.Birthdate = birthday;
            

            Console.Write("Введите пол: ");
            if (bool.TryParse(Console.ReadLine(), out bool gender))
                person.Gender = gender;

            Console.Write("Введите рабочий телефон: ");
            person.WorkPhoneNum = Console.ReadLine();

            Console.Write("Введите телефон: ");
            person.PhoneNum = Console.ReadLine();

            Console.Write("Введите адрес: ");
            person.Address = Console.ReadLine();

            Console.Write("Введите e-mail: ");
            person.Email = Console.ReadLine();

            Console.Write("Введите дату начала работы гг.мм.дд.: ");
            if (DateTime.TryParse(Console.ReadLine(), out DateTime begin))
                person.WorkBegin = begin;

            Console.Write("Введите дату увольнения гг.мм.дд.: ");
            if (DateTime.TryParse(Console.ReadLine(), out DateTime end))
                person.WorkEnd = end;

            Console.WriteLine("Выберите ID позиции из списка (Default = \"Инженер\"): ");
            printPositions(peopleRepo.GetPositions()); // Вывести список позиции
            person.PositionId = int.TryParse(Console.ReadLine(), out int pos) ? pos : 3; // pos либо 3 (инженер)
           
            return person;
        }

    }
}
