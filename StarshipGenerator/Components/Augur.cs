using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StarshipGenerator.Utils;

namespace StarshipGenerator.Components
{
    /// <summary>
    /// Augur array essential component
    /// </summary>
    public class Augur : Component
    {
        /// <summary>
        /// Manoeuvrability modifier this array grants
        /// </summary>
        public int Manoeuvrability { get; private set; }
        /// <summary>
        /// Detection rating modifier this array grants
        /// </summary>
        public int DetectionRating { get; private set; }
        /// <summary>
        /// Ballistic Skill modifier this array grants
        /// </summary>
        public int BSModifier { get; private set; }
        /// <summary>
        /// Any special effects of the array
        /// </summary>
        public override string Special
        {
            get
            {
                if (!base.Special.Contains("External"))
                    return "External" + (String.IsNullOrEmpty(base.Special) ? "" : "; " + base.Special);
                return base.Special;
            }
        }
        /// <summary>
        /// Modifiers to Mining Objectives
        /// </summary>
        public int MiningObjective { get; protected set; }
        /// <summary>
        /// Modifiers to Creed Objectives
        /// </summary>
        public int CreedObjective { get; protected set; }
        /// <summary>
        /// Modifiers to Military Objectives
        /// </summary>
        public int MilitaryObjective { get; protected set; }
        /// <summary>
        /// Modifiers to Trade Objectives
        /// </summary>
        public int TradeObjective { get; protected set; }
        /// <summary>
        /// Modifiers to Criminal Objectives
        /// </summary>
        public int CriminalObjective { get; protected set; }
        /// <summary>
        /// Modifiers to Exploration Objectives
        /// </summary>
        public int ExplorationObjective { get; protected set; }

        /// <summary>
        /// Create new augur array
        /// </summary>
        /// <param name="name">name of augur arrray</param>
        /// <param name="power">power used by this array</param>
        /// <param name="origin">rulebook containign this array</param>
        /// <param name="page">page this array can be found on</param>
        /// <param name="det">detection rating modifier of this array</param>
        /// <param name="special">special rules of this array</param>
        /// <param name="quality">quality of this array</param>
        /// <param name="sp">cost of this array</param>
        /// <param name="man">manoeuvrability modifier of this array</param>
        /// <param name="bs">ballistic skill modifier of this array</param>
        public Augur(string name, int power, RuleBook origin, byte page, int det = 0, String special = null,
            Quality quality = Quality.Common, int sp = 0, int man = 0, int bs = 0, int mining = 0, 
            int creed = 0, int military = 0, int trade = 0, int criminal = 0, int exploration = 0,
            ComponentOrigin comp = ComponentOrigin.Standard)
            : base(name, sp, power, 0, special, origin, page, HullType.All, quality,comp)
        {
            this.Manoeuvrability = man;
            this.DetectionRating = det;
            this.BSModifier = bs;
            this.MiningObjective = mining;
            this.CreedObjective = creed;
            this.MilitaryObjective = military;
            this.TradeObjective = trade;
            this.CriminalObjective = criminal;
            this.ExplorationObjective = exploration;
        }

        /// <summary>
        /// Serialises the Augur Array
        /// </summary>
        /// <returns>JSON object as string</returns>
        public override string ToJSON()
        {
            /*
             * { 
             * "Augur" : { 
             *  "Name" : name, 
             *  "Power" : power, 
             *  "Origin" : origin, 
             *  "Page" : page, 
             *  "Det" : det, 
             *  "Special" : special, 
             *  "Quality" : quality, 
             *  "SP" : sp, 
             *  "Man": man, 
             *  "BS" : bs,
             *  "Comp" : comp,
             *  "Mining" : mining,
             *  "Creed" : creed,
             *  "Military" : military,
             *  "Trade" : trade,
             *  "Criminal" : criminal,
             *  "Explore" : explore }
             * }
             * */
            return @"{""Augur"":{""Name"":""" + Name.Escape() + @""",""Power"":" + Power + @",""Origin"":" + (byte)Origin + @",""Page"":" + PageNumber
                + @",""Det"":" + DetectionRating + @",""Special"":""" + base.Special.Escape() + @""",""Quality"":" + (byte)Quality + @",""SP"":" + SP
                + @",""Man"":" + Manoeuvrability + @",""BS"":" + BSModifier + @",""Comp"":" + (byte)ComponentOrigin 
                + @",""Mining"":" + MiningObjective + @",""Creed"":" + CreedObjective + @",""Military"":" + MilitaryObjective
                + @",""Trade"":" + TradeObjective + @",""Criminal"":" + CriminalObjective + @",""Explore"":" + ExplorationObjective + @"}}";
        }

        /// <summary>
        /// Description of the Augur Array to display while picking
        /// </summary>
        public override string Description
        {
            get
            {
                StringBuilder output = new StringBuilder();
                //Show DetectionRating specially
                //if (DetectionRating > 0)
                //    output.Append("+" + DetectionRating + " Detection Rating; ");
                //else if (DetectionRating < 0)
                //    output.Append(DetectionRating + " Detection Rating; ");
                if (Manoeuvrability > 0)
                    output.Append("+" + Manoeuvrability + " Manoeuvrability; ");
                else if (Manoeuvrability < 0)
                    output.Append(Manoeuvrability + " Manoeuvrability; ");
                if (BSModifier > 0)
                    output.Append("+" + BSModifier + " Ballistic Skill; ");
                else if (BSModifier < 0)
                    output.Append(BSModifier + " Ballistic Skill; ");
                if (MiningObjective > 0)
                    output.Append("+" + MiningObjective + " to mining objectives; ");
                else if (MiningObjective < 0)
                    output.Append(MiningObjective + " to mining objectives; ");
                if (CreedObjective > 0)
                    output.Append("+" + CreedObjective + " to mining objectives; ");
                else if (CreedObjective < 0)
                    output.Append(CreedObjective + " to mining objectives; ");
                if (MilitaryObjective > 0)
                    output.Append("+" + MilitaryObjective + " to mining objectives; ");
                else if (MilitaryObjective < 0)
                    output.Append(MilitaryObjective + " to mining objectives; ");
                if (TradeObjective > 0)
                    output.Append("+" + TradeObjective + " to mining objectives; ");
                else if (TradeObjective < 0)
                    output.Append(TradeObjective + " to mining objectives; ");
                if (CriminalObjective > 0)
                    output.Append("+" + CriminalObjective + " to mining objectives; ");
                else if (CriminalObjective < 0)
                    output.Append(CriminalObjective + " to mining objectives; ");
                if (ExplorationObjective > 0)
                    output.Append("+" + ExplorationObjective + " to mining objectives; ");
                else if (ExplorationObjective < 0)
                    output.Append(ExplorationObjective + " to mining objectives; ");
                output.Append(Special);
                return output.ToString();
            }
        }
    }
}
