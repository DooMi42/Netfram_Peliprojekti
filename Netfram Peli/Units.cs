using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netfram_Peli
{
    public class Units
    {
        //What unit has
        public string Name { get; set; }
        public int Hp { get; set; }
        public int MaxHP { get; set; }
        public int Dmg { get; set; }
        public int Power;

        //Constructor
        public Units(string name, int hp, int dmg)
        {
            this.Name = name;
            this.Hp = hp;
            this.Dmg = dmg;
            this.MaxHP = MaxHP + hp;
            this.Power = 1;
        }

        public override string ToString()
        {
            return this.Name;
        }
    }
}
