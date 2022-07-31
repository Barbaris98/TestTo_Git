using System;
using System.IO;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Linq;





//Чек-лист проблем:
//можно ещё не сохранять в .txt а попробовать сериализацю через dat. давай как раз так стрелец работает
// в другую папку это сохранять в Supdir
//см 42-ю минуту урока про Сериализацию
//
//
//ОНО ЖЕ STATIC т.е ты сделал копию и копию изменял , а надо число ЭТО, поток один будет работать с итератором
//поэтому это уже НЕ разделяемый ресурс!!
//
//Console.WriteLine($"Время прохождения теста: {0}", x); тоже не показывает Х, лан  завтра попробуем через
//последнее жедалание как бы или типо того, или чтоб принимал этот метоод х....


namespace UppgradeTest
{
    public class Person
    {
        public string Name { get; set; }
        public string Group { get; set; }
    }
    public class Test
    {
        public decimal bonusPoint { get; set; }


        public void StartTest()
        {
            Console.WriteLine("Чтобы начать ТЕСТ нажмите Enter : ");
            ConsoleKey key = Console.ReadKey().Key;

            //содержание теста
            if (key == ConsoleKey.Enter)
            {
                Console.WriteLine("Вы нажали enter, запуск тестирования ");



                Console.WriteLine("ТЕСТ №1" + "\n" + "\n");
                Console.WriteLine("1 Кто проживает на дне океана" + "\n");

                Console.WriteLine("a- Губка боб;" + "\n"
                    + "b- Утопленник;" + "\n" +
                    "c- Кирпич;" + "\n" +
                    "d- Рыбка." + "\n");

                string inputValue = Console.ReadLine();
                var array = new[] { inputValue };

                Regex regex = new Regex(@"^a+$");
                foreach (string array_ in array)
                {
                    if (regex.IsMatch(array_))
                    {
                        bonusPoint++;
                        Console.WriteLine("Верно" + "\n");
                    }
                    else
                    {
                        Console.WriteLine("Ответ неверен, дальше" + "\n");
                    }

                    inputValue = null;
                }
                //
                Console.WriteLine("2 Лучший друг Чебурашки" + "\n");

                Console.WriteLine("a- Шепокляк;" + "\n"
                    + "b- Крокодил Гена;" + "\n" +
                    "c- Ребята;" + "\n" +
                    "d- Голубой вагон." + "\n" + "\n");

                inputValue = Console.ReadLine();
                array = new[] { inputValue };

                regex = new Regex(@"^b+$");
                foreach (string array_ in array)
                {
                    Console.WriteLine(regex.IsMatch(array_) ? bonusPoint++ + "Верно" : "Ответ неверен, дальше" + "\n");
                    //можно и так но тогда выводит ещё не увиеличенное значение bonusPoint
                    inputValue = null;
                }
                //
                Console.WriteLine("3 Что плохого делал Конёк-Горбунок?" + "\n");

                Console.WriteLine("a- Много тыдыгыкал;" + "\n"
                    + "b- Топтал урожай;" + "\n" +
                    "c- Сжёг деревню;" + "\n" +
                    "d- Не пахал землю." + "\n" + "\n");

                inputValue = Console.ReadLine();
                array = new[] { inputValue };

                regex = new Regex(@"^b+$");
                foreach (string array_ in array)
                {
                    Console.WriteLine(regex.IsMatch(array_) ? bonusPoint++ + "Верно" : "Ответ неверен, дальше" + "\n");
                    inputValue = null;
                }

                Console.WriteLine("РЕЗУЛЬТАТ:");
                Console.WriteLine("Кол-во верных ответов: " + bonusPoint);
                Console.WriteLine("Проецнтное соотношение" + (bonusPoint / 3) * 100);
                Console.WriteLine("___________________________________" + "\n");

            }

            else
            {
                Console.WriteLine("Ты проебался");
            }
        }


    }

    class Program
    {
        static readonly object locker = new object();//объекt блокировщик
        static public int x = 0; //раздел. ресурс, хотя щас он особо то и не разделяемый)
        static public void Time()
        {
            while (true)
            {
                for (int i = 0; i < 1800000; i++)//ставим ограничение/костыль на тест 
                {
                    x++;
                    Thread.Sleep(1000);
                    Console.WriteLine(x);
                }

            }

        }


