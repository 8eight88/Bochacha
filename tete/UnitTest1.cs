using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Bochacha.Domain;
using TestProject1;

namespace MyFirstUnitTestsKyaaa
{
    public class UnitTest1
    {
        [Fact]
        public void VoidTest()
        {
            var testHelper = new HelpmeTest();
            var roomRepository = testHelper.RoomRepository;

            Assert.Equal(1, 1);
        }
        [Fact]
        public void TestAdd()
        {
            var testHelper = new HelpmeTest();
            var roomRepository = testHelper.RoomRepository;
            var room = new Room { number = 13 };

            roomRepository.AddAsync(room).Wait();
            //Запрещаем отслеживание сущностей (разрываем связи с БД)
            roomRepository.ChangeTrackerClear();

            Assert.True(roomRepository.GetAllAsync().Result.Count == 2);
            Assert.Equal(11, roomRepository.GetByNameAsync(11).Result.number);
            Assert.Equal(13, roomRepository.GetByNameAsync(13).Result.number);
            Assert.Equal(2, roomRepository.GetByNameAsync(11).Result.EquiCount);
        }
        [Fact]
        public void TestUpdateAdd()
        {
            var testHelper = new HelpmeTest();
            var roomRepository = testHelper.RoomRepository;
            var roo = roomRepository.GetByNameAsync(11).Result;
            //Запрещаем отслеживание сущностей (разрываем связи с БД)
            roomRepository.ChangeTrackerClear();
            roo.number = 10;
            var Jonumber = new Equipment { id = Guid.NewGuid(), name = "Frog" };
            roo.AddEquipment(Jonumber);

            roomRepository.UpdateAsync(roo).Wait();

            Assert.Equal(10, roomRepository.GetByNameAsync(10).Result.number);
            Assert.Equal(3, roomRepository.GetByNameAsync(10).Result.EquiCount);
        }
        [Fact]
        public void TestUpdateDelete()
        {
            var testHelper = new HelpmeTest();
            var roomRepository = testHelper.RoomRepository;
            var jo = roomRepository.GetByNameAsync(11).Result;
            //Запрещаем отслеживание сущностей (разрываем связи с БД)
            roomRepository.ChangeTrackerClear();
            jo.RemoveAt(0);

            roomRepository.UpdateAsync(jo).Wait();

            Assert.Equal(1, roomRepository.GetByNameAsync(11).Result.EquiCount);
        }
    }
}