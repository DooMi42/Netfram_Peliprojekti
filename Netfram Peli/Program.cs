using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netfram_Peli
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.Clear();
            // Calling Init from Battle.cs
            Battle.Init();
            // Starting the game
            Battle.WriteLine("Welcome to the game!\n\nPlayer Army:  Enemy Army:");
            Console.WriteLine();
            Battle.pArmy.ForEach(pUnit => Battle.WriteLine(pUnit.name, ConsoleColor.DarkYellow));
            int y = 4;
            foreach (var eUnit in Battle.eArmy)
            {
                Battle.WriteAt(eUnit.name, 15, y, ConsoleColor.DarkMagenta);
                y++;
            }
            // Starting the fight
            Console.WriteLine();
            Battle.WriteLine("\nBattle Begins.");
            Console.ReadLine();
            Console.Clear();
            Battle.PlayerFighting();
            //Telling who won
            Battle.WriteLine("Thanks for playing!\n");
            Battle.WhatTeamWon();
        }
    }
}