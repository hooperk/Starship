using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace StarshipGenerator
{
    public struct DiceRoll
    {
        public static Regex ValidDice = new Regex(@"^([+-]?\d*d10)([+-]\d*d5)?([+-]\d+)?|([+-]?\dd5)([+-]\d+)?|[+-]?\d+");
        public static Regex ParseDice = new Regex(@"^(([+-]\d*)d10)?([+-]?(\d*)d5)?([+-]\d+)?");

        /// <summary>
        /// Number of d10
        /// </summary>
        int d10;
        /// <summary>
        /// Number of d5
        /// </summary>
        int d5;
        /// <summary>
        /// integer modifier
        /// </summary>
        int modifier;

        /// <summary>
        /// Make a new dice object, initialising from values for each component
        /// </summary>
        /// <param name="d10">Number of d10</param>
        /// <param name="d5">Number of d5</param>
        /// <param name="mod">Modifier to the roll</param>
        public DiceRoll(int d10, int d5, int mod)
        {
            this.d10 = d10;
            this.d5 = d5;
            this.modifier = mod;
        }

        /// <summary>
        /// Parse a string representation of a dice roll (a roll string)
        /// </summary>
        /// <param name="input">Roll string to parse</param>
        public DiceRoll(String input)
        {
            String roll = new string(input.Where(c => Char.IsWhiteSpace(c)).ToArray());//strip whitespace
            this.d10 = this.d5 = 0;
            int index = 0;
            int previous = index;
            while (index < roll.Length)
            {
                if (roll[index++] == 'd')
                {
                    if (roll[index] == '1' && roll[index + 1] == '0')
                    {
                        if (previous == index - 1)
                            d10 = 1;
                        else
                            d10 = Int32.Parse(roll.Substring(previous, index - previous - 1));
                        index++;//move to the 0 so the final ++ puts after the dice
                    }
                    else if (roll[index] == '5')
                    {
                        if (previous == index - 2)
                            d5 = 1;
                        else
                            d5 = Int32.Parse(roll.Substring(previous, index - previous - 1));
                    }
                    else
                        throw new FormatException("Only d10 and d5 are supported");
                    index++;//point after the dice
                    previous = index;//advance last found
                }
            }
            if (previous == index)
                modifier = 0;
            else
                modifier = Int32.Parse(roll.Substring(previous, index - previous));
            //if (String.IsNullOrWhiteSpace(roll) || !ValidDice.IsMatch(roll))
            //    throw new FormatException("Roll not in valid format");
            //Match match = ParseDice.Match(roll);
            //if (String.IsNullOrWhiteSpace(match.Groups[1].Value))
            //    this.d10 = 0;
            //else if (String.IsNullOrWhiteSpace(match.Groups[2].Value))
            //    this.d10 = 1;
            //else
            //    this.d10 = int.Parse(match.Groups[2].Value);
            //if (String.IsNullOrWhiteSpace(match.Groups[3].Value))
            //    this.d5 = 0;
            //else if (String.IsNullOrWhiteSpace(match.Groups[4].Value))
            //    this.d5 = 1;
            //else
            //    this.d5 = int.Parse(match.Groups[4].Value);
            //if (String.IsNullOrWhiteSpace(match.Groups[5].Value))
            //    this.modifier = 0;
            //else
            //    this.modifier = int.Parse(match.Groups[5].Value);
        }

        /// <summary>
        /// Show dice roll in the format XdY+Z
        /// </summary>
        /// <returns>dice r9oll as a roll string</returns>
        public override string ToString()
        {
            StringBuilder output = new StringBuilder();
            if (d10 != 0) 
                output.Append(d10 + "d10");
            if (d5 != 0)
                output.Append((d10 == 0 || d5 < 0 ? "" : "+") + d5 + "d5");
            if (modifier != 0 || (d10 == 0 && d5 == 0))//if modifier is non-null or rest of string is null
                output.Append((modifier < 0 || (d10 == 0 && d5 == 0) ? "" : "+") + modifier);
            return output.ToString();
        }

        //Print to string and include + or - at start
        private string ToStringAsModifier()
        {
            if (d10 > 0 || (d10 == 0 && d5 > 0) || (d10 == 0 && d5 == 0 && modifier >= 0))
                return "+" + this.ToString();
            return this.ToString();
        }

        /// <summary>
        /// Return a string representation of the dice roll with a custom format
        /// </summary>
        /// <param name="format">String with letters denoting format options</param>
        /// <returns>Formatted strings</returns>
        public string ToString(String format)
        {
            if (format.Contains("m"))
                return this.ToStringAsModifier();
            //if else for other things
            else
                throw new FormatException("Input format does not contain a valid custom format pattern");
        }

        /// <summary>
        /// Parse a Dice from the String
        /// </summary>
        /// <param name="roll">String to parse</param>
        /// <returns>A Dice Object representing the roll string</returns>
        public static DiceRoll Parse(String roll)
        {
            return new DiceRoll(roll);
        }

        #region arithmetic
        public DiceRoll Add(DiceRoll other)
        {
            return new DiceRoll(this.d10 + other.d10, this.d5 + other.d5, this.modifier + other.modifier);
        }

        public DiceRoll Add(int mod)
        {
            return new DiceRoll(this.d10, this.d5, this.modifier + mod);
        }

        public DiceRoll Sub(DiceRoll other)
        {
            return new DiceRoll(this.d10 - other.d10, this.d5 - other.d5, this.modifier - other.modifier);
        }

        public DiceRoll Sub(int mod)
        {
            return new DiceRoll(this.d10, this.d5, this.modifier - mod);
        }

        public DiceRoll Mul(int mod)
        {
            return new DiceRoll(this.d10 * mod, this.d5 * mod, this.modifier * mod);
        }

        public DiceRoll Mul(DiceRoll other)
        {
            if (this.d10 == 0 && this.d5 == 0)
            {
                if (other.d10 != 0 || other.d5 != 0)//by putting this if inside the other instead if as an and, the else if won't be evaluated and the final return statement will perform just modifier * modifier
                    return other.Mul(this.modifier);
            }
            else if (other.d10 != 0 || other.d5 != 0)
                throw new FormatException("Cannot multiply by dice values, only modifiers and ints");
            return this.Mul(other.modifier);
        }
        #endregion

        #region operator overloads
        public static DiceRoll operator +(DiceRoll first, DiceRoll second)
        {
            return first.Add(second);
        }

        public static DiceRoll operator +(DiceRoll first, int second)
        {
            return first.Add(second);
        }

        public static DiceRoll operator -(DiceRoll first, DiceRoll second)
        {
            return first.Sub(second);
        }

        public static DiceRoll operator -(DiceRoll first, int second)
        {
            return first.Sub(second);
        }

        public static DiceRoll operator *(DiceRoll first, DiceRoll second)
        {
            return first.Mul(second);
        }

        public static DiceRoll operator *(DiceRoll first, int second)
        {
            return first.Mul(second);
        }
        #endregion
    }
}
