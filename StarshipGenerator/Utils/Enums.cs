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
        Auxiliary = 0x00,//Hold Landing bay or custom others
        Prow = 0x01,
        Port = 0x02,
        Starboard = 0x04,
        Side = 0x06,
        Aft = 0x08,
        Dorsal = 0x10,
        Keel = 0x20,
        Lance = 0x40,//prow for transport, raider and frigates, any for other classes
        Heavy = 0x80,//Voidsunder, prow unless grand, in which case also dorsal
        All = 0xFF
    }

    /// <summary>
    /// Denotes hull classes
    /// </summary>
    [Flags]
    public enum HullType : byte
    {
        None = 0x00,
        Transport = 0x01,
        Raider = 0x02,
        Frigate = 0x04,
        LightCruiser = 0x08,
        Cruiser = 0x10,
        BattleCruiser = 0x20,
        GrandCruiser = 0x40,
        BattleShip = 0x80,//NOT IMPLEMENTED, currently hand coded onto grand
        CruiserPlus = 0xF0,//Cruiser or bigger
        AllCruiser= 0xF8,//Light cruiser or bigger
        All = 0xFF
    }


    /// <summary>
    /// Component qualities
    /// </summary>
    /// <remarks>
    /// Slim and efficient are for deciding what bonus good quality grants.
    /// None for Upgrades
    /// </remarks>
    public enum Quality : byte { None, Poor, Common, Good, Slim, Efficient, Best }

    /// <summary>
    /// Races enumeration
    /// </summary>
    public enum Race : byte { Human, Servitor, Ork, Eldar, DarkEldar, Stryxis, Kroot, Chaos, Rakgol, Goff, EvilSunz, BadMoons, Deathskulls, BloodAxes, SnakeBites }//more to come?

    /// <summary>
    /// Enumeration denoting location of original component details
    /// </summary>
    public enum RuleBook : byte
    {
        Custom = 0,
        CoreRulebook,
        IntoTheStorm,
        HostileAcquisition,
        BattlefleetKoronus,
        LureoftheExpanse,
        SoulReaver
    }

    /// <summary>
    /// Machine Spirit Complications
    /// </summary>
    public enum MachineSpirit : byte
    {
        None = 0,
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
        None = 0,
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
    /// List of strengths of squadrons
    /// </summary>
    public enum Strength : byte
    {
        Destroyed = 0,
        Half,
        Full
    }

    /// <summary>
    /// Listing of Torpedo warheads
    /// </summary>
    public enum Warhead : byte
    {
        Plasma = 0,
        Boarding,
        Melta,
        Virus,
        Vortex,
        Void,
        Leech
    }

    /// <summary>
    /// Listing of Torpedo guidance systems
    /// </summary>
    public enum Guidance : byte
    {
        Standard = 0,
        Guided,
        Seeking,
        ShortBurn
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
        PlanetBoundForMillenia = 0x07,//first three bits used to show the result of the d5
        ThulianExploratorVessel = 0x08,
        ReaverOfTheUnbeholdenReaches = 0x10,
        VeteranOfTheAngevinCrusade = 0x20,
        ImplacableFoeOfTheFleet = 0x40,
        SteadfastAllyofTheFleet = 0x80,
        VesselOfTheFleet = 0xC0//both of the fleet for adding command
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
    public enum ComponentOrigin : byte { Standard, Archeotech, Xenotech }

    public static class EnumerationExtensions
    {
        /// <summary>
        /// Returns the shortened name of the rulebook for dispaly
        /// </summary>
        /// <param name="self">Rulebook to display</param>
        /// <returns>Shortened name of the rulebook</returns>
        public static string Name(this RuleBook self)
        {
            switch (self)
            {
                case RuleBook.CoreRulebook:
                    return "Core Rulebook";
                case RuleBook.IntoTheStorm:
                    return "Into the Storm";
                case RuleBook.HostileAcquisition:
                    return "Hostile Acquisitions";
                case RuleBook.BattlefleetKoronus:
                    return "Battlefleet Koronus";
                case RuleBook.LureoftheExpanse:
                    return "Lure of the Expanse";
                case RuleBook.SoulReaver:
                    return "The Soul Reaver";
                default:
                    return "Custom";
            }
        }

        /// <summary>
        /// Returns the full name of the rulebook for dispaly
        /// </summary>
        /// <param name="self">Rulebook to display</param>
        /// <returns>Full name of the rulebook</returns>
        public static string LongName(this RuleBook self)
        {
            switch (self)
            {
                case RuleBook.CoreRulebook:
                    return "Rogue Trader Core Rulebook";
                case RuleBook.IntoTheStorm:
                    return "Into the Storm: The Explorer's Handbook";
                case RuleBook.HostileAcquisition:
                    return "Hostile Acquisitions: Profit and Plunder in teh Lawless Expanse";
                case RuleBook.BattlefleetKoronus:
                    return "Battlefleet Koronus: Voidshps and Warfare in the Koronus Expanse";
                case RuleBook.LureoftheExpanse:
                    return "Lure of the Expanse";
                case RuleBook.SoulReaver:
                    return "The Soul Reaver";
                default:
                    return "Custom";    
            }
        }
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
            switch (self)
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

        /// <summary>
        /// Name of the Race
        /// </summary>
        /// <param name="self">Race</param>
        /// <returns>Human readable name of the race</returns>
        public static string Name(this Race self)
        {
            switch (self)
            {
                case Race.Human:
                    return "Human";
                case Race.Servitor:
                    return "Servitor";
                case Race.Ork:
                    return "Ork";
                case Race.Eldar:
                    return "Eldar";
                case Race.DarkEldar:
                    return "Dark Eldar";
                case Race.Stryxis:
                    return "Stryxis";
                case Race.Rakgol:
                    return "Rak'Gol";
                case Race.Kroot:
                    return "Kroot";
                case Race.Chaos:
                    return "Chaos";
                case Race.Goff:
                    return "Goff Orks";
                case Race.EvilSunz:
                    return "Evil Sunz Orks";
                case Race.BadMoons:
                    return "Bad Moons Orks";
                case Race.Deathskulls:
                    return "Death Skulls Orks";
                case Race.BloodAxes:
                    return "Blood Axes Orks";
                case Race.SnakeBites:
                    return "Snake Bites Orks";
                default:
                    return null;
            }
        }
        
        /// <summary>
        /// Description of advantages of each race
        /// </summary>
        /// <param name="self">Race</param>
        /// <returns>Details of each race</returns>
        public static string Description(this Race self)
        {
            switch (self)
            {
                case Race.Human:
                    return "Standard Imperial Citizens";
                case Race.Servitor:
                    return "-10 Ballistic and Command tests, Always morale 100 and half crew losses rounding up, Tech Use instead of Medicae for Triage";
                case Race.Ork:
                    return "+10 hit and run and boarding tests. +10 to pilot tests to increase speed. Ship moves +1d5 VU when it does not turn. Reduce crew losses by 1. -10 to silent running";
                case Race.Eldar:
                    return "May reroll any pilot tests for manoeuvre actions. +10 to boarding actions and inflicts 1d5+2 morale and crew damage instead of 1d5. Increase crew losses by 1, loses 1 less morale to minimum of 1.";
                case Race.DarkEldar:
                    return "May reroll any pilot tests for manoeuvre actions. +10 to boarding actions and inflicts 1d5+2 morale and crew damage instead of 1d5. +20 to hit and runs and may inflict 1d10 crew and morale damage instead of inflicting a critical hit. Replenish crew's crew population and morale equalt to the amount of damage done to each in boarding and hit and run attacks.";
                case Race.Stryxis:
                    return "A Strange race who appear addicted to haggling";
                case Race.Rakgol:
                    return "Strange radiation mutated creatures";
                case Race.Kroot:
                    return "Avian mercenaries";
                case Race.Chaos:
                    return "Worshippers of the dark gods";
                case Race.Goff:
                    return Race.Ork.Description() + ". Further +10 pilot tests to ram and boarding test";
                case Race.EvilSunz:
                    return Race.Ork.Description() + ". +1 speed";
                case Race.BadMoons:
                    return Race.Ork.Description() + ". Roll twice and pick highest to determine strength of ork macrobatteries";
                case Race.Deathskulls:
                    return Race.Ork.Description() + ". May have 1 imperial component on their ork vessel";
                case Race.BloodAxes:
                    return "+10 hit and run and boarding tests. +10 to pilot tests to increase speed. Ship moves +1d5 VU when it does not turn. Reduce crew losses by 1., No usual penalties to silent running";
                case Race.SnakeBites:
                    return Race.Ork.Description() + ". Does additional 1d5 crew and morale damage in boarding actions, may only be taken as crews for a Rok";
                default:
                    return null;
            }
        }

        /// <summary>
        /// Special rules of a race
        /// </summary>
        /// <param name="self">Race</param>
        /// <returns>Special rules of a race for inclusion in ship profile</returns>
        public static string Special(this Race self)
        {
            switch (self)
            {
                case Race.Human:
                    return null;
                case Race.Servitor:
                    return "Tech Use test instead of Medicae for Triage";
                case Race.Ork:
                case Race.EvilSunz:
                    return "+10 hit and run and boarding tests. +10 to pilot tests to increase speed. Ship moves +1d5 VU when it does not turn. -10 to silent running";
                case Race.Eldar:
                    return "May reroll any pilot tests for manoeuvre actions. +10 to boarding actions and inflicts 1d5+2 morale and crew damage instead of 1d5";
                case Race.DarkEldar:
                    return "May reroll any pilot tests for manoeuvre actions. +10 to boarding actions and inflicts 1d5+2 morale and crew damage instead of 1d5. +20 to hit and runs and may inflict 1d10 crew and morale damage instead of inflicting a critical hit. Replenish crew's crew population and morale equalt to the amount of damage done to each in boarding and hit and run attacks.";
                case Race.Goff:
                    return Race.Ork.Special() + ". Further +10 pilot tests to ram and boarding test";
                case Race.BadMoons:
                    return Race.Ork.Special() + ". Roll twice and pick highest to determine strength of ork macrobatteries";
                case Race.Deathskulls:
                    return Race.Ork.Special() + ". May have 1 imperial component on their ork vessel";
                case Race.BloodAxes:
                    return "+10 hit and run and boarding tests. +10 to pilot tests to increase speed. Ship moves +1d5 VU when it does not turn.";
                case Race.SnakeBites:
                    return Race.Ork.Description() + ". Does additional 1d5 crew and morale damage in boarding actions, may only be taken as crews for a Rok";
                default:
                    return null;
            }
        }

        /// <summary>
        /// Get the name for printing of this background
        /// </summary>
        /// <param name="self">Background</param>
        /// <returns>Enum name as printable name</returns>
        public static string Name(this Background self)
        {
            switch (self)
            {
                case Background.ThulianExploratorVessel:
                    return "Thulian Explorator Vessel";
                case Background.ReaverOfTheUnbeholdenReaches:
                    return "Reaver of the Unbeholden Reaches";
                case Background.VeteranOfTheAngevinCrusade:
                    return "Veteran of the Angevin Crusade";
                case Background.ImplacableFoeOfTheFleet:
                    return "Implacable Foe of The Fleet";
                case Background.SteadfastAllyofTheFleet:
                    return "Steadfast Ally of the Fleet";
                default:
                    if((self & Background.PlanetBoundForMillenia) > 0)
                        return "Planetbound for Millenia";
                    return null;
            }
        }

        /// <summary>
        /// Description of effects of each background
        /// </summary>
        /// <param name="self">Background</param>
        /// <returns>Details of each background</returns>
        public static string Description(this Background self)
        {
            switch (self)
            {
                case Background.ThulianExploratorVessel:
                    return "+10 Detection, must take 1 archeotech component, -1 speed and -5 manoeuvrabilty";
                case Background.ReaverOfTheUnbeholdenReaches:
                    return "Long-term repairs fix 1d10+5 Hull Integrity, +10 to Silent Running, -10 to all social tests with anyone that knows where the crew is from";
                case Background.VeteranOfTheAngevinCrusade:
                    return "+10 Ballistic Skill with ship weapons, -40 to Silent Running, +10 to charm and intimidate tests with anyone who understands the deeds of the ship";
                case Background.ImplacableFoeOfTheFleet:
                    return "+10 Command Test on board, Command test to ignore effects of crippled for first round after being crippled, crew members gain Enemy(Imperial Navy)";
                case Background.SteadfastAllyofTheFleet:
                    return "+10 Command Test on board, Command test to ignore effects of crippled for first round after being crippled, crew members gain Good Reputation(Imperial Navy)";
                default:
                    if((self & Background.PlanetBoundForMillenia) > 0)
                        return "Begins play with a modified drive at no cost, and may take one other archeotech component, +10 manoeuvrability within 5VU of a planet, Hull integrity -1d5-1 for Frigate, Transport or Raider, -1d5 for larger";
                    return null;
            }
        }

        /// <summary>
        /// The special rules added by the background
        /// </summary>
        /// <param name="self">Background</param>
        /// <returns>Special rules of a background for inclusion on ship profile</returns>
        public static string Special(this Background self)
        {
            switch (self)
            {
                case Background.ThulianExploratorVessel:
                    return "Must take 1 archeotech component";
                case Background.ReaverOfTheUnbeholdenReaches:
                    return "Long-term repairs fix 1d10+5 Hull Integrity, +10 to Silent Running, -10 to all social tests with anyone that knows where the crew is from";
                case Background.VeteranOfTheAngevinCrusade:
                    return "-40 to Silent Running, +10 to charm and intimidate tests with anyone who understands the deeds of the ship";
                case Background.ImplacableFoeOfTheFleet:
                    return "Command test to ignore effects of crippled for first round after being crippled, crew members gain Enemy(Imperial Navy)";
                case Background.SteadfastAllyofTheFleet:
                    return "Command test to ignore effects of crippled for first round after being crippled, crew members gain Good Reputation(Imperial Navy)";
                default:
                    if ((self & Background.PlanetBoundForMillenia) > 0)
                        return "Begins play with a modified drive at no cost, and may take one other archeotech component, +10 manoeuvrability within 5VU of a planet";
                    return null;
            }
        }

        /// <summary>
        /// Hull types a Background can be used for
        /// </summary>
        /// <param name="self">Background</param>
        /// <returns>Classes of ship which can have this upgrade</returns>
        public static HullType Hulltypes(this Background self)
        {
            switch (self)
            {
                case Background.ThulianExploratorVessel:
                    return HullType.Frigate | HullType.LightCruiser | HullType.CruiserPlus;
                case Background.ReaverOfTheUnbeholdenReaches:
                    return HullType.Transport | HullType.Raider | HullType.Frigate;
                case Background.VeteranOfTheAngevinCrusade:
                case Background.ImplacableFoeOfTheFleet:
                case Background.SteadfastAllyofTheFleet:
                case Background.PlanetBoundForMillenia:
                    return HullType.All;
                default:
                    return HullType.None;
            }
        }

        /// <summary>
        /// Description of a Guidance system
        /// </summary>
        /// <param name="self">Guidance</param>
        /// <returns>String to list for a Guidance System</returns>
        public static string Description(this Guidance self)
        {
            switch (self)
            {
                case Guidance.Standard:
                    return "+20 Torpedo Rating";
                case Guidance.Guided:
                    return "One character may make +0 Tech-Use+Detection test to turn torpedo up to 45 degrees at start of it's movement each turn. Enemies which have identified the torpedo tubes may make -40 Tech-Use Test to gain control instead. +20 Torpedo Rating";
                case Guidance.Seeking:
                    return "+30 Torpedo Rating";
                case Guidance.ShortBurn:
                    return "Torpedo moves 15 VUs a turn but has max range of 30VU. +15 Torpedo Rating";
                default:
                    return null;
            }
        }

        /// <summary>
        /// Special Rules of a Guidance system
        /// </summary>
        /// <param name="self">Guidance</param>
        /// <returns>String to list for a Guidance System</returns>
        public static string Special(this Guidance self)
        {
            if(self == Guidance.Guided)
                return "One character may make +0 Tech-Use+Detection test to turn torpedo up to 45 degrees at start of it's movement each turn. Enemies which have identified the torpedo tubes may make -40 Tech-Use Test to gain control instead";
            return null;
        }
    }
}
