using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netfram_Peli
{
    public class Units
    {
        private string name;
        public int hp { get; set; }
        public int dmg { get; set; }
        public Units(string name, int hp, int dmg)
        {
            this.name = name;
            this.hp = 0;
            this.dmg = 0;
        }
        public static void Fighting()
        {
            List<Units> pArmy = new List<Units>();

            pArmy.Add(new Units("Human Warrior ", 100, 50));
            pArmy.Add(new Units("Human Archer ", 100, 50));

            List<Units> eArmy = new List<Units>();

            eArmy.Add(new Units("Skeleton Warrior ", 100 , 50));
            eArmy.Add(new Units("Skeleton Archer ", 100, 50));
            eArmy.Add(new Units("Skeleton Mage ", 100, 50));
            
            string unitChoice = AskUnit();

            while (!(int.TryParse(unitChoice, out int result) && IsBetween1And2(result)))
            {
                unitChoice = AskUnit();
            }
            string targetChoice = AskTarget();

            while (!(int.TryParse(targetChoice, out int result) && IsBetween1And3(result)))
            {
                targetChoice = AskTarget();
            }

            if (unitChoice == "1" && targetChoice == "1")
            {
                Console.WriteLine(pArmy[0] + "Attacks " + eArmy[0] + ", dealing 50 damage");
                
            }
            else if (unitChoice == "1" && targetChoice == "2")
            {
                Console.WriteLine(pArmy[0] + "Attacks " + eArmy[1]);
            }
            else if (unitChoice == "1" && targetChoice == "3")
            {
                Console.WriteLine(pArmy[0] + "Attacks " + eArmy[2]);
            }
            else if (unitChoice == "2" && targetChoice == "1")
            {
                Console.WriteLine(pArmy[1] + "Attacks " + eArmy[0]);
            }
            else if (unitChoice == "2" && targetChoice == "2")
            {
                Console.WriteLine(pArmy[1] + "Attacks " + eArmy[1]);
            }
            else if (unitChoice == "2" && targetChoice == "3")
            {
                Console.WriteLine(pArmy[1] + "Attacks " + eArmy[2]);
            }
        }
        private static bool IsBetween1And2(int value)
        {
            return value <= 2 && value >= 1;
        }
        private static bool IsBetween1And3(int value)
        {
            return value <= 3 && value >= 1;
        }
        private static string AskUnit()
        {
            Console.WriteLine("\nPlayer's turn: Choose unit by giving a number:");
            Console.WriteLine("1: Human Warrior\n" +
                "2: Human Archer\n");
            return Console.ReadLine();
        }
        private static string AskTarget()
        {
            Console.WriteLine("\nChoose target:");
            Console.WriteLine("1: Skeleton Warrior\n" +
                "2: Skeleton Archer\n" +
                "3: Skeleton Mage");
            return Console.ReadLine();
        }
        public override string ToString()
        {
            return this.name;
        }
    }
}
