using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netfram_Peli
{
    public class Units
    {
        public string name { get; set; }
        public int hp { get; set; }
        public int dmg { get; set; }
        public Units(string name, int hp, int dmg)
        {
            this.name = name;
            this.hp = hp;
            this.dmg = dmg;
        }
        public override string ToString()
        {
            return this.name;
        }
    }
}
