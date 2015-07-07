using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StarshipGenerator.Components;
using StarshipGenerator.Utils;

namespace StarshipGenerator
{
    /// <summary>
    /// Starship and all its internal components
    /// </summary>
    public class Starship
    {
        /// <summary>
        /// Hull of the Starship
        /// </summary>
        public Hull Hull
        {
            get { return _hull; }
            set
            {
                _hull = value;
                Weapons = new Weapon[_hull.WeaponSlots];
                SupplementalComponents = new List<Supplemental>();
                if (_hull.DefaultProw != null)
                    Weapons[0] = _hull.DefaultProw;
                if (_hull.DefaultBroadside != null)
                    Weapons[_hull.ProwSlots + _hull.DorsalSlots] = //Port slot
                        Weapons[_hull.ProwSlots + _hull.DorsalSlots + _hull.SideSlots] = //Starboard slot
                            _hull.DefaultBroadside;
                if (_hull.DefaultComponents != null)
                    SupplementalComponents.AddRange(_hull.DefaultComponents);
            }
        }
        private Hull _hull;
        /// <summary>
        /// Plasma Drive of the Starship
        /// </summary>
        public PlasmaDrive PlasmaDrive { get; set; }
        /// <summary>
        /// Warp Drive of the Starship
        /// </summary>
        public WarpDrive WarpDrive { get; set; }
        /// <summary>
        /// Gellar Field of the Starship
        /// </summary>
        public GellarField GellarField { get; set; }
        /// <summary>
        /// Void Shield of the Starship
        /// </summary>
        public VoidShield VoidShield { get; set; }
        /// <summary>
        /// Bridge of the Starship
        /// </summary>
        public Bridge ShipBridge { get; set; }
        /// <summary>
        /// Life Sustainer of the Starship
        /// </summary>
        public LifeSustainer LifeSustainer { get; set; }//seperate later with subclass or differentiate through filtering on something?
        /// <summary>
        /// Crew Quarters of the Starship
        /// </summary>
        public CrewQuarters CrewQuarters { get; set; }
        /// <summary>
        /// Augur Arrays of the Starships
        /// </summary>
        public Augur AugurArrays { get; set; }
        /// <summary>
        /// Weapons mounted onf the Starship
        /// </summary>
        public Weapon[] Weapons { get; set; }//Ordered Prow, Dorsal, Port, Starboard, Keel, Aft
        /// <summary>
        /// Supplemental Components mounted in the Starship
        /// </summary>
        public List<Supplemental> SupplementalComponents { get; set; }
        /// <summary>
        /// Listing of all the weapons on this ship and their slots
        /// </summary>
        public List<Tuple<WeaponSlot, Weapon>> WeaponList
        {
            get
            {
                if (Hull == null)
                    return null;
                int i = 0;
                List<Tuple<WeaponSlot, Weapon>> list = new List<Tuple<WeaponSlot, Weapon>>();
                while (i < Hull.ProwSlots)
                    list.Add(new Tuple<WeaponSlot, Weapon>(WeaponSlot.Prow, Weapons[i++]));//Add the weapon and move the pointer
                while (i < Hull.ProwSlots + Hull.DorsalSlots)
                    list.Add(new Tuple<WeaponSlot, Weapon>(WeaponSlot.Dorsal, Weapons[i++]));//Add the weapon and move the pointer
                while (i < Hull.ProwSlots + Hull.DorsalSlots + Hull.SideSlots)
                    list.Add(new Tuple<WeaponSlot, Weapon>(WeaponSlot.Port, Weapons[i++]));//Add the weapon and move the pointer
                while (i < Hull.ProwSlots + Hull.DorsalSlots + (Hull.SideSlots*2))
                    list.Add(new Tuple<WeaponSlot, Weapon>(WeaponSlot.Starboard, Weapons[i++]));//Add the weapon and move the pointer
                while (i < Hull.ProwSlots + Hull.DorsalSlots + (Hull.SideSlots * 2) + Hull.KeelSlots)
                    list.Add(new Tuple<WeaponSlot, Weapon>(WeaponSlot.Keel, Weapons[i++]));//Add the weapon and move the pointer
                while (i < Hull.ProwSlots + Hull.DorsalSlots + (Hull.SideSlots * 2) + Hull.KeelSlots + Hull.AftSlots)
                    list.Add(new Tuple<WeaponSlot, Weapon>(WeaponSlot.Aft, Weapons[i++]));//Add the weapon and move the pointer
                foreach (Supplemental component in SupplementalComponents)
                    if (component.AuxiliaryWeapon != null)
                        list.Add(new Tuple<WeaponSlot, Weapon>(WeaponSlot.Auxiliary, component.AuxiliaryWeapon));
                return list;
            }
        }

