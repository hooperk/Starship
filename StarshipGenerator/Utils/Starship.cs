﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StarshipGenerator.Components;

namespace StarshipGenerator.Utils
{
    /// <summary>
    /// Starship and all its internal components
    /// </summary>
    public class Starship
    {
        public String Name;
        /// <summary>
        /// Hull of the Starship
        /// </summary>
        public Hull Hull
        {
            get { return _hull; }
            set
            {
                _hull = value;
                SupplementalComponents = new List<Supplemental>();
                if (_hull == null)
                {
                    Weapons = new Weapon[0];
                }
                else
                {
                    Weapons = new Weapon[_hull.WeaponSlots];
                    if (_hull.DefaultProw != null)
                        Weapons[0] = _hull.DefaultProw;
                    if (_hull.DefaultBroadside != null)
                        Weapons[_hull.ProwSlots + _hull.DorsalSlots] = //Port slot
                            Weapons[_hull.ProwSlots + _hull.DorsalSlots + _hull.SideSlots] = //Starboard slot
                                _hull.DefaultBroadside;
                    if (_hull.DefaultComponents != null)
                        SupplementalComponents.AddRange(_hull.DefaultComponents);
                    if (_hull.History != Utils.ShipHistory.None)
                    {
                        ShipHistory = _hull.History;
                        GMShipHistory = null;
                    }
                }
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
                while (i < Hull.ProwSlots + Hull.DorsalSlots + (Hull.SideSlots * 2))
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
        /// Final list of Supplementals minus replaced ones for printing
        /// </summary>
        public List<Supplemental> SupplementalList
        {
            get
            {
                if (SupplementalComponents == null)
                    return null;
                List<Supplemental> temp = new List<Supplemental>();
                List<String> toRemove = new List<string>();
                foreach (Supplemental component in SupplementalComponents)
                {
                    temp.Add(component);
                    if (component.Replace != null)
                        toRemove.Add(component.Replace);
                }
                foreach (String current in toRemove)
                {
                    try
                    {
                        temp.Remove(temp.Single(x => x.GetName().Equals(current)));//Sadly, yes, this was the most efficient way I could find for this
                    }
                    catch (ArgumentNullException) { }//if item is not in the list TODO work out handling instead of just ignore
                }
                return temp;
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
        /// Quality of this Starship's crew, base one set from CrewRating enumeration
        /// </summary>
        public int CrewRating { get; set; }
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
                {
                    total += PlasmaDrive.SP;
                    if ((Background & Background.PlanetBoundForMillenia) != 0 && PlasmaDrive.Modified)
                        total -= 3;//subtract the price of modified drive as planetboundformillenia grants modified drive for free
                }
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
                if (Weapons != null)
                    foreach (Weapon gun in Weapons)
                        if (gun != null)
                            total += gun.SP;
                if (SupplementalComponents != null)
                    foreach (Supplemental component in SupplementalComponents)
                        if (component != null)
                            total += component.SP;
                switch (Background)
                {
                    case Background.ThulianExploratorVessel:
                    case Background.ImplacableFoeOfTheFleet:
                        total += 1;
                        break;
                    case Background.ReaverOfTheUnbeholdenReaches:
                    case Background.SteadfastAllyofTheFleet:
                        total += 2;
                        break;
                    case Background.VeteranOfTheAngevinCrusade:
                        total += 3;
                        break;
                    default:
                        if ((Background & Background.PlanetBoundForMillenia) > 0)
                            total += 3;
                        break;
                }
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
                if (SupplementalComponents != null)
                    foreach (Supplemental component in SupplementalComponents)
                        if (component != null)
                            total += component.Speed;
                if (MachineSpirit == MachineSpirit.Resolute)
                    total -= 1;
                if (ShipHistory == ShipHistory.WrestedFromASpaceHulk)
                    total += 1;
                if (CrewRace == Race.EvilSunz)
                    total += 1;
                if (Background == Background.ThulianExploratorVessel)
                    total -= 1;
                total += GMSpeed;
                if (Hull.MaxSpeed < 1)
                    return Math.Max(total, 1);//Math.max in case ship has lots of negatives to speed and is universe class or something equally stupid
                return (Math.Min(Hull.MaxSpeed, Math.Max(total, 1)));//no faster than max speed, no slower than 1
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
                if (SupplementalComponents != null)
                    foreach (Supplemental component in SupplementalComponents)
                        if (component != null)
                            total += component.Manoeuvrability;
                if (ShipHistory == ShipHistory.WrestedFromASpaceHulk)
                    total += 3;
                if (Background == Background.ThulianExploratorVessel)
                    total -= 5;
                total += GMManoeuvrability;
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
                if (SupplementalComponents != null)
                    foreach (Supplemental component in SupplementalComponents)
                        if (component != null)
                            total += component.DetectionRating;
                if (MachineSpirit == MachineSpirit.ANoseForTrouble)
                    total += 5;
                if (ShipHistory == ShipHistory.Haunted)
                    total += 6;
                if (Background == Background.ThulianExploratorVessel)
                    total += 10;
                total += GMDetection;
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
                int total = 0;
                if (VoidShield != null)
                    total = VoidShield.Strength;
                if (OverloadShieldCapacitors == Quality.Best)
                    total += 1;
                total += GMShields;
                return total;
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
                if (Hull.ArmourLocked)
                    return total;
                if (SupplementalComponents != null)
                    foreach (Supplemental component in SupplementalComponents)
                        if (component != null)
                            total += component.Armour;
                if (MachineSpirit == MachineSpirit.ANoseForTrouble)
                    total -= 1;
                if (ShipHistory == ShipHistory.WrestedFromASpaceHulk)
                    total += 1;
                total += GMArmour;
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
                if (SupplementalComponents != null)
                    foreach (Supplemental component in SupplementalComponents)
                        if (component != null)
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
                if (SupplementalComponents != null)
                    foreach (Supplemental component in SupplementalComponents)
                        if (component != null)
                            total += component.TurretRating;
                total += GMTurretRating;
                return Math.Max(total, 0);
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
                if (VaultedCeilings != Quality.None)
                {
                    if ((Hull.HullTypes & HullType.Raider) > 0)
                        total -= 1;
                    else if ((Hull.HullTypes & (HullType.Transport | HullType.Frigate)) > 0)
                        total -= 2;
                    else if ((Hull.HullTypes & HullType.LightCruiser) > 0)
                        total -= 3;
                    else
                        total -= 4;
                    if (VaultedCeilings == Quality.Poor)
                        total -= 1;
                }
                if (SecondaryReactor != Quality.None)
                {
                    if ((Hull.HullTypes & HullType.Raider | HullType.Frigate) > 0)
                        total -= 1;
                    else if ((Hull.HullTypes & (HullType.Transport)) > 0)
                        total -= 2;
                    else if ((Hull.HullTypes & HullType.LightCruiser) > 0)
                        total -= 3;
                    else
                        total -= 4;
                }
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
                if (Hull != null)
                    total += Hull.Power;//if power != 0, hull grants or uses excess power
                if (SupplementalComponents != null)
                    foreach (Supplemental component in SupplementalComponents)
                        if (component != null && component.PowerGenerated)
                            total += component.Power;
                if (ShipHistory == ShipHistory.WolfInSheepsClothing)
                    total -= 2;
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
                if (Weapons != null)
                    foreach (Weapon gun in Weapons)
                        if (gun != null)
                            total += gun.Space;
                if (SupplementalComponents != null)
                    foreach (Supplemental component in SupplementalComponents)
                        if (component != null)
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
                if (Weapons != null)
                    foreach (Weapon gun in Weapons)
                        if (gun != null)
                            total += gun.Power;
                if (SupplementalComponents != null)
                    foreach (Supplemental component in SupplementalComponents)
                        if (component != null && !component.PowerGenerated)
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
                if (SupplementalComponents != null)
                    foreach (Supplemental component in SupplementalComponents)
                        if (component != null)
                            total += component.CrewPopulation;
                if (ShipHistory == ShipHistory.DeathCult)
                    total -= 8;
                total += GMCrewPopulation;
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
                if (CrewRace == Race.Servitor)
                    return 100;
                int total = 100;
                if (LifeSustainer != null)
                    total += LifeSustainer.Morale;
                if (CrewQuarters != null)
                    total += CrewQuarters.Morale;
                if (SupplementalComponents != null)
                    foreach (Supplemental component in SupplementalComponents)
                        if (component != null)
                            total += component.Morale;
                if (ShipHistory == ShipHistory.Haunted)
                    total -= 10;
                if (CherubimAerie == Quality.Poor)
                    total -= 2;
                else if (CherubimAerie != Quality.None)
                    total -= 1;
                switch (CrewImprovements)
                {
                    case Quality.Poor:
                        total += 1;
                        break;
                    case Quality.Common:
                        total += 2;
                        break;
                    case Quality.Good:
                        total += 3;
                        break;
                    case Quality.Best:
                        total += 5;
                        break;
                }
                switch (VaultedCeilings)
                {
                    case Quality.Poor:
                    case Quality.Common:
                        total += 5;
                        break;
                    case Quality.Good:
                        total += 7;
                        break;
                    case Quality.Best:
                        total += 10;
                        break;
                }
                switch (ResolutionArena)
                {
                    case Quality.Poor:
                        total += 2;
                        break;
                    case Quality.Common:
                    case Quality.Good:
                        total += 3;
                        break;
                    case Quality.Best:
                        total += 5;
                        break;
                }
                if (DistributedCargoHold != Quality.None)
                    total -= 2;
                total += GMMorale;
                return total;
            }
        }
        /// <summary>
        /// Current morale rating of the Starship
        /// </summary>
        public int CurrentMorale
        {
            get
            {
                if (CrewRace == Race.Servitor)
                    return 100;
                else return _currentMorale;
            }
            set { _currentMorale = value; }
        }//When live ship is ready to go
        private int _currentMorale;
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
                if (SupplementalComponents != null)
                    foreach (Supplemental component in SupplementalComponents)
                        if (component != null)
                            total += component.HullIntegrity;
                //history
                if (MachineSpirit == MachineSpirit.Resolute)
                    total += 3;
                if ((Background & Background.PlanetBoundForMillenia) > 0)
                {
                    total -= (int)(Background & Background.PlanetBoundForMillenia);//lower 3 bits are the value to deduct by
                    if ((Hull.HullTypes & (HullType.Transport | HullType.Raider | HullType.Frigate)) > 0)
                        total -= 1;
                }
                total += GMHullIntegrity;
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
                int total = CrewRating;
                total += GMCrewRating;
                if (SupplementalComponents != null)
                    foreach (Supplemental component in SupplementalComponents)
                        if (component != null)
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
                if (Hull != null)
                    total += Hull.BSModifier;
                if (ShipBridge != null)
                    total += ShipBridge.BSModifier;
                if (AugurArrays != null)
                    total += AugurArrays.BSModifier;
                if (SupplementalComponents != null)
                    foreach (Supplemental component in SupplementalComponents)
                        if (component != null)
                            total += component.BSModifier;
                if (TargettingMatrix == Quality.Common)
                    total += 5;
                if (CrewRace == Race.Servitor)
                    total -= 10;
                if (MachineSpirit == MachineSpirit.MartialHubris)
                    total += 5;
                if (Background == Background.VeteranOfTheAngevinCrusade)
                    total += 10;
                return total;
            }
        }

