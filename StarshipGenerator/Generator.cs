﻿using System;
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
        public static TorpedoTubes VossTubes = new TorpedoTubes("Voss-Pattern Torpedo Tubes", HullType.All, 1, 0, 0, 2, 6, RuleBook.BattlefleetKoronus, 37);
        public static NovaCannon MarsCannon = new NovaCannon("Mars-Pattern Nova Cannon", HullType.CruiserPlus, 4, 0, 0, new DiceRoll(0, 2, 5), 36, RuleBook.BattlefleetKoronus, 36, special: "Always revealed by successful active augury");
        public static Supplemental ArmouredProw = new Supplemental("Armoured Prow", HullType.CruiserPlus, 0, 0, 0, RuleBook.CoreRulebook, 204, new DiceRoll(1, 0, 0), "Cannot take macrobatteries or lance in prow", prowArmour: 4, max:1);
        public static Supplemental PlasmaRefinery = new Supplemental("Plasma Refinery", HullType.Transport, 10, 0, 0, RuleBook.BattlefleetKoronus, 30, special: "May spend 3 days and make 2 +10 pilot tests to harvest plasma granting +100 to objectives if fuel is used or sold. Failure on either test by 5 or more degrees destroyes the ship. If the ship does not harvest plasma for a year, reduce maximum power by 10");
        public static LandingBay HoldLandingBay = new LandingBay("Hold Landing Bay", HullType.Transport, WeaponSlot.Auxiliary, 0, 0, 0, 2, RuleBook.BattlefleetKoronus, 36, special: "Attack Craft launched from this reduce their movement by 2VUs on the turn they launch. While in combat, squadron attempting to land must make a +10 Piloting + craft rating test to land safely. If this test is failed by 4 or more degrees teh component is considered damaged. it takes half an hour to land outside of combat");
        
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
            comps.Add(new PlasmaDrive("Jovian-Pattern Class 8.4 Drive", HullType.GrandCruiser | HullType.BattleShip, 93, 20, "On a critical hit to this drive roll a d10, ignore the crit on a 4+", RuleBook.BattlefleetKoronus, 31, 1));
            comps.Add(new PlasmaDrive("Lathe-Pattern Class 1 Drive", HullType.Transport, 40, 12, null, RuleBook.CoreRulebook, 199, 1));
            comps.Add(new PlasmaDrive(@"Lathe-Pattern Class 2a ""Sprint Trader"" Drive", HullType.Transport, 40, 14, null, RuleBook.IntoTheStorm, 156, 2, speed: 1, man: 3));
            comps.Add(new PlasmaDrive(@"Lathe-Pattern Class 2b ""Escort"" Drive", HullType.Raider | HullType.Frigate, 47, 14, null, RuleBook.IntoTheStorm, 156, 2, speed: 1, man: 3));
            comps.Add(new PlasmaDrive("Mezoa-Pattern Theta-7 Drive", HullType.Transport, 44, 18, "If vessel suffers thrusters damaged or engine crippled critical hits, the roll to determine severity is automatically 10", RuleBook.BattlefleetKoronus, 31, 1, speed: 2, man: 5));
            comps.Add(new PlasmaDrive("Mimic Engine", HullType.CruiserPlus, 75, 14, "Navigator may make a +10 perception test to disguise ship as one it has already encountered, this lasts until seen visually", RuleBook.HostileAcquisition, 74, 3, comp: ComponentOrigin.Xenotech));
            comps.Add(new PlasmaDrive("Mimic Engine", HullType.LightCruiser, 60, 12, "Navigator may make a +10 perception test to disguise ship as one it has already encountered, this lasts until seen visually", RuleBook.HostileAcquisition, 74, 3, comp: ComponentOrigin.Xenotech));
            comps.Add(new PlasmaDrive("Mimic Engine", HullType.Raider | HullType.Frigate, 45, 10, "Navigator may make a +10 perception test to disguise ship as one it has already encountered, this lasts until seen visually", RuleBook.HostileAcquisition, 74, 3, comp: ComponentOrigin.Xenotech));
            comps.Add(new PlasmaDrive("Mimic Engine", HullType.Transport, 40, 12, "Navigator may make a +10 perception test to disguise ship as one it has already encountered, this lasts until seen visually", RuleBook.HostileAcquisition, 74, 3, comp: ComponentOrigin.Xenotech));
            comps.Add(new PlasmaDrive(@"Saturine-Pattern Class 4A ""Ultra"" Drive", HullType.BattleCruiser, 90, 14, null, RuleBook.BattlefleetKoronus, 31));
            comps.Add(new PlasmaDrive("Saturine-Pattern Class 5 Drive", HullType.GrandCruiser | HullType.BattleShip, 95, 18, null, RuleBook.BattlefleetKoronus, 31));
            comps.Add(new PlasmaDrive(@"Segrazian ""Viperdrive"" Pirate Engine", HullType.Raider | HullType.Frigate, 45, 16, "If the vessel suffers an engine crippled critical hit, the severity roll is automatically 8-10, engines wrecked", RuleBook.HostileAcquisition, 69, 2, man: 5, speed: 2));
            comps.Add(new PlasmaDrive("Aconite Solar Sails", HullType.Frigate, 50, 0, "A ship with this component may interupt its Manoeuvre Action at any point to perform a Shooting Action. Once the Shooting Action is resolved, it must complete the remainder of its Manoeuvre Action. May still only make one Shooting Action per turn", RuleBook.LureoftheExpanse, 140, comp:ComponentOrigin.Xenotech));
            //End of Plasma Drives
            //Warp Drives
            comps.Add(new WarpDrive("Albanov 1 Warp Engine", ~HullType.AllCruiser, 10, 11, RuleBook.HostileAcquisition, 69, 1, "Double base travel time through immaterium, +20 to warp travel encounters, +10 to leave the warp"));
            comps.Add(new WarpDrive("Klenova Class M Warp Engine", ~HullType.AllCruiser, 10, 10, RuleBook.HostileAcquisition, 69, 0, "Navigator not needed or used, may only make calculated jumps, must test for warp encounters daily instead of per 5 days, ignore all navigate warp modifiers"));
            comps.Add(new WarpDrive("Markov 1 Warp Engine", ~HullType.AllCruiser, 12, 12, RuleBook.IntoTheStorm, 156, 1, "Reduce Warp Travel time by 1d5 weeks"));
            comps.Add(new WarpDrive("Markov 2 Warp Engine", HullType.AllCruiser, 13, 13, RuleBook.IntoTheStorm, 156, 1, "Reduce Warp Travel time by 1d10 days"));
            comps.Add(new WarpDrive("Miloslav G-616.b Warp Engine", ~HullType.AllCruiser, 8, 10, RuleBook.BattlefleetKoronus, 31, 0, "Half warp passage time but roll for encounters every 3 days instead of 5"));
            comps.Add(new WarpDrive("Miloslav H-616.b Warp Engine", HullType.AllCruiser, 10, 12, RuleBook.BattlefleetKoronus, 31, 0, "Half warp passage time but roll for encounters every 3 days instead of 5"));
            comps.Add(new WarpDrive("Strelov 1 Warp Engine", ~HullType.AllCruiser, 10, 10, RuleBook.CoreRulebook, 199));
            comps.Add(new WarpDrive("Strelov 2 Warp Engine", HullType.AllCruiser, 12, 12, RuleBook.CoreRulebook, 199));
            //End of Warp Drives
            //Gellar Fields
            comps.Add(new GellarField("Belecace-pattern 90.r Gellar Field", HullType.All, 1, "-20 on warp encounters table", RuleBook.BattlefleetKoronus, 32, 0, 10));
            comps.Add(new GellarField("Emergency Gellar Field", HullType.All, 2, "If vessel suddenly enters the warp roll a d10: on a 3+ Gellar Field activates automatically", RuleBook.IntoTheStorm, 156));
            comps.Add(new GellarField("Gellar Field", HullType.All, 1, null, RuleBook.CoreRulebook, 199));
            comps.Add(new GellarField("Mezoa Gellar Void Integrant", HullType.Transport | HullType.Raider, 0, "-5 to rolls on warp encounters table; Damage to Void shields also affects Gellar Field", RuleBook.HostileAcquisition, 70, 0));
            comps.Add(new GellarField("Warpsbane Hull", HullType.All, 1, "When rolling for warp encounters two rolls are made and the Navigator may choose which to apply", RuleBook.CoreRulebook, 199, 2, 10));
            //End of Gellar Fields
            //Void Shields
            comps.Add(new VoidShield("Castellan Shield", HullType.All, 5, 1, 1, RuleBook.IntoTheStorm, 161, "During enemy turn, may make free -10 tech use to double number of shields", sp: 2, comp: ComponentOrigin.Archeotech));
            comps.Add(new VoidShield("Castellan Shield Array", HullType.CruiserPlus, 7, 2, 2, RuleBook.IntoTheStorm, 161, "During enemy turn, may make free -10 tech use to double number of shields", sp: 2, comp: ComponentOrigin.Archeotech));
            comps.Add(new VoidShield("Multiple Void Shield Array", HullType.CruiserPlus, 8, 1, 2, RuleBook.CoreRulebook, 200));
            comps.Add(new VoidShield("Repulsor Shield Array", HullType.CruiserPlus, 8, 1, 2, RuleBook.IntoTheStorm, 157, "No penalties for moving through nebulae, ice rings, plasma clouds or celestial phenomonon"));
            comps.Add(new VoidShield("Repulsor Shield", HullType.All, 6, 1, 1, RuleBook.IntoTheStorm, 156, "No penalties for moving through nebulae, ice rings, plasma clouds or celestial phenomonon"));
            comps.Add(new VoidShield("Single Void Shield Array", HullType.All, 5, 1, 1, RuleBook.CoreRulebook, 199));
            comps.Add(new VoidShield("Triple Void Shield Array", HullType.GrandCruiser | HullType.BattleShip, 9, 3, 3, RuleBook.BattlefleetKoronus, 32));
            comps.Add(new VoidShield(@"Voss ""Glimmer""-Pattern Multiple Void Shield Array", HullType.CruiserPlus, 5, 1, 2, RuleBook.BattlefleetKoronus, 32, "When this cancels a hit roll a d10: on a 3 or lower the void shield fails to stop the hit"));
            comps.Add(new VoidShield(@"Voss ""Glimmer""-Pattern Void Shield Array", HullType.All, 3, 1, 1, RuleBook.BattlefleetKoronus, 32, "When this cancels a hit roll a d10: on a 3 or lower the void shield fails to stop the hit"));
            comps.Add(new VoidShield("Holo Field", HullType.All, 8, 4, 0, RuleBook.LureoftheExpanse, 140, "All attacks made against a ship while this is functioning suffer -40 to any Tests to hit in addition to other penalties. Macrobatteries only suffer -20 from this. Detection Actions against this ship suffer -30. This replaces void shields", comp: ComponentOrigin.Xenotech));
            comps.Add(new VoidShield("Ghost Field", HullType.All, 8, 4, 0, RuleBook.CoreRulebook, 207, "All ships firing at this vessel while this is active suffer -20 to ballistic skill tests. If the enemy is firing a lance or attempting a Hit and Run they take -30 instead to those tests. Replaces void shields entirely", sp: 3, comp: ComponentOrigin.Xenotech));
            //End of Void Shields
            //Ship's Bridges
            comps.Add(new Bridge("Armoured Bridge", HullType.AllCruiser, 3, 2, RuleBook.CoreRulebook, 200, "Ignore critical hits, damaged or unpowered on a d10 of 4+"));
            comps.Add(new Bridge("Armoured Bridge", HullType.Raider | HullType.Frigate, 2, 2, RuleBook.CoreRulebook, 200, "Ignore critical hits, damaged or unpowered on a d10 of 4+"));
            comps.Add(new Bridge("Bridge of Antiquity", HullType.AllCruiser, 2, 1, RuleBook.CoreRulebook, 207, "+10 to social skills tests for characters on the bridge", 2, man: 5, command: 10, comp: ComponentOrigin.Archeotech));
            comps.Add(new Bridge("Bridge of Antiquity", ~HullType.AllCruiser, 1, 1, RuleBook.CoreRulebook, 207, "+10 to social skills tests for characters on the bridge", 2, man: 5, command: 10, comp: ComponentOrigin.Archeotech));
            comps.Add(new Bridge("Combat Bridge", HullType.AllCruiser, 2, 2, RuleBook.CoreRulebook, 200, repair: 10));
            comps.Add(new Bridge("Combat Bridge", ~HullType.AllCruiser, 1, 1, RuleBook.CoreRulebook, 200, repair: 10));
            comps.Add(new Bridge("Command Bridge", HullType.AllCruiser, 3, 2, RuleBook.CoreRulebook, 200, sp: 1, bs: 5, command: 5));
            comps.Add(new Bridge("Command Bridge", HullType.Raider | HullType.Frigate, 2, 1, RuleBook.CoreRulebook, 200, sp: 1, bs: 5, command: 5));
            comps.Add(new Bridge("Commerce Bridge", HullType.Transport, 1, 1, RuleBook.CoreRulebook, 200, trade: 50));
            comps.Add(new Bridge("Exploration Bridge", HullType.AllCruiser, 4, 2, RuleBook.IntoTheStorm, 157, "+5 to active augury", 1, exploration: 50));
            comps.Add(new Bridge("Exploration Bridge", ~HullType.AllCruiser, 4, 1, RuleBook.IntoTheStorm, 157, "+5 to active augury", 1, exploration: 50));
            comps.Add(new Bridge("Fleet Flag Bridge", HullType.GrandCruiser | HullType.BattleShip, 4, 4, RuleBook.BattlefleetKoronus, 32, "+5 to Navigate(Stellar) tests; Allied ships within 30VU also gain +5 to Pilot and Navigate tests", 1, command: 10, pilot: 5, navigate: 5));
            comps.Add(new Bridge("Flight Command Bridge", HullType.AllCruiser, 2, 2, RuleBook.BattlefleetKoronus, 32, "+5 to command tests for small craft squadrons; Tests to ready new squadrons for launch are automatically passed; Gain +25 to objectives when using small craft for ground to air actions"));
            comps.Add(new Bridge("Invasion Bridge", HullType.CruiserPlus, 4, 3, RuleBook.BattlefleetKoronus, 32, "+10 to Ballistic Skill tests against planetary targets, units on a planet orbitted by this vessel coutn as equipped with a multicompass as long as they are in vox contact"));
            comps.Add(new Bridge("Ship Master's Bridge", HullType.CruiserPlus, 4, 3, RuleBook.CoreRulebook, 200, "+5 to Navigate(Stellar) tests", bs: 10, pilot: 5, navigate: 5));
            comps.Add(new Bridge("Smuggler's Bridge", HullType.Transport, 1, 1, RuleBook.HostileAcquisition, 70, criminal: 50));
            //End of Ships Bridges
            //Life Sustainers
            comps.Add(new LifeSustainer("Ancient Life Sustainer", HullType.AllCruiser, 2, 2, 2, RuleBook.CoreRulebook, 206, "Reduce loss to crew population from non-combat sources by 1", sp: 2, comp: ComponentOrigin.Archeotech));
            comps.Add(new LifeSustainer("Ancient Life Sustainer", ~HullType.AllCruiser, 2, 1, 2, RuleBook.CoreRulebook, 206, "Reduce loss to crew population from non-combat sources by 1", sp: 2, comp: ComponentOrigin.Archeotech));
            comps.Add(new LifeSustainer("Clemency-Pattern Life Sustainer", HullType.AllCruiser, 5, 5, 1, RuleBook.BattlefleetKoronus, 32, "Reduce damage by depressurisation by 4 to a minimum of 0"));
            comps.Add(new LifeSustainer("Clemency-Pattern Life Sustainer", ~HullType.AllCruiser, 4, 4, 1, RuleBook.BattlefleetKoronus, 32, "Reduce damage by depressurisation by 4 to a minimum of 0"));
            comps.Add(new LifeSustainer("Euphoric Life Sustainer", HullType.AllCruiser, 5, 3, 0, RuleBook.HostileAcquisition, 70, "This may be activated to provide these effects: +10 to Morale, -10 to crew rating, -10 to opponents command test if they perform a hit & run on this vessel; After this is deactivated the vessel suffers -10 morale for a day while the crew sobers up", sp: 1));
            comps.Add(new LifeSustainer("Euphoric Life Sustainer", ~HullType.AllCruiser, 4, 2, 0, RuleBook.HostileAcquisition, 70, "This may be activated to provide these effects: +10 to Morale, -10 to crew rating, -10 to opponents command test if they perform a hit & run on this vessel; After this is deactivated the vessel suffers -10 morale for a day while the crew sobers up", sp: 1));
            comps.Add(new LifeSustainer("Mark 1.r Life Sustainer", HullType.AllCruiser, 4, 2, 0, RuleBook.CoreRulebook, 200, moraleLoss: 1));
            comps.Add(new LifeSustainer("Mark 1.r Life Sustainer", ~HullType.AllCruiser, 3, 1, 0, RuleBook.CoreRulebook, 200, moraleLoss: 1));
            comps.Add(new LifeSustainer("Vitae-Pattern Life Sustainer", HullType.AllCruiser, 5, 3, 0, RuleBook.CoreRulebook, 200));
            comps.Add(new LifeSustainer("Vitae-Pattern Life Sustainer", ~HullType.AllCruiser, 4, 2, 0, RuleBook.CoreRulebook, 200));
            //End of Life Sustainers
            //Crew Quarters
            comps.Add(new CrewQuarters("Bilge Rat Quarters", HullType.AllCruiser, 2, 3, -2, RuleBook.BattlefleetKoronus, 33, "Reduce crew loss by 2 for depressurisation"));
            comps.Add(new CrewQuarters("Bilge Rat Quarters", ~HullType.AllCruiser, 1, 2, -2, RuleBook.BattlefleetKoronus, 33, "Reduce crew loss by 2 for depressurisation"));
            comps.Add(new CrewQuarters("Clan-kin Quarters", HullType.AllCruiser, 2, 5, 0, RuleBook.IntoTheStorm, 157, "+5 to command tests to defend agaisnt boarding and hit and run", sp: 1, loss: -1));
            comps.Add(new CrewQuarters("Clan-kin Quarters", ~HullType.AllCruiser, 1, 4, 0, RuleBook.IntoTheStorm, 157, "+5 to command tests to defend agaisnt boarding and hit and run", sp: 1, loss: -1));
            comps.Add(new CrewQuarters("Cold Quarters", HullType.AllCruiser, 4, 5, 0, RuleBook.IntoTheStorm, 157, "Once per session the captain may choose to reduce one source of crew population loss to 0", sp: 1));
            comps.Add(new CrewQuarters("Cold Quarters", ~HullType.AllCruiser, 3, 4, 0, RuleBook.IntoTheStorm, 157, "Once per session the captain may choose to reduce one source of crew population loss to 0", sp: 1));
            comps.Add(new CrewQuarters("Pressed Crew Quarters", HullType.AllCruiser, 2, 3, -1, RuleBook.CoreRulebook, 200));
            comps.Add(new CrewQuarters("Pressed Crew Quarters", ~HullType.AllCruiser, 1, 2, -1, RuleBook.CoreRulebook, 200));
            comps.Add(new CrewQuarters("Slave Quarters", HullType.AllCruiser, 1, 2, -5, RuleBook.HostileAcquisition, 71));
            comps.Add(new CrewQuarters("Slave Quarters", ~HullType.AllCruiser, 1, 1, -5, RuleBook.HostileAcquisition, 71));
            comps.Add(new CrewQuarters("Voidsmen Quarters", HullType.AllCruiser, 2, 4, 0, RuleBook.CoreRulebook, 200));
            comps.Add(new CrewQuarters("Voidsmen Quarters", ~HullType.AllCruiser, 1, 3, 0, RuleBook.CoreRulebook, 200));
            //End of Crew Quarters
            //Augur Arrays
            comps.Add(new Augur("Auto-stabalised Logis-Targeter", 5, RuleBook.CoreRulebook, 207, 5, bs: 5, comp: ComponentOrigin.Archeotech));
            comps.Add(new Augur("BG-15 Assault Scanners", 5, RuleBook.BattlefleetKoronus, 33, 0, "+5 to Ballistic Skills tests against planetary targets", military: 50));
            comps.Add(new Augur("Deep Void Augur Array", 7, RuleBook.CoreRulebook, 202, 10, sp: 1));
            comps.Add(new Augur("Mark 100 Augur Array", 3, RuleBook.CoreRulebook, 201));
            comps.Add(new Augur("Mark 201.b Augur Array", 5, RuleBook.CoreRulebook, 201, 5));
            comps.Add(new Augur("R-50 Auspex Multi-band", 4, RuleBook.CoreRulebook, 202, -2, "+5 to maneouvrability tests to avoid celestial phenomena", exploration: 50));
            comps.Add(new Augur("W-240 Passive Detection Arrays", 3, RuleBook.HostileAcquisition, 71, special: "May perform any detection actions on silent running without penalty", sp: 1));
            comps.Add(new Augur("X-470 Ultimo Array", 6, RuleBook.BattlefleetKoronus, 33, 10, "+15 to detect ships on silent runnign with active augury; +5 to opponents ballistic skill tests to hit this vessel"));
            //End of Augur Arrays
            //Weapons
            comps.Add(new Weapon("Stygies-Pattern Bombardment Cannons", WeaponType.Macrobattery, HullType.AllCruiser, WeaponSlot.Prow | WeaponSlot.Dorsal | WeaponSlot.Keel, 5, 3, 3, 3, new DiceRoll(1, 0, 6), 2, 4, RuleBook.BattlefleetKoronus, 34, special: "Add +1 to critical table for crits rolled, +20 to intimidate tests while ship armed with this is in orbit, may add 50 to military objectives on that planet, for planetary bombardments affects double the area, +20 damage to large enemies, +10 damage to individuals and vehicles"));
            comps.Add(new Weapon("Dark Cannon", WeaponType.Macrobattery, HullType.All, WeaponSlot.All, 4, 2, 3, 3, new DiceRoll(1, 0, 1), 6, 6, RuleBook.HostileAcquisition, 74, special: "Vessels hit by this weapon suffer -15 to ballistic skill in their following turn", comp: ComponentOrigin.Xenotech));
            comps.Add(new Weapon("Disruption Macrocannons", WeaponType.Macrobattery, HullType.All, WeaponSlot.All, 4, 2, 2, 3, new DiceRoll(1, 0, 1), 0, 5, RuleBook.BattlefleetKoronus, 34, special: "Does not cause damage but for every 5 damage rolled, ignoring armour, oen random compoent on target vessel becomes unpowered; Cannot crit and may only be combined into a salvo with other Disruption Macrocannon batteries"));
            comps.Add(new Weapon("Disruption Macrocannon Broadside", WeaponType.Macrobattery, HullType.AllCruiser, WeaponSlot.Side, 6, 5, 2, 6, new DiceRoll(1, 0, 1), 0, 5, RuleBook.BattlefleetKoronus, 34, special: "Does not cause damage but for every 5 damage rolled, ignoring armour, oen random compoent on target vessel becomes unpowered; Cannot crit and may only be combined into a salvo with other Disruption Macrocannon batteries"));
            comps.Add(new Weapon("Dragon's Breath Lance", WeaponType.Lance, HullType.All, WeaponSlot.Prow, 13, 8, 3, 3, new DiceRoll(1, 0, 6), 3, 3, RuleBook.LureoftheExpanse, 139, comp: ComponentOrigin.Archeotech));
            comps.Add(new Weapon("Energy Drain Matrix", WeaponType.Macrobattery, HullType.All, WeaponSlot.All, 3, 1, 2, 4, default(DiceRoll), 0, 4, RuleBook.HostileAcquisition, 74, special: "Does not do damage or critical hits; For every hit that gets past the target's void shields, may reduce either target's Speed by 1 or Maneouverability by 5"));
            comps.Add(new TorpedoTubes("Fortis Pattern Torpedo Tubes", HullType.AllCruiser, 2, 8, 3, 6, 42, RuleBook.BattlefleetKoronus, 37, special: "+2VUs Torpedo speed on turn they are launched"));
            comps.Add(new Weapon("Godsbane Lance", WeaponType.Lance, HullType.BattleCruiser | HullType.GrandCruiser | HullType.BattleShip, WeaponSlot.Lance, 9, 4, 3, 1, new DiceRoll(1, 0, 2), 3, 12, RuleBook.BattlefleetKoronus, 34, special: "If the target is over 20VUs away, reduce damage to 1d10"));
            comps.Add(new Weapon("Godsbane Lance Battery", WeaponType.Lance, HullType.BattleCruiser | HullType.GrandCruiser | HullType.BattleShip, WeaponSlot.Lance, 12, 6, 3, 2, new DiceRoll(1, 0, 2), 3, 12, RuleBook.BattlefleetKoronus, 34, special: "If the target is over 20VUs away, reduce damage to 1d10"));
            comps.Add(new Weapon("Grapple Cannon", WeaponType.Macrobattery, HullType.Raider, WeaponSlot.All, 2, 2, 1, 1, default(DiceRoll), 0, 0, RuleBook.HostileAcquisition, 71, special: "When making a boarding test, may make a -10 ballistic skill test instead of the -20 Pilot+Maneouverability test, and if you do the test for the target to escape the boarding action is -40 instead of -20"));
            comps.Add(new TorpedoTubes("Gryphonne-Pattern Torpedo Tubes", ~HullType.Transport, 2, 6, 1, 4, 24, RuleBook.BattlefleetKoronus, 27));
            comps.Add(new Weapon("Hecutor-Pattern Plasma Battery", WeaponType.Macrobattery, HullType.AllCruiser, WeaponSlot.All, 8, 3, 2, 3, new DiceRoll(1, 0, 2), 4, 11, RuleBook.BattlefleetKoronus, 34, special: "When this rolls a critical result of 1 or 2, it affects 2 components instead of 1"));
            comps.Add(new Weapon("Hecutor-Pattern Plasma Broadside", WeaponType.Macrobattery, HullType.BattleCruiser | HullType.GrandCruiser | HullType.BattleShip, WeaponSlot.Side, 12, 5, 2, 5, new DiceRoll(1, 0, 2), 4, 11, RuleBook.BattlefleetKoronus, 34, special: "When this rolls a critical result of 1 or 2, it affects 2 components instead of 1"));
            comps.Add(new Weapon("Jovian-Pattern Missile Battery", WeaponType.Macrobattery, HullType.All, WeaponSlot.All, 3, 1, 1, 5, new DiceRoll(1, 0, 1), 6, 6, RuleBook.IntoTheStorm, 158, special: "May not fire two turns ion a row"));
            comps.Add(new LandingBay("Jovian-Pattern Escort Bay", HullType.AllCruiser, WeaponSlot.Side, 1, 4, 1, 1, RuleBook.BattlefleetKoronus, 36));
            comps.Add(new LandingBay("Jovian-Pattern Landing Bay", HullType.CruiserPlus, WeaponSlot.Side, 1, 6, 2, 2, RuleBook.BattlefleetKoronus, 36));
            comps.Add(new NovaCannon("Jovian-Pattern Nova Cannon", HullType.CruiserPlus, 6, 7, 5, new DiceRoll(0, 2, 7), 35, RuleBook.BattlefleetKoronus, 42, comp: ComponentOrigin.Archeotech, special:"Ships hit by this suffer 1d5 morale damage even if no damage is inflicted; If this is ever damaged: instead it is destroyed and ship takes 1d10 hull integrity damage with no reduction from armour or shields"));
            comps.Add(new Weapon("Las-Burner", WeaponType.Lance, HullType.All, WeaponSlot.Lance | WeaponSlot.Dorsal | WeaponSlot.Keel, 7, 3, 2, 2, new DiceRoll(0, 1, 1), 3, 3, RuleBook.BattlefleetKoronus, 35, special: "Grants +5 to opposed command test for boarding actions"));
            comps.Add(new Weapon("Lathe-Pattern Grav-Culverin Broadside", WeaponType.Macrobattery, HullType.AllCruiser, WeaponSlot.Side, 5, 5, 1, 6, new DiceRoll(1, 0, 3), 6, 5, RuleBook.IntoTheStorm, 158, special: "May reduce damage to 1d10+1 to increase range by 2VUs"));
            comps.Add(new LandingBay("Lathe-Pattern Landing Bay", HullType.CruiserPlus, WeaponSlot.Side, 1, 5, 2, 2, RuleBook.BattlefleetKoronus, 36, special: "If this component becomes unpowered while open, it also becomes depressurised. The component must be open during the strategic turn that craft take off or land"));
            comps.Add(new Weapon("Mars-Pattern Macrocannon Broadside", WeaponType.Macrobattery, HullType.AllCruiser, WeaponSlot.Side, 4, 5, 1, 6, new DiceRoll(1, 0, 2), 5, 6, RuleBook.CoreRulebook, 202));
            comps.Add(new Weapon("Mars-Pattern Macrocannons", WeaponType.Macrobattery, HullType.All, WeaponSlot.All, 4, 2, 1, 3, new DiceRoll(1, 0, 2), 5, 6, RuleBook.CoreRulebook, 202));
            comps.Add(new NovaCannon("Mars-Pattern Nova Cannon", HullType.CruiserPlus, 3, 7, 3, new DiceRoll(0, 2, 4), 40, RuleBook.BattlefleetKoronus, 36));
            comps.Add(new TorpedoTubes("Mars-Pattern Torpedo Tubes", HullType.AllCruiser, 2, 8, 2, 6, 42, RuleBook.BattlefleetKoronus, 37));
            comps.Add(new Weapon("Mezoa-Pattern Hybrid Lance Battery", WeaponType.Lance, HullType.AllCruiser, WeaponSlot.Lance, 13, 6, 3, 2, new DiceRoll(1, 0, 5), 4, 4, RuleBook.BattlefleetKoronus, 35));
            comps.Add(new Weapon("Mezoa-Pattern Hybrid Lance Weapon", WeaponType.Lance, HullType.All, WeaponSlot.Lance, 9, 4, 3, 1, new DiceRoll(1, 0, 5), 4, 4, RuleBook.BattlefleetKoronus, 35));
            comps.Add(new Weapon("Mezoa-Pattern Macrocannons", WeaponType.Macrobattery, HullType.All, WeaponSlot.All, 4, 4, 1, 4, new DiceRoll(1, 0, 3), 5, 5, RuleBook.IntoTheStorm, 158));
            comps.Add(new TorpedoTubes("Plasma Accelerated Torpedo Tubes", HullType.All, 2, 4, 2, 2, 16, RuleBook.BattlefleetKoronus, 42, special: "Torpedoes launched from this component gain an additional +4VUs speed on the turn they are launched; Torpedoes launched by this gain +10 to hit", comp: ComponentOrigin.Archeotech));
            comps.Add(new Weapon("Pyros Melta-Cannons", WeaponType.Macrobattery, HullType.All, WeaponSlot.All, 4, 3, 2, 3, new DiceRoll(1, 0, 4), 4, 4, RuleBook.IntoTheStorm, 158, special: "When this weapon component inflicts a critical hit, it is automatically a Fire! Critical"));
            comps.Add(new Weapon("Ryza-Pattern Plasma Battery", WeaponType.Macrobattery, HullType.All, WeaponSlot.All, 8, 4, 1, 4, new DiceRoll(1, 0, 4), 4, 5, RuleBook.CoreRulebook, 203, special: "When this rolls a critical result of 1 or 2, it affects 2 components instead of 1"));
            comps.Add(new NovaCannon("Ryza-Pattern Nova Cannon", HullType.CruiserPlus, 4, 7, 4, new DiceRoll(0, 2, 5), 36, RuleBook.BattlefleetKoronus, 37, "For every 5 degrees of failure on a test to fire this, the firing vessel suffers a critical hit. If the critical would affect a component, it affects this weapon"));
            comps.Add(new Weapon("Shard Battery Cannon", WeaponType.Macrobattery, HullType.All, WeaponSlot.All, 0, 3, 2, 4, new DiceRoll(1, 0, 2), 3, 6, RuleBook.CoreRulebook, 208, special: "Cannont become unpowered; If this is ever destroyed vessel suffers 2d5 hull integrity damage with no armour or shields", comp: ComponentOrigin.Xenotech));
            comps.Add(new Weapon("Staravar Laser Macrobattery", WeaponType.Macrobattery, HullType.All, WeaponSlot.All, 4, 4, 2, 4, new DiceRoll(1, 0, 2), 4, 12, RuleBook.IntoTheStorm, 162, comp: ComponentOrigin.Archeotech));
            comps.Add(new Weapon("Starbreaker Lance Weapon", WeaponType.Lance, HullType.All, WeaponSlot.Lance, 6, 4, 2, 1, new DiceRoll(1, 0, 2), 3, 5, RuleBook.CoreRulebook, 203));
            comps.Add(new Weapon("Star-Flare Lance", WeaponType.Lance, HullType.AllCruiser, WeaponSlot.Lance, 12, 6, 3, 3, new DiceRoll(1, 0, 3), 3, 7, RuleBook.IntoTheStorm, 162, special: "Scores additional hit per 2 degrees of success instead of 3", comp: ComponentOrigin.Archeotech));
            comps.Add(new Weapon("Stygies-Pattern Macrocannon Battery", WeaponType.Macrobattery, HullType.All, WeaponSlot.All, 4, 5, 1, 3, new DiceRoll(1, 0, 2), 5, 5, RuleBook.BattlefleetKoronus, 34, special: "When calculating damage for a salvo in which at least one shot from this got past shields, reduce targets armour by 3"));
            comps.Add(new Weapon("Sunhammer Lance", WeaponType.Lance, HullType.All, WeaponSlot.Lance, 9, 4, 2, 1, new DiceRoll(1, 0, 3), 3, 9, RuleBook.IntoTheStorm, 158));
            comps.Add(new Weapon("Sunhammer Lance Battery", WeaponType.Lance, HullType.AllCruiser, WeaponSlot.Lance, 13, 6, 2, 2, new DiceRoll(1, 0, 3), 3, 9, RuleBook.IntoTheStorm, 158));
            comps.Add(new Weapon("Sunsear Las-Broadside", WeaponType.Macrobattery, HullType.AllCruiser, WeaponSlot.Side, 9, 6, 1, 6, new DiceRoll(1, 0, 2), 4, 9, RuleBook.IntoTheStorm, 158));
            comps.Add(new Weapon("Sunsear Laser Battery", WeaponType.Macrobattery, HullType.All, WeaponSlot.All, 6, 4, 1, 4, new DiceRoll(1, 0, 2), 4, 9, RuleBook.CoreRulebook, 202));
            comps.Add(new Weapon("Thunderstrike Macrocannons", WeaponType.Macrobattery, HullType.All, WeaponSlot.All, 2, 2, 1, 3, new DiceRoll(1, 0, 1), 6, 4, RuleBook.CoreRulebook, 202));
            comps.Add(new Weapon("Titanforge Lance Battery", WeaponType.Lance, HullType.AllCruiser, WeaponSlot.Lance, 13, 6, 2, 2, new DiceRoll(1, 0, 4), 3, 6, RuleBook.CoreRulebook, 203));
            comps.Add(new Weapon("Titanforge Lance Weapon", WeaponType.Lance, HullType.All, WeaponSlot.Lance, 9, 4, 2, 1, new DiceRoll(1, 0, 4), 3, 6, RuleBook.CoreRulebook, 203));
            comps.Add(new Weapon("Voidsunder Lance Battery", WeaponType.Lance, HullType.AllCruiser, WeaponSlot.Heavy, 15, 8, 3, 3, new DiceRoll(1, 0, 4), 3, 6, RuleBook.BattlefleetKoronus, 35, special: "If mounted in prow, may only fire forward regardless of ship class"));
            comps.Add(new TorpedoTubes("Voss-Pattern Torpedo Tubes", HullType.All, 1, 5, 2, 2, 12, RuleBook.BattlefleetKoronus, 37));
            comps.Add(new Weapon("Starcannon Cluster Battery", WeaponType.Macrobattery, HullType.All, WeaponSlot.All, 5, 3, 0, 4, new DiceRoll(1, 0, 2), 4, 6, RuleBook.LureoftheExpanse, 140, special: "Eldar vessels gain a +10 to Ballistic Skill tests to fire this weapon", comp: ComponentOrigin.Xenotech));
            //End of Weapons
            //Supplemental Components
            comps.Add(new Supplemental("Hold Landing Bay", HullType.Transport, 1, 0, 2, RuleBook.BattlefleetKoronus, 36, man: -5, hullint: -5, replace: "Main Cargo Hold", max: 1, aux: HoldLandingBay));
            comps.Add(new Supplemental("Auxiliary Plasma Banks", ~HullType.AllCruiser, 8, 5, 1, RuleBook.IntoTheStorm, 159, generated: true, special: "If this component becomes damaged, the vessel takes 1d5 hull integrity damage and the plasma drive is set on fire"));
            comps.Add(new Supplemental("Auxiliary Plasma Banks", HullType.AllCruiser, 10, 6, 1, RuleBook.IntoTheStorm, 159, generated: true, special: "If this component becomes damaged, the vessel takes 1d5 hull integrity damage and the plasma drive is set on fire"));
            comps.Add(new Supplemental("Arboretum", ~HullType.AllCruiser, 2, 2, 1, RuleBook.IntoTheStorm, 160, null, "Double the time a ship may spend at void before suffering crew and morale damage", crew: 2, max: 1));
            comps.Add(new Supplemental("Arboretum", HullType.AllCruiser, 2, 3, 1, RuleBook.IntoTheStorm, 160, null, "Double the time a ship may spend at void before suffering crew and morale damage", crew: 2, max: 1));
            comps.Add(new Supplemental("Armour Plating", ~HullType.AllCruiser, 0, 1, 2, RuleBook.CoreRulebook, 204, man: -2, armour: 1, max: 1));
            comps.Add(new Supplemental("Armour Plating", HullType.AllCruiser, 0, 2, 2, RuleBook.CoreRulebook, 204, man: -2, armour: 1, max: 1));
            comps.Add(new Supplemental("Armoured Prow", HullType.CruiserPlus, 0, 4, 2, RuleBook.CoreRulebook, 204, new DiceRoll(1, 0, 0), "Cannot take macrobatteries or lance in prow", prowArmour: 4, max: 1));
            comps.Add(new Supplemental("Asteroid Mining Facility", HullType.All, 6, 10, 3, RuleBook.IntoTheStorm, 160, null, "May construct trade endeavours based on mining, can gain +200 to mining objectives", max: 1));
            comps.Add(new Supplemental("Astropathic Choir-Chambers", HullType.All, 1, 1, 1, RuleBook.IntoTheStorm, 160, null, "+10 to focus power tests for astro-telepathy while in this component and +5VU range on psychic powers made by psykers in this component during combat", max: 1));
            comps.Add(new Supplemental("Augmented Retro-Thrusters", HullType.Raider | HullType.Frigate, 3, 0, 2, RuleBook.CoreRulebook, 203, man: 5, special: "External"));
            comps.Add(new Supplemental("Augmented Retro-Thrusters", HullType.LightCruiser | HullType.Transport, 4, 0, 2, RuleBook.CoreRulebook, 203, man: 5, special: "External"));
            comps.Add(new Supplemental("Augmented Retro-Thrusters", HullType.CruiserPlus, 5, 0, 2, RuleBook.CoreRulebook, 203, man: 5, special: "External"));
            comps.Add(new Supplemental("Auto-Temple", HullType.All, 1, 1, 0, RuleBook.HostileAcquisition, 72, null, "May be returned to ship in 2-3 days by work crew and lifters", morale: 2, creed: 150, max: 1));
            comps.Add(new Supplemental("Barracks", HullType.All, 2, 4, 2, RuleBook.CoreRulebook, 203, null, "+20 to command tests for boarding and hit and run", military: 100));
            comps.Add(new Supplemental("Brig", HullType.All, 1, 1, 1, RuleBook.BattlefleetKoronus, 37, null, "+5 to intimidate tests as part of extended actions, may earn 25 objective points to objectives involding the capture or transport of prisoners", morale: 1));
            comps.Add(new Supplemental("Broadband Hymn-Casters", HullType.All, 3, 0, 1, RuleBook.IntoTheStorm, 161, null, "External; All other ships within 30VUs must make a -10 Tech-Use Test in order to use vox or other broadcast communications; While active characters aboard this vessel gain +10 to intimidate tests on against all ships within 30VUs", max: 1));
            comps.Add(new Supplemental("Cargo Hold and Lighter Bay", ~HullType.Transport, 1, 2, 1, RuleBook.CoreRulebook, 203, man: -3, trade: 50, criminal: 50));
            comps.Add(new Supplemental("Chameleon Hull", HullType.All, 1, 0, 2, RuleBook.HostileAcquisition, 74, null, "External; May program a pattern, including markings, for the hull to show with a -10 Tech-Use Test and may change between programmed markings with a -10 Tech-Use Test"));
            comps.Add(new Supplemental("Cloudmining Facility", HullType.Transport, 3, 4, 1, RuleBook.BattlefleetKoronus, 39, null, "After comets have been located with a +0 Scrutiny+Detection Test they may be mined which takes 1d10+5 days. This may either grant 1d5 morale and extend time at void by 1 month, or grant +50 to objectives if it can be used or sold. May be possible to construct an endeavour to mine comets", max: 1));
            comps.Add(new Supplemental("Cogitator Interlink", HullType.All, 1, 1, 1, RuleBook.IntoTheStorm, 161, crewRating: 5, comp: ComponentOrigin.Archeotech, max: 1));
            comps.Add(new Supplemental("Compartmentalised Cargo Hold", ~HullType.Transport, 2, 5, 1, RuleBook.CoreRulebook, 203, trade: 100));
            comps.Add(new Supplemental("Crew Reclamation Facility", HullType.All, 1, 1, 1, RuleBook.CoreRulebook, 205, crewLoss: -3, moraleLoss: 1, max: 1));
            comps.Add(new Supplemental("Defensive Countermeasures", HullType.All, 1, 1, 2, RuleBook.BattlefleetKoronus, 38, null, "When deployed, ships targetting this vessel suffer a -20 to ballistic skills test, -30 if using torpedoes. This lasts for 1d5+1 strategic rounds and may not be used again until refitted at a shipyard with an upkeep test", max: 1));
            comps.Add(new Supplemental("Drop Pod Launch Bays", HullType.All, 1, 3, 2, RuleBook.IntoTheStorm, 159, null, "Can hold 20 pods, may deploy 10 per 30 minutes(strategic turn). The pods must be recovered from the planet's surface before being reused", military: 50));
            comps.Add(new Supplemental("Emergency Energy Reserves", ~HullType.AllCruiser, 2, 1, 2, RuleBook.HostileAcquisition, 73, null, "When crippled, the captain may choose to have either his weapons of speed unaffected by the usual penalties for crippled ships. If this is damaged, the component has a 25% chance of exploding. If it does, the component is destroyed and the ship takes 1d5 damage to hull integrity and a component of the GM's choice is set on fire", max: 1, comp: ComponentOrigin.Archeotech));
            comps.Add(new Supplemental("Emergency Energy Reserves", HullType.AllCruiser, 3, 2, 2, RuleBook.HostileAcquisition, 73, null, "When crippled, the captain may choose to have either his weapons of speed unaffected by the usual penalties for crippled ships. If this is damaged, the component has a 25% chance of exploding. If it does, the component is destroyed and the ship takes 1d5 damage to hull integrity and a component of the GM's choice is set on fire", max: 1, comp: ComponentOrigin.Archeotech));
            comps.Add(new Supplemental("Empyrean Mantle", ~HullType.AllCruiser, 3, 0, 2, RuleBook.IntoTheStorm, 159, null, "External; When travelling on Silent Running, all tests to detect this vessel have their Difficulty increased by two degrees", criminal: 50, max: 1));
            comps.Add(new Supplemental("Empyrean Mantle", HullType.AllCruiser, 5, 0, 2, RuleBook.IntoTheStorm, 159, null, "External; When travelling on Silent Running, all tests to detect this vessel have their Difficulty increased by two degrees", criminal: 50, max: 1));
            comps.Add(new Supplemental("Energistic Conversion Matrix", HullType.Frigate | HullType.Raider, 1, 1, 1, RuleBook.IntoTheStorm, 161, null, "May use 3 power to gain 1 speed, to a maximum of 5 extra speed. May divert power from other components by making them unpowered until this is turned off", max: 1));
            comps.Add(new Supplemental("Energistic Conversion Matrix", HullType.LightCruiser, 1, 1, 1, RuleBook.IntoTheStorm, 161, null, "May use 4 power to gain 1 speed, to a maximum of 5 extra speed. May divert power from other components by making them unpowered until this is turned off", max: 1));
            comps.Add(new Supplemental("Energistic Conversion Matrix", HullType.Transport | HullType.CruiserPlus, 1, 1, 1, RuleBook.IntoTheStorm, 161, null, "May use 5 power to gain 1 speed, to a maximum of 5 extra speed. May divert power from other components by making them unpowered until this is turned off", max: 1));
            comps.Add(new Supplemental("Evacuation Bay", ~HullType.Transport, 2, 4, 1, RuleBook.HostileAcquisition, 71, null, "As a free action, a member of the bridge may open the cargo hatches to forcibly eject all of the cargo into the void, cleansing the hold", trade: 75));
            comps.Add(new Supplemental("Excess Void Armour", ~HullType.AllCruiser, 0, 2, 2, RuleBook.LureoftheExpanse, 139, speed: -2, man: -3, armour: 3));
            comps.Add(new Supplemental("Excess Void Armour", HullType.AllCruiser, 0, 3, 2, RuleBook.LureoftheExpanse, 139, speed: -2, man: -3, armour: 3));
            comps.Add(new Supplemental("Extended Supply Vaults", HullType.All, 1, 4, 2, RuleBook.CoreRulebook, 205, null, "Double the time this vessel may remain at void without suffering Crew Population or Morale Loss. When making Extended Repairs repair an additional 1 Hull Integrity", morale: 1, max: 1));
            comps.Add(new Supplemental("Field Bracing", HullType.All, 0, 4, 2, RuleBook.BattlefleetKoronus, 38, null, "May exchange 1 Power for 2 Hull Integrity, up to +6 Hull Integrity. Should this component be damaged, unpowered or supplied with less power, the hull loses the bonus value proportionally, although this won't bring the Ship's Hull Integrity below 0. The amount of power supplied to this may be increased with a +0 Tech-Use Test, and may divert power from other components by making them unpowered"));
            comps.Add(new Supplemental("Fire Suppression Systems", ~HullType.AllCruiser, 1, 1, 2, RuleBook.BattlefleetKoronus, 38, null, "Once per turn, so long as the Bridge is powered and undamaged, a character may make a -10 Tech-Use Test as an extended action to extinguish one component on fire", max: 1));
            comps.Add(new Supplemental("Fire Suppression Systems", HullType.LightCruiser | HullType.Cruiser | HullType.BattleCruiser, 2, 2, 2, RuleBook.BattlefleetKoronus, 38, null, "Once per turn, so long as the Bridge is powered and undamaged, a character may make a -10 Tech-Use Test as an extended action to extinguish one component on fire", max: 1));
            comps.Add(new Supplemental("Fire Suppression Systems", HullType.GrandCruiser | HullType.BattleShip, 3, 3, 2, RuleBook.BattlefleetKoronus, 38, null, "Once per turn, so long as the Bridge is powered and undamaged, a character may make a -10 Tech-Use Test as an extended action to extinguish one component on fire", max: 1));
            comps.Add(new Supplemental("Flak Turrets", HullType.All, 1, 1, 1, RuleBook.BattlefleetKoronus, 38, null, "When in use, ship gains +1 Turret Rating but suffers -10 to Detection Rating", max: 1));
            comps.Add(new Supplemental("Ghost Field", HullType.All, 8, 4, 3, RuleBook.CoreRulebook, 207, null, "All ships firing at this vessel while this is active suffer -20 to ballistic skill tests. If the enemy is firing a lance or attempting a Hit and Run they take -30 instead to those tests. This vessel must decide if it will use its void shields or ghost shields each turn", comp: ComponentOrigin.Xenotech));
            comps.Add(new Supplemental("Gilded Hull", ~HullType.AllCruiser, 0, 1, 2, RuleBook.LureoftheExpanse, 139, null, "+10 to Fellowship tests made by the captain while on or in sight of this vessel", armour: -3));
            comps.Add(new Supplemental("Gilded Hull", HullType.AllCruiser, 0, 2, 2, RuleBook.LureoftheExpanse, 139, null, "+10 to Fellowship tests made by the captain while on or in sight of this vessel", armour: -3));
            comps.Add(new Supplemental("Grav Repulsors", HullType.All, 0, 1, 2, RuleBook.HostileAcquisition, 74, null, "External; May allocate 1 to 3 power to this component, for each 1 allocated: reduce damage from asteroids or space debris, torpedoes, bomber attacks or being rammed by 1. In the case of torpedoes it applies to each individual source and agaisnt all other sources it applies to the combined damage caused", comp: ComponentOrigin.Xenotech));
            comps.Add(new Supplemental("Graviton Flare", HullType.Raider | HullType.Frigate, 2, 0, 2, RuleBook.HostileAcquisition, 73, null, "External; When triggered: all vessels in the star system suffer -30 to Detection for 2 rounds. This component takes 24 hours to recharge", comp: ComponentOrigin.Archeotech));
            comps.Add(new Supplemental("Gravity Sails", ~HullType.AllCruiser, 3, 0, 3, RuleBook.CoreRulebook, 208, null, "External", speed: 1, man: 5, comp: ComponentOrigin.Xenotech));
            comps.Add(new Supplemental("Gravity Sails", HullType.AllCruiser, 5, 0, 3, RuleBook.CoreRulebook, 208, null, "External", speed: 1, man: 5, comp: ComponentOrigin.Xenotech));
            comps.Add(new Supplemental("Gyro-Stabalisation Matrix", HullType.All, 1, 1, 1, RuleBook.IntoTheStorm, 162, null, "Adjust Speed and Bearing, Come to New Heading and Evasive Manoeuvres are +0 tests instead of -20 or -10", comp: ComponentOrigin.Archeotech, max: 1));
            comps.Add(new Supplemental("Hydraphurian KL-247 Jamming System", HullType.All, 4, 0, 1, RuleBook.BattlefleetKoronus, 39, null, "External; While this component is active: this vessel may not perform Silent Running but any Focussed Augury Tests to scan it suffer a -20 penalty", max: 1));
            comps.Add(new Supplemental("Laboratoreum", HullType.All, 2, 1, 3, RuleBook.HostileAcquisition, 72, null, "This component grants +20 bonus to all tests to identify, analyse or repair artefacts of ancient or xenos origin, or to craft single items", max: 1));
            comps.Add(new Supplemental("Librarium Vault", HullType.All, 1, 1, 1, RuleBook.CoreRulebook, 205, null, "+10 to Investigate Skill Tests made aboard this vessel", max: 1));
            comps.Add(new Supplemental("Lux Net", HullType.All, 0, 2, 2, RuleBook.BattlefleetKoronus, 38, null, "This may only be deployed while a ship is stationary and inside a solar system. It takes 2 hours to deploy and 10 to retract. If the ship has to move during the net's operation, the net is destroyed. The Net counts as exposed when deployed. While deployed this generates 10 power and adds +1 to the number of degrees of successes on extended repairs"));
            comps.Add(new Supplemental("Luxury Passenger Quarters", HullType.All, 2, 1, 1, RuleBook.CoreRulebook, 203, morale: -3, trade: 100, criminal: 100, creed: 100));


            //End of Supplemental Components
            //Squadrons
            List<Squadron> squads = new List<Squadron>();
            squads.Add(new Squadron("Fury Interceptor", Race.Human, 10, 10, 20, "When checking for squadron losses, this squadron reduces its losses by one ot a minimum of zero or gains +5 to the upkeep test"));
            squads.Add(new Squadron("Starhawk Bomber", Race.Human, 0, 6, 10, "When checking for squadron losses, this squadron reduces its losses by one ot a minimum of zero or gains +5 to the upkeep test"));
            squads.Add(new Squadron("Shark Assault Boat", Race.Human, 5, 10, 8));
            squads.Add(new Squadron("Swiftdeath Fighter", Race.Chaos, 10, 11, 30, "When checking for squadron losses, this squadron increases its losses by 1 to the squadron maximum or suffers -5 on the Upkeep test"));
            squads.Add(new Squadron("Doomfire Bomber", Race.Chaos, 0, 7, 15, "When checking for squadron losses, this squadron increases its losses by 1 to the squadron maximum or suffers -5 on the Upkeep test"));
            squads.Add(new Squadron("Dreadclaw Assault Boat", Race.Chaos, 5, 11, 15));
            squads.Add(new Squadron("Darkstar Fighter", Race.Eldar, 15, 12, 12, "Suffers no penalties for being below half strength"));
            squads.Add(new Squadron("Eagle Bomber", Race.Eldar, 6, 9, 6, "Suffers no penalties for being below half strength"));
            squads.Add(new Squadron("Bloodflayer", Race.Rakgol, 8, 9, 15, "May be used as fighter craft or assault craft, but craft rating drops to +4 when used as fighters"));
            squads.Add(new Squadron("Fighta-bommerz", Race.Ork, 8, 8, 25, "May be used as fighter craft or bombers, but craft rating drops to +5 when used as bombers"));
            squads.Add(new Squadron("Assault Boats", Race.Ork, 8, 10, 15));
            squads.Add(new Squadron("Raptor Interceptors", Race.DarkEldar, 15, 12, 12, "Suffers no penalties for being below half strength, negate any Turret Rating bonuses that the target ship would normally receive when attempting to shoot down these craft in an incoming attack wave", RuleBook.SoulReaver, 136));
            squads.Add(new Squadron("Tormentor Bombers", Race.DarkEldar, 6, 9, 6, "Suffers no penalties for being below half strength, negate any Turret Rating bonuses that the target ship would normally receive when attempting to shoot down these craft in an incoming attack wave", RuleBook.SoulReaver, 136));
            squads.Add(new Squadron("Slavebringer Assault Boats", Race.DarkEldar, 11, 12, 5, "Suffers no penalties for being below half strength, negate any Turret Rating bonuses that the target ship would normally receive when attempting to shoot down these craft in an incoming attack wave", RuleBook.SoulReaver, 136));
            //End of Squadrons
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
