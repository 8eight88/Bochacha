using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Bochacha.Domain
{
    public class Building
    {

        public Guid id { get; set; }
        public string? address { get; set; } = String.Empty;
        public string? name { get; set; } = String.Empty;

        //NAvi \__/\\\

        public List<Room?> Rooms { get; set; } = new List<Room?>();


        public void AddRoom(Room room)
        {
            Rooms.Add(room);
        }

        public void RemoveAt(int index)
        {
            Rooms.RemoveAt(index);
        }
    }
}