        /// <summary>
        /// Modifier to command tests
        /// </summary>
        public int Command
        {
            get
            {
                int total = 0;
                if (Hull != null)
                    total += Hull.Command;
                if (ShipBridge != null)
                    total += ShipBridge.Command;
                if (CrewRace == Race.Servitor)
                    total -= 10;
                if (Disciplinarium == Quality.Good || Disciplinarium == Quality.Best)
                    total += 5;
                if ((Background & Background.VesselOfTheFleet) > 0)
                    total += 10;
                return total;
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
                if (SupplementalComponents != null)
                    foreach (Supplemental component in SupplementalComponents)
                        if (component != null)
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
                if (ShipBridge != null)
                    total += ShipBridge.MiningObjective;
                if (AugurArrays != null)
                    total += AugurArrays.MiningObjective;
                if (SupplementalComponents != null)
                    foreach (Supplemental component in SupplementalComponents)
                        if (component != null)
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
                if (ShipBridge != null)
                    total += ShipBridge.CreedObjective;
                if (AugurArrays != null)
                    total += AugurArrays.CreedObjective;
                if (SupplementalComponents != null)
                    foreach (Supplemental component in SupplementalComponents)
                        if (component != null)
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
                if (ShipBridge != null)
                    total += ShipBridge.MilitaryObjective;
                if (AugurArrays != null)
                    total += AugurArrays.MilitaryObjective;
                if (SupplementalComponents != null)
                    foreach (Supplemental component in SupplementalComponents)
                        if (component != null)
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
                if (ShipBridge != null)
                    total += ShipBridge.TradeObjective;
                if (AugurArrays != null)
                    total += AugurArrays.TradeObjective;
                if (SupplementalComponents != null)
                    foreach (Supplemental component in SupplementalComponents)
                        if (component != null)
                            total += component.TradeObjective;
                if (OstentatiousDisplayOfWealth != Quality.None)
                    total += 25;
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
                if (ShipBridge != null)
                    total += ShipBridge.CriminalObjective;
                if (AugurArrays != null)
                    total += AugurArrays.CriminalObjective;
                if (SupplementalComponents != null)
                    foreach (Supplemental component in SupplementalComponents)
                        if (component != null)
                            total += component.CriminalObjective;
                if (OstentatiousDisplayOfWealth != Quality.None)
                    total += 25;
                if (DistributedCargoHold == Quality.Best)
                    total += 75;
                else if (DistributedCargoHold != Quality.None)
                    total += 50;
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
                if (ShipBridge != null)
                    total += ShipBridge.ExplorationObjective;
                if (AugurArrays != null)
                    total += AugurArrays.ExplorationObjective;
                if (SupplementalComponents != null)
                    foreach (Supplemental component in SupplementalComponents)
                        if (component != null)
                            total += component.ExplorationObjective;
                if (StarchartCollection == Quality.Best)
                    total += 50;
                else if (StarchartCollection != Quality.None)
                    total += 25;
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
                if (SupplementalComponents != null)
                    foreach (Supplemental component in SupplementalComponents)
                        if (component != null)
                            total += component.MacrobatteryModifier;
                return total;
            }
        }
        /// <summary>
        /// Morale Loss modifier from components
        /// </summary>
        public int MoraleLoss
        {
            get
            {
                int total = 0;
                if (CrewQuarters != null)
                    total += CrewQuarters.MoraleLoss;
                if (LifeSustainer != null)
                    total += LifeSustainer.MoraleLoss;
                if (SupplementalComponents != null)
                    foreach (Supplemental component in SupplementalComponents)
                        if (component != null)
                            total += component.MoraleLoss;
                if (ShipHistory == ShipHistory.DeathCult)
                    total -= 2;
                if (CrewRace == Race.Eldar)
                    total -= 1;
                return total;
            }
        }
        /// <summary>
        /// Crew Loss modifier from components
        /// </summary>
        public int CrewLoss
        {
            get
            {
                double total = 0;
                if (LifeSustainer != null)
                    total += LifeSustainer.CrewLoss;
                if (SupplementalComponents != null)
                    foreach (Supplemental component in SupplementalComponents)
                        if (component != null)
                            total += component.CrewLoss;
                if (CrewRace == Race.Ork)
                    total -= 1;
                if (CrewRace == Race.Eldar)
                    total += 1;
                if (CrewRace == Race.Servitor)
                    total = total / 2;
                return (int)Math.Ceiling(total);
            }
        }

