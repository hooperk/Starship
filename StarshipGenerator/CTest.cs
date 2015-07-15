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
    public class CTest
    {
        public static void Main() 
        {
            List<Component> comps = new List<Component>();
            comps.Add(new Augur("Auto-stabalised Logis-Targeter", 5, RuleBook.CoreRulebook, 207, 5, bs: 5, comp:ComponentOrigin.Archeotech));
            comps.Add(new Bridge("Armoured Bridge", HullType.CruiserPlus | HullType.LightCruiser, 3, 2, RuleBook.CoreRulebook, 200, "Ignore critical hits, damaged or unpowered on a d10 of 4+"));
            comps.Add(new CrewQuarters("Bilge Rat Quarters", HullType.CruiserPlus | HullType.LightCruiser, 2, 3, -2, RuleBook.BattlefleetKoronus, 33, "Reduce crew loss by 2 for depressurisation"));
            comps.Add(new GellarField("Belecace-pattern 90.r Gellar Field", HullType.All, 1, "-20 on warp encounters table", RuleBook.BattlefleetKoronus, 32, 0, 10));
            comps.Add(new Hull("Ambition-Class Cruiser", 5, 12, 15, 66, 17, 75, 57, HullType.Cruiser, null, RuleBook.BattlefleetKoronus, 24, 2, 1, 0, 2));
            comps.Add(new LandingBay("Jovian Pattern Escort Bay", HullType.CruiserPlus | HullType.LightCruiser, WeaponSlot.Side, 1, 4, 1, 1, RuleBook.BattlefleetKoronus, 36));
            comps.Add(new LifeSustainer("Ancient Life Sustainer", HullType.CruiserPlus | HullType.LightCruiser, 2, 2, 2, RuleBook.CoreRulebook, 206, "Reduce loss to crew population from non-combat sources by 1", sp: 2, comp: ComponentOrigin.Archeotech));
            comps.Add(new PlasmaDrive("Cypra-pattern Class 1 Drive", HullType.Transport, 30, 10, "+15 to silent running, any attempts to detect this vessel suffer -15", RuleBook.BattlefleetKoronus, 42, 2, comp: ComponentOrigin.Archeotech));
            comps.Add(new Supplemental("Arboretum", HullType.Transport | HullType.Raider | HullType.Frigate, 2, 2, 1, RuleBook.IntoTheStorm, 160, null, "Double the time a ship may spend at void before suffering crew and morale damage", crew: 2));
            comps.Add(new TorpedoTubes("Fortis Pattern Torpedo Tubes", HullType.CruiserPlus | HullType.LightCruiser, WeaponSlot.Prow | WeaponSlot.Keel, 2, 8, 3, 6, 42, RuleBook.BattlefleetKoronus, 37, special: "+2VUs Torpedo speed on turn they are launched"));
            comps.Add(new VoidShield("Castellan Shield", HullType.All, 5, 1, 1, RuleBook.IntoTheStorm, 161, "During enemy turn, may make free -10 tech use to double number of shields", sp: 2, comp: ComponentOrigin.Archeotech));
            comps.Add(new WarpDrive("Albanov 1 Warp Engine", HullType.Transport | HullType.Raider | HullType.Frigate, 10, 11, RuleBook.HostileAcquisition, 69, 1, "Double base travel time through immaterium, +20 to warp travel encounters, +10 to leave the warp"));
            comps.Add(new Weapon("Bombardment Cannons", WeaponType.Macrobattery, HullType.CruiserPlus | HullType.LightCruiser, WeaponSlot.Prow | WeaponSlot.Dorsal | WeaponSlot.Keel, 5, 3, 3, 3, new DiceRoll(1, 0, 6), 2, 4, RuleBook.BattlefleetKoronus, 34, special: "Add +1 to crits rolled, +20 to intimidate tests while ship armed with this is in orbit, may add 50 to military objectives on that planet, for planetary bombardments affects double the area, +20 damage to large enemies, +10 damage to individuals and vehicles"));
            Torpedo torp = new Torpedo(Warhead.Plasma);
            Squadron squad = new Squadron("Fury Interceptor", Race.Human, 10, 10, 20);
            using (FileStream fs = File.Create("Sample.config"))
            using (StreamWriter sw = new StreamWriter(fs))
            {
                sw.Write(@"{""Components"":[");
                for (int i = 0; i < comps.Count; i++)
                {
                    sw.Write(comps[i].ToJSON());
                    if (i < comps.Count - 1)
                        sw.Write(',');
                }
                sw.Write(@"],""Ammo"":[" + torp.ToJSON() + @"," + squad.ToJSON() + @"]}");
            }
        }
    }
}
