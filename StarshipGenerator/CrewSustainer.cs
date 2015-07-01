using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarshipGenerator
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
        /// Create a new Crew Quarters or Life Sustainer
        /// </summary>
        /// <param name="types">classes of ship which can use this component</param>
        /// <param name="power">power used by this component</param>
        /// <param name="space">space used by this component</param>
        /// <param name="morale">morale modifier of this component</param>
        /// <param name="origin">rulebook containing this component</param>
        /// <param name="page">page this component can be found on</param>
        /// <param name="special">special rules for this component</param>
        /// <param name="quality">quality of this component</param>
        /// <param name="sp">cost of this component</param>
        public CrewSustainer(HullType types, int power, int space, int morale, RuleBook origin, byte page,
            string special = null, Quality quality = Quality.Common, int sp = 0)
            : base(sp, power, space, special, origin, page, types, quality)
        {
            this.Morale = morale;
        }
    }
}
