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
            Console.ForegroundColor = ConsoleColor.DarkGreen;

            Console.WriteLine("Welcome to the game!\n" +
                "\nPlayer Army:");
            Console.WriteLine("Human Warrior\n" +
                "Human Archer\n");

            Console.WriteLine("\nEnemy Army:");
            Console.WriteLine("Skeleton Warrior\n" +
                "Skeleton Archer\n" +
                "Skeleton Mage\n");

            Console.WriteLine("\nBattle Begins.");
            Console.ReadLine();
            Console.Clear();

            Units.Fighting();
        }
    }
}