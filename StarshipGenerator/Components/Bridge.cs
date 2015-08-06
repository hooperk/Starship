using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StarshipGenerator.Utils;

namespace StarshipGenerator.Components
{
    /// <summary>
    /// Ship bridge essential component
    /// </summary>
    public class Bridge : Component
    {
        /// <summary>
        /// Manoeuvrability modifier this bridge grants
        /// </summary>
        public int Manoeuvrability { get; private set; }
        /// <summary>
        /// Ballistic Skill modifier this bridge grants
        /// </summary>
        public int BSModifier { get; private set; }
        /// <summary>
        /// Modifier to Command skill which this bridge grants
        /// </summary>
        public int Command { get; private set; }
        /// <summary>
        /// Modifier to repair tests
        /// </summary>
        public int Repair { get; private set; }
        /// <summary>
        /// Modifier to pilot tests
        /// </summary>
        public int Pilot { get; private set; }
        /// <summary>
        /// Modifier to navigate the warp
        /// </summary>
        public int NavigateWarp { get; private set; }
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
        /// Create a new bridge
        /// </summary>
        /// <param name="name">name of teh component</param>
        /// <param name="types">ships which can use this bridge</param>
        /// <param name="power">power used by this bridge</param>
        /// <param name="space">space used by this bridge</param>
        /// <param name="origin">rulebook containing this bridge</param>
        /// <param name="page">page this bridge can be found on</param>
        /// <param name="special">special rules for this bridge</param>
        /// <param name="sp">cost of this bridge</param>
        /// <param name="quality">quality of this bridge</param>
        /// <param name="man">manoeuvrability modifier of this bridge</param>
        /// <param name="bs">ballistic skill modifier of this bridge</param>
        /// <param name="command">command modifier of this bridge</param>
        /// <param name="repair">modifier to repair tests from this bridge</param>
        /// <param name="pilot">modifier to pilot tests from this bridge</param>
        /// <param name="navigate">modifier to navigate the warp from this bridge</param>
        public Bridge(string name, HullType types, int power, int space, RuleBook origin, byte page, string special = null,
            int sp = 0, Quality quality = Quality.Common, int man = 0, int bs = 0, int command = 0,
            int repair = 0, int pilot = 0, int navigate = 0, ComponentOrigin comp = ComponentOrigin.Standard, int mining = 0,
            int creed = 0, int military = 0, int trade = 0, int criminal = 0, int exploration = 0, Condition cond = Condition.Intact)
            : base(name, sp, power, space, special, origin, page, types, quality, comp, cond)
        {
            this.Manoeuvrability = man;
            this.BSModifier = bs;
            this.Command = command;
            this.Repair = repair;
            this.Pilot = pilot;
            this.Repair = repair;
            this.MiningObjective = mining;
            this.CreedObjective = creed;
            this.MilitaryObjective = military;
            this.TradeObjective = trade;
            this.CriminalObjective = criminal;
            this.ExplorationObjective = exploration;
        }

        /// <summary>
        /// Serialises the Bridge
        /// </summary>
        /// <returns>JSON object as string</returns>
        public override string ToJSON()
        {
            /*{
             * "Bridge" : {
             *  "Name" : name,
             *  "Types" : types,
             *  "Power" : power,
             *  "Space" : space,
             *  "Origin" : origin,
             *  "Page" : page,
             *  "Special" : special,
             *  "SP" : sp,
             *  "Quality" : quality,
             *  "Man" : man,
             *  "BS" : bs,
             *  "Command" : command,
             *  "Repair" : repair,
             *  "Pilot" : pilot,
             *  "Nav" : nav,
             *  "Comp" : comp,
             *  "Mining" : mining,
             *  "Creed" : creed,
             *  "Military" : military,
             *  "Trade" : trade,
             *  "Criminal" : criminal,
             *  "Explore" : explore,
             *  "Cond" : condition}
             *}*/
            return @"{""Bridge"":{""Name"":""" + Name.Escape() + @""",""Types"":" + (byte)HullTypes + @",""Power"":" + RawPower + @",""Space"":" + RawSpace
                + @",""Origin"":" + (byte)Origin + @",""Page"":" + PageNumber + @",""Special"":""" + RawSpecial.Escape() + @""",""SP"":" + RawSP
                + @",""Quality"":" + (byte)Quality + @",""Man"":" + Manoeuvrability + @",""BS"":" + BSModifier + @",""Command"":" + Command
                + @",""Repair"":" + Repair + @",""Pilot"":" + Pilot + @",""Nav"":" + NavigateWarp + @",""Comp"":" + (byte)ComponentOrigin 
                + @",""Mining"":" + MiningObjective + @",""Creed"":" + CreedObjective + @",""Military"":" + MilitaryObjective
                + @",""Trade"":" + TradeObjective + @",""Criminal"":" + CriminalObjective + @",""Explore"":" + ExplorationObjective + @",""Cond"":" + Condition + @"}}";
        }

        /// <summary>
        /// Description of the bridge to display while picking
        /// </summary>
        public override string Description
        {
            get
            {
                StringBuilder output = new StringBuilder();
                if (Manoeuvrability > 0)
                    output.Append("+" + Manoeuvrability + " Manoeuvrability; ");
                else if (Manoeuvrability < 0)
                    output.Append(Manoeuvrability + " Manoeuvrability; ");
                if (BSModifier > 0)
                    output.Append("+" + BSModifier + " Ballistic Skill; ");
                else if (BSModifier < 0)
                    output.Append(BSModifier + " Ballistic Skill; ");
                if (Pilot > 0)
                    output.Append("+" + Pilot + " to pilot tests; ");
                else if (Pilot < 0)
                    output.Append(Pilot + " to pilot tests; ");
                if (NavigateWarp > 0)
                    output.Append("+" + NavigateWarp + " to navigate through the warp; ");
                else if (NavigateWarp < 0)
                    output.Append(NavigateWarp + " to navigate through the warp; ");
                if (Command > 0)
                    output.Append("+" + Command + " to command tests; ");
                else if (Command < 0)
                    output.Append(Command + " to command tests; ");
                if (Repair > 0)
                    output.Append("+" + Repair + " to repair tests; ");
                else if (Repair < 0)
                    output.Append(Repair + " to repair tests; ");
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
                output.Append(base.Description);
                return output.ToString();
            }
        }
    }
}
