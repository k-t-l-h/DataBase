using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LINQtoSQL
{
    class Program
    {
        public static DataContext db;

        public static void SelectRockGenres()
        {
            Table<GenreDB> genres = db.GetTable<GenreDB>();
            var res = from genre in genres
                                    where genre.GenreID < 5
                                    select genre.GenreName;
            foreach (var r in res)
            {
                Console.WriteLine(r);
            }

        }

        public static void GenreNameGet()
        {
            Table<GenreDB> genres = db.GetTable<GenreDB>();
            Table<GroupDB> groups = db.GetTable<GroupDB>();

            //лямбда-выражение

            var res = groups.Join(genres, e => e.GenreID, o => o.GenreID, 
                (e, o) => new {GenreID = e.GenreID, Name = e.GroupName, GenreName = o.GenreName});


            var addresses = from groupe in groups
                            join genre in genres on groupe.GenreID equals genre.GenreID
                            select new { Name = groupe.GroupName, Genre = genre.GenreName };

            foreach (var address in addresses)
            {
                Console.WriteLine($"\"{address.Name}\"{address.Genre}"); ;
            }
        }

        public static void InsertPerson()
        {
            GenreDB newGenre = new GenreDB();
            newGenre.GenreID = 70;
            newGenre.GenreName = "New genre";
            Table<GenreDB> genres = db.GetTable<GenreDB>();

            var newgen = from c in genres
                         where c.GenreID == 70
                         select c.GenreName;

            if (newgen.Count() > 0)
            {
                Console.WriteLine("Информация уже была добавлена\n");
            }
            else
            {
                genres.InsertOnSubmit(newGenre);
                db.SubmitChanges();
                var me = from c in genres
                         where c.GenreID == 70
                         select c;

                foreach (var atr in me)
                {
                    Console.WriteLine($"\"{atr.GenreID}\" {atr.GenreName}\n"); ;
                }
            }
        }
        
                    

        public static void UpdatePerson()
        {

            
            Table<GenreDB> genres = db.GetTable<GenreDB>();

            var me = from c in genres
                         where c.GenreID == 70
                         select c;

            if (me.Count() > 0)
            {
                foreach (var atr in me)
                {
                    atr.GenreName = "Brand New Genre";
                    Console.WriteLine($"\"{atr.GenreID}\" {atr.GenreName}\n"); ;
                }
                db.SubmitChanges();
            }
            else
            {
                Console.WriteLine("Нет информации обо мне для обновления\n");
            }            
        }

        
        public static void DeletePerson()
        {
            Table<GroupDB> groups = db.GetTable<GroupDB>();
            

            var me = from c in groups
                     where c.GenreID == 11
                     select c;

            Console.WriteLine("Было удалено " + me.Count() + " строк");
            foreach (var atr in me)
            {
                groups.DeleteOnSubmit(atr);
                db.SubmitChanges();
            };
        }

        
        public static void Groupswith(int id)
        {
            Table<GroupDB> groups = db.GetTable<GroupDB>();
            

            var result = from groupe in groups
                         where groupe.GenreID == id
                         select groupe;

            foreach (var r in result)
            {
                Console.WriteLine($"\"{r.GroupName}\n"); ;
            }
        }
        

        static void Main(string[] args)
        {
            db = new DataContext(@"Data Source = .\SQLEXPRESS; Database = Lab01; Integrated Security = true");

            int status = -1;
            while (status == -1)
            {
                Console.WriteLine("1. Однотабличный запрос\n2. Многотабличный запрос\n3. Добавить данные\n4. Изменить данные\n5. Удалить данные\n6. Хранимая процедура\n0. Выход");
                Console.WriteLine("Выбор: ");
                status = Convert.ToInt32(Console.ReadLine());
                switch (status)
                {
                    case 1:
                        Console.WriteLine("\nРок-группы с жанром < 5:\n");
                        SelectRockGenres();
                        Console.WriteLine(" ");
                        status = -1;
                        break;
                    case 2:
                        Console.WriteLine("\nГруппы и названия их жанров: \n");
                        GenreNameGet();
                        status = -1;
                        break;
                    case 3:
                        Console.WriteLine("\nДобавление информации обо мне \n");
                        InsertPerson();
                        status = -1;
                        break;
                    case 4:
                        Console.WriteLine("\nИзменение информации обо мне \n");
                       UpdatePerson();
                        status = -1;
                        break;
                    case 5:
                        Console.WriteLine("\nУдаление информации обо мне \n");
                       DeletePerson();
                        status = -1;
                        break;
                    case 6:
                        Console.WriteLine("\nГруппы с жанром 70\n");
                        Groupswith(70);
                        status = -1;
                        break;
                    case 0:
                        Console.WriteLine("\nЗавершение\n");
                        break;
                    default:
                        Console.WriteLine("\nНеизвестный пункт меню\n");
                        status = -1;
                        break;
                }
            }
        }
    }
}
