using Bochacha.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Bochacha.Domain
{
    public class Room
    {

        public Guid id { get; set; }
        public string? type { get; set; } = String.Empty;
        public int? number { get; set; }
        public int? capacity { get; set; }
        public string? curator { get; set; } = String.Empty;
        public DateTime? cur_ass_date { get; set; }
        public DateTime? mod_date { get; set; }
        public double? space { get; set; }
        public bool? sat_con { get; set; }

        //NAvi \__/\\\

        public List<Equipment?> Equipmenti { get; set; } = new List<Equipment?>();
        public Building? Building { get; set; }
        public USER? USER { get; set; }

        public Guid? UserId { get; set; }
        public Guid BuId { get; set; }

        public int EquiCount { get { return Equipmenti.Count; } }

        public void AddEquipment(Equipment equip)
        {
            Equipmenti.Add(equip);
        }

        public void RemoveAt(int index)
        {
            Equipmenti.RemoveAt(index);
        }
    }
}
