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
        /// Modifier to only the prow armour
        /// </summary>
        public int ProwArmour { get; protected set; }
        /// <summary>
        /// Additional damage to ramming
        /// </summary>
        public DiceRoll RamDamage { get; protected set; }
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
        /// If the power listed is generated instead of used
        /// </summary>
        public bool PowerGenerated { get; protected set; }
        /// <summary>
        /// The modifier to detection rating from this component
        /// </summary>
        public int DetectionRating { get; protected set; }
        /// <summary>
        /// Power Used or supplied by component
        /// </summary>
        /// <remarks>Override for PowerGenerated = true</remarks>
        public override int Power
        {
            get
            {
                if (PowerGenerated)
                {
                    switch (this.Quality)
                    {
                        case Quality.Poor:
                            return Math.Max(_power - 2, 1);//poor quality generates 2 less instead of granting 1 more
                        case Quality.Good:
                        case Quality.Efficient:
                        case Quality.Best:
                            return _power + 1;
                        default:
                            return _power;
                    }
                }
                return base.Power;
            }
        }

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
        /// <param name="prowArmour">armour modifier to only the prow from this component</param>
        /// <param name="ramming">Additional damage dealt by ramming with this component</param>
        /// <param name="crewRating">Bonus or penalty to crew rating</param>
        /// <param name="mining">modifier to mining objectives</param>
        /// <param name="creed">modifier to creed objectives</param>
        /// <param name="military">modifier to military objectives</param>
        /// <param name="trade">modifier to trade objectives</param>
        /// <param name="criminal">modifier to criminal objectives</param>
        /// <param name="exploration">modifier to exploration objectives</param>
        /// <param name="generated">If the power listed is generated instead of used</param>
        /// <param name="detection">modifier to detection rating fromt he component</param>
        public Supplemental(HullType types, int power, int space, int sp, RuleBook origin, byte page,
            String special = null, Quality quality = Quality.Common, int speed = 0, int man = 0,
            int hullint = 0, int armour = 0, int turrets = 0, int morale = 0, int crew = 0,
            DiceRoll ramming = default(DiceRoll), int prowArmour = 0, int crewRating = 0,
            int mining = 0, int creed = 0, int military = 0, int trade = 0, int criminal = 0,
            int exploration = 0, bool generated = false, int detection = 0)
            : base(sp, power, space, special, origin, page, types, quality)
        {
            this.Speed = speed;
            this.Manoeuvrability = man;
            this.Armour = armour;
            this.TurretRating = turrets;
            this.Morale = morale;
            this.CrewPopulation = crew;
            this.ProwArmour = prowArmour;
            this.RamDamage = ramming;
            this.MiningObjective = mining;
            this.CreedObjective = creed;
            this.MilitaryObjective = military;
            this.TradeObjective = trade;
            this.CriminalObjective = criminal;
            this.ExplorationObjective = exploration;
            this.PowerGenerated = generated;
            this.DetectionRating = detection;
        }

        /// <summary>
        /// Create a new supplemental Component
        /// </summary>
        /// <param name="types">Classes of ship that can use this component</param>
        /// <param name="power">power used or granted by this component</param>
        /// <param name="space">space used by this component</param>
        /// <param name="sp">cost of this component</param>
        /// <param name="origin">rulebook this component is found in</param>
        /// <param name="page">page number to find this component</param>
        /// <param name="ramming">Additional damage dealt by ramming with this component</param>
        /// <param name="special">special rules of the component</param>
        /// <param name="quality">quality of the component</param>
        /// <param name="speed">speed modifier of the component</param>
        /// <param name="man">manoeuvrability modifier of the component</param>
        /// <param name="hullint">hull integrity modifier of the component</param>
        /// <param name="armour">armour modifier of the component</param>
        /// <param name="turrets">turret modifier of the component</param>
        /// <param name="morale">morale modifier of the component</param>
        /// <param name="crew">crew population modifier of the component</param>
        /// <param name="prowArmour">armour modifier to only the prow from this component</param>
        /// <param name="crewRating">Bonus or penalty to crew rating</param>
        /// <param name="mining">modifier to mining objectives</param>
        /// <param name="creed">modifier to creed objectives</param>
        /// <param name="military">modifier to military objectives</param>
        /// <param name="trade">modifier to trade objectives</param>
        /// <param name="criminal">modifier to criminal objectives</param>
        /// <param name="exploration">modifier to exploration objectives</param>
        /// <param name="generated">If the power listed is generated instead of used</param>
        /// <param name="detection">modifier to detection rating fromt he component</param>
        public Supplemental(HullType types, int power, int space, int sp, RuleBook origin, byte page,
            String ramming, String special = null, Quality quality = Quality.Common, int speed = 0,
            int man = 0, int hullint = 0, int armour = 0, int turrets = 0, int morale = 0,
            int crew = 0, int prowArmour = 0, int crewRating = 0, int mining = 0, int creed = 0, int military = 0,
            int trade = 0, int criminal = 0, int exploration = 0, bool generated = false, int detection = 0)
            : this(types, power, space, sp, origin, page, special, quality, speed, man, hullint,
                armour, turrets, morale, crew, new DiceRoll(ramming), prowArmour, crewRating,
                mining, creed, military, trade, criminal, exploration, generated, detection) { }
    }
}
