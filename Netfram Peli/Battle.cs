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

        public static List<int> pArmySavedHP = new List<int>();
        public static List<int> eArmySavedHP = new List<int>();
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

            foreach (var pUnit in pArmy)
            {
                pArmySavedHP.Add(pUnit.Hp);
            }

            foreach (var eUnit in eArmy)
            {
                eArmySavedHP.Add(eUnit.Hp);
            }
        }
        #endregion
        // Player Fighting
        #region
        public static void PlayerFighting()
        {
            int attacker = -1;
            for (int i = 0; i < pArmy.Count(); i++)
            {
                pArmySavedHP[i] = pArmy[i].Hp;
            }
            for (int i = 0; i < eArmy.Count(); i++)
            {
                eArmySavedHP[i] = eArmy[i].Hp;
            }
            
            //Asking for the attacker
            while (attacker < 0)
            {
                WriteLine("[---------- Choices ----------]\n\nPlayer's turn: Choose unit by giving a number:\n");
                pArmy.ForEach(pUnit => WriteLine(pUnit.Name + " (" + pUnit.Hp + "/" + pUnit.MaxHP + ") ", ConsoleColor.DarkYellow));
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
                if (pArmy[attacker].Power == 1)
                {
                    pArmy[attacker].Power--;
                }
                else if (pArmy[attacker].Power == 0)
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
                eArmy.ForEach(eUnit => WriteLine(eUnit.Name + " (" + eUnit.Hp + "/" + eUnit.MaxHP + ") ", ConsoleColor.DarkMagenta));
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
            Units enemy = eArmy[target];

            //Printing unit choices
            Console.Clear();
            WaitTime(0.2);
            PlayerTurnText();
            for (int i = 0; i < pArmy.Count(); i++)
            {
                if (attacker == i)
                {
                    WriteLine(pArmy[i].Name + " (" + pArmy[i].Hp + "/" + pArmy[i].MaxHP + ") ", ConsoleColor.Cyan);
                }
                else
                {
                    WriteLine(pArmy[i].Name + " (" + pArmy[i].Hp + "/" + pArmy[i].MaxHP + ") ", ConsoleColor.DarkYellow);
                }
            }
            WaitTime(0.2);
            
            TargetChoiceText();
            for (int x = 0; x < eArmy.Count(); x++)
            {
                WriteLine(eArmy[x].Name + " (" + eArmy[x].Hp + "/" + eArmy[x].MaxHP + ") ", target == x ? ConsoleColor.White : ConsoleColor.DarkMagenta);
            }

            WriteLine("\nPress enter to FIGHT!");
            Console.ReadLine();

            //Printing the fight result
            while (true)
            {
                if (enemy.Hp > 0)
                {
                    WriteLine("Player Attacks!\n");
                    enemy.Hp = enemy.Hp - pArmy[attacker].Dmg;
                    Write(pArmy[attacker].ToString(), ConsoleColor.Cyan);
                    Write(" Attacks ");
                    Write(enemy.ToString(), ConsoleColor.White);
                    Write(". Dealing ");
                    Write(pArmy[attacker].Dmg.ToString(), ConsoleColor.DarkBlue);
                    Write(" damage.");
                    Console.WriteLine();
                    Write(enemy.ToString(), ConsoleColor.White);
                    Write(" has ");
                    Write(enemy.Hp.ToString(), ConsoleColor.DarkRed);
                    Write(" HP remaining.\n");

                    break;
                }
            }

            if (enemy.Hp <= 0)
                eArmy.RemoveAt(target);

            //Going to enemy attack
            EnemyAttack();
            //Then going to check if player units are ready
            AreUnitsReady();
            //After that checking if game is still on
            //Going to UNDO
            TEST();
            WriteLine("Undo? Use Z to undo and TAB to continue with current settings");
            ConsoleKeyInfo undochoice = Console.ReadKey();
            switch (undochoice.Key)
            {
                case ConsoleKey.Z:
                    WriteLine("Lets undo this round");
                        for (int i = 0; i < pArmy.Count(); i++)
                        {
                            pArmy[i].Hp = pArmySavedHP[i];
                        }
                        for (int i = 0; i < eArmy.Count(); i++)
                        {
                            eArmy[i].Hp = eArmySavedHP[i];
                        }
                    break;
                case ConsoleKey.Tab:
                    WriteLine("Just continuing");
                    break;
            }
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

            Units enemysTarget = pArmy[eTarget];
            Units enemyAttacker = eArmy[eAttacker];


            if (eArmy.Any() && enemysTarget.Hp > 0)
            {
                WriteLine("\nEnemy Attacks\n");        
                enemysTarget.Hp = enemysTarget.Hp - enemyAttacker.Dmg;
                Write(enemyAttacker.ToString(), ConsoleColor.DarkMagenta);
                Write(" Attacks ");
                Write(enemysTarget.ToString(), ConsoleColor.DarkYellow);
                Write(". Dealing ");
                Write(enemyAttacker.Dmg.ToString(), ConsoleColor.DarkBlue);
                Write(" damage.");
                Console.WriteLine();
                Write(enemysTarget.ToString(), ConsoleColor.DarkYellow);
                Write(" has ");
                Write(enemysTarget.Hp.ToString(), ConsoleColor.DarkRed);
                Write(" HP remaining.\n");
                WriteLine("\nPress enter to continue!");
                Console.ReadLine();
                Console.Clear();
            }

            if (pArmy[eTarget].Hp <= 0)
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
                if (pArmy[i].Power > 0)
                {
                    unitsReady++;
                }
            }

            if (unitsReady == 1)
            {
                TEST();
            }
            else if (unitsReady == 0)
            {
                for (int i = 0; i < pArmy.Count(); i++)
                {
                    pArmy[i].Power++;
                }
            }
        }
        static void TEST()
        {
            WriteLine("YOU WANT TO UNDO?");
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