        /// <summary>
        /// Machine Spirit Complication of the Starship
        /// </summary>
        public MachineSpirit MachineSpirit { get; set; }
        /// <summary>
        /// Ship History Complication of the Starship
        /// </summary>
        public ShipHistory ShipHistory { get; set; }

        /// <summary>
        /// Quality of this Starship's crew
        /// </summary>
        public CrewRating CrewRating { get; set; }
        /// <summary>
        /// Race of the crwe on this Starship
        /// </summary>
        public Race CrewRace { get; set; }

        //modifiers such as gm crew modifier

        /// <summary>
        /// Total SP cost of the Starship
        /// </summary>
        public int SP
        {
            get
            {
                int total = 0;
                if (Hull != null)
                    total += Hull.SP;
                if (PlasmaDrive != null)
                    total += PlasmaDrive.SP;
                if (WarpDrive != null)
                    total += WarpDrive.SP;
                if (GellarField != null)
                    total += GellarField.SP;
                if (VoidShield != null)
                    total += VoidShield.SP;
                if (ShipBridge != null)
                    total += ShipBridge.SP;
                if (LifeSustainer != null)
                    total += LifeSustainer.SP;
                if (CrewQuarters != null)
                    total += CrewQuarters.SP;
                if (AugurArrays != null)
                    total += AugurArrays.SP;
                foreach (Weapon gun in Weapons)
                    total += gun.SP;
                foreach (Supplemental component in SupplementalComponents)
                    total += component.SP;
                return total;
            }
        }
        /// <summary>
        /// Total Speed of the Starship
        /// </summary>
        public int Speed
        {
            get
            {
                if (Hull == null)
                    return 0;
                int total = Hull.Speed;
                if (PlasmaDrive != null)
                    total += PlasmaDrive.Speed;
                foreach (Supplemental component in SupplementalComponents)
                    total += component.Speed;
                return total;
            }
        }
        /// <summary>
        /// Total Maneouvrability modifier of the Starship
        /// </summary>
        public int Manoeuvrability
        {
            get
            {
                if (Hull == null)
                    return 0;
                int total = Hull.Manoeuvrability;
                if (PlasmaDrive != null)
                    total += PlasmaDrive.Manoeuvrability;
                if (ShipBridge != null)
                    total += ShipBridge.Manoeuvrability;
                if (AugurArrays != null)
                    total += AugurArrays.Manoeuvrability;
                foreach (Supplemental component in SupplementalComponents)
                    total += component.Manoeuvrability;
                return total;
            }
        }
        /// <summary>
        /// Total Detection Rating modifier of the Starship
        /// </summary>
        public int DetectionRating
        {
            get
            {
                if (Hull == null)
                    return 0;
                int total = Hull.DetectionRating;
                if (AugurArrays != null)
                    total += AugurArrays.DetectionRating;
                foreach (Supplemental component in SupplementalComponents)
                    total += component.DetectionRating;
                return total;
            }
        }
        /// <summary>
        /// Void Shield Strength of the Starship
        /// </summary>
        public int Shields
        {
            get
            {
                if (VoidShield == null)
                    return 0;
                else//upgrade can increase
                    return VoidShield.Strength;
            }
        }
        /// <summary>
        /// Total Armour of the Starship
        /// </summary>
        public int Armour
        {
            get
            {
                if (Hull == null)
                    return 0;
                int total = Hull.Armour;
                foreach (Supplemental component in SupplementalComponents)
                    total += component.Armour;
                return total;
            }
        }
        /// <summary>
        /// Frontal Armour of the Starship
        /// </summary>
        public int ProwArmour
        {
            get
            {
                int total = this.Armour;
                foreach (Supplemental component in SupplementalComponents)
                    total += component.ProwArmour;
                return total;
            }
        }
        /// <summary>
        /// Turret Rating of the Starship
        /// </summary>
        public int TurretRating
        {
            get
            {
                if (Hull == null)
                    return 0;
                int total = Hull.TurretRating;
                foreach (Supplemental component in SupplementalComponents)
                    total += component.TurretRating;
                return total;
            }
        }
        /// <summary>
        /// Maximum available space of the Starship
        /// </summary>
        public int MaxSpace
        {
            get
            {
                if (Hull == null)
                    return 0;
                int total = Hull.Space;
                //There was something that reduced max space... one of the histories?
                return total;
            }
        }
        /// <summary>
        /// Maximum available power of the Starship
        /// </summary>
        public int MaxPower
        {
            get
            {
                if (PlasmaDrive == null)
                    return 0;
                int total = PlasmaDrive.Power;
                total += Hull.Power;//if power != 0, hull grants or uses excess power
                foreach (Supplemental component in SupplementalComponents)
                    if (component.PowerGenerated)
                        total += component.Power;
                return total;
            }
        }
        /// <summary>
        /// Total space used on the Starship
        /// </summary>
        public int UsedSpace
        {
            get
            {
                int total = 0;
                if (PlasmaDrive != null)
                    total += PlasmaDrive.Space;
                if (WarpDrive != null)
                    total += WarpDrive.Space;
                if (VoidShield != null)
                    total += VoidShield.Space;
                if (ShipBridge != null)
                    total += ShipBridge.Space;
                if (LifeSustainer != null)
                    total += LifeSustainer.Space;
                if (CrewQuarters != null)
                    total += CrewQuarters.Space;
                foreach (Weapon gun in Weapons)
                    total += gun.Space;
                foreach (Supplemental component in SupplementalComponents)
                    total += component.Space;
                return total;
            }
        }
        /// <summary>
        /// Total power used on the Starship
        /// </summary>
        public int UsedPower
        {
            get
            {
                int total = 0;
                if (WarpDrive != null)
                    total += WarpDrive.Power;
                if (GellarField != null)
                    total += GellarField.Power;
                if (VoidShield != null)
                    total += VoidShield.Power;
                if (ShipBridge != null)
                    total += ShipBridge.Power;
                if (LifeSustainer != null)
                    total += LifeSustainer.Power;
                if (CrewQuarters != null)
                    total += CrewQuarters.Power;
                if (AugurArrays != null)
                    total += AugurArrays.Power;
                foreach (Weapon gun in Weapons)
                    total += gun.Power;
                foreach (Supplemental component in SupplementalComponents)
                    if (!component.PowerGenerated)
                        total += component.Power;
                return total;
            }
        }
        /// <summary>
        /// Maximum crew population of the Starship
        /// </summary>
        public int CrewPopulation
        {
            get
            {
                int total = 100;
                foreach (Supplemental component in SupplementalComponents)
                    total += component.CrewPopulation;
                return total;
            }
        }
        /// <summary>
        /// Current crew population of the Starship
        /// </summary>
        public int CurrentCrew { get; set; }//When live ship is ready to go
        /// <summary>
        /// Maximum morale rating of the Starship
        /// </summary>
        public int Morale
        {
            get
            {
                int total = 100;
                if (LifeSustainer != null)
                    total += LifeSustainer.Morale;
                if (CrewQuarters != null)
                    total += CrewQuarters.Morale;
                foreach (Supplemental component in SupplementalComponents)
                    total += component.Morale;
                //histories still
                return total;
            }
        }
        /// <summary>
        /// Current morale rating of the Starship
        /// </summary>
        public int CurrentMorale { get; set; }//When live ship is ready to go
        /// <summary>
        /// Maximum hull integrity of the Starship
        /// </summary>
        public int HullIntegrity
        {
            get
            {
                if (Hull == null)
                    return 0;
                int total = Hull.HullIntegrity;
                foreach (Supplemental component in SupplementalComponents)
                    total += component.HullIntegrity;
                //history
                return total;
            }
        }
        /// <summary>
        /// Current hull integrity of the Starship
        /// </summary>
        public int CurrentIntegrity { get; set; }//When live ship is ready to go

