using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netfram_Peli
{
    public class Units
    {
        // What unit has
        public string name { get; set; }
        public int hp { get; set; }
        public int maxHP { get; set; }
        public int dmg { get; set; }
        public int power;

        // constructor
        public Units(string name, int hp, int dmg)
        {
            this.name = name;
            this.hp = hp;
            this.dmg = dmg;
            this.maxHP = maxHP + hp;
            this.power = 1;
        }
        public override string ToString()
        {
            return this.name;
        }
    }
}
