using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Netfram_Peli
{
    public class Battle
    {
        //Army lists
        public static List<Units> pArmy = new List<Units>();
        public static List<Units> eArmy = new List<Units>();

        public static int bAttacker = -1;
        public static int bTarget = -1;
        public static int bEnemyAttacker = -1;
        public static int bEnemyTarget = -1;
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
            //Asking for the attacker
            while (attacker < 0)
            {
                WriteLine("[---------- Choices ----------]\n\nPlayer's turn: Choose unit by giving a number:\n");
                pArmy.ForEach(pUnit => WriteLine(pUnit.name + " (" + pUnit.hp + "/" + pUnit.maxHP + ") ", ConsoleColor.DarkYellow));
                ConsoleKeyInfo unitChoice = Console.ReadKey();

                switch (unitChoice.Key)
                {
                    case ConsoleKey.D1:
                        attacker = 0;
                        break;
                    case ConsoleKey.D2:
                        attacker = 1;
                        break;
                    case ConsoleKey.D3:
                        attacker = 2;
                        break;
                    default:
                        attacker = -1;
                        WriteLine("\nNot valid input.\n");
                        continue;
                }
                if (pArmy[attacker].power == 1)
                {
                    pArmy[attacker].power--;
                }
                else if (pArmy[attacker].power == 0)
                {
                    attacker = -1;
                    WriteLine("NOT READY TO USE");
                }
            }
            int target = -1;

            //Asking for the target
            while (target < 0)
            {
                WriteLine("\nChoose target:\n");
                eArmy.ForEach(eUnit => WriteLine(eUnit.name + " (" + eUnit.hp + "/" + eUnit.maxHP + ") ", ConsoleColor.DarkMagenta));
                ConsoleKeyInfo targetChoice = Console.ReadKey();

                switch (targetChoice.Key)
                {
                    case ConsoleKey.D1:
                        target = 0;
                        break;
                    case ConsoleKey.D2:
                        target = 1;
                        break;
                    case ConsoleKey.D3:
                        target = 2;
                        break;
                    default:
                        target = -1;
                        WriteLine("\nNot valid input.");
                        continue;
                }
            }
            Stack<Units> pUndo = new Stack<Units>();
            Stack<Units> eUndo = new Stack<Units>();
            bAttacker = attacker;
            bTarget = target;
            for (int i = 0; i < pArmy.Count(); i++)
            {
                pUndo.Push(pArmy[i]);
            }
            for (int i = 0; i < eArmy.Count(); i++)
            {
                eUndo.Push(eArmy[i]);
            }
            //Printing unit choices
            Console.Clear();
            WaitTime(0.2);
            PlayerTurnText();
            for (int i = 0; i < pArmy.Count(); i++)
            {
                if (attacker == i)
                {
                    WriteLine(pArmy[i].name + " (" + pArmy[i].hp + "/" + pArmy[i].maxHP + ") ", ConsoleColor.Cyan);
                }
                else
                {
                    WriteLine(pArmy[i].name + " (" + pArmy[i].hp + "/" + pArmy[i].maxHP + ") ", ConsoleColor.DarkYellow);
                }
            }
            WaitTime(0.2);
            TargetChoiceText();
            for (int x = 0; x < eArmy.Count(); x++)
            {
                if (target == x)
                {
                    WriteLine(eArmy[x].name + " (" + eArmy[x].hp + "/" + eArmy[x].maxHP + ") ", ConsoleColor.White);
                }
                else
                {
                    WriteLine(eArmy[x].name + " (" + eArmy[x].hp + "/" + eArmy[x].maxHP + ") ", ConsoleColor.DarkMagenta);
                }
            }
            WriteLine("\nPress enter to FIGHT!");
            Console.ReadLine();

            //Printing the fight result
            while (true)
            {
                if (eArmy[target].hp > 0)
                {
                    WriteLine("Player Attacks!\n");
                    eArmy[target].hp = eArmy[target].hp - pArmy[attacker].dmg;
                    Write(pArmy[attacker].ToString(), ConsoleColor.Cyan);
                    Write(" Attacks ");
                    Write(eArmy[target].ToString(), ConsoleColor.White);
                    Write(". Dealing ");
                    Write(pArmy[attacker].dmg.ToString(), ConsoleColor.DarkBlue);
                    Write(" damage.");
                    Console.WriteLine();
                    Write(eArmy[target].ToString(), ConsoleColor.White);
                    Write(" has ");
                    Write(eArmy[target].hp.ToString(), ConsoleColor.DarkRed);
                    Write(" HP remaining.\n");

                    break;
                }
            }
            if (eArmy[target].hp <= 0)
                eArmy.RemoveAt(target);
            //Going to enemy attack
            EnemyAttack();
            //Then going to check if player units are ready
            AreUnitsReady();
            //Going to UNDO
            WriteLine("Undo? Use Z to undo and TAB to continue with current settings");
            ConsoleKeyInfo undochoice = Console.ReadKey();
            switch (undochoice.Key)
            {
                case ConsoleKey.Z:
                    WriteLine("Lets undo this round");
                    pUndo.Peek();
                    eUndo.Peek();

                    break;
                case ConsoleKey.Tab:
                    WriteLine("Just continuing");
                    break;
            }
            //After that checking if game is still on
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

            bEnemyTarget = eTarget;
            bEnemyAttacker = eAttacker;
            while (eArmy.Count() > 0)
            {
                if (pArmy[eTarget].hp > 0)
                {
                    WriteLine("\nEnemy Attacks\n");
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
        //Checking if player units are ready
        #region
        static void AreUnitsReady()
        {
            int unitsReady = 0;

            for (int i = 0; i < pArmy.Count(); i++)
            {
                if (pArmy[i].power > 0)
                {
                    unitsReady++;
                }
            }

            if (unitsReady == 1)
            {
                GameStillOn();
            }
            else if (unitsReady == 0)
            {
                for (int i = 0; i < pArmy.Count(); i++)
                {
                    pArmy[i].power++;
                }
            }
        }
        #endregion
        //Check if lists still have units
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
        //Telling which team won the game
        public static void WhatTeamWon()
        {
            if (pArmy.Count() > eArmy.Count())
            {
                WriteLine("Congrats you destroyed all enemies and you WIN the game.");
            }
            else if (eArmy.Count() > pArmy.Count())
            {
                WriteLine("Enemy destroyed your vikings and WINS the game. Better luck next time!");
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

        //This allows to print text on set position
        #region
        public static int origRow;
        public static int origCol;

        public static void WriteAt(string s, int x, int y, ConsoleColor color = ConsoleColor.DarkGreen)
        {
            try
            {
                Console.ForegroundColor = color;
                Console.SetCursorPosition(origCol + x, origRow + y);
                Console.Write(s);
            }
            catch (ArgumentOutOfRangeException e)
            {
                Console.Clear();
                Console.WriteLine(e.Message);
            }
        }

        #endregion

        //TEXTS that replaces after choosing attacker & target
        #region
        public static void PlayerTurnText()
        {
            WriteLine("[---------- Fight ----------]\n");
            WriteLine("Attacker Locked:\n");
        }
        public static void TargetChoiceText()
        {
            WriteLine("\nTarget Locked:\n");
        }
        #endregion

        //Using for text to wait before appearing
        public static void WaitTime(double seconds)
        {
            System.Threading.Thread.Sleep((int)(seconds * 1000));
        }
    }
}
