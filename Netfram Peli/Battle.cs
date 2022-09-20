using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netfram_Peli
{
    public class Battle
    {
        static List<Units> pArmy = new List<Units>();
        static List<Units> eArmy = new List<Units>();
        public static void Init()
        {
            pArmy.Add(new Units("Human Warrior ", 100, 50));
            pArmy.Add(new Units("Human Archer ", 100, 50));

            eArmy.Add(new Units("Skeleton Warrior ", 100, 50));
            eArmy.Add(new Units("Skeleton Archer ", 100, 50));
            eArmy.Add(new Units("Skeleton Mage ", 100, 50));
        }
        public static void Fighting()
        {

            int attacker = -1;
            string unitChoice;

            while (attacker < 0)
            {
                unitChoice = AskUnit();
                int n = Convert.ToInt32(unitChoice);

                if (n > 0 && n <= pArmy.Count())
                {
                    attacker = n - 1;
                }
            }
            int target = -1;
            string targetChoice;

            while (target < 0)
            {
                targetChoice = AskTarget();
                int nn = Convert.ToInt32(targetChoice);

                if (nn > 0 && nn <= eArmy.Count())
                {
                    target = nn - 1;
                }
            }

            string attack;
            Units toRemove = null;
            while (true)
            {
                if (eArmy[target].hp > 0)
                {
                    attack = Attack();

                    eArmy[target].hp = eArmy[target].hp - pArmy[attacker].dmg;
                    Console.WriteLine(pArmy[attacker] + " Attacks " + eArmy[target] + " , dealing " + pArmy[attacker].dmg);
                    Console.WriteLine(eArmy[target] + " Has " + eArmy[target].hp + " HP");
                }
                else if (eArmy[target].hp <= 0)
                {
                    toRemove = eArmy[target];
                    eArmy.Remove(toRemove);
                    break;
                }
            }
        }

        private static string Attack()
        {
            Console.WriteLine("\nPress enter to Attack!!");
            return Console.ReadLine();
        }
        private static string AskUnit()
        {
            Console.WriteLine("\nPlayer's turn: Choose unit by giving a number:");
            foreach (Units pUnit in pArmy)
            {
                Console.WriteLine(pUnit.name);
            }
            return Console.ReadLine();
        }
        private static string AskTarget()
        {
            Console.WriteLine("\nChoose target:");
            foreach (Units unit in eArmy)
                {
                    Console.WriteLine(unit.name);
                }

            return Console.ReadLine();
        }
    }
}
