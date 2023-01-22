using System;
using Bochacha.Domain;
using Microsoft.VisualBasic;
using System.Collections;
using System.ComponentModel;
using System.Numerics;

namespace Bochacha.Domain
{ 
    public class USER
    {
        public Guid id { get; set; }

        public string Type { get; set; } = String.Empty;
        /*
        {
            get;set;
            //get { return type; }
            //set { type = value; }
        }
        */
        //NAvi \__/\\\



        public List<Request?> Requests { get; set; } = new List<Request?>();
        public List<Room?> Rooms { get; set; } = new List<Room?>();

        public void AddRequest(Request? request)
        {
            Requests.Add(request);
        }

        public void RemoveReqAt(int index)
        {
            Requests.RemoveAt(index);
        }

        //------

        public void AddRoom(Room? room)
        {
            Rooms.Add(room);
        }

        public void RemoveRoomAt(int index)
        {
            Rooms.RemoveAt(index);
        }

    }


}

