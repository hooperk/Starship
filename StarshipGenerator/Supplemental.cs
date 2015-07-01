using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarshipGenerator
{
    /// <summary>
    /// Base for all supplemental components
    /// </summary>
    public class Supplemental : Component
    {
        /// <summary>
        /// Speed modifier of this component
        /// </summary>
        public int Speed { get; protected set; }
        /// <summary>
        /// Manoeuvrability modifier this component grants
        /// </summary>
        public int Manoeuvrability { get; protected set; }
        /// <summary>
        /// Armour modifier this component grants
        /// </summary>
        public int Armour { get; protected set; }
        /// <summary>
        /// Turret rating modifier this component grants
        /// </summary>
        public int TurretRating { get; protected set; }
        /// <summary>
        /// Morale modifier of this component
        /// </summary>
        public int Morale { get; protected set; }
        /// <summary>
        /// Crew Population modifier of this component
        /// </summary>
        public int CrewPopulation { get; protected set; }

        /// <summary>
        /// Create a new supplemental Component
        /// </summary>
        /// <param name="types">Classes of ship that can use this component</param>
        /// <param name="power">power used or granted by this component</param>
        /// <param name="space">space used by this component</param>
        /// <param name="sp">cost of this component</param>
        /// <param name="origin">rulebook this component is found in</param>
        /// <param name="page">page number to find this component</param>
        /// <param name="special">special rules of the component</param>
        /// <param name="quality">quality of the component</param>
        /// <param name="speed">speed modifier of the component</param>
        /// <param name="man">manoeuvrability modifier of the component</param>
        /// <param name="hullint">hull integrity modifier of the component</param>
        /// <param name="armour">armour modifier of the component</param>
        /// <param name="turrets">turret modifier of the component</param>
        /// <param name="morale">morale modifier of the component</param>
        /// <param name="crew">crew population modifier of the component</param>
        public Supplemental(HullType types, int power, int space, int sp, RuleBook origin, byte page, 
            String special = null, Quality quality = Quality.Common, int speed = 0, int man = 0,
            int hullint = 0, int armour = 0, int turrets = 0, int morale = 0, int crew = 0)
            : base(sp, power, space, special, origin, page, types, quality)
        {
            this.Speed = speed;
            this.Manoeuvrability = man;
            this.Armour = armour;
            this.TurretRating = turrets;
            this.Morale = morale;
            this.CrewPopulation = crew;
        }
    }
}
