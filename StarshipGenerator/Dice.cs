using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace StarshipGenerator
{
    public class Dice
    {
        public Regex ValidDice = new Regex(@"^(\d*d\d+)?([+-]\d+)?");

        /// <summary>
        /// Number of dice to roll
        /// </summary>
        byte diceNumber;
        /// <summary>
        /// Number of sides on these dice
        /// </summary>
        byte diceSides;
        /// <summary>
        /// Modifier on dice roll
        /// </summary>
        byte modifier;

        /// <summary>
        /// Make a new dice object, initialising from values for each component
        /// </summary>
        /// <param name="x">Number of dice to roll</param>
        /// <param name="y">Number of sides on these dice</param>
        /// <param name="z">Modifier to the roll</param>
        public Dice(byte x, byte y, byte z)
        {
            diceNumber = x;
            diceSides = y;
            modifier = z;
        }

        /// <summary>
        /// MAke a new dice object, reading a roll string
        /// </summary>
        /// <param name="roll">String to parse diceroll from</param>
        public Dice(String roll)
        {
            if (String.IsNullOrWhiteSpace(roll) || !ValidDice.IsMatch(roll))
                throw new FormatException("Roll not in valid format");
            int dPos = roll.IndexOf('d', 0);
            if (dPos == -1)
            {
                diceNumber = diceSides = 0;//no dice
                modifier = byte.Parse(roll);
            }
            else
            {
                diceNumber = byte.Parse(roll.Substring(0, dPos));
                dPos++;//add 1 to skip the 'd' char
                int signPos = roll.IndexOf('+', dPos);//if dPos is -1 for a failure, start from 0
                if (signPos < 0)
                {
                    signPos = roll.IndexOf('-', dPos);//try again for minus sign
                }
                if (signPos < 0)//if still no sign
                {
                    diceSides = byte.Parse(roll.Substring(dPos, roll.Length - dPos));
                }
                else
                {
                    diceSides = byte.Parse(roll.Substring(dPos, signPos - dPos));
                    signPos++;//add 1 to skip the '+' or '-' char
                    modifier = byte.Parse(roll.Substring(signPos, roll.Length - signPos));
                }
            }
        }

        /// <summary>
        /// Show dice in the format XdY+Z
        /// </summary>
        /// <returns>dice as a roll string</returns>
        public override string ToString()
        {
            if (diceSides > 0)
                return String.Format("{0}d{1}{2}", diceNumber, diceSides, (modifier == 0 ? "" : (modifier < 0 ? modifier.ToString() : ("+" + modifier))));
            else
                return modifier.ToString();
        }

        /// <summary>
        /// Parse a Dice from the String
        /// </summary>
        /// <param name="roll">String to parse</param>
        /// <returns>A Dice Object representign the roll string</returns>
        public static Dice Parse(String roll)
        {
            return new Dice(roll);
        }
    }
}