        /// <summary>
        /// Modifier to repair tests
        /// </summary>
        public int RepairTest
        {
            get
            {
                int total = 0;
                if (ShipBridge != null)
                    total += ShipBridge.Repair;
                if (MachineSpirit == MachineSpirit.Resolute)
                    total += 10;
                if (ShipHistory == ShipHistory.ReliquaryOfMars)
                    total -= 20;
                if (ShipHistory == ShipHistory.Xenophilous)
                    total -= 10;
                switch (DistributedCargoHold)
                {
                    case Quality.Poor:
                        total -= 15;
                        break;
                    case Quality.Common:
                        total -= 10;
                        break;
                    case Quality.Good:
                    case Quality.Best:
                        total -= 5;
                        break;
                }
                return total;
            }
        }

        /// <summary>
        /// Modifier to pilot tests
        /// </summary>
        public int PilotTest
        {
            get
            {
                int total = 0;
                if (ShipBridge != null)
                    total += ShipBridge.Pilot;
                //upgrades
                if (MachineSpirit == MachineSpirit.AncientAndWise)
                    total += 10;
                return total;
            }
        }

        /// <summary>
        /// Modifier to navigate warp tests
        /// </summary>
        public int NavigateTest
        {
            get
            {
                int total = 0;
                if (Hull != null)
                    total += Hull.NavigateWarp;
                if (GellarField != null)
                    total += GellarField.NavigateWarp;
                if (ShipBridge != null)
                    total += ShipBridge.NavigateWarp;
                if (SupplementalComponents != null)
                    foreach (Supplemental component in SupplementalComponents)
                        if (component != null)
                            total += component.NavigateWarp;
                return total;
            }
        }

