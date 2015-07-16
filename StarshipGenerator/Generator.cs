using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StarshipGenerator.Utils;
using StarshipGenerator.Components;
using StarshipGenerator.Ammo;
using System.IO;

namespace StarshipGenerator
{
    public class Generator
    {
        public static Supplemental MainCargoHold = new Supplemental("Main Cargo Hold", HullType.Transport, 2, 0, 0, RuleBook.CoreRulebook, 203, trade: 125);
        public static LandingBay EscortBay = new LandingBay("Jovian-Pattern Escort Bay", HullType.LightCruiser | HullType.CruiserPlus, WeaponSlot.Side, 1, 0, 0, 1, RuleBook.BattlefleetKoronus, 36);
        public static LandingBay LandingBay = new LandingBay("Jovian-Pattern Landing Bay", HullType.CruiserPlus, WeaponSlot.Side, 1, 0, 0, 2, RuleBook.BattlefleetKoronus, 36);
        public static TorpedoTubes VossTubes = new TorpedoTubes("Voss-Pattern Torpedo Tubes", HullType.All, WeaponSlot.Prow | WeaponSlot.Keel, 1, 0, 0, 2, 6, RuleBook.BattlefleetKoronus, 37);
        public static Weapon MarsCannon = new Weapon("Mars-Pattern Nova Cannon", WeaponType.NovaCannon, HullType.CruiserPlus, WeaponSlot.Prow, 4, 0, 0, 1, new DiceRoll(0, 2, 5), 0, 36, RuleBook.BattlefleetKoronus, 36, special: "Always revealed by successful active augury");
        public static Supplemental ArmouredProw = new Supplemental("Armoured Prow", HullType.CruiserPlus, 0, 0, 0, RuleBook.CoreRulebook, 204, new DiceRoll(1, 0, 0), "Cannot take macrobatteries or lance in prow", prowArmour: 4, max:1);
        public static Supplemental PlasmaRefinery = new Supplemental("Plasma Refinery", HullType.Transport, 10, 0, 0, RuleBook.BattlefleetKoronus, 30, special: "May spend 3 days and make 2 +10 pilot tests to harvest plasma granting +100 to objectives if fuel is used or sold. Failure on either test by 5 or more degrees destroyes the ship. If the ship does not harvest plasma for a year, reduce maximum power by 10");
        
