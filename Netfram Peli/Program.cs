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
            // Console Designs
            Console.ForegroundColor = ConsoleColor.DarkGreen;

            // Calling Init from Battle.cs
            Battle.Init();

            // Starting the game
            Console.WriteLine("Welcome to the game!\n" +
                "\nPlayer Army:");
            foreach (Units pUnit in Battle.pArmy)
            {
                Console.WriteLine(pUnit.name);
            }

            Console.WriteLine("\nEnemy Army:");
            foreach (Units eUnit in Battle.eArmy)
            {
                Console.WriteLine(eUnit.name);
            }
            // Starting the fight
            Console.WriteLine("\nBattle Begins.");
            Console.ReadLine();
            Console.Clear();

            Battle.PlayerFighting();
            //Telling who won
            Console.WriteLine("Thanks for playing");
            Battle.WhatTeamWon();
        }
    }
}