        static void Main(string[] args)
        {
            Person person = new Person();
            Console.WriteLine(" Ты кто? введи имя!");
            person.Name = Convert.ToString(Console.ReadLine());
            Console.WriteLine(" Откуда будешь? какая группа!");
            person.Group = Convert.ToString(Console.ReadLine());



            //запускаем тестирование
            Test test = new Test();
            test.bonusPoint = 0;


            Thread thread = new(Time);
            thread.Name = "Time";
            thread.IsBackground = true;
            thread.Start();

            test.StartTest();
            /*
            Thread thread = new(Time);
            thread.Name = "Time";
            thread.IsBackground = true;
            thread.Start();
            */

            //thread.Abort(); не работает это уже , исп исбэкграунд
            //!! А можно сделать StartTest() статическим и вызвать на уровне класса Test.StartTest(), а не на уровне объекта класса!

            //создали папки
            DirectoryInfo catalog = new DirectoryInfo(@"D:\TEST3");
            catalog.Create();
            catalog.CreateSubdirectory("DopPapka");
            //создали текст файл и поток открыли
            FileInfo Textfile = new FileInfo(@"D:\TEST3\Textfaile.txt");
            FileStream stream = Textfile.Create();

            // создали writer
            using (BinaryWriter writer = new BinaryWriter(stream, Encoding.UTF8, leaveOpen: true))
            {
                // stream- Поток , Encoding.UTF8- тип кодировки (по-умолчанию), leaveOpen: true - поток не закрывать


                writer.Write(person.Name);
                writer.Write(person.Group);
                writer.Write(test.bonusPoint);

                writer.Close();

                Console.WriteLine("Резульат тестирования" + "\n" + "Имя: {0}" + "\n"
                    + "Группа: {1}" + "\n" + "Балы: {2}", person.Name, person.Group, test.bonusPoint);

            }
            //Console.WriteLine($"Время прохождения теста: {0}", x );




            /*  Давай не будем себе жизнь усложнять
            stream.Position = 0;
            // создали reader
            BinaryReader reader = new BinaryReader(stream);
            foreach(var Reuzlt in reader.ReadString())
            {
                Console.WriteLine("{0}", Reuzlt);
            }

            reader.Close();
            */

            /*

            //создали папки
            DirectoryInfo catalog = new DirectoryInfo(@"D:\TEST2");
            catalog.Create();
            catalog.CreateSubdirectory("Subdir");
            //создали текст файл и поток открыли
            FileInfo Textfile = new FileInfo(@"D:\TEST2\Textfaile.txt");
            FileStream stream = Textfile.Create();

            // создали writer
            BinaryWriter writer = new BinaryWriter(stream, Encoding.UTF8, leaveOpen: true);
            // stream- Поток , Encoding.UTF8- тип кодировки (по-умолчанию), leaveOpen: true - поток не закрывать
            //BinaryWriter writer = new BinaryWriter(stream);// подвязываем к писцу поток

            int A = 20, B = 5, C;
            C = A + B;
            writer.Write(C);
            Console.WriteLine("File has been written");
            writer.Close();


            // возвращаем позицию потока на его начало
            stream.Position = 0;
            // создали reader
            BinaryReader reader = new BinaryReader(stream);


            int Rezult = reader.ReadInt32();  ///!!!!

            Console.WriteLine(Rezult + "\n");

            //Math.sqrt возвращает sqrt, он не изменяет переданное значение.
            Math.Sqrt(Rezult);

            //Надо вот так вот:
            Rezult = (int)Math.Sqrt(Rezult); // исп. явное приведение типов

            //читаем/записываем в переменную 
            Console.WriteLine(Rezult + "\n");

            reader.Close();




            // ЗАВТРА  А+В=С  С ЗАСУНЕМ В ПЕРЕМЕННУЮ ПОТОМ ПРОЧИТАЕМ ВОТ ТАК  ВОТ

            /* ЕЩЁ РАЗ С НАЧАЛА ПОПРОБУЕМ
            Dictionary<int, string> outValus = new Dictionary<int, string>()
            {
                {10, "Tom" },
                {12, "Sam" },
                {14, "Nick" }
            };
            // засунуть Dictionary не получается пока, ну ничего, разберёмся
            string zalupaTexst = "Zalupa";

            DirectoryInfo catalog = new DirectoryInfo(@"D:\TEST");
            catalog.Create();
            catalog.CreateSubdirectory("SubCatalog");


            FileInfo Textfaile = new FileInfo(@"D:\TEST\Textfaile.txt");
            //Textfaile.Create(); можно так
            
            FileStream stream = Textfaile.Create();// а можно и вот так

            byte[] buffer = Encoding.Default.GetBytes(zalupaTexst); // здесь проблема тк только в байты переводит
            stream.Write(buffer);

            //
            Console.WriteLine($"Name: {Textfaile.FullName}");
            Console.WriteLine($"Time Create: {Textfaile.CreationTime}");
            Console.WriteLine(Textfaile.DirectoryName);
            Console.WriteLine(Textfaile.Exists);
            Console.WriteLine(Textfaile.Length);
            Console.WriteLine(Textfaile.Extension);
            

            stream.Close();
            
            */

            /*
            DirectoryInfo directory = new DirectoryInfo(@"D:\TEST");

            // Создание в папку TEST новых подпапок/ подкаталогов
            if (directory.Exists)
            {
                directory.CreateSubdirectory("SUBDIR");

                directory.CreateSubdirectory(@"MyDir\SubMyDir");

                Console.WriteLine("Директории созданы");
            }
            else
            {
                Console.WriteLine("Директория с именем: {0} не существует.", directory.FullName);
            }

            try
            {
                Directory.Delete(@"D:\TEST\MyDir", true);

            }
            catch (Exception e) { Console.WriteLine(e.Message); }
            */


            /*
             BinaryWriter
            Этот тип реализует интерфейс IDisposable. По окончании использования выдаленную ему 
            память следует прямо или косвенно освободить. Чтобы сделать это прямо, вызовите его 
            метод Dispose в блоке try/catch. Чтобы сделать это косвенно, используйте языковые 
            конструкции, такие как using (в C#) или Using (в Visual Basic). Дополнительные сведения
            см. в разделе "Использование объекта, реализующего IDisposable" в статье об интерфейсе IDisposable.

            BinaryWriter(Stream, Encoding, Boolean)	
            Инициализирует новый экземпляр класса BinaryWriter на 
            основании указанного потока и кодировки символов, а также при необходимости оставляет поток открытым.

            */

            Console.WriteLine("\n" + new string('_', 12));
            Console.WriteLine("I done the");
            Console.ReadLine();

        }
    }
}
