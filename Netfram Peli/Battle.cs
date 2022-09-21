using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netfram_Peli
{
    public class Battle
    {
        //Army lists
        public static List<Units> pArmy = new List<Units>();
        public static List<Units> eArmy = new List<Units>();

        // Init the battle
        #region
        public static void Init()
        {
            pArmy.Add(new Units("Human Warrior", 100, 50));
            pArmy.Add(new Units("Human Archer", 100, 50));
            pArmy.Add(new Units("Human Mage", 100, 50));

            eArmy.Add(new Units("Skeleton Warrior", 100, 50));
            eArmy.Add(new Units("Skeleton Archer", 100, 50));
            eArmy.Add(new Units("Skeleton Mage", 100, 50));
        }
        #endregion

        // Player Fighting
        #region
        public static void PlayerFighting()
        {
            int attacker = -1;
            string unitChoice;

            while (attacker < 0)
            {
                unitChoice = AskUnit();
                int convertUnitChoice = Convert.ToInt32(unitChoice);

                if (convertUnitChoice > 0 && convertUnitChoice <= pArmy.Count())
                {
                    attacker = convertUnitChoice - 1;
                }
            }
            int target = -1;
            string targetChoice;

            while (target < 0)
            {
                targetChoice = AskTarget();
                int convertTargetChoice = Convert.ToInt32(targetChoice);

                if (convertTargetChoice > 0 && convertTargetChoice <= eArmy.Count())
                {
                    target = convertTargetChoice - 1;
                }
            }
            while (true)
            {
                if (eArmy[target].hp > 0)
                {
                    Console.WriteLine("\nPress enter to attack");
                    Console.ReadLine();

                    eArmy[target].hp = eArmy[target].hp - pArmy[attacker].dmg;
                    Console.WriteLine(pArmy[attacker] + " Attacks " + eArmy[target] + ". Dealing " + pArmy[attacker].dmg + " damage.");
                    Console.WriteLine(eArmy[target] + " has " + eArmy[target].hp + " HP remaining.");

                    Console.WriteLine("\nPress enter to continue");
                    Console.ReadLine();
                    break;
                }
            }
            if (eArmy[target].hp <= 0)
            {
                eArmy.RemoveAt(target);
            }
            EnemyAttack();
            GameStillOn();
        }
        #endregion

        //Enemy Fighting
        #region
        public static void EnemyAttack()
        {
            Random rnd = new Random();

            int eTarget = rnd.Next(pArmy.Count());
            int eAttacker = rnd.Next(pArmy.Count());

            while (true)
            {
                if (pArmy[eTarget].hp > 0)
                {
                    Console.WriteLine("Enemy's turn\n");

                    pArmy[eTarget].hp = pArmy[eTarget].hp - eArmy[eAttacker].dmg;
                    Console.WriteLine(eArmy[eAttacker] + " Attacks " + pArmy[eTarget] + ". Dealing " + eArmy[eAttacker].dmg + " damage.");
                    Console.WriteLine(pArmy[eTarget] + " has " + pArmy[eTarget].hp + " HP remaining.");

                    Console.WriteLine("\nPress enter to continue");
                    Console.ReadLine();
                    Console.Clear();
                    break;
                }
            }
            if (pArmy[eTarget].hp <= 0)
            {
                pArmy.RemoveAt(eTarget);
            }
        }
        #endregion

        //Check if list still have units
        #region
        static void GameStillOn()
        {
            while (true)
            {
                if (pArmy.Count() > 0 && eArmy.Count() > 0)
                {
                    PlayerFighting();
                }
                else if (pArmy.Count() == 0 || eArmy.Count() == 0)
                {
                    break;
                }
            }
        }
        public static void WhatTeamWon()
        {
            if (pArmy.Count() > eArmy.Count())
            {
                Console.WriteLine("Player WINS");
            }
            else if (eArmy.Count() > pArmy.Count())
            {
                Console.WriteLine("Enemy WINS");
            }
        }
        #endregion
        // Asking user for unit and target
        #region
        private static string AskUnit()
        {
            Console.WriteLine("Player's turn: Choose unit by giving a number:\n");
            foreach (Units pUnit in pArmy)
            {
                Console.WriteLine(pUnit.name);
            }
            return Console.ReadLine();
        }
        private static string AskTarget()
        {
            Console.WriteLine("\nChoose target:");
            foreach (Units eUnit in eArmy)
            {
                Console.WriteLine(eUnit.name);
            }
            return Console.ReadLine();
        }
        #endregion
    }
}