        /// <summary>
        /// Crew's skill level on this Starship
        /// </summary>
        public int CrewValue
        {
            get
            {
                int total = (int)CrewRating;
                //upgrades
                foreach (Supplemental component in SupplementalComponents)
                    total += component.CrewRating;
                return total;
            }
        }

        /// <summary>
        /// Ballistic skill modifier while shooting this Starship's weapons
        /// </summary>
        public int BSModifier
        {
            get
            {
                int total = 0;
                if (ShipBridge != null)
                    total += ShipBridge.BS;
                if (AugurArrays != null)
                    total += AugurArrays.BS;
                //get modifier from components and Augur Arrays
                return 0;
            }
        }

        /// <summary>
        /// Ramming damage from this Starship
        /// </summary>
        public DiceRoll Ramming
        {
            get
            {
                DiceRoll ram = new DiceRoll(0, 0, 0);
                if (Hull == null)
                    return ram;
                if ((Hull.HullTypes & (HullType.BattleShip | HullType.GrandCruiser | HullType.BattleCruiser
                    | HullType.Cruiser)) > 0)
                    ram = new DiceRoll(2, 0, 0);
                if ((Hull.HullTypes & HullType.LightCruiser) > 0)
                    ram = new DiceRoll(0, 2, 0);
                if ((Hull.HullTypes & HullType.Frigate) > 0)
                    ram = new DiceRoll(1, 0, 0);
                if ((Hull.HullTypes & (HullType.Raider | HullType.Transport)) > 0)
                    ram = new DiceRoll(0, 1, 0);
                foreach (Supplemental component in SupplementalComponents)
                    ram += component.RamDamage;
                ram += ProwArmour;
                return ram;
            }
        }

