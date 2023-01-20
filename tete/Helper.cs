using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Numerics;
using Bochacha.Domain;
using Bochacha.Infrastructure.Data;
using Bochacha.Infrastructure;

namespace TestProject1
{
    public class HelpmeTest
    {
        private readonly Context _context;
        public HelpmeTest()
        {
            //Используем базу обычную базу данных, не в памяти
            //Имя тестовой базы данных должно отличатсья от базы данных проекта
            var contextOptions = new DbContextOptionsBuilder<Context>()
                .UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=BONCH; Integrated Security=true")
                .Options;

            _context = new Context(contextOptions);


            _context.Database.EnsureDeleted();
            _context.Database.EnsureCreated();

            //Значение идентификатора явно не задаем. Используем для идентификации уникальное в рамках теста имя
            var roo1 = new Room
            {
                number = 11,
                BuId = Guid.NewGuid(),
            };
            roo1.AddEquipment(new Equipment { id = Guid.NewGuid(), type = "Cool", name = "PC", serialID = 12 });//, addition_date = new DateTime(2022, 11, 5) });
            roo1.AddEquipment(new Equipment { id = Guid.NewGuid(), type = "Shitty", name = "PC_2", serialID = 22 });//, addition_date = new DateTime(2022, 12, 5) });

            _context.Rooms.Add(roo1);
            _context.SaveChanges();
            //Запрещаем отслеживание (разрываем связи с БД)
            _context.ChangeTracker.Clear();

        }

        public RoomRepository RoomRepository
        {
            get
            {
                return new RoomRepository(_context);
            }
        }

    }
}