        public static void Main() 
        {
            List<Component> comps = new List<Component>();
            //Hulls
            comps.Add(new Hull("Ambition-Class Cruiser", 5, 12, 15, 66, 17, 75, 57, HullType.Cruiser, null, RuleBook.BattlefleetKoronus, 24, 2, 1, 0, 2));
            comps.Add(new Hull("Armageddon-Class Battlecruiser", 5, 10, 10, 70, 20, 73, 63, HullType.BattleCruiser, "Can only stay at void half as long as other ships", RuleBook.BattlefleetKoronus, 23, 2, 1, 1, 2));
            comps.Add(new Hull("Avenger-Class Grand Cruiser", 5, 5, 10, 90, 21, 90, 70, HullType.GrandCruiser, null, RuleBook.BattlefleetKoronus, 20, 3, 0, 0, 3));
            comps.Add(new Hull("Carrack-Class Transport", 4, -5, 10, 45, 15, 38, 25, HullType.Transport, null, RuleBook.BattlefleetKoronus, 29, dorsal: 2, comps: new Supplemental[]{MainCargoHold}));
            comps.Add(new Hull("Chalice-Class Battlecruiser", 6, 10, 10, 70, 19, 75, 63, HullType.BattleCruiser, "Any critical received has a 25% chance to cause an additional Fire! critical", RuleBook.BattlefleetKoronus, 22, 2, 1, 2, 2, power: 4));
            comps.Add(new Hull("Claymore-Class Corvette", 8, 18, 12, 30, 17, 38, 38, HullType.Frigate, null, RuleBook.BattlefleetKoronus, 27, dorsal: 2));
            comps.Add(new Hull("Cobra-Class Destroyer", 10, 30, 10, 30, 15, 35, 30, HullType.Raider, null, RuleBook.IntoTheStorm, 152, 1, 1, 1));
            comps.Add(new Hull("Conquest-Class Star Galleon", 4, 5, 10, 65, 16, 56, 52, HullType.Cruiser | HullType.Transport, null, RuleBook.BattlefleetKoronus, 23, side: 2, comps: new Supplemental[] { MainCargoHold, MainCargoHold }));
            comps.Add(new Hull("Dauntless-Class Light Cruiser", 7, 15, 20, 60, 19, 60, 55, HullType.LightCruiser, null, RuleBook.CoreRulebook, 196, prow: 1, side: 1));
            comps.Add(new Hull("Defiant-Class Light Cruiser", 6, 12, 15, 60, 20, 55, 58, HullType.LightCruiser, null, RuleBook.BattlefleetKoronus, 26, 2, 1, 0, 1, broadside: EscortBay));
            comps.Add(new Hull("Dictator-Class Cruiser", 5, 8, 18, 70, 20, 65, 63, HullType.Cruiser, null, RuleBook.BattlefleetKoronus, 24, 3, 1, 0, 2, broadside: LandingBay));
            comps.Add(new Hull("Endeavour-Class Light Cruiser", 6, 12, 15, 60, 20, 58, 57, HullType.LightCruiser, null, RuleBook.BattlefleetKoronus, 26, 2, 2, 0, 1, frontal: VossTubes));
            comps.Add(new Hull("Exorcist-Class Grand Cruiser", 4, 4, 9, 85, 20, 80, 71, HullType.GrandCruiser, null, RuleBook.BattlefleetKoronus, 23, 3, side: 3, broadside: LandingBay));
            comps.Add(new Hull("Falchion-Class Frigate", 8, 17, 14, 36, 18, 34, 42, HullType.Frigate, null, RuleBook.BattlefleetKoronus, 26, prow: 1, dorsal: 2, frontal: VossTubes));
            comps.Add(new Hull("Firestorm-Class Frigate", 7, 20, 15, 38, 18, 40, 41, HullType.Frigate, null, RuleBook.IntoTheStorm, 152, 1, 1, 1));
            comps.Add(new Hull("Goliath-Class Factory Ship", 3, -10, 4, 50, 14, 40, 25, HullType.Transport, null, RuleBook.BattlefleetKoronus, 30, dorsal: 1, side: 1, comps: new Supplemental[] { MainCargoHold, MainCargoHold, PlasmaRefinery }));
            comps.Add(new Hull("Havoc-Class Merchant Raider", 9, 25, 10, 30, 16, 40, 35, HullType.Raider, null, RuleBook.CoreRulebook, 195, 1, 1, 1));
            comps.Add(new Hull("Hazeroth-Class Privateer", 10, 23, 12, 32, 14, 35, 30, HullType.Raider, null, RuleBook.CoreRulebook, 194, 1, 1, 1));
            comps.Add(new Hull("Iconoclast-Class Destroyee", 10, 25, 10, 28, 14, 32, 29, HullType.Raider, "Long term repairs repair an additional 2 hull integrity", RuleBook.BattlefleetKoronus, 28, dorsal: 2));
            comps.Add(new Hull("Jericho-Class Pligrim Vessel", 3, -10, 5, 50, 12, 45, 20, HullType.Transport, null, RuleBook.CoreRulebook, 194, prow: 1, side: 1, comps: new Supplemental[] { MainCargoHold }));
            comps.Add(new Hull("Lathe-Class Monitor-Cruiser", 5, 12, 15, 62, 20, 60, 55, HullType.LightCruiser, null, RuleBook.IntoTheStorm, 152, 1, 1, 1, 1));
            comps.Add(new Hull("Loki-Class Q-Ship", 4, -5, 10, 40, 13, 45, 21, HullType.Transport, null, RuleBook.IntoTheStorm, 151, 1, 1, 1, comps: new Supplemental[] { MainCargoHold }, history: ShipHistory.WolfInSheepsClothing));
            comps.Add(new Hull("Lunar-Class Cruiser", 5, 10, 10, 70, 20, 75, 60, HullType.Cruiser, null, RuleBook.CoreRulebook, 196, 2, 1, 0, 2));
            comps.Add(new Hull("Mars-Class Battlecruiser", 5, 10, 10, 70, 20, 54, 71, HullType.BattleCruiser, null, RuleBook.BattlefleetKoronus, 22, 2, 1, 1, 2, frontal: MarsCannon, broadside: LandingBay, comps: new Supplemental[] { ArmouredProw }));
            comps.Add(new Hull("Meritech Shrike-Class Raider", 10, 25, 20, 30, 16, 35, 34, HullType.Raider, null, RuleBook.BattlefleetKoronus, 28, 2, 0, 2, bs: 5));
            comps.Add(new Hull("Orion-Class Star Clipper", 10, 25, 10, 35, 12, 40, 25, HullType.Transport, null, RuleBook.IntoTheStorm, 151, dorsal: 1, keel: 1, comps: new Supplemental[] { MainCargoHold }, locked:true));
            comps.Add(new Hull("Overlord-Class BattleCruiser", 5, 10, 10, 70, 20, 78, 64, HullType.BattleCruiser, null, RuleBook.BattlefleetKoronus, 22, 2, 1, 1, 2));
            comps.Add(new Hull("Repulsive-Class Grand Cruiser", 5, 8, 10, 85, 19, 90, 69, HullType.GrandCruiser, null, RuleBook.BattlefleetKoronus, 20, 3, 1, 1, 2, navigate: -10, locked: true));
            comps.Add(new Hull("Secutor-Class Monitor-Cruiser", 5, 12, 15, 65, 20, 58, 58, HullType.LightCruiser, null, RuleBook.IntoTheStorm, 152, 2, 1, 1, 1, shields: HullType.LightCruiser | HullType.Cruiser));
            comps.Add(new Hull("Sword-Class Frigate", 8, 20, 15, 35, 18, 40, 40, HullType.Frigate, null, RuleBook.CoreRulebook, 195, 2, 0, 2));
            comps.Add(new Hull("Tempest-Class Strike Frigate", 8, 18, 12, 36, 19, 42, 40, HullType.Frigate, null, RuleBook.CoreRulebook, 195, dorsal: 2));
            comps.Add(new Hull("Turbulent-Class Heavy Frigate", 7, 18, 15, 40, 20, 42, 42, HullType.Frigate, null, RuleBook.BattlefleetKoronus, 17, dorsal: 2, power: 2, command: -5));
            comps.Add(new Hull("Tyrant-Class Cruiser", 5, 10, 10, 70, 20, 77, 61, HullType.Cruiser, null, RuleBook.IntoTheStorm, 153, 2, 1, 0, 2));
            comps.Add(new Hull("Universe-Class Mass Conveyor", 2, -20, 5, 65, 12, 94, 45, HullType.Transport, null, RuleBook.BattlefleetKoronus, 30, dorsal: 1, side: 1, maxspeed: 2, power: 10, comps: new Supplemental[] { MainCargoHold, MainCargoHold, MainCargoHold, MainCargoHold }));
            comps.Add(new Hull("Vagabond-Class Merchant Trader", 4, -5, 10, 40, 13, 40, 20, HullType.Transport, null, RuleBook.CoreRulebook, 194, 1, 1, 1));
            comps.Add(new Hull("Viper-Class Scout Sloop", 11, 30, 25, 25, 14, 29, 27, HullType.Raider, null, RuleBook.BattlefleetKoronus, 28, dorsal: 1));
            //End of hulls
            //Plasma Drives
            comps.Add(new PlasmaDrive("Cypra-Pattern Class 1 Drive", HullType.Transport, 30, 10, "+15 to silent running, any attempts to detect this vessel suffer -15", RuleBook.BattlefleetKoronus, 42, 2, comp: ComponentOrigin.Archeotech));
            comps.Add(new PlasmaDrive("Cypra-Pattern Class 2 Drive", HullType.Raider | HullType.Frigate, 40, 12, "+15 to silent running, any attempts to detect this vessel suffer -15", RuleBook.BattlefleetKoronus, 42, 2, comp: ComponentOrigin.Archeotech));
            comps.Add(new PlasmaDrive("Jovian-Pattern Class 1 Drive", HullType.Transport, 35, 8, null, RuleBook.CoreRulebook, 199));
            comps.Add(new PlasmaDrive("Jovian-Pattern Class 2 Drive", HullType.Raider | HullType.Frigate, 45, 10, null, RuleBook.CoreRulebook, 199));
            comps.Add(new PlasmaDrive("Jovian-Pattern Class 3 Drive", HullType.LightCruiser, 60, 12, null, RuleBook.CoreRulebook, 199));
            comps.Add(new PlasmaDrive("Jovian-Pattern Class 4 Drive", HullType.CruiserPlus, 75, 14, null, RuleBook.CoreRulebook, 199));
            comps.Add(new PlasmaDrive(@"Jovian-Pattern ""Warcruiser"" Drive", HullType.CruiserPlus, 85, 17, null, RuleBook.IntoTheStorm, 156, 2));
            comps.Add(new PlasmaDrive(@"Jovian-Pattern ""Warcruiser"" Drive", HullType.LightCruiser, 65, 14, null, RuleBook.IntoTheStorm, 156, 2));
            comps.Add(new PlasmaDrive("Jovian-Pattern Class 8.1 Drive", HullType.Frigate, 44, 11, "On a critical hit to this drive roll a d10, ignore the crit on a 4+", RuleBook.BattlefleetKoronus, 31, 1));
            comps.Add(new PlasmaDrive("Jovian-Pattern Class 8.2 Drive", HullType.LightCruiser, 59, 13, "On a critical hit to this drive roll a d10, ignore the crit on a 4+", RuleBook.BattlefleetKoronus, 31, 1));
            comps.Add(new PlasmaDrive("Jovian-Pattern Class 8.3 Drive", HullType.CruiserPlus, 74, 15, "On a critical hit to this drive roll a d10, ignore the crit on a 4+", RuleBook.BattlefleetKoronus, 31, 1));
            comps.Add(new PlasmaDrive("Jovian-Pattern Class 8.4 Drive", HullType.GrandCruiser, 93, 20, "On a critical hit to this drive roll a d10, ignore the crit on a 4+", RuleBook.BattlefleetKoronus, 31, 1));
            comps.Add(new PlasmaDrive("Lathe-Pattern Class 1 Drive", HullType.Transport, 40, 12, null, RuleBook.CoreRulebook, 199, 1));
            comps.Add(new PlasmaDrive(@"Lathe-Pattern Class 2a ""Sprint Trader"" Drive", HullType.Transport, 40, 14, null, RuleBook.IntoTheStorm, 156, 2, speed: 1, man: 3));
            comps.Add(new PlasmaDrive(@"Lathe-Pattern Class 2b ""Escort"" Drive", HullType.Raider | HullType.Frigate, 47, 14, null, RuleBook.IntoTheStorm, 156, 2, speed: 1, man: 3));
            comps.Add(new PlasmaDrive("Mezoa-Pattern Theta-7 Drive", HullType.Transport, 44, 18, "If vessel suffers thrusters damaged or engine crippled critical hits, the roll to determine severity is automatically 10", RuleBook.BattlefleetKoronus, 31, 1, speed: 2, man: 5));
            comps.Add(new PlasmaDrive("Mimic Engine", HullType.CruiserPlus, 75, 14, "Navigator may make a +10 perception test to disguise ship as one it has already encountered, this lasts until seen visually", RuleBook.HostileAcquisition, 74, 3, comp: ComponentOrigin.Xenotech));
            comps.Add(new PlasmaDrive("Mimic Engine", HullType.LightCruiser, 60, 12, "Navigator may make a +10 perception test to disguise ship as one it has already encountered, this lasts until seen visually", RuleBook.HostileAcquisition, 74, 3, comp: ComponentOrigin.Xenotech));
            comps.Add(new PlasmaDrive("Mimic Engine", HullType.Raider | HullType.Frigate, 45, 10, "Navigator may make a +10 perception test to disguise ship as one it has already encountered, this lasts until seen visually", RuleBook.HostileAcquisition, 74, 3, comp: ComponentOrigin.Xenotech));
            comps.Add(new PlasmaDrive("Mimic Engine", HullType.Transport, 40, 12, "Navigator may make a +10 perception test to disguise ship as one it has already encountered, this lasts until seen visually", RuleBook.HostileAcquisition, 74, 3, comp: ComponentOrigin.Xenotech));
            comps.Add(new PlasmaDrive(@"Saturine-Pattern Class 4A ""Ultra"" Drive", HullType.BattleCruiser, 90, 14, null, RuleBook.BattlefleetKoronus, 31));
            comps.Add(new PlasmaDrive("Saturine-Pattern Class 5 Drive", HullType.GrandCruiser, 95, 18, null, RuleBook.BattlefleetKoronus, 31));
            comps.Add(new PlasmaDrive(@"Segrazian ""Viperdrive"" Pirate Engine", HullType.Raider | HullType.Frigate, 45, 16, "If the vessel suffers an engine crippled critical hit, the severity roll is automatically 8-10, engines wrecked", RuleBook.HostileAcquisition, 69, 2, man: 5, speed: 2));
            //End of Plasma Drives
            comps.Add(new Augur("Auto-stabalised Logis-Targeter", 5, RuleBook.CoreRulebook, 207, 5, bs: 5, comp:ComponentOrigin.Archeotech));
            comps.Add(new Bridge("Armoured Bridge", HullType.CruiserPlus | HullType.LightCruiser, 3, 2, RuleBook.CoreRulebook, 200, "Ignore critical hits, damaged or unpowered on a d10 of 4+"));
            comps.Add(new CrewQuarters("Bilge Rat Quarters", HullType.CruiserPlus | HullType.LightCruiser, 2, 3, -2, RuleBook.BattlefleetKoronus, 33, "Reduce crew loss by 2 for depressurisation"));
            comps.Add(new GellarField("Belecace-pattern 90.r Gellar Field", HullType.All, 1, "-20 on warp encounters table", RuleBook.BattlefleetKoronus, 32, 0, 10));
            comps.Add(new LandingBay("Jovian Pattern Escort Bay", HullType.CruiserPlus | HullType.LightCruiser, WeaponSlot.Side, 1, 4, 1, 1, RuleBook.BattlefleetKoronus, 36));
            comps.Add(new LifeSustainer("Ancient Life Sustainer", HullType.CruiserPlus | HullType.LightCruiser, 2, 2, 2, RuleBook.CoreRulebook, 206, "Reduce loss to crew population from non-combat sources by 1", sp: 2, comp: ComponentOrigin.Archeotech));
            comps.Add(new Supplemental("Arboretum", HullType.Transport | HullType.Raider | HullType.Frigate, 2, 2, 1, RuleBook.IntoTheStorm, 160, null, "Double the time a ship may spend at void before suffering crew and morale damage", crew: 2));
            comps.Add(new TorpedoTubes("Fortis Pattern Torpedo Tubes", HullType.CruiserPlus | HullType.LightCruiser, WeaponSlot.Prow | WeaponSlot.Keel, 2, 8, 3, 6, 42, RuleBook.BattlefleetKoronus, 37, special: "+2VUs Torpedo speed on turn they are launched"));
            comps.Add(new VoidShield("Castellan Shield", HullType.All, 5, 1, 1, RuleBook.IntoTheStorm, 161, "During enemy turn, may make free -10 tech use to double number of shields", sp: 2, comp: ComponentOrigin.Archeotech));
            comps.Add(new WarpDrive("Albanov 1 Warp Engine", HullType.Transport | HullType.Raider | HullType.Frigate, 10, 11, RuleBook.HostileAcquisition, 69, 1, "Double base travel time through immaterium, +20 to warp travel encounters, +10 to leave the warp"));
            comps.Add(new Weapon("Bombardment Cannons", WeaponType.Macrobattery, HullType.CruiserPlus | HullType.LightCruiser, WeaponSlot.Prow | WeaponSlot.Dorsal | WeaponSlot.Keel, 5, 3, 3, 3, new DiceRoll(1, 0, 6), 2, 4, RuleBook.BattlefleetKoronus, 34, special: "Add +1 to crits rolled, +20 to intimidate tests while ship armed with this is in orbit, may add 50 to military objectives on that planet, for planetary bombardments affects double the area, +20 damage to large enemies, +10 damage to individuals and vehicles"));
            List<Squadron> squads = new List<Squadron>();
            squads.Add(new Squadron("Fury Interceptor", Race.Human, 10, 10, 20));
            using (FileStream fs = File.Create("ComponentsAndAmmo.config"))
            using (StreamWriter sw = new StreamWriter(fs))
            {
                sw.Write(@"{""Items"":[");
                foreach (Component comp in comps)
                    sw.Write(comp.ToJSON()+",");
                for (int i = 0; i < squads.Count; i++)
                {
                    sw.Write(squads[i].ToJSON());
                    if (i < squads.Count - 1)
                        sw.Write(',');
                }
                sw.Write(@"]}");
            }
        }
    }
}
