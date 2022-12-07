using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Netfram_Peli
{
    public class TurnState
    {
        public List<int> playerHPs { get; private set; }
        public List<int> enemyHPs { get; private set; }

        public TurnState() 
        {
            this.playerHPs = new List<int>();
            this.enemyHPs = new List<int>();
        }
    }
}
