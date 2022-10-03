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
            Battle.WriteLine("Welcome to the game!\n\nPlayer Army:");
            Battle.pArmy.ForEach(pUnit => Battle.WriteLine(pUnit.name, ConsoleColor.DarkYellow));
            Battle.WriteLine("\nEnemy Army:");
            Battle.eArmy.ForEach(eUnit => Battle.WriteLine(eUnit.name, ConsoleColor.DarkMagenta));
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