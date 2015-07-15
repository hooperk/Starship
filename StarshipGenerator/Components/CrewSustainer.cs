using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StarshipGenerator.Utils;

namespace StarshipGenerator.Components
{
    /// <summary>
    /// Crew quarters or life sustainer essential component
    /// </summary>
    public abstract class CrewSustainer : Component
    {
        /// <summary>
        /// Morale modifier of this component
        /// </summary>
        public int Morale { get; protected set; }
        /// <summary>
        /// Modifier to morale losses granted by this component
        /// </summary>
        public int MoraleLoss { get; protected set; }

        /// <summary>
        /// Create a new Crew Quarters or Life Sustainer
        /// </summary>
        /// <param name="name">name of teh life sustainer of crew quarters</param>
        /// <param name="types">classes of ship which can use this component</param>
        /// <param name="power">power used by this component</param>
        /// <param name="space">space used by this component</param>
        /// <param name="morale">morale modifier of this component</param>
        /// <param name="origin">rulebook containing this component</param>
        /// <param name="page">page this component can be found on</param>
        /// <param name="special">special rules for this component</param>
        /// <param name="quality">quality of this component</param>
        /// <param name="sp">cost of this component</param>
        /// <param name="loss">modifier to morale loss granted by this component</param>
        public CrewSustainer(string name, HullType types, int power, int space, int morale, RuleBook origin, byte page,
            string special = null, Quality quality = Quality.Common, int sp = 0, int loss = 0, ComponentOrigin comp = ComponentOrigin.Standard)
            : base(name, sp, power, space, special, origin, page, types, quality, comp)
        {
            this.Morale = morale;
            this.MoraleLoss = loss;
        }

        /// <summary>
        /// Description of the Crew Quarters or Life Sustainer to display while picking
        /// </summary>
        public override string Description
        {
            get
            {
                StringBuilder output = new StringBuilder();
                if (Morale > 0)
                    output.Append("+" + Morale + " to maximum morale; ");
                else if (Morale < 0)
                    output.Append(Morale + " to maximum morale; ");
                if (MoraleLoss > 0)
                    output.Append("+" + MoraleLoss + " to morale losses; ");
                else if (MoraleLoss < 0)
                    output.Append(MoraleLoss + " to morale losses; ");
                output.Append(base.Description);
                return output.ToString();
            }
        }
    }
}
