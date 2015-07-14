using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StarshipGenerator.Utils;

namespace StarshipGenerator.Components
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
        /// Hull Integrity modifier this component grants
        /// </summary>
        public int HullIntegrity { get; protected set; }
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
        /// Modifier to Crew Rating of this component
        /// </summary>
        public int CrewRating { get; protected set; }
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
        /// Auxiliary weapons such as hold landing bay granted by component
        /// </summary>
        public Weapon AuxiliaryWeapon { get; private set; }
        /// <summary>
        /// Modifier to Macrobattery Damage 
        /// </summary>
        public int MacrobatteryModifier { get; private set; }
        /// <summary>
        /// Morale loss modifier granted by the component
        /// </summary>
        public int MoraleLoss { get; private set; }
        /// <summary>
        /// Crew loss modifier granted by the component
        /// </summary>
        public int CrewLoss { get; private set; }
        /// <summary>
        /// Ballistic Skill modifier granted by the component
        /// </summary>
        public int BSModifier { get; private set; }
        /// <summary>
        /// Modifier to navigate the warp
        /// </summary>
        public int NavigateWarp { get; private set; }
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
        /// <param name="name">name of the component</param>
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
        /// <param name="aux">Auxiliary weapons like hold landing bay granted by this component</param>
        /// <param name="macrodamage">damage modifier to macrobatteries such as from the munitorium</param>
        /// <param name="bs">ballistic skill modifier from this component</param>
        /// <param name="navigate">modifer to navigate the warp</param>
        /// <param name="crewLoss">modifier to crew losses</param>
        /// <param name="moraleLoss">modifier to morale losses</param>
        public Supplemental(string name, HullType types, int power, int space, int sp, RuleBook origin, byte page,
            String special = null, Quality quality = Quality.Common, int speed = 0, int man = 0,
            int hullint = 0, int armour = 0, int turrets = 0, int morale = 0, int crew = 0,
            DiceRoll ramming = default(DiceRoll), int prowArmour = 0, int crewRating = 0,
            int mining = 0, int creed = 0, int military = 0, int trade = 0, int criminal = 0,
            int exploration = 0, bool generated = false, int detection = 0, Weapon aux = null, 
            int macrodamage = 0, int bs = 0, int navigate = 0, int crewLoss = 0, int moraleLoss = 0)
            : base(name, sp, power, space, special, origin, page, types, quality)
        {
            this.Speed = speed;
            this.Manoeuvrability = man;
            this.HullIntegrity = hullint;
            this.Armour = armour;
            this.TurretRating = turrets;
            this.Morale = morale;
            this.CrewPopulation = crew;
            this.ProwArmour = prowArmour;
            this.RamDamage = ramming;
            this.CrewRating = crewRating;
            this.MiningObjective = mining;
            this.CreedObjective = creed;
            this.MilitaryObjective = military;
            this.TradeObjective = trade;
            this.CriminalObjective = criminal;
            this.ExplorationObjective = exploration;
            this.PowerGenerated = generated;
            this.DetectionRating = detection;
            this.AuxiliaryWeapon = aux;
            this.MacrobatteryModifier = macrodamage;
            this.BSModifier = bs;
            this.NavigateWarp = navigate;
            this.CrewLoss = crewLoss;
            this.MoraleLoss = moraleLoss;
        }

        /// <summary>
        /// Create a new supplemental Component
        /// </summary>
        /// <param name="name">name of the supplemental component</param>
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
        /// <param name="aux">Auxiliary weapons like hold landing bay granted by this component</param>
        /// <param name="macrodamage">damage modifier to macrobatteries such as from the munitorium</param>
        /// <param name="bs">ballistic skill modifier from this component</param>
        /// <param name="navigate">modifer to navigate the warp</param>
        /// <param name="crewLoss">modifier to crew losses</param>
        /// <param name="moraleLoss">modifier to morale losses</param>
        public Supplemental(string name, HullType types, int power, int space, int sp, RuleBook origin, byte page,
            String ramming, String special = null, Quality quality = Quality.Common, int speed = 0,
            int man = 0, int hullint = 0, int armour = 0, int turrets = 0, int morale = 0,
            int crew = 0, int prowArmour = 0, int crewRating = 0, int mining = 0, int creed = 0, int military = 0,
            int trade = 0, int criminal = 0, int exploration = 0, bool generated = false, int detection = 0,
            Weapon aux = null, int macrodamage = 0, int bs = 0, int navigate = 0, int crewLoss = 0, int moraleLoss = 0)
            : this(name, types, power, space, sp, origin, page, special, quality, speed, man, hullint,
                armour, turrets, morale, crew, new DiceRoll(ramming), prowArmour, crewRating,
                mining, creed, military, trade, criminal, exploration, generated, detection, aux,
                macrodamage, bs, navigate, crewLoss, moraleLoss) { }

        /// <summary>
        /// Serialises the Supplemental Component
        /// </summary>
        /// <returns>JSON object as string</returns>
        public override string ToJSON()
        {
            /*{
             * "Supplemental" : {
             *  "Name" : name,
             *  "Types" : types,
             *  "Power" : power.
             *  "Space" : space,
             *  "SP" : sp,
             *  "Origin" : origin,
             *  "Page" : page,
             *  "Ram" : ram,
             *  "Special" : special,
             *  "Quality" : quality,
             *  "Speed" : speed,
             *  "Man" : man,
             *  "Int" : int,
             *  "Armour" : armour,
             *  "Turrets" : turrets,
             *  "Morale" : morale,
             *  "Crew" : crew,
             *  "Prow" : prow,
             *  "Rating" : rating,
             *  "Mining" : mining,
             *  "Creed" : creed,
             *  "Military" : military,
             *  "Trade" : trade,
             *  "Criminal" : criminal,
             *  "Explore" : explore,
             *  "Gen" : gen,
             *  "Det" : det,
             *  "Aux" : {Weapon : {...}},
             *  "Macro" : damage,
             *  "BS" : bs,
             *  "Nav" : nav,
             *  "CrewLoss" : crewLoss,
             *  "MoraleLoss" : moraleLoss }
             *}
             */
            return @"{""Supplemental"":{""Name"":""" + Name + @""",""Types"":" + (byte)HullTypes + @",""Power"":" + Power
                + @",""Space"":" + Space + @",""SP"":" + SP + @",""Origin"":" + (byte)Origin + @",""Page"":" + PageNumber
                + @",""Ram"":""" + RamDamage.ToString() + @""",""Special"":""" + Special + @""",""Quality"":"
                + (byte)Quality + @",""Speed"":" + Speed + @",""Man"":" + Manoeuvrability + @",""Int"":" + HullIntegrity
                + @",""Armour"":" + Armour + @",""Turrets"":" + TurretRating + @",""Morale"":" + Morale + @",""Crew"":"
                + CrewPopulation + @",""Prow"":" + ProwArmour + @",""Rating"":" + CrewRating + @",""Mining"":"
                + MiningObjective + @",""Creed"":" + CreedObjective + @",""Military"":" + MilitaryObjective
                + @",""Trade"":" + TradeObjective + @",""Criminal"":" + CriminalObjective + @",""Explore"":"
                + ExplorationObjective + @",""Gen"":" + PowerGenerated.ToString() + @",""Det"":" + DetectionRating
                + @",""Aux"":" + (AuxiliaryWeapon == null ? @"null" : AuxiliaryWeapon.ToJSON()) + @",""Macro"":"
                + MacrobatteryModifier + @",""BS"":" + BSModifier + @",""Nav"":" + NavigateWarp + @",""CrewLoss"":"
                + CrewLoss + @",""MoraleLoss"":" + MoraleLoss + @"}}";
        }

        /// <summary>
        /// Description of the Augur Array to display while picking
        /// </summary>
        public override string Description
        {
            get
            {
                StringBuilder output = new StringBuilder();
                if (Speed > 0)
                    output.Append("+" + Speed + " Speed; ");
                else if (Speed < 0)
                    output.Append(Speed + " Speed; ");
                if (Manoeuvrability > 0)
                    output.Append("+" + Manoeuvrability + " Manoeuvrability; ");
                else if (Manoeuvrability < 0)
                    output.Append(Manoeuvrability + " Manoeuvrability; ");
                if (HullIntegrity > 0)
                    output.Append("+" + HullIntegrity + " Hull Integrity; ");
                else if (HullIntegrity < 0)
                    output.Append(HullIntegrity + " Hull Integrity; ");
                if (Armour > 0)
                    output.Append("+" + Armour + " Armour; ");
                else if (Armour < 0)
                    output.Append(Armour + " Armour; ");
                if (ProwArmour > 0)
                    output.Append("+" + ProwArmour + " Armour; ");
                else if (ProwArmour < 0)
                    output.Append(ProwArmour + " Armour; ");
                if (TurretRating > 0)
                    output.Append("+" + TurretRating + " Turret Rating; ");
                else if (TurretRating < 0)
                    output.Append(TurretRating + " Turret Rating; ");
                if (Morale > 0)
                    output.Append("+" + Morale + " to maximum morale; ");
                else if (Morale < 0)
                    output.Append(Morale + " to maximum morale; ");
                if (CrewPopulation > 0)
                    output.Append("+" + CrewPopulation + " to maximum crew population; ");
                else if (CrewPopulation < 0)
                    output.Append(CrewPopulation + " to maximum crew population; ");
                if (RamDamage != default(DiceRoll))
                    output.Append(RamDamage.ToString("m") + " to ramming damage; ");
                if (CrewRating > 0)
                    output.Append("+" + CrewRating + " to crew rating; ");
                else if (CrewRating < 0)
                    output.Append(CrewRating + " to crew rating; ");
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
                if (DetectionRating > 0)
                    output.Append("+" + DetectionRating + " Detection Rating; ");
                else if (DetectionRating < 0)
                    output.Append(DetectionRating + " Detection Rating; ");
                if (MacrobatteryModifier > 0)
                    output.Append("+" + MacrobatteryModifier + " to macrobattery damage; ");
                else if (MacrobatteryModifier < 0)
                    output.Append(MacrobatteryModifier + " to macrobattery damage; ");
                if (MoraleLoss > 0)
                    output.Append("+" + MoraleLoss + " to morale losses; ");
                else if (MoraleLoss < 0)
                    output.Append(MoraleLoss + " to morale losses; ");
                if (CrewLoss > 0)
                    output.Append("+" + CrewLoss + " to morale losses; ");
                else if (CrewLoss < 0)
                    output.Append(CrewLoss + " to morale losses; ");
                if (BSModifier > 0)
                    output.Append("+" + BSModifier + " Ballistic Skill; ");
                else if (BSModifier < 0)
                    output.Append(BSModifier + " Ballistic Skill; ");
                if (NavigateWarp > 0)
                    output.Append("+" + NavigateWarp + " to navigate through the warp; ");
                else if (NavigateWarp < 0)
                    output.Append(NavigateWarp + " to navigate through the warp; ");
                output.Append(base.Description);
                return output.ToString();
            }
        }
    }
}
