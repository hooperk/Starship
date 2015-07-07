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

    public static class EnumerationExtensions
    {
        public static string Print(this MachineSpirit self)
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
                    return "";
            }
        }

        public static string Print(this ShipHistory self)
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
                    return "";
            }
        }
    }
}