        //upgrades
        public Quality CherubimAerie;
        public Quality CrewImprovements;
        public Quality OstentatiousDisplayOfWealth;
        public Quality StarchartCollection;
        public Quality StormTrooperDetachment;
        public Quality VaultedCeilings;
        public Quality ArresterEngines;
        public Quality DistributedCargoHold;
        public Quality Disciplinarium;
        public Quality MimicDrive;
        public Quality OverloadShieldCapacitors;
        public Quality ResolutionArena;
        public Quality SecondaryReactor;
        public Quality SuperiorDamageControl;
        public Quality TargettingMatrix;
        public int Matrix;//weapon upgraded by poor Targetting Matrix if any

        public Background Background;

        //GM or Custom Modifiers
        public int GMSpeed { get; set; }
        public int GMHullIntegrity { get; set; }
        public int GMDetection { get; set; }
        public int GMManoeuvrability { get; set; }
        public int GMArmour { get; set; }
        public int GMTurretRating { get; set; }
        public int GMMorale { get; set; }
        public int GMCrewPopulation { get; set; }
        public int GMShields { get; set; }
        public int GMCrewRating { get; set; }
        public string GMSpecial { get; set; }
        public string GMMachineSpirit { get; set; }
        public string GMShipHistory { get; set; }

        public string ToJSON()
        {
            /*{
             * "Starship" : {
             *  "Name" : name,
             *  "Hull" : Hull,
             *  "Plasma" : PlasmaDrive,
             *  "Warp" : WarpDrive,
             *  "Gellar" : GellarField,
             *  "Void" : VoidShield,
             *  "Bridge" : ShipBridge,
             *  "Sustainer" : LifeSustainer,
             *  "Quarters" : CrewQuarters,
             *  "Augurs" : AugurArrays,
             *  "Weapons" : [...],
             *  "Supplementals" : [...],
             *  "Machine" : MachineSpirit,
             *  "History" : ShipHistory,
             *  "Rating" : CrewRating,
             *  "Race" : CrewRace,
             *  "CrewPop" : CurrentCrew,
             *  "Morale" : CurrentMorale,
             *  "Integrity" : CurrentIntegrity,
             *  "Cherubim" : CherubimAerie,
             *  "Improvements" : CrewImprovements,
             *  "Ostentatious" : OstentatiousDisplayOfWealth,
             *  "Starchart" : StarchartCollection,
             *  "StormTrooper" : StormTrooperDetachment,
             *  "Vaulted" : VaultedCeilings,
             *  "Arrester" : ArresterEngines,
             *  "Distributed" : DistributedCargoHold,
             *  "Disciplinarium" : Disciplinarium,
             *  "Mimic" : MimicDrive,
             *  "Overload" : OverloadShieldCapacitors,
             *  "Resolution" : ResolutionArena,
             *  "Secondary" : SecondaryReactor,
             *  "DamageControl" : SuperiorDamageControl,
             *  "Targetting" : TargettingMatrix,
             *  "Matrix" : Matrix,
             *  "Background" : Background }
             *  }
             */
            return @"{""Starship"":""Name"":""" + Name.Escape() + @""",""Hull"":" + Hull.JSON() + @",""Plasma"":" + PlasmaDrive.JSON() + @",""Warp"":" + WarpDrive.JSON() + @",""Gellar"":" + GellarField.JSON()
                + @",""Void"":" + VoidShield.JSON() + @",""Bridge"":" + ShipBridge.JSON() + @",""Sustainer"":" + LifeSustainer.JSON() + @",""Quarters"":" + CrewQuarters.JSON() + @",""Augurs"":" + AugurArrays.JSON()
                + @",""Weapons"":[" + String.Join(",", Weapons.Select(x => x.JSON())) + @"],""Supplementals"":[" + String.Join(",", SupplementalComponents.Select(x => x.ToJSON())) + @"],""Machine"":"
                + (byte)MachineSpirit + @",""History"":" + (byte)ShipHistory + @",""Rating"":" + CrewRating + @",""Race"":" + (byte)CrewRace + @",""CrewPop"":" + CurrentCrew + @",""Morale"":" + Morale
                + @",""Integrity"":" + CurrentIntegrity + @",""Cherubim"":" + (byte)CherubimAerie + @",""Improvements"":" + (byte)CrewImprovements + @",""Ostentatious"":" + (byte)OstentatiousDisplayOfWealth
                + @",""Starchart"":" + (byte)StarchartCollection + @",""StormTrooper"":" + (byte)StormTrooperDetachment + @",""Vaulted"":" + (byte)VaultedCeilings + @",""Arrester"":" + (byte)ArresterEngines
                + @",""Distributed"":" + (byte)DistributedCargoHold + @",""Disciplinarium"":" + (byte)Disciplinarium + @",""Mimic"":" + (byte)MimicDrive + @",""Overload"":" + (byte)OverloadShieldCapacitors
                + @",""Resolution"":" + (byte)ResolutionArena + @",""Secondary"":" + (byte)SecondaryReactor + @",""DamageControl"":" + (byte)SuperiorDamageControl + @",""Targetting"":" + (byte)TargettingMatrix
                + @",""Matrix"":" + Matrix + @",""Background"":" + (byte)Background;
        }
    }
}
