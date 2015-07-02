using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarshipGenerator
{
    public class CTest
    {
        public static void Main() 
        {
            DiceRoll d1 = new DiceRoll(2, 1, 4);
            DiceRoll d2 = new DiceRoll("2d10+1d5+4");
            DiceRoll d3 = new DiceRoll("1d10");
            DiceRoll d4 = new DiceRoll("5");
            DiceRoll d5 = new DiceRoll(2, 0, 0);

            Console.WriteLine(d1.ToString());
            Console.WriteLine(d2.ToString());
            Console.WriteLine(d3.ToString());
            Console.WriteLine(d4.ToString());
            Console.WriteLine(d5.ToString());

            Console.ReadLine();
        }
    }
}
