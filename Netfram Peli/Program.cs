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
            // Calling Init from Battle.cs
            Battle.Init();

            // Starting the game
            Battle.WriteLine("Welcome to the game!\n" +
                "\nPlayer Army:");
            foreach (Units pUnit in Battle.pArmy)
            {
                Battle.WriteLine(pUnit.name, ConsoleColor.DarkYellow);
            }

            Battle.WriteLine("\nEnemy Army:");
            foreach (Units eUnit in Battle.eArmy)
            {
                Battle.WriteLine(eUnit.name, ConsoleColor.DarkMagenta);
            }
            // Starting the fight
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