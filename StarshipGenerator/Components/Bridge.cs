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
        public int BS { get; private set; }
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
            int repair = 0, int pilot = 0, int navigate = 0)
            : base(name, sp, power, space, special, origin, page, types, quality)
        {
            this.Manoeuvrability = man;
            this.BS = bs;
            this.Command = command;
            this.Repair = repair;
            this.Pilot = pilot;
            this.Repair = repair;
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
                if (BS > 0)
                    output.Append("+" + BS + " Ballistic Skill; ");
                else if (BS < 0)
                    output.Append(BS + " Ballistic Skill; ");
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
                if (!String.IsNullOrWhiteSpace(Special))
                    output.Append(Special + ";");
                return output.ToString();
            }
        }
    }
}
