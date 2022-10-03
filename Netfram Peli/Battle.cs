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
            pArmy.Add(new Units("Eivor", 120, 30));
            pArmy.Add(new Units("Basim", 110, 40));
            pArmy.Add(new Units("Astrid", 100, 50));
            eArmy.Add(new Units("Rikiwulf", 140, 30));
            eArmy.Add(new Units("Alrek", 120, 50));
            eArmy.Add(new Units("Bjarke Broadside", 100, 70));
        }
        #endregion
        // Player Fighting
        #region
        public static void PlayerFighting()
        {
            int attacker = -1;

            while (attacker < 0)
            {
                WriteLine("Player's turn: Choose unit by giving a number:\n");
                pArmy.ForEach(pUnit => WriteLine(pUnit.name, ConsoleColor.DarkYellow));
                ConsoleKeyInfo unitChoice = Console.ReadKey();
                if (unitChoice.Key == ConsoleKey.D1 && 1 <= pArmy.Count())
                {
                    attacker = 0;
                }
                else if (unitChoice.Key == ConsoleKey.D2 && 2 <= pArmy.Count())
                {
                    attacker = 1;
                }
                else if (unitChoice.Key == ConsoleKey.D3 && 3 <= pArmy.Count())
                {
                    attacker = 2;
                }
                else
                {
                    attacker = -1;
                    WriteLine("\nNot valid input.\n");
                }
            }
            int target = -1;

            while (target < 0)
            {
                WriteLine("\nChoose target:\n");
                eArmy.ForEach(eUnit => WriteLine(eUnit.name, ConsoleColor.DarkMagenta));
                ConsoleKeyInfo targetChoice = Console.ReadKey();
                if (targetChoice.Key == ConsoleKey.D1 && 1 <= eArmy.Count())
                {
                    target = 0;
                }
                else if (targetChoice.Key == ConsoleKey.D2 && 2 <= eArmy.Count())
                {
                    target = 1;
                }
                else if (targetChoice.Key == ConsoleKey.D3 && 3 <= eArmy.Count())
                {
                    target = 2;
                }
                else
                {
                    target = -1;
                    WriteLine("\nNot valid input.");
                }
            }
            while (true)
            {
                if (eArmy[target].hp > 0)
                {
                    WriteLine("\nPlayer turn!");
                    Console.ReadLine();
                    eArmy[target].hp = eArmy[target].hp - pArmy[attacker].dmg;
                    Write(pArmy[attacker].ToString(), ConsoleColor.DarkYellow);
                    Write(" Attacks ");
                    Write(eArmy[target].ToString(), ConsoleColor.DarkMagenta);
                    Write(". Dealing ");
                    Write(pArmy[attacker].dmg.ToString(), ConsoleColor.DarkBlue);
                    Write(" damage.");
                    Console.WriteLine();
                    Write(eArmy[target].ToString(), ConsoleColor.DarkMagenta);
                    Write(" has ");
                    Write(eArmy[target].hp.ToString(), ConsoleColor.DarkRed);
                    Write(" HP remaining.\n");
                    WriteLine("\nPress enter to continue!");
                    Console.ReadLine();
                    break;
                }
            }
            if (eArmy[target].hp <= 0)
                eArmy.RemoveAt(target);

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
            int eAttacker = rnd.Next(eArmy.Count());
            while (eArmy.Count() > 0)
            {
                if (pArmy[eTarget].hp > 0)
                {
                    WriteLine("Enemy's turn\n");
                    pArmy[eTarget].hp = pArmy[eTarget].hp - eArmy[eAttacker].dmg;
                    Write(eArmy[eAttacker].ToString(), ConsoleColor.DarkMagenta);
                    Write(" Attacks ");
                    Write(pArmy[eTarget].ToString(), ConsoleColor.DarkYellow);
                    Write(". Dealing ");
                    Write(eArmy[eAttacker].dmg.ToString(), ConsoleColor.DarkBlue);
                    Write(" damage.");
                    Console.WriteLine();
                    Write(pArmy[eTarget].ToString(), ConsoleColor.DarkYellow);
                    Write(" has ");
                    Write(pArmy[eTarget].hp.ToString(), ConsoleColor.DarkRed);
                    Write(" HP remaining.\n");
                    WriteLine("\nPress enter to continue!");
                    Console.ReadLine();
                    Console.Clear();
                    break;
                }
            }
            if (pArmy[eTarget].hp <= 0)
                pArmy.RemoveAt(eTarget);
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
                WriteLine("Congrats you destroyed all enemies and you WIN the game.");
            }
            else if (eArmy.Count() > pArmy.Count())
            {
                WriteLine("Enemy destroyed all humans and WINS the game. Better luck next time!");
            }
        }
        #endregion
        //COLLORS
        #region
        public static void Write(string text, ConsoleColor color = ConsoleColor.DarkGreen)
        {
            Console.ForegroundColor = color;
            Console.Write(text);
            Console.ResetColor();
        }
        public static void WriteLine(string text, ConsoleColor color = ConsoleColor.DarkGreen)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(text);
            Console.ResetColor();
        }
        #endregion
    }
}