        /// <summary>
        /// Modifiers to Mining Objectives
        /// </summary>
        public int MiningObjective
        {
            get
            {
                int total = 0;
                foreach (Supplemental component in SupplementalComponents)
                    total += component.MiningObjective;
                return total;
            }
        }
        /// <summary>
        /// Modifiers to Creed Objectives
        /// </summary>
        public int CreedObjective
        {
            get
            {
                int total = 0;
                foreach (Supplemental component in SupplementalComponents)
                    total += component.CreedObjective;
                return total;
            }
        }
        /// <summary>
        /// Modifiers to Military Objectives
        /// </summary>
        public int MilitaryObjective
        {
            get
            {
                int total = 0;
                foreach (Supplemental component in SupplementalComponents)
                    total += component.MilitaryObjective;
                return total;
            }
        }
        /// <summary>
        /// Modifiers to Trade Objectives
        /// </summary>
        public int TradeObjective
        {
            get
            {
                int total = 0;
                foreach (Supplemental component in SupplementalComponents)
                    total += component.TradeObjective;
                return total;
            }
        }
        /// <summary>
        /// Modifiers to Criminal Objectives
        /// </summary>
        public int CriminalObjective
        {
            get
            {
                int total = 0;
                foreach (Supplemental component in SupplementalComponents)
                    total += component.CriminalObjective;
                return total;
            }
        }
        /// <summary>
        /// Modifiers to Exploration Objectives
        /// </summary>
        public int ExplorationObjective
        {
            get
            {
                int total = 0;
                foreach (Supplemental component in SupplementalComponents)
                    total += component.ExplorationObjective;
                return total;
            }
        }
        /// <summary>
        /// Modifiers to weapon damage for macrobatteries
        /// </summary>
        public int MacrobatteryModifier
        {
            get
            {
                int total = 0;
                foreach (Supplemental component in SupplementalComponents)
                    total += component.MacrobatteryModifier;
                return total;
            }
        }

    }
}
