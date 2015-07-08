using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarshipGenerator.Utils
{
    /// <summary>
    /// Class of Weapon
    /// </summary>
    public enum WeaponType : byte { Macrobattery, Lance, TorpedoTube, LandingBay, NovaCannon }

    /// <summary>
    /// Denotes slots for weapons
    /// </summary>
    [Flags]
    public enum WeaponSlot : byte
    {
        None = 0x00,
        Prow = 0x01,
        Port = 0x02,
        Starboard = 0x04,
        Aft = 0x08,
        Dorsal = 0x10,
        Keel = 0x20,
        Lance = 0x40,
        Auxiliary = 0x80,//Hold Landing bay or custom others
        All = 0xFF
    }

    /// <summary>
    /// Denotes hull classes
    /// </summary>
    [Flags]
    public enum HullType : byte { 
        None = 0x00,
        Transport = 0x01, 
        Raider = 0x02, 
        Frigate = 0x04, 
        LightCruiser = 0x08, 
        Cruiser = 0x10, 
        BattleCruiser = 0x20, 
        GrandCruiser = 0x40,
        BattleShip = 0x80,//NOT IMPLEMENTED
        All = 0xFF
    }

    
    /// <summary>
    /// Component qualities
    /// </summary>
    /// <remarks>Slim and efficient are for deciding what bonus good quality grants</remarks>
    public enum Quality : byte { Poor, Common, Good, Slim, Efficient, Best }

    /// <summary>
    /// Races enumeration
    /// </summary>
    public enum Race : byte { Human, Servitor, Ork, Eldar, Kroot, Chaos, Rakgol }//more to come?

    /// <summary>
    /// Enumeration denoting location of original component details
    /// </summary>
    public enum RuleBook : byte { 
        Custom = 0, 
        CoreRulebook, 
        IntoTheStorm, 
        HostileAcquisition, 
        BattlefleetKoronus, 
        LureoftheExpanse
    }

    /// <summary>
    /// Machine Spirit Complications
    /// </summary>
    public enum MachineSpirit : byte
    {
        ANoseForTrouble,
        BlasphemousTendencies,
        MartialHubris,
        Rebellious,
        Stoic,
        Skittish,
        Wrothful,
        Resolute,
        Adventurous,
        AncientAndWise
    }

    /// <summary>
    /// Ship History Complications
    /// </summary>
    public enum ShipHistory : byte
    {
        ReliquaryOfMars,
        Haunted,
        EmissaryOfTheImperator,
        WolfInSheepsClothing,
        TurbulentPast,
        DeathCult,
        WrestedFromASpaceHulk,
        TemperamentalWarpEngine,
        FinancesInArrears,
        Xenophilous
    }

    /// <summary>
    /// Crew Rating listing
    /// </summary>
    public enum CrewRating : int
    {
        Incompetent = 20,
        Competent = 30,
        Crack = 40,
        Veteran = 50,
        Elite = 60
    }

    /// <summary>
    /// Listing of Servitor Crew qualities
    /// </summary>
    public enum ServitorQuality : int
    {
        Poor = 20,
        Common = 30,
        Good = 35,
        Best = 40
    }

    /// <summary>
    /// Background upgrades
    /// </summary>
    [Flags]
    public enum Background : byte
    {
        None = 0x00,
        PlanetBoundForMillenia1 = 0x01,
        PlanetBoundForMillenia2 = 0x02,
        PlanetBoundForMillenia3 = 0x03,
        PlanetBoundForMillenia4 = 0x04,
        PlanetBoundForMillenia5 = 0x05,
        PlanetDoundForMillenia = 0x07,//first three bits used to show the result of the d5
        ThulianExploratorVessel = 0x08,
        ReaverOfTheUnbeholdenReaches = 0x10,
        VeteranOfTheAngevinCrusade = 0x20,
        ImplacableFoeOfTheFleet = 0x40,
        SteadfastAllyofTheFleet = 0x80
    }

    /// <summary>
    /// Enumeration to show which stats are being affected by a weapon's quality
    /// </summary>
    [Flags]
    public enum WeaponQuality : byte
    {
        None = 0x00,
        SP = 0x01,
        Space = 0x02,
        Range = 0x04,
        Crit = 0x08,
        Damage = 0x10,
        Strength = 0x20
    }

    /// <summary>
    /// Enumeration stating if component is standard, Archeotech or Xenotech
    /// </summary>
    public enum ComponentOrigin : byte { Standard, Archeotech, Xenotech}

    public static class EnumerationExtensions
    {
        /// <summary>
        /// Get the name for printing of this complication
        /// </summary>
        /// <param name="self">Complication</param>
        /// <returns>Enum name as printable name</returns>
        public static string Name(this MachineSpirit self)
        {
            switch (self)
            {
                case MachineSpirit.ANoseForTrouble:
                    return "A Nose For Trouble";
                case MachineSpirit.BlasphemousTendencies:
                    return "Blasphemous Tendencies";
                case MachineSpirit.MartialHubris:
                    return "Martial Hubris";
                case MachineSpirit.Rebellious:
                    return "Rebellious";
                case MachineSpirit.Stoic:
                    return "Stoic";
                case MachineSpirit.Skittish:
                    return "Skittish";
                case MachineSpirit.Wrothful:
                    return "Wrothful";
                case MachineSpirit.Resolute:
                    return "Resolute";
                case MachineSpirit.Adventurous:
                    return "Adventurous";
                case MachineSpirit.AncientAndWise:
                    return "Ancient and Wise";
                default:
                    return null;
            }
        }

        /// <summary>
        /// String description of complication
        /// </summary>
        /// <param name="self">Complication</param>
        /// <returns>Enum description to print</returns>
        public static string Description(this MachineSpirit self)
        {
            switch (self)
            {
                case MachineSpirit.ANoseForTrouble:
                    return "+5 to Detection, -1 to Armour, Ship looks for battles crew might wish to avoid";
                case MachineSpirit.BlasphemousTendencies:
                    return "+15 Navigate through the warp, -5 to willpower tests";
                case MachineSpirit.MartialHubris:
                    return "+5 to ballistic skill tests with ship, -15 to pilot tests to escape combat";
                case MachineSpirit.Rebellious:
                    return "Components can randomly become unpowered no more than once per combat. On receiving critical, ignore on an 8+";
                case MachineSpirit.Stoic:
                    return "-1 profit factor from endeavours, when component is unpowered or damaged ignore on 7+";
                case MachineSpirit.Skittish:
                    return "-1 speed in combat, reduce long distance travels by 1d5 weeks min 1";
                case MachineSpirit.Wrothful:
                    return "+1 speed and 7 manoeuvrability in combat, -1 speed and -5 manoeuvrability and detection otherwise";
                case MachineSpirit.Resolute:
                    return "-1 Speed, +3 Hull Integrity, +10 to Repair tests";
                case MachineSpirit.Adventurous:
                    return "+10 detection while in an endeavour, -10 when not";
                case MachineSpirit.AncientAndWise:
                    return "-4 Hull Integrity, +10 to manouvre actions";
                default:
                    return null;
            }
        }

        /// <summary>
        /// Just special rules to display as text
        /// </summary>
        /// <param name="self">Complication</param>
        /// <returns>Special rules to print</returns>
        public static string Special(this MachineSpirit self)
        {
            switch (self)
            {
                case MachineSpirit.ANoseForTrouble:
                    return "Ship looks for battles crew might wish to avoid";
                case MachineSpirit.BlasphemousTendencies:
                    return "-5 to willpower tests";
                case MachineSpirit.MartialHubris:
                    return "-15 to pilot tests to escape combat";
                case MachineSpirit.Rebellious:
                    return "Components can randomly become unpowered no more than once per combat. On receiving critical, ignore on an 8+";
                case MachineSpirit.Stoic:
                    return "-1 profit factor from endeavours, when component is unpowered or damaged ignore on 7+";
                case MachineSpirit.Skittish:
                    return "-1 speed in combat, reduce long distance travels by 1d5 weeks min 1";
                case MachineSpirit.Wrothful:
                    return "+1 speed and 7 manoeuvrability in combat, -1 speed and -5 manoeuvrability and detection otherwise";
                case MachineSpirit.Resolute:
                    return null;
                case MachineSpirit.Adventurous:
                    return "+10 detection while in an endeavour, -10 when not";
                case MachineSpirit.AncientAndWise:
                    return null;
                default:
                    return null;
            }
        }

        /// <summary>
        /// Get the name for printing of this complication
        /// </summary>
        /// <param name="self">Complication</param>
        /// <returns>Enum name as printable name</returns>
        public static string Name(this ShipHistory self)
        {
            switch(self)
            {
                case ShipHistory.ReliquaryOfMars:
                    return "Reliquary of Mars";
                case ShipHistory.Haunted:
                    return "Haunted";
                case ShipHistory.EmissaryOfTheImperator:
                    return "Emissary of the Imperator";
                case ShipHistory.WolfInSheepsClothing:
                    return "Wolf in Sheeps Clothing";
                case ShipHistory.TurbulentPast:
                    return "Turbulent Past";
                case ShipHistory.DeathCult:
                    return "Death Cult";
                case ShipHistory.WrestedFromASpaceHulk:
                    return "Wrested from a Space Hulk";
                case ShipHistory.TemperamentalWarpEngine:
                    return "Temperamental Warp Engine";
                case ShipHistory.FinancesInArrears:
                    return "Finances in Arrears";
                case ShipHistory.Xenophilous:
                    return "Xenophilous";
                default:
                    return null;
            }
        }

        /// <summary>
        /// String description of complication
        /// </summary>
        /// <param name="self">Complication</param>
        /// <returns>Enum description to print</returns>
        public static string Description(this ShipHistory self)
        {
            switch (self)
            {
                case ShipHistory.ReliquaryOfMars:
                    return "Must have an archeotech component, -20 tech-use tests to repair ship. Tech priests of Mars see vessel as holy and may wish to visit or possess it.";
                case ShipHistory.Haunted:
                    return "-10 Morale, +6 Detection, -5 to command tests against this vessel. Spirits may cause other issues.";
                case ShipHistory.EmissaryOfTheImperator:
                    return "+15 to intimidate tests and -5 to all other social tests, ship is not subtle";
                case ShipHistory.WolfInSheepsClothing:
                    return "-2 Power, Up to 3 components will be hidden from scans or appear as a smaller component of same type, may have hidden passageways or storage holds";
                case ShipHistory.TurbulentPast:
                    return "All crew memebers have -20 interacting with one group and +20 with an opposing group chosen at creation";
                case ShipHistory.DeathCult:
                    return "-8 Crew Population, Reduce morale loss by 2. Death cult on board";
                case ShipHistory.WrestedFromASpaceHulk:
                    return "+1 Armour, +1 speed, +3 Manoeuvrability. When the crew suffers a misfortune, GM rolls twice and chooses the worse result";
                case ShipHistory.TemperamentalWarpEngine:
                    return "When travelling through the warp roll d10. 1-6: increase time by 1d5 weeks, 7+ reduce time by 1d5 weeks. May also occasionally end up somewhere other than where it intended";
                case ShipHistory.FinancesInArrears:
                    return "Bound to a financer, all objectives require 50 extra achievement points. The financer may also act as a contact too";
                case ShipHistory.Xenophilous:
                    return "Ship must have 1 Xenotech component, -30 to repair, -10 if character has Forbidden Lore(Xenos)";
                default:
                    return null;
            }
        }

        /// <summary>
        /// Just special rules to display as text
        /// </summary>
        /// <param name="self">Complication</param>
        /// <returns>Special rules to print</returns>
        public static string Special(this ShipHistory self)
        {
            switch (self)
            {
                case ShipHistory.ReliquaryOfMars:
                    return "Must have an archeotech component. Tech priests of Mars see vessel as holy and may wish to visit or possess it.";
                case ShipHistory.Haunted:
                    return "-5 to command tests against this vessel. Spirits may cause other issues.";
                case ShipHistory.EmissaryOfTheImperator:
                    return "+15 to intimidate tests and -5 to all other social tests, ship is not subtle";
                case ShipHistory.WolfInSheepsClothing:
                    return "Up to 3 components will be hidden from scans or appear as a smaller component of same type, may have hidden passageways or storage holds";
                case ShipHistory.TurbulentPast:
                    return "All crew memebers have -20 interacting with one group and +20 with an opposing group chosen at creation";
                case ShipHistory.DeathCult:
                    return "Death cult on board";
                case ShipHistory.WrestedFromASpaceHulk:
                    return "When the crew suffers a misfortune, GM rolls twice and chooses the worse result";
                case ShipHistory.TemperamentalWarpEngine:
                    return "When travelling through the warp roll d10. 1-6: increase time by 1d5 weeks, 7+ reduce time by 1d5 weeks. May also occasionally end up somewhere other than where it intended";
                case ShipHistory.FinancesInArrears:
                    return "Bound to a financer, all objectives require 50 extra achievement points. The financer may also act as a contact too";
                case ShipHistory.Xenophilous:
                    return "Ship must have 1 Xenotech component, -20 to repair if character does not have Forbidden Lore(Xenos)";
                default:
                    return null;
            }
        }
    }
}
