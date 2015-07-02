using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarshipGenerator
{
    public class Starship
    {
        Hull Hull;
        PlasmaDrive PlasmaDrive;
        WarpDrive WarpDrive;
        GellarField GellarField;
        VoidShield VoidShield;
        Bridge ShipBridge;
        CrewSustainer LifeSustainer;//seperate later with subclass or differentiate through filtering on something?
        CrewSustainer CrewQuarters;
        Augur AugurArrays;
        Weapon[] Weapons;//Initialise new when hull is chosen?
        List<Supplemental> SupplementalComponents;

        MachineSpirit MachineSpirit;
        ShipHistory ShipHistory;

        CrewRating CrewRating;
        Race CrewRace;

        //modifiers such as gm crew modifier

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
                //component as well
                return total;
            }
        }
        public int Speed
        {
            get
            {
                if (Hull == null)
                    return 0;
                int total = Hull.Speed;
                if (PlasmaDrive != null)
                    total += PlasmaDrive.Speed;
                //Supplemental component alterations
                return total;
            }
        }
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
                //Supplemental compoennts
                return total;
            }
        }
        public int DetectionRating
        {
            get
            {
                if (Hull == null)
                    return 0;
                int total = Hull.DetectionRating;
                if (AugurArrays != null)
                    total += AugurArrays.DetectionRating;
                //check if the ship has that one component that alters detection(check detection of all components)
                return total;
            }
        }
        public int Shields
        {
            get
            {
                if (VoidShield == null)
                    return 0;
                else
                    return VoidShield.Strength;
            }
        }
        public int Armour
        {
            get
            {
                if(Hull == null)
                    return 0;
                int total = Hull.Armour;
                //Extra armour from supplemental components
                return total;
            }
        }
        public int ProwArmour
        {
            get
            {
                int total = this.Armour;
                //if has prow armour + 4, else if has reinforced +2; components.ProwArmour
                return total;
            }
        }
        public int TurretRating
        {
            get
            {
                if(Hull == null)
                    return 0;
                int total = Hull.TurretRating;
                //supplemental turrets
                return total;
            }
        }
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
        public int MaxPower
        {
            get
            {
                if (PlasmaDrive == null)
                    return 0;
                int total = PlasmaDrive.Power;
                //add modifiers from histories or similar, include auxilary generators (components with generatepower true) 
                return total;
            }
        }
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
                //component as well
                return total;
            }
        }
        public int UsedPower
        {
            get
            {
                int total = 0;
                if (PlasmaDrive != null)
                    total += PlasmaDrive.Power;
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
                //component as well
                return total;
            }
        }
        public int CrewPopulation
        {
            get
            {
                int total = 100;
                //modify here
                return total;
            }
        }
        public int CurrentCrew { get; set; }//When live ship is ready to go
        public int Morale
        {
            get
            {
                int total = 100;
                if (LifeSustainer != null)
                    total += LifeSustainer.Morale;
                if (CrewQuarters != null)
                    total += CrewQuarters.Morale;
                //supplemental Components and histories
                return total;
            }
        }
        public int CurrentMorale { get; set; }//When live ship is ready to go
        public int HullIntegrity
        {
            get
            {
                if (Hull == null)
                    return 0;
                int total = Hull.HullIntegrity;
                //modifiers here
                return total;
            }
        }
        public int CurrentIntegrity { get; set; }//When live ship is ready to go

        public int CrewValue
        {
            get
            {
                int total = (int)CrewRating;
                //crew rating bonuses
                return total;
            }
        }

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

        //get objective bonuses
    }
}
