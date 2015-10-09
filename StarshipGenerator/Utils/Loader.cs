using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using StarshipGenerator.Components;
using StarshipGenerator.Ammo;

namespace StarshipGenerator.Utils
{
    /// <summary>
    /// Class for loading saved files or config. Also stores components loaded from config
    /// </summary>
    public class Loader
    {
        public static readonly Supplemental MainCargoHold = new Supplemental("Main Cargo Hold", HullType.Transport, 2, 0, 0, RuleBook.CoreRulebook, 203, trade: 125);
        public static readonly LandingBay EscortBay = new LandingBay("Jovian-Pattern Escort Bay", HullType.LightCruiser | HullType.CruiserPlus, WeaponSlot.Side, 1, 0, 0, 1, RuleBook.BattlefleetKoronus, 36);
        public static readonly LandingBay LandingBay = new LandingBay("Jovian-Pattern Landing Bay", HullType.CruiserPlus, WeaponSlot.Side, 1, 0, 0, 2, RuleBook.BattlefleetKoronus, 36);
        public static readonly TorpedoTubes VossTubes = new TorpedoTubes("Voss-Pattern Torpedo Tubes", HullType.All, 1, 0, 0, 2, 6, RuleBook.BattlefleetKoronus, 37);
        public static readonly NovaCannon MarsCannon = new NovaCannon("Mars-Pattern Nova Cannon", HullType.CruiserPlus, 4, 0, 0, new DiceRoll(0, 2, 5), 36, RuleBook.BattlefleetKoronus, 36);
        public static readonly Supplemental ArmouredProw = new Supplemental("Armoured Prow", HullType.CruiserPlus, 0, 0, 0, RuleBook.CoreRulebook, 204, new DiceRoll(1, 0, 0), "Cannot take macrobatteries or lance in prow", prowArmour: 4, max: 1);
        public static readonly Supplemental PlasmaRefinery = new Supplemental("Plasma Refinery", HullType.Transport, 10, 0, 0, RuleBook.BattlefleetKoronus, 30, special: "May spend 3 days and make 2 +10 pilot tests to harvest plasma granting +100 to objectives if fuel is used or sold. Failure on either test by 5 or more degrees destroyes the ship. If the ship does not harvest plasma for a year, reduce maximum power by 10");
        public static readonly LandingBay HoldLandingBay = new LandingBay("Hold Landing Bay", HullType.Transport, WeaponSlot.Auxiliary, 0, 0, 0, 2, RuleBook.BattlefleetKoronus, 36, special: "Attack Craft launched from this reduce their movement by 2VUs on the turn they launch. While in combat, squadron attempting to land must make a +10 Piloting + craft rating test to land safely. If this test is failed by 4 or more degrees the component is considered damaged. it takes half an hour to land outside of combat");
        //Components
        public List<Hull> Hulls;
        public List<PlasmaDrive> PlasmaDrives;
        public List<WarpDrive> WarpDrives;
        public List<GellarField> GellarFields;
        public List<VoidShield> VoidShields;
        public List<Bridge> Bridges;
        public List<LifeSustainer> LifeSustainers;
        public List<CrewQuarters> CrewQuarters;
        public List<Augur> AugurArrays;
        public List<Weapon> Weapons;
        public List<Supplemental> Supplementals;
        public List<Squadron> Squadrons;
        //end components

        /// <summary>
        /// Select which loader to use based on extension
        /// </summary>
        /// <param name="path">Path to the starship to open</param>
        /// <returns>The loaded Starship</returns>
        public Starship LoadStarship(String path)
        {
            if (Path.GetExtension(path).Equals(".sss"))
                return LoadSSS(path);
            else if (Path.GetExtension(path).Equals(".lss"))
                return LoadLSS(path);
            return null;
        }

        public Starship LoadLSS(String path)
        {
            using (FileStream fs = File.OpenRead(path))
            {
                return LoadLSS(fs);
            }
        }

        public Starship LoadLSS(FileStream fs)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Load a starship form the old .sss format
        /// </summary>
        /// <param name="path">Path to .sss file to import</param>
        /// <returns>Starship imported from old format</returns>
        /// <exception cref="FormatException">If string values stored in numeric fields</exception>
        /// <exception cref="ArgumentException">If non-standard classes listed</exception>
        public Starship LoadSSS(String path)
        {
            using (FileStream fs = File.OpenRead(path))
            {
                return LoadSSS(fs);
            }
        }

        /// <summary>
        /// Load a starship form the old .sss format
        /// </summary>
        /// <param name="fs">Filestream open to active .sss file</param>
        /// <returns>Starship imported from old format</returns>
        /// <exception cref="FormatException">If string values stored in numeric fields</exception>
        /// <exception cref="ArgumentException">If non-standard classes listed</exception>
        public Starship LoadSSS(FileStream fs)
        {
            Dictionary<String, String> file = new Dictionary<string, string>();
            using (StreamReader sr = new StreamReader(fs))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    int pos = line.IndexOf(":");
                    if (pos > 0)
                    {
                        file.Add(line.Substring(0, pos), line.Substring(pos + 1, line.Length - pos - 1));
                    }
                }
            }
            Starship ship = new Starship();
            if (!(String.IsNullOrWhiteSpace(file["customhullname"]) || String.IsNullOrWhiteSpace(file["customhullspeed"]) || String.IsNullOrWhiteSpace(file["customhullman"])
                || String.IsNullOrWhiteSpace(file["customhulldet"]) || String.IsNullOrWhiteSpace(file["customhullint"]) || String.IsNullOrWhiteSpace(file["customhullarmour"])
                || String.IsNullOrWhiteSpace(file["customhullturret"]) || String.IsNullOrWhiteSpace(file["customhullspace"]) || String.IsNullOrWhiteSpace(file["customhullclass"])))
            {
                int sp = 0;
                if (!String.IsNullOrWhiteSpace(file["customhullsp"]))
                    sp = int.Parse(file["customhullsp"]);
                HullType type = HullType.None;
                switch (file["customhullclass"])
                {
                    case "Battle":
                        type = HullType.BattleCruiser;
                        break;
                    case "Grand":
                        type = HullType.GrandCruiser;
                        break;
                    default:
                        type = (HullType)Enum.Parse(typeof(HullType), file["customhullclass"]);
                        break;
                }
                int prow = 0;
                int port = 0;
                int starboard = 0;
                int aft = 0;
                int dorsal = 0;
                int keel = 0;
                for (int i = 1; i <= 6; i++)
                {
                    switch (file["customslot" + i])
                    {
                        case "Prow":
                            prow++;
                            break;
                        case "Port":
                            port++;
                            break;
                        case "Starboard":
                            starboard++;
                            break;
                        case "Dorsal":
                            dorsal++;
                            break;
                        case "Keel":
                            keel++;
                            break;
                        case "Aft":
                            aft++;
                            break;
                    }
                }
                Hulls.Add(new Hull(file["customhullname"], int.Parse(file["customhullspeed"]), int.Parse(file["customhullman"]), int.Parse(file["customhulldet"]), int.Parse(file["customhullint"]),
                    int.Parse(file["customhullarmour"]), int.Parse(file["customhullspace"]), sp, type, file["customhullspecial"], RuleBook.Custom, 0, int.Parse(file["customhullturret"]), prow, dorsal,
                    (port + starboard) / 2, keel, aft));

            }
            ship.Hull = Hulls.Where(x => x.Name.Equals(file["hull"], StringComparison.OrdinalIgnoreCase)).FirstOrDefault();
            HullType shipClass = HullType.None;
            if (ship.Hull != null)
                shipClass = ship.Hull.HullTypes;
            //now that ship class and hopefully hull is set, can add custom weapons, then other components custom components can have their hulltypes == shipClass
            for (int i = 1; i <= 6; i++)
            {
                //add each custom weapon - check for duplication
                if (!(String.IsNullOrWhiteSpace(file["customweapon" + i]) || String.IsNullOrWhiteSpace(file["customweapon" + i + "type"]) || String.IsNullOrWhiteSpace(file["customweapon" + i + "range"])))
                {
                    String name = file["customweapon" + i];
                    WeaponType type = WeaponType.Macrobattery;
                    switch (file["customweapon" + i + "type"])
                    {
                        case "Lance":
                            type = WeaponType.Lance;
                            break;
                        case "Landing Bays":
                            type = WeaponType.LandingBay;
                            break;
                        case "Torpedo Tubes":
                            type = WeaponType.TorpedoTube;
                            break;
                        case "Nova Cannon":
                            type = WeaponType.NovaCannon;
                            break;
                    }
                    WeaponSlot slots = WeaponSlot.Auxiliary;//just to be very certain not to have uninitialised
                    if (!String.IsNullOrWhiteSpace(file["customslot" + i]))
                    {
                        slots = (WeaponSlot)Enum.Parse(typeof(WeaponSlot), file["customslot" + i]);
                    }
                    else
                    {
                        switch (type)
                        {
                            case WeaponType.Macrobattery:
                                if (name.IndexOf("Broadside", StringComparison.OrdinalIgnoreCase) >= 0)//ignorecase search for broadside to apply broadside rules
                                    slots = WeaponSlot.Side;
                                else
                                    slots = WeaponSlot.All;
                                break;
                            case WeaponType.Lance:
                                slots = WeaponSlot.Lance;
                                break;
                            case WeaponType.LandingBay:
                                slots = WeaponSlot.Side;
                                break;
                            //case WeaponType.NovaCannon:
                            //    slots = WeaponSlot.Prow;
                            //    break;
                            //case WeaponType.TorpedoTube:
                            //    slots = WeaponSlot.Prow | WeaponSlot.Keel;
                            //    break;
                        }
                    }
                    int str = 0;
                    if (!String.IsNullOrWhiteSpace(file["customweapon" + i + "str"]))
                        str = int.Parse(file["customweapon" + i + "str"]);
                    DiceRoll damage = new DiceRoll(file["customweapon" + i + "dice"]);
                    if (!String.IsNullOrWhiteSpace(file["customweapon" + i + "damage"]))
                        damage += int.Parse(file["customweapon" + i + "damage"]);
                    int range = int.Parse(file["customweapon" + i + "range"]);
                    int crit = 0;
                    if (!String.IsNullOrWhiteSpace(file["customweapon" + i + "crit"]))
                        crit = int.Parse(file["customweapon" + i + "crit"]);
                    int space = 0;
                    if (!String.IsNullOrWhiteSpace(file["customweapon" + i + "space"]))
                        space = int.Parse(file["customweapon" + i + "space"]);
                    int sp = 0;
                    if (!String.IsNullOrWhiteSpace(file["customweapon" + i + "sp"]))
                        sp = int.Parse(file["customweapon" + i + "sp"]);
                    int power = 0;
                    if (!String.IsNullOrWhiteSpace(file["customweapon" + i + "power"]))
                        power = int.Parse(file["customweapon" + i + "power"]);
                    string special = file["customweapon" + i + "special"];
                    HullType weaponClass = HullType.All;
                    if (ship.Hull != null)
                        weaponClass = ship.Hull.HullTypes;
                    switch (type)
                    {
                        case WeaponType.TorpedoTube:
                            Weapons.Add(new TorpedoTubes(name, weaponClass, power, space, sp, str, 0, RuleBook.Custom, 0, special: special));
                            break;
                        case WeaponType.NovaCannon:
                            Weapons.Add(new NovaCannon(name, weaponClass, power, space, sp, damage, range, RuleBook.Custom, 0, special));
                            break;
                        case WeaponType.LandingBay:
                            Weapons.Add(new LandingBay(name, weaponClass, slots, power, space, sp, str, RuleBook.Custom, 0, special: special));
                            break;
                        default:
                            Weapons.Add(new Weapon(name, type, weaponClass, slots, power, space, sp, str, damage, crit, range, RuleBook.Custom, 0, special: special));
                            break;
                    }
                }
            }
            int length = ship.Weapons.Length;
            for (int i = 0; i < length; i++)
            {
                //add each weapon
                string name = file["weapon" + (length - i)];
                switch (name)
                {
                    case Old.BombardmentCannons:
                        name = Names.BombardmentCannons;
                        break;
                    case Old.JovianMissiles:
                        name = Names.JovianMissiles;
                        break;
                    case Old.LatheGravCulverin:
                        name = Names.LatheGravCulverin;
                        break;
                    case Old.MarsBroadsides:
                        name = Names.MarsBroadsides;
                        break;
                    case Old.MarsMacrocannons:
                        name = Names.MarsMacrocannons;
                        break;
                    case Old.MezoaLanceBattery:
                        name = Names.MezoaLanceBattery;
                        break;
                    case Old.MezoaLance:
                        name = Names.MezoaLance;
                        break;
                    case Old.RyzaPlasma:
                        name = Names.RyzaPlasma;
                        break;
                }
                Weapon weapon = Weapons.Where(x => x.Name.Equals(name, StringComparison.OrdinalIgnoreCase)).FirstOrDefault();
                if (weapon != null)
                {
                    Quality quality = Quality.Common;
                    if (!String.IsNullOrWhiteSpace(file["weapon" + (length - i) + "quality"]))
                        quality = (Quality)Enum.Parse(typeof(Quality), file["weapon" + (length - i) + "quality"]);
                    WeaponQuality wq = WeaponQuality.None;
                    //wxq1 & wxq2 for WeaponQuality, weapxmod for turbo
                    for (int j = 1; j <= 2; j++)
                    {
                        switch (file["w" + (length - i) + "q" + j])
                        {
                            case "Space":
                                wq |= WeaponQuality.Space;
                                break;
                            case "Range":
                                wq |= WeaponQuality.Range;
                                break;
                            case "Crit Rating":
                                wq |= WeaponQuality.Crit;
                                break;
                            case "Strength":
                                wq |= WeaponQuality.Strength;
                                break;
                            case "Damage":
                                wq |= WeaponQuality.Damage;
                                break;
                        }
                    }
                    Quality turbo = Quality.None;
                    if (!String.IsNullOrWhiteSpace(file["weap" + (length - i) + "mod"]))
                        turbo = (Quality)Enum.Parse(typeof(Quality), file["weap" + (length - i) + "mod"]);
                    if (quality != Quality.Common || turbo != Quality.None)
                        ship.Weapons[i] = new Weapon(weapon.Name, weapon.Type, weapon.HullTypes, weapon.Slots, weapon.RawPower, weapon.RawSpace, weapon.RawSP, weapon.RawStrength, weapon.RawDamage, weapon.RawCrit, weapon.RawRange,
                            weapon.Origin, weapon.PageNumber, quality, wq, weapon.RawSpecial, turbo, weapon.ComponentOrigin);
                    else
                        ship.Weapons[i] = weapon;
                }
            }
            if (!(String.IsNullOrWhiteSpace(file["customplasmaname"]) || String.IsNullOrWhiteSpace(file["customplasmapower"]) || String.IsNullOrWhiteSpace(file["customplasmaspace"])))
            {
                string name = file["customplasmaname"];
                int power = int.Parse(file["customplasmapower"]);
                int space = int.Parse(file["customplasmaspace"]);
                int sp = 0;
                if (!String.IsNullOrWhiteSpace(file["customplasmasp"]))
                {
                    sp = int.Parse(file["customplasmasp"]);
                }
                string special = file["customplasmaspecial"];
                PlasmaDrives.Add(new PlasmaDrive(name, shipClass, power, space, special, RuleBook.Custom, 0, sp));
            }
            if (!(String.IsNullOrWhiteSpace(file["customwarpname"]) || String.IsNullOrWhiteSpace(file["customwarppower"]) || String.IsNullOrWhiteSpace(file["customwarpspace"])))
            {
                string name = file["customwarpname"];
                int power = int.Parse(file["customwarppower"]);
                int space = int.Parse(file["customwarpspace"]);
                int sp = 0;
                if (!String.IsNullOrWhiteSpace(file["customwarpsp"]))
                {
                    sp = int.Parse(file["customwarpsp"]);
                }
                string special = file["customwarpspecial"];
                WarpDrives.Add(new WarpDrive(name, shipClass, power, space, RuleBook.Custom, 0, sp, special));
            }
            if (!(String.IsNullOrWhiteSpace(file["customgellarname"]) || String.IsNullOrWhiteSpace(file["customgellarpower"])))
            {
                string name = file["customgellarname"];
                int power = int.Parse(file["customgellarpower"]);
                int sp = 0;
                if (!String.IsNullOrWhiteSpace(file["customgellarsp"]))
                {
                    sp = int.Parse(file["customgellarsp"]);
                }
                string special = file["customgellarspecial"];
                GellarFields.Add(new GellarField(name, shipClass, power, special, RuleBook.Custom, 0, sp));
            }
            bool shieldsDone = false;//if a custom shield was used, don't count the custom shields twice
            if (!(String.IsNullOrWhiteSpace(file["customvoidname"]) || String.IsNullOrWhiteSpace(file["customvoidpower"])
                || String.IsNullOrWhiteSpace(file["customvoidspace"])))
            {
                string name = file["customvoidname"];
                int power = int.Parse(file["customvoidpower"]);
                int space = int.Parse(file["customvoidspace"]);
                int sp = 0;
                int str = 0;
                if(!String.IsNullOrWhiteSpace(file["customshield"]))
                    int.Parse(file["customshield"]);
                if (!String.IsNullOrWhiteSpace(file["customvoidsp"]))
                {
                    sp = int.Parse(file["customvoidsp"]);
                }
                string special = file["customvoidspecial"];
                VoidShields.Add(new VoidShield(name, shipClass, power, space, str, RuleBook.Custom, 0, special, sp: sp));
                shieldsDone = true;
            }
            if (!(String.IsNullOrWhiteSpace(file["custombridgename"]) || String.IsNullOrWhiteSpace(file["custombridgepower"]) || String.IsNullOrWhiteSpace(file["custombridgespace"])))
            {
                string name = file["custombridgename"];
                int power = int.Parse(file["custombridgepower"]);
                int space = int.Parse(file["custombridgespace"]);
                int sp = 0;
                if (!String.IsNullOrWhiteSpace(file["custombridgesp"]))
                {
                    sp = int.Parse(file["custombridgesp"]);
                }
                string special = file["custombridgespecial"];
                Bridges.Add(new Bridge(name, shipClass, power, space, RuleBook.Custom, 0, special, sp));
            }
            if (!(String.IsNullOrWhiteSpace(file["customlifename"]) || String.IsNullOrWhiteSpace(file["customlifepower"]) || String.IsNullOrWhiteSpace(file["customlifespace"])))
            {
                string name = file["customlifename"];
                int power = int.Parse(file["customlifepower"]);
                int space = int.Parse(file["customlifespace"]);
                int sp = 0;
                if (!String.IsNullOrWhiteSpace(file["customlifesp"]))
                {
                    sp = int.Parse(file["customlifesp"]);
                }
                string special = file["customlifespecial"];
                LifeSustainers.Add(new LifeSustainer(name, shipClass, power, space, 0, RuleBook.Custom, 0, special, sp: sp));
            }
            if (!(String.IsNullOrWhiteSpace(file["customcrewname"]) || String.IsNullOrWhiteSpace(file["customcrewpower"]) || String.IsNullOrWhiteSpace(file["customcrewspace"])))
            {
                string name = file["customcrewname"];
                int power = int.Parse(file["customcrewpower"]);
                int space = int.Parse(file["customcrewspace"]);
                int sp = 0;
                if (!String.IsNullOrWhiteSpace(file["customcrewsp"]))
                {
                    sp = int.Parse(file["customcrewsp"]);
                }
                string special = file["customcrewspecial"];
                CrewQuarters.Add(new CrewQuarters(name, shipClass, power, space, 0, RuleBook.Custom, 0, special, sp: sp));
            }
            if (!(String.IsNullOrWhiteSpace(file["customaugurname"]) || String.IsNullOrWhiteSpace(file["customaugurpower"])))
            {
                string name = file["customaugurname"];
                int power = int.Parse(file["customaugurpower"]);
                int sp = 0;
                if (!String.IsNullOrWhiteSpace(file["customaugursp"]))
                {
                    sp = int.Parse(file["customaugursp"]);
                }
                string special = file["customaugurspecial"];
                AugurArrays.Add(new Augur(name, power, RuleBook.Custom, 0, special: special, sp: sp));
            }
            if (!String.IsNullOrWhiteSpace(file["custommachine"]))
                ship.GMMachineSpirit = file["custommachine"];
            if (!String.IsNullOrWhiteSpace(file["customhistory"]))
                ship.GMShipHistory = file["customhistory"];
            if (!String.IsNullOrWhiteSpace(file["customspeed"]))
                ship.GMSpeed = int.Parse(file["customspeed"]);
            if (!String.IsNullOrWhiteSpace(file["customint"]))
                ship.GMHullIntegrity = int.Parse(file["customint"]);
            if (!String.IsNullOrWhiteSpace(file["customdet"]))
                ship.GMDetection = int.Parse(file["customdet"]);
            if (!String.IsNullOrWhiteSpace(file["customman"]))
                ship.GMManoeuvrability = int.Parse(file["customman"]);
            if (!String.IsNullOrWhiteSpace(file["customarmour"]))
                ship.GMArmour = int.Parse(file["customarmour"]);
            if (!String.IsNullOrWhiteSpace(file["customturret"]))
                ship.GMTurretRating = int.Parse(file["customturret"]);
            if (!String.IsNullOrWhiteSpace(file["custommorale"]))
                ship.GMMorale = int.Parse(file["custommorale"]);
            if (!String.IsNullOrWhiteSpace(file["customcrew"]))
                ship.GMCrewPopulation = int.Parse(file["customcrew"]);
            if (!(shieldsDone || String.IsNullOrWhiteSpace(file["customshield"])))
                ship.GMShields = int.Parse(file["customshield"]);
            ship.GMSpecial = file["customspecial"];
            //custom components as one blob
            if (!(String.IsNullOrWhiteSpace(file["customcomppower"]) && String.IsNullOrWhiteSpace(file["customcompgenerate"])))
            {
                bool doBoth = !(String.IsNullOrWhiteSpace(file["customcomppower"]) || String.IsNullOrWhiteSpace(file["customcompgenerate"]));//if both present, separate comps for generate
                if (doBoth)
                    ship.SupplementalComponents.Add(new Supplemental("Custom Generators", ship.Hull.HullTypes, int.Parse(file["customcompgenerate"]), 0, 0, RuleBook.Custom, 0, generated: true));
                bool usingPower = doBoth || !String.IsNullOrWhiteSpace(file["customcompgenerate"]);
                int space = 0;
                if (!String.IsNullOrWhiteSpace(file["customcompspace"]))
                    space = int.Parse(file["customcompspace"]);
                int sp = 0;
                if (!String.IsNullOrWhiteSpace(file["customcompsp"]))
                    sp = int.Parse(file["customcompsp"]);
                ship.SupplementalComponents.Add(new Supplemental("Custom Components", ship.Hull.HullTypes, (usingPower ? int.Parse(file["customcomppower"]) : int.Parse(file["customcompgenerate"])), space, sp, RuleBook.Custom, 0, special: file["customcomponents"], generated: !usingPower));//account for power being used or generated, all added as one blob to be shown in special field
            }
            //essential components
            {//Plasmadrive
                string name = file["plasma"];
                PlasmaDrive plasma = null;
                HullType size = HullType.None;
                bool modified = false;
                name = name.Replace(" Pattern", "-Pattern");
                if (name.Equals(Old.SprintTrader))
                    name = Names.SprintTrader;
                if (name.Equals(Old.EscortDrive))
                    name = Names.EscortDrive;
                if (name.StartsWith("Modified "))
                {
                    modified = true;
                    name = name.Substring(9);
                }
                if (name.Equals(Old.Viperdrive))
                    name = Names.Viperdrive;
                if (name.StartsWith(Names.WarcruiserDrive))
                {
                    if (name.EndsWith("Large"))
                        size = HullType.CruiserPlus;
                    else
                        size = HullType.LightCruiser;
                    plasma = PlasmaDrives.Where(x => x.Name.Equals(Names.WarcruiserDrive) && x.HullTypes == size).FirstOrDefault();
                }
                else if (name.StartsWith(Names.MimicDrive))
                {
                    switch (name.Substring(Names.MimicDrive.Length))
                    {
                        case "Huge":
                            size = HullType.CruiserPlus;
                            break;
                        case "Large":
                            size = HullType.LightCruiser;
                            break;
                        case "Medium":
                            size = HullType.Raider | HullType.Frigate;
                            break;
                        case "Small":
                            size = HullType.Transport;
                            break;
                    }
                    plasma = PlasmaDrives.Where(x => x.Name.Equals(Names.MimicDrive) && x.HullTypes == size).FirstOrDefault();
                }
                else
                {
                    plasma = PlasmaDrives.Where(x => x.Name.Equals(name, StringComparison.OrdinalIgnoreCase)).FirstOrDefault();
                }
                if (plasma != null)
                {
                    Quality quality = Quality.Common;
                    if (!String.IsNullOrEmpty(file["plasmaquality"]))
                    {
                        quality = (Quality)Enum.Parse(typeof(Quality), file["plasmaquality"]);
                        if (quality == Quality.Good)
                        {
                            switch (file["plasmachoice"])
                            {
                                case "Power":
                                    quality = Quality.Efficient;
                                    break;
                                case "Space":
                                    quality = Quality.Slim;
                                    break;
                            }
                        }
                    }
                    ship.PlasmaDrive = new PlasmaDrive(plasma.RawName, plasma.HullTypes, plasma.RawPower, plasma.RawSpace, plasma.RawSpecial, plasma.Origin, plasma.PageNumber, plasma.RawSP, quality, plasma.Speed, plasma.Manoeuvrability, plasma.ComponentOrigin, modified);
                }//Warp Drive
                WarpDrive warp = WarpDrives.Where(x => x.Name.Equals(file["warp"], StringComparison.OrdinalIgnoreCase)).FirstOrDefault();
                if (warp != null)
                {
                    Quality quality = Quality.Common;
                    if (!String.IsNullOrEmpty(file["warpquality"]))
                    {
                        quality = (Quality)Enum.Parse(typeof(Quality), file["warpquality"]);
                        if (quality == Quality.Good)
                        {
                            switch (file["warpchoice"])
                            {
                                case "Power":
                                    quality = Quality.Efficient;
                                    break;
                                case "Space":
                                    quality = Quality.Slim;
                                    break;
                            }
                        }
                    }
                    ship.WarpDrive = new WarpDrive(warp.Name, warp.HullTypes, warp.RawPower, warp.RawSpace, warp.Origin, warp.PageNumber, warp.RawSP, warp.RawSpecial, quality, warp.ComponentOrigin);
                }//Gellar Field
                GellarField gellar = GellarFields.Where(x => x.Name.Equals(file["gellar"], StringComparison.OrdinalIgnoreCase)).FirstOrDefault();
                if (gellar != null)
                {
                    Quality quality = Quality.Common;
                    if (!String.IsNullOrEmpty(file["gellarquality"]))
                    {
                        quality = (Quality)Enum.Parse(typeof(Quality), file["gellarquality"]);
                    }
                    ship.GellarField = new GellarField(gellar.Name, gellar.HullTypes, gellar.RawPower, gellar.RawSpecial, gellar.Origin, gellar.PageNumber, gellar.RawSP, gellar.NavigateWarp, quality, gellar.ComponentOrigin);
                }//Void shield
                name = file["void"];
                if (name.Equals(Old.RepulsorShield))
                    name = Names.RepulsorShield;
                VoidShield shield = VoidShields.Where(x => x.Name.Equals(name, StringComparison.OrdinalIgnoreCase)).FirstOrDefault();
                if (shield != null)
                {
                    Quality quality = Quality.Common;
                    if (!String.IsNullOrEmpty(file["voidquality"]))
                    {
                        quality = (Quality)Enum.Parse(typeof(Quality), file["voidquality"]);
                        if (quality == Quality.Good)
                        {
                            switch (file["voidchoice"])
                            {
                                case "Power":
                                    quality = Quality.Efficient;
                                    break;
                                case "Space":
                                    quality = Quality.Slim;
                                    break;
                            }
                        }
                    }
                    ship.VoidShield = new VoidShield(shield.Name, shield.HullTypes, shield.RawPower, shield.RawSpace, shield.Strength, shield.Origin, shield.PageNumber, shield.RawSpecial, quality, shield.RawSP, shield.ComponentOrigin);
                }//Ship's Bridge
                name = file["bridge"];
                size = HullType.All;
                if (name.EndsWith(", Large"))
                {
                    size = HullType.AllCruiser;
                }
                else if (name.EndsWith(", Small"))
                {
                    size = ~HullType.AllCruiser;
                }
                Bridge bridge = Bridges.Where(x => name.StartsWith(x.Name, StringComparison.OrdinalIgnoreCase) && (x.HullTypes & size) != 0).FirstOrDefault();
                if (bridge != null)
                {
                    Quality quality = Quality.Common;
                    if (!String.IsNullOrEmpty(file["bridgequality"]))
                    {
                        quality = (Quality)Enum.Parse(typeof(Quality), file["bridgequality"]);
                        if (quality == Quality.Good)
                        {
                            switch (file["bridgechoice"])
                            {
                                case "Power":
                                    quality = Quality.Efficient;
                                    break;
                                case "Space":
                                    quality = Quality.Slim;
                                    break;
                            }
                        }
                    }
                    ship.ShipBridge = new Bridge(bridge.Name, bridge.HullTypes, bridge.RawPower, bridge.RawSpace, bridge.Origin, bridge.PageNumber, bridge.RawSpecial, bridge.RawSP, quality, bridge.Manoeuvrability, bridge.BSModifier, bridge.Command, bridge.Repair, bridge.Pilot, bridge.NavigateWarp, bridge.ComponentOrigin, bridge.MiningObjective, bridge.CreedObjective, bridge.MilitaryObjective, bridge.TradeObjective, bridge.CriminalObjective, bridge.ExplorationObjective);
                }//Life Sustainer
                name = file["life"];
                size = HullType.All;
                name = name.Replace("Vitae ", "Vitae-");
                if (name.EndsWith(", Large"))
                {
                    size = HullType.AllCruiser;
                }
                else if (name.EndsWith(", Small"))
                {
                    size = ~HullType.AllCruiser;
                }
                LifeSustainer sustainer = LifeSustainers.Where(x => name.StartsWith(x.Name, StringComparison.OrdinalIgnoreCase) && (x.HullTypes & size) != 0).FirstOrDefault();
                if (sustainer != null)
                {
                    Quality quality = Quality.Common;
                    if (!String.IsNullOrEmpty(file["lifequality"]))
                    {
                        quality = (Quality)Enum.Parse(typeof(Quality), file["lifequality"]);
                        if (quality == Quality.Good)
                        {
                            switch (file["lifechoice"])
                            {
                                case "Power":
                                    quality = Quality.Efficient;
                                    break;
                                case "Space":
                                    quality = Quality.Slim;
                                    break;
                            }
                        }
                    }
                    ship.LifeSustainer = new LifeSustainer(sustainer.Name, sustainer.HullTypes, sustainer.RawPower, sustainer.RawSpace, sustainer.Morale, sustainer.Origin, sustainer.PageNumber, sustainer.RawSpecial, quality, sustainer.RawSP, sustainer.MoraleLoss, sustainer.CrewLoss, sustainer.ComponentOrigin);
                }//crew quarters
                name = file["quarters"];
                size = HullType.All;
                if (name.EndsWith(", Large"))
                {
                    size = HullType.AllCruiser;
                }
                else if (name.EndsWith(", Small"))
                {
                    size = ~HullType.AllCruiser;
                }
                CrewQuarters quarters = CrewQuarters.Where(x => name.StartsWith(x.Name, StringComparison.OrdinalIgnoreCase) && (x.HullTypes & size) != 0).FirstOrDefault();
                if (quarters != null)
                {
                    Quality quality = Quality.Common;
                    if (!String.IsNullOrEmpty(file["quartersquality"]))
                    {
                        quality = (Quality)Enum.Parse(typeof(Quality), file["quartersquality"]);
                        if (quality == Quality.Good)
                        {
                            switch (file["quarterschoice"])
                            {
                                case "Power":
                                    quality = Quality.Efficient;
                                    break;
                                case "Space":
                                    quality = Quality.Slim;
                                    break;
                            }
                        }
                    }
                    ship.CrewQuarters = new CrewQuarters(quarters.Name, quarters.HullTypes, quarters.RawPower, quarters.RawSpace, quarters.Morale, quarters.Origin, quarters.PageNumber, quarters.RawSpecial, quality, quarters.RawSP, quarters.MoraleLoss, quarters.ComponentOrigin);
                }//Augur Arrays
                Augur arrays = AugurArrays.Where(x => x.Name.Equals(file["augur"], StringComparison.OrdinalIgnoreCase)).FirstOrDefault();
                if (arrays != null)
                {
                    Quality quality = Quality.Common;
                    if (!String.IsNullOrEmpty(file["augurquality"]))
                    {
                        quality = (Quality)Enum.Parse(typeof(Quality), file["augurquality"]);
                    }
                    ship.AugurArrays = new Augur(arrays.Name, arrays.RawPower, arrays.Origin, arrays.PageNumber, arrays.DetectionRating, arrays.RawSpecial, quality, arrays.RawSP, arrays.Manoeuvrability, arrays.BSModifier, arrays.MiningObjective, arrays.CreedObjective, arrays.MilitaryObjective, arrays.TradeObjective, arrays.CriminalObjective, arrays.ExplorationObjective, arrays.ComponentOrigin);
                }
            }//end of essential components
            switch (file["machine"])
            {
                case "A Nose For Trouble":
                    ship.MachineSpirit = MachineSpirit.ANoseForTrouble;
                    break;
                case "Blasphemous Tendencies":
                    ship.MachineSpirit = MachineSpirit.BlasphemousTendencies;
                    break;
                case "Martial Hubris":
                    ship.MachineSpirit = MachineSpirit.MartialHubris;
                    break;
                case "Rebellious":
                    ship.MachineSpirit = MachineSpirit.Rebellious;
                    break;
                case "Stoic":
                    ship.MachineSpirit = MachineSpirit.Stoic;
                    break;
                case "Skittish":
                    ship.MachineSpirit = MachineSpirit.Skittish;
                    break;
                case "Wrothful":
                    ship.MachineSpirit = MachineSpirit.Wrothful;
                    break;
                case "Resolute":
                    ship.MachineSpirit = MachineSpirit.Resolute;
                    break;
                case "Adventurous":
                    ship.MachineSpirit = MachineSpirit.Adventurous;
                    break;
                case "Ancient and Wise":
                    ship.MachineSpirit = MachineSpirit.AncientAndWise;
                    break;
            }
            switch (file["history"])
            {
                case "Reliquary of Mars":
                    ship.ShipHistory = ShipHistory.ReliquaryOfMars;
                    break;
                case "Haunted":
                    ship.ShipHistory = ShipHistory.Haunted;
                    break;
                case "Emissary of the Imperator":
                    ship.ShipHistory = ShipHistory.EmissaryOfTheImperator;
                    break;
                case "Wolf in Sheeps Clothing":
                    ship.ShipHistory = ShipHistory.WolfInSheepsClothing;
                    break;
                case "Turbulent Past":
                    ship.ShipHistory = ShipHistory.TurbulentPast;
                    break;
                case "Death Cult":
                    ship.ShipHistory = ShipHistory.DeathCult;
                    break;
                case "Wrested from a Space Hulk":
                    ship.ShipHistory = ShipHistory.WrestedFromASpaceHulk;
                    break;
                case "Temperamental Warp Engine":
                    ship.ShipHistory = ShipHistory.TemperamentalWarpEngine;
                    break;
                case "Finances in Arrears":
                    ship.ShipHistory = ShipHistory.FinancesInArrears;
                    break;
                case "Xenophilous":
                    ship.ShipHistory = ShipHistory.Xenophilous;
                    break;
            }
            if (!String.IsNullOrWhiteSpace(file["crew"]))
                ship.CrewRace = (Race)Enum.Parse(typeof(Race), file["crew"]);
            if (!String.IsNullOrWhiteSpace(file["crewrating"]))
            {
                CrewRating rating = 0;
                if (Enum.TryParse<CrewRating>(file["crewrating"], out rating))
                    ship.CrewRating = (int)rating;
                else
                    ship.CrewRating = (int)Enum.Parse(typeof(ServitorQuality), file["crewrating"]);
            }
            if (!String.IsNullOrWhiteSpace(file["crewmod"]))
                ship.GMCrewRating = int.Parse(file["crewmod"]);
            if (!String.IsNullOrWhiteSpace(file["arrestor"]))
                ship.ArresterEngines = (Quality)Enum.Parse(typeof(Quality), file["arrestor"]);
            if (!String.IsNullOrWhiteSpace(file["cherubim"]))
                ship.CherubimAerie = (Quality)Enum.Parse(typeof(Quality), file["cherubim"]);
            if (!String.IsNullOrWhiteSpace(file["improvements"]))
                ship.CrewImprovements = (Quality)Enum.Parse(typeof(Quality), file["imperovements"]);
            if (!String.IsNullOrWhiteSpace(file["disciplinarium"]))
                ship.Disciplinarium = (Quality)Enum.Parse(typeof(Quality), file["disciplinarium"]);
            if (!String.IsNullOrWhiteSpace(file["distribute"]))
                ship.DistributedCargoHold = (Quality)Enum.Parse(typeof(Quality), file["distributed"]);
            if (!String.IsNullOrWhiteSpace(file["mimic"]))
                ship.MimicDrive = (Quality)Enum.Parse(typeof(Quality), file["mimic"]);
            if (!String.IsNullOrWhiteSpace(file["ostentatious"]))
                ship.OstentatiousDisplayOfWealth = (Quality)Enum.Parse(typeof(Quality), file["ostentatious"]);
            if (!String.IsNullOrWhiteSpace(file["overload"]))
                ship.OverloadShieldCapacitors = (Quality)Enum.Parse(typeof(Quality), file["overload"]);
            if (!String.IsNullOrWhiteSpace(file["resolution"]))
                ship.ResolutionArena = (Quality)Enum.Parse(typeof(Quality), file["resolution"]);
            if (!String.IsNullOrWhiteSpace(file["secondary"]))
                ship.SecondaryReactor = (Quality)Enum.Parse(typeof(Quality), file["secondary"]);
            if (!String.IsNullOrWhiteSpace(file["starchart"]))
                ship.StarchartCollection = (Quality)Enum.Parse(typeof(Quality), file["starchart"]);
            if (!String.IsNullOrWhiteSpace(file["trooper"]))
                ship.StormTrooperDetachment = (Quality)Enum.Parse(typeof(Quality), file["trooper"]);
            if (!String.IsNullOrWhiteSpace(file["superior"]))
                ship.SuperiorDamageControl = (Quality)Enum.Parse(typeof(Quality), file["superior"]);
            if (!String.IsNullOrWhiteSpace(file["targeting"]))
                ship.TargettingMatrix = (Quality)Enum.Parse(typeof(Quality), file["targeting"]);
            if (!String.IsNullOrWhiteSpace(file["vaulted"]))
                ship.VaultedCeilings = (Quality)Enum.Parse(typeof(Quality), file["vaulted"]);
            switch (file["background"])
            {
                case "Thulian Explorator Vessel":
                    ship.Background = Background.ThulianExploratorVessel;
                    break;
                case "Reaver of the Unbeholden Reaches":
                    ship.Background = Background.ReaverOfTheUnbeholdenReaches;
                    break;
                case "Veteran of the Angevin Crusade":
                    ship.Background = Background.VeteranOfTheAngevinCrusade;
                    break;
                case "Implacable Foe of The Fleet":
                    ship.Background = Background.ImplacableFoeOfTheFleet;
                    break;
                case "Steadfast Ally of the Fleet":
                    ship.Background = Background.SteadfastAllyofTheFleet;
                    break;
                case "Planet-Bound for Millenia":
                    if (!String.IsNullOrWhiteSpace(file["hullloss"]))
                    {
                        byte loss = byte.Parse(file["hullloss"]);
                        ship.Background = (Background)loss;//and it to get number of hull lost
                    }
                    else
                        ship.Background = Background.PlanetBoundForMillenia;
                    break;
            }
            //supplemental components
            if (shipClass == HullType.None)
                shipClass = HullType.All;//if not set by a hull, now set assumed class to max
            foreach (KeyValuePair<String, String> pair in file.Skip(243))//supplemental components start after the first 243 entries
            {
                if (!String.IsNullOrWhiteSpace(pair.Value))
                {
                    String name = pair.Key.Replace("†","");
                    Quality quality = Quality.Common;
                    if (name.StartsWith("Poor Quality "))
                    {
                        name = name.Substring(13);
                        quality = Quality.Poor;
                    }
                    else if (name.StartsWith("Slim "))
                    {
                        name = name.Substring(5);
                        quality = Quality.Slim;
                    }
                    else if (name.StartsWith("Efficient "))
                    {
                        name = name.Substring(10);
                        quality = Quality.Efficient;
                    }
                    else if (name.StartsWith("Good Quality "))
                    {
                        name = name.Substring(13);
                        quality = Quality.Good;
                    }
                    else if (name.StartsWith("Best Quality"))
                    {
                        name = name.Substring(13);
                        quality = Quality.Best;
                    }
                    switch (name)
                    {
                        case Old.CogitatorInterlink:
                            name = Names.CogitatorInterlink;
                            break;
                        case Old.JammingSystem:
                            name = Names.JammingSystem;
                            break;
                    }
                    //check for all with same name, then get the largest one(ships capable of say cruiser and transport use cruiser size if that is what is available)
                    Supplemental component = Supplementals.Where(x => name.StartsWith(x.Name, StringComparison.OrdinalIgnoreCase)).OrderByDescending(x => x.HullTypes).FirstOrDefault();
                    int count = int.Parse(pair.Value);
                    if (component != null)
                    {
                        for (int i = 0; i < count; i++)
                            ship.SupplementalComponents.Add(new Supplemental(component.Name, component.HullTypes, component.RawPower, component.RawSpace, component.RawSP, component.Origin, component.PageNumber, component.RamDamage, component.RawSpecial, quality, component.Speed, component.Manoeuvrability, component.HullIntegrity, component.Armour, component.TurretRating, component.Morale, component.CrewPopulation, component.ProwArmour, component.CrewRating, component.MiningObjective, component.CreedObjective, component.MilitaryObjective, component.TradeObjective, component.CriminalObjective, component.ExplorationObjective, component.PowerGenerated, component.DetectionRating, component.AuxiliaryWeapon, component.MacrobatteryModifier, component.BSModifier, component.NavigateWarp, component.CrewLoss, component.MoraleLoss, component.ComponentOrigin, component.Replace, component.Max));
                    }
                }
            }
            return ship;
        }

        //Modified -> Regular w/ Modified = true
        //Some components were renamed for move to c#
        //name.Replace("†","") for finding supplementals

        /// <summary>
        /// do fetches by name to update information in case of fixes
        /// </summary>
        public static void Update(Starship ship)
        {

        }

        public static Loader Load(string path)
        {
            using (FileStream fs = File.OpenRead(path))
            {
                return Load(fs);
            }
        }

        public static Loader Load(FileStream fs)
        {
            return new Loader(fs);
        }

        public Loader(String path)
        {
            using (FileStream fs = File.OpenRead(path))
            {
                _load(fs);
            }
        }

        public Loader(FileStream fs)
        {
            _load(fs);
        }

        private void _load(FileStream fs)
        {
            throw new NotImplementedException();
        }

        public static Loader Default()
        {
            return new Loader();
        }

        public Loader()
        {
            //Hulls
            Hulls = new List<Hull>(36);
            Hulls.Add(new Hull("Ambition-Class Cruiser", 5, 12, 15, 66, 17, 75, 57, HullType.Cruiser, null, RuleBook.BattlefleetKoronus, 24, 2, 1, 0, 2));
            Hulls.Add(new Hull("Armageddon-Class Battlecruiser", 5, 10, 10, 70, 20, 73, 63, HullType.BattleCruiser, "Can only stay at void half as long as other ships", RuleBook.BattlefleetKoronus, 23, 2, 1, 1, 2));
            Hulls.Add(new Hull("Avenger-Class Grand Cruiser", 5, 5, 10, 90, 21, 90, 70, HullType.GrandCruiser, null, RuleBook.BattlefleetKoronus, 20, 3, 0, 0, 3));
            Hulls.Add(new Hull("Carrack-Class Transport", 4, -5, 10, 45, 15, 38, 25, HullType.Transport, null, RuleBook.BattlefleetKoronus, 29, dorsal: 2, comps: new Supplemental[] { MainCargoHold }));
            Hulls.Add(new Hull("Chalice-Class Battlecruiser", 6, 10, 10, 70, 19, 75, 63, HullType.BattleCruiser, "Any critical received has a 25% chance to cause an additional Fire! critical", RuleBook.BattlefleetKoronus, 22, 2, 1, 1, 2, power: 4));
            Hulls.Add(new Hull("Claymore-Class Corvette", 8, 18, 12, 30, 17, 38, 38, HullType.Frigate, null, RuleBook.BattlefleetKoronus, 27, dorsal: 2));
            Hulls.Add(new Hull("Cobra-Class Destroyer", 10, 30, 10, 30, 15, 35, 30, HullType.Raider, null, RuleBook.IntoTheStorm, 152, 1, 1, 1));
            Hulls.Add(new Hull("Conquest-Class Star Galleon", 4, 5, 10, 65, 16, 56, 52, HullType.Cruiser | HullType.Transport, null, RuleBook.BattlefleetKoronus, 23, side: 2, comps: new Supplemental[] { MainCargoHold, MainCargoHold }));
            Hulls.Add(new Hull("Dauntless-Class Light Cruiser", 7, 15, 20, 60, 19, 60, 55, HullType.LightCruiser, null, RuleBook.CoreRulebook, 196, prow: 1, side: 1));
            Hulls.Add(new Hull("Defiant-Class Light Cruiser", 6, 12, 15, 60, 20, 55, 58, HullType.LightCruiser, null, RuleBook.BattlefleetKoronus, 26, 2, 1, 0, 1, broadside: EscortBay));
            Hulls.Add(new Hull("Dictator-Class Cruiser", 5, 8, 18, 70, 20, 65, 63, HullType.Cruiser, null, RuleBook.BattlefleetKoronus, 24, 3, 1, 0, 2, broadside: LandingBay));
            Hulls.Add(new Hull("Endeavour-Class Light Cruiser", 6, 12, 15, 60, 20, 58, 57, HullType.LightCruiser, null, RuleBook.BattlefleetKoronus, 26, 2, 2, 0, 1, frontal: VossTubes));
            Hulls.Add(new Hull("Exorcist-Class Grand Cruiser", 4, 4, 9, 85, 20, 80, 71, HullType.GrandCruiser, null, RuleBook.BattlefleetKoronus, 23, 3, side: 3, broadside: LandingBay));
            Hulls.Add(new Hull("Falchion-Class Frigate", 8, 17, 14, 36, 18, 34, 42, HullType.Frigate, null, RuleBook.BattlefleetKoronus, 26, prow: 1, dorsal: 2, frontal: VossTubes));
            Hulls.Add(new Hull("Firestorm-Class Frigate", 7, 20, 15, 38, 18, 40, 41, HullType.Frigate, null, RuleBook.IntoTheStorm, 152, 1, 1, 1));
            Hulls.Add(new Hull("Goliath-Class Factory Ship", 3, -10, 4, 50, 14, 40, 25, HullType.Transport, null, RuleBook.BattlefleetKoronus, 30, dorsal: 1, side: 1, comps: new Supplemental[] { MainCargoHold, MainCargoHold, PlasmaRefinery }));
            Hulls.Add(new Hull("Havoc-Class Merchant Raider", 9, 25, 10, 30, 16, 40, 35, HullType.Raider, null, RuleBook.CoreRulebook, 195, 1, 1, 1));
            Hulls.Add(new Hull("Hazeroth-Class Privateer", 10, 23, 12, 32, 14, 35, 30, HullType.Raider, null, RuleBook.CoreRulebook, 194, 1, 1, 1));
            Hulls.Add(new Hull("Iconoclast-Class Destroyer", 10, 25, 10, 28, 14, 32, 29, HullType.Raider, "Long term repairs repair an additional 2 hull integrity", RuleBook.BattlefleetKoronus, 28, dorsal: 2));
            Hulls.Add(new Hull("Jericho-Class Pligrim Vessel", 3, -10, 5, 50, 12, 45, 20, HullType.Transport, null, RuleBook.CoreRulebook, 194, prow: 1, side: 1, comps: new Supplemental[] { MainCargoHold }));
            Hulls.Add(new Hull("Lathe-Class Monitor-Cruiser", 5, 12, 15, 62, 20, 60, 55, HullType.LightCruiser, null, RuleBook.IntoTheStorm, 152, 1, 1, 1, 1));
            Hulls.Add(new Hull("Loki-Class Q-Ship", 4, -5, 10, 40, 13, 45, 21, HullType.Transport, null, RuleBook.IntoTheStorm, 151, 1, 1, 1, comps: new Supplemental[] { MainCargoHold }, history: ShipHistory.WolfInSheepsClothing));
            Hulls.Add(new Hull("Lunar-Class Cruiser", 5, 10, 10, 70, 20, 75, 60, HullType.Cruiser, null, RuleBook.CoreRulebook, 196, 2, 1, 0, 2));
            Hulls.Add(new Hull("Mars-Class Battlecruiser", 5, 10, 10, 70, 20, 54, 71, HullType.BattleCruiser, null, RuleBook.BattlefleetKoronus, 22, 2, 1, 1, 2, frontal: MarsCannon, broadside: LandingBay, comps: new Supplemental[] { ArmouredProw }));
            Hulls.Add(new Hull("Meritech Shrike-Class Raider", 10, 25, 20, 30, 16, 35, 34, HullType.Raider, null, RuleBook.BattlefleetKoronus, 28, 2, 0, 2, bs: 5));
            Hulls.Add(new Hull("Orion-Class Star Clipper", 10, 25, 10, 35, 12, 40, 25, HullType.Transport, null, RuleBook.IntoTheStorm, 151, dorsal: 1, keel: 1, comps: new Supplemental[] { MainCargoHold }, locked: true));
            Hulls.Add(new Hull("Overlord-Class BattleCruiser", 5, 10, 10, 70, 20, 78, 64, HullType.BattleCruiser, null, RuleBook.BattlefleetKoronus, 22, 2, 1, 1, 2));
            Hulls.Add(new Hull("Repulsive-Class Grand Cruiser", 5, 8, 10, 85, 19, 90, 69, HullType.GrandCruiser, null, RuleBook.BattlefleetKoronus, 20, 3, 1, 1, 2, navigate: -10, locked: true));
            Hulls.Add(new Hull("Secutor-Class Monitor-Cruiser", 5, 12, 15, 65, 20, 58, 58, HullType.LightCruiser, null, RuleBook.IntoTheStorm, 152, 2, 1, 1, 1, shields: HullType.LightCruiser | HullType.Cruiser));
            Hulls.Add(new Hull("Sword-Class Frigate", 8, 20, 15, 35, 18, 40, 40, HullType.Frigate, null, RuleBook.CoreRulebook, 195, 2, 0, 2));
            Hulls.Add(new Hull("Tempest-Class Strike Frigate", 8, 18, 12, 36, 19, 42, 40, HullType.Frigate, null, RuleBook.CoreRulebook, 195, dorsal: 2));
            Hulls.Add(new Hull("Turbulent-Class Heavy Frigate", 7, 18, 15, 40, 20, 42, 42, HullType.Frigate, null, RuleBook.BattlefleetKoronus, 17, dorsal: 2, power: 2, command: -5));
            Hulls.Add(new Hull("Tyrant-Class Cruiser", 5, 10, 10, 70, 20, 77, 61, HullType.Cruiser, null, RuleBook.IntoTheStorm, 153, 2, 1, 0, 2));
            Hulls.Add(new Hull("Universe-Class Mass Conveyor", 2, -20, 5, 65, 12, 94, 45, HullType.Transport, null, RuleBook.BattlefleetKoronus, 30, dorsal: 1, side: 1, maxspeed: 2, power: 10, comps: new Supplemental[] { MainCargoHold, MainCargoHold, MainCargoHold, MainCargoHold }));
            Hulls.Add(new Hull("Vagabond-Class Merchant Trader", 4, -5, 10, 40, 13, 40, 20, HullType.Transport, null, RuleBook.CoreRulebook, 194, 1, 1, 1));
            Hulls.Add(new Hull("Viper-Class Scout Sloop", 11, 30, 25, 25, 14, 29, 27, HullType.Raider, null, RuleBook.BattlefleetKoronus, 28, dorsal: 1));
            //End of hulls
            //Plasma Drives
            PlasmaDrives = new List<PlasmaDrive>(24);
            PlasmaDrives.Add(new PlasmaDrive("Cypra-Pattern Class 1 Drive", HullType.Transport, 30, 10, "+15 to silent running, any attempts to detect this vessel suffer -15", RuleBook.BattlefleetKoronus, 42, 2, comp: ComponentOrigin.Archeotech));
            PlasmaDrives.Add(new PlasmaDrive("Cypra-Pattern Class 2 Drive", HullType.Raider | HullType.Frigate, 40, 12, "+15 to silent running, any attempts to detect this vessel suffer -15", RuleBook.BattlefleetKoronus, 42, 2, comp: ComponentOrigin.Archeotech));
            PlasmaDrives.Add(new PlasmaDrive("Jovian-Pattern Class 1 Drive", HullType.Transport, 35, 8, null, RuleBook.CoreRulebook, 199));
            PlasmaDrives.Add(new PlasmaDrive("Jovian-Pattern Class 2 Drive", HullType.Raider | HullType.Frigate, 45, 10, null, RuleBook.CoreRulebook, 199));
            PlasmaDrives.Add(new PlasmaDrive("Jovian-Pattern Class 3 Drive", HullType.LightCruiser, 60, 12, null, RuleBook.CoreRulebook, 199));
            PlasmaDrives.Add(new PlasmaDrive("Jovian-Pattern Class 4 Drive", HullType.CruiserPlus, 75, 14, null, RuleBook.CoreRulebook, 199));
            PlasmaDrives.Add(new PlasmaDrive(Names.WarcruiserDrive, HullType.CruiserPlus, 85, 17, null, RuleBook.IntoTheStorm, 156, 2));
            PlasmaDrives.Add(new PlasmaDrive(Names.WarcruiserDrive, HullType.LightCruiser, 65, 14, null, RuleBook.IntoTheStorm, 156, 2));
            PlasmaDrives.Add(new PlasmaDrive("Jovian-Pattern Class 8.1 Drive", HullType.Frigate, 44, 11, "On a critical hit to this drive roll a d10, ignore the crit on a 4+", RuleBook.BattlefleetKoronus, 31, 1));
            PlasmaDrives.Add(new PlasmaDrive("Jovian-Pattern Class 8.2 Drive", HullType.LightCruiser, 59, 13, "On a critical hit to this drive roll a d10, ignore the crit on a 4+", RuleBook.BattlefleetKoronus, 31, 1));
            PlasmaDrives.Add(new PlasmaDrive("Jovian-Pattern Class 8.3 Drive", HullType.CruiserPlus, 74, 15, "On a critical hit to this drive roll a d10, ignore the crit on a 4+", RuleBook.BattlefleetKoronus, 31, 1));
            PlasmaDrives.Add(new PlasmaDrive("Jovian-Pattern Class 8.4 Drive", HullType.GrandCruiser | HullType.BattleShip, 93, 20, "On a critical hit to this drive roll a d10, ignore the crit on a 4+", RuleBook.BattlefleetKoronus, 31, 1));
            PlasmaDrives.Add(new PlasmaDrive("Lathe-Pattern Class 1 Drive", HullType.Transport, 40, 12, null, RuleBook.CoreRulebook, 199, 1));
            PlasmaDrives.Add(new PlasmaDrive(Names.SprintTrader, HullType.Transport, 40, 14, null, RuleBook.IntoTheStorm, 156, 2, speed: 1, man: 3));
            PlasmaDrives.Add(new PlasmaDrive(Names.EscortDrive, HullType.Raider | HullType.Frigate, 47, 14, null, RuleBook.IntoTheStorm, 156, 2, speed: 1, man: 3));
            PlasmaDrives.Add(new PlasmaDrive("Mezoa-Pattern Theta-7 Drive", HullType.Transport, 44, 18, "If vessel suffers thrusters damaged or engine crippled critical hits, the roll to determine severity is automatically 10", RuleBook.BattlefleetKoronus, 31, 1, speed: 2, man: 5));
            PlasmaDrives.Add(new PlasmaDrive(Names.MimicDrive, HullType.CruiserPlus, 75, 14, "Navigator may make a +10 perception test to disguise ship as one it has already encountered, this lasts until seen visually", RuleBook.HostileAcquisition, 74, 3, comp: ComponentOrigin.Xenotech));
            PlasmaDrives.Add(new PlasmaDrive(Names.MimicDrive, HullType.LightCruiser, 60, 12, "Navigator may make a +10 perception test to disguise ship as one it has already encountered, this lasts until seen visually", RuleBook.HostileAcquisition, 74, 3, comp: ComponentOrigin.Xenotech));
            PlasmaDrives.Add(new PlasmaDrive(Names.MimicDrive, HullType.Raider | HullType.Frigate, 45, 10, "Navigator may make a +10 perception test to disguise ship as one it has already encountered, this lasts until seen visually", RuleBook.HostileAcquisition, 74, 3, comp: ComponentOrigin.Xenotech));
            PlasmaDrives.Add(new PlasmaDrive(Names.MimicDrive, HullType.Transport, 40, 12, "Navigator may make a +10 perception test to disguise ship as one it has already encountered, this lasts until seen visually", RuleBook.HostileAcquisition, 74, 3, comp: ComponentOrigin.Xenotech));
            PlasmaDrives.Add(new PlasmaDrive(@"Saturine-Pattern Class 4A ""Ultra"" Drive", HullType.BattleCruiser, 90, 14, null, RuleBook.BattlefleetKoronus, 31));
            PlasmaDrives.Add(new PlasmaDrive("Saturine-Pattern Class 5 Drive", HullType.GrandCruiser | HullType.BattleShip, 95, 18, null, RuleBook.BattlefleetKoronus, 31));
            PlasmaDrives.Add(new PlasmaDrive(Names.Viperdrive, HullType.Raider | HullType.Frigate, 45, 16, "If the vessel suffers an engine crippled critical hit, the severity roll is automatically 8-10, engines wrecked", RuleBook.HostileAcquisition, 69, 2, man: 5, speed: 2));
            PlasmaDrives.Add(new PlasmaDrive("Aconite Solar Sails", HullType.Frigate, 50, 0, "A ship with this component may interupt its Manoeuvre Action at any point to perform a Shooting Action. Once the Shooting Action is resolved, it must complete the remainder of its Manoeuvre Action. May still only make one Shooting Action per turn", RuleBook.LureoftheExpanse, 140, comp: ComponentOrigin.Xenotech));
            //End of Plasma Drives
            //Warp Drives
            WarpDrives = new List<WarpDrive>(8);
            WarpDrives.Add(new WarpDrive("Albanov 1 Warp Engine", ~HullType.AllCruiser, 10, 11, RuleBook.HostileAcquisition, 69, 1, "Double base travel time through immaterium, +20 to warp travel encounters, +10 to leave the warp"));
            WarpDrives.Add(new WarpDrive("Klenova Class M Warp Engine", ~HullType.AllCruiser, 10, 10, RuleBook.HostileAcquisition, 69, 0, "Navigator not needed or used, may only make calculated jumps, must test for warp encounters daily instead of per 5 days, ignore all navigate warp modifiers"));
            WarpDrives.Add(new WarpDrive("Markov 1 Warp Engine", ~HullType.AllCruiser, 12, 12, RuleBook.IntoTheStorm, 156, 1, "Reduce Warp Travel time by 1d5 weeks"));
            WarpDrives.Add(new WarpDrive("Markov 2 Warp Engine", HullType.AllCruiser, 13, 13, RuleBook.IntoTheStorm, 156, 1, "Reduce Warp Travel time by 1d10 days"));
            WarpDrives.Add(new WarpDrive("Miloslav G-616.b Warp Engine", ~HullType.AllCruiser, 8, 10, RuleBook.BattlefleetKoronus, 31, 0, "Half warp passage time but roll for encounters every 3 days instead of 5"));
            WarpDrives.Add(new WarpDrive("Miloslav H-616.b Warp Engine", HullType.AllCruiser, 10, 12, RuleBook.BattlefleetKoronus, 31, 0, "Half warp passage time but roll for encounters every 3 days instead of 5"));
            WarpDrives.Add(new WarpDrive("Strelov 1 Warp Engine", ~HullType.AllCruiser, 10, 10, RuleBook.CoreRulebook, 199));
            WarpDrives.Add(new WarpDrive("Strelov 2 Warp Engine", HullType.AllCruiser, 12, 12, RuleBook.CoreRulebook, 199));
            //End of Warp Drives
            //Gellar Fields
            GellarFields = new List<GellarField>(5);
            GellarFields.Add(new GellarField("Belecace-pattern 90.r Gellar Field", HullType.All, 1, "-20 on warp encounters table", RuleBook.BattlefleetKoronus, 32, 0, 10));
            GellarFields.Add(new GellarField("Emergency Gellar Field", HullType.All, 2, "If vessel suddenly enters the warp roll a d10: on a 3+ Gellar Field activates automatically", RuleBook.IntoTheStorm, 156));
            GellarFields.Add(new GellarField("Gellar Field", HullType.All, 1, null, RuleBook.CoreRulebook, 199));
            GellarFields.Add(new GellarField("Mezoa Gellar Void Integrant", HullType.Transport | HullType.Raider, 0, "-5 to rolls on warp encounters table; Damage to Void shields also affects Gellar Field", RuleBook.HostileAcquisition, 70, 0));
            GellarFields.Add(new GellarField("Warpsbane Hull", HullType.All, 1, "When rolling for warp encounters two rolls are made and the Navigator may choose which to apply", RuleBook.CoreRulebook, 199, 2, 10));
            //End of Gellar Fields
            //Void Shields
            VoidShields = new List<VoidShield>(12);
            VoidShields.Add(new VoidShield("Castellan Shield", HullType.All, 5, 1, 1, RuleBook.IntoTheStorm, 161, "During enemy turn, may make free -10 tech use to double number of shields", sp: 2, comp: ComponentOrigin.Archeotech));
            VoidShields.Add(new VoidShield("Castellan Shield Array", HullType.CruiserPlus, 7, 2, 2, RuleBook.IntoTheStorm, 161, "During enemy turn, may make free -10 tech use to double number of shields", sp: 2, comp: ComponentOrigin.Archeotech));
            VoidShields.Add(new VoidShield("Multiple Void Shield Array", HullType.CruiserPlus, 8, 1, 2, RuleBook.CoreRulebook, 200));
            VoidShields.Add(new VoidShield("Repulsor Shield Array", HullType.CruiserPlus, 8, 1, 2, RuleBook.IntoTheStorm, 157, "No penalties for moving through nebulae, ice rings, plasma clouds or celestial phenomonon"));
            VoidShields.Add(new VoidShield(Names.RepulsorShield, HullType.All, 6, 1, 1, RuleBook.IntoTheStorm, 156, "No penalties for moving through nebulae, ice rings, plasma clouds or celestial phenomonon"));
            VoidShields.Add(new VoidShield("Single Void Shield Array", HullType.All, 5, 1, 1, RuleBook.CoreRulebook, 199));
            VoidShields.Add(new VoidShield("Triple Void Shield Array", HullType.GrandCruiser | HullType.BattleShip, 9, 3, 3, RuleBook.BattlefleetKoronus, 32));
            VoidShields.Add(new VoidShield(@"Voss ""Glimmer""-Pattern Multiple Void Shield Array", HullType.CruiserPlus, 5, 1, 2, RuleBook.BattlefleetKoronus, 32, "When this cancels a hit roll a d10: on a 3 or lower the void shield fails to stop the hit"));
            VoidShields.Add(new VoidShield(@"Voss ""Glimmer""-Pattern Void Shield Array", HullType.All, 3, 1, 1, RuleBook.BattlefleetKoronus, 32, "When this cancels a hit roll a d10: on a 3 or lower the void shield fails to stop the hit"));
            VoidShields.Add(new VoidShield("Holo Field", HullType.All, 8, 4, 0, RuleBook.LureoftheExpanse, 140, "All attacks made against a ship while this is functioning suffer -40 to any Tests to hit in addition to other penalties. Macrobatteries only suffer -20 from this. Detection Actions against this ship suffer -30. This replaces Void Shields entirely", comp: ComponentOrigin.Xenotech));
            VoidShields.Add(new VoidShield("Ghost Field", HullType.All, 8, 4, 0, RuleBook.CoreRulebook, 207, "All ships firing at this vessel while this is active suffer -20 to ballistic skill tests. If the enemy is firing a lance or attempting a Hit and Run they take -30 instead to those tests. Replaces Void Shields entirely", sp: 3, comp: ComponentOrigin.Xenotech));
            VoidShields.Add(new VoidShield("Shadowfield", HullType.All, 8, 4, 0, RuleBook.HostileAcquisition, 75, "All ships firing at this vessel while this is active suffer -20 to ballistic skill tests. -30 to the pilot test to hit and run. +20 to Silent Running and any active or focussed augury against the vessel suffers -20. This replaces Void Chields", sp: 4, comp: ComponentOrigin.Xenotech));
            //End of Void Shields
            //Ship's Bridges
            Bridges = new List<Bridge>(16);
            Bridges.Add(new Bridge(Names.ArmouredBridge, HullType.AllCruiser, 3, 2, RuleBook.CoreRulebook, 200, "Ignore critical hits, damaged or unpowered on a d10 of 4+"));
            Bridges.Add(new Bridge(Names.ArmouredBridge, HullType.Raider | HullType.Frigate, 2, 2, RuleBook.CoreRulebook, 200, "Ignore critical hits, damaged or unpowered on a d10 of 4+"));
            Bridges.Add(new Bridge(Names.BridgeOfAntiquity, HullType.AllCruiser, 2, 1, RuleBook.CoreRulebook, 207, "+10 to social skills tests for characters on the bridge", 2, man: 5, command: 10, comp: ComponentOrigin.Archeotech));
            Bridges.Add(new Bridge(Names.BridgeOfAntiquity, ~HullType.AllCruiser, 1, 1, RuleBook.CoreRulebook, 207, "+10 to social skills tests for characters on the bridge", 2, man: 5, command: 10, comp: ComponentOrigin.Archeotech));
            Bridges.Add(new Bridge(Names.CombatBridge, HullType.AllCruiser, 2, 2, RuleBook.CoreRulebook, 200, repair: 10));
            Bridges.Add(new Bridge(Names.CombatBridge, ~HullType.AllCruiser, 1, 1, RuleBook.CoreRulebook, 200, repair: 10));
            Bridges.Add(new Bridge(Names.CommandBridge, HullType.AllCruiser, 3, 2, RuleBook.CoreRulebook, 200, sp: 1, bs: 5, command: 5));
            Bridges.Add(new Bridge(Names.CommandBridge, HullType.Raider | HullType.Frigate, 2, 1, RuleBook.CoreRulebook, 200, sp: 1, bs: 5, command: 5));
            Bridges.Add(new Bridge("Commerce Bridge", HullType.Transport, 1, 1, RuleBook.CoreRulebook, 200, trade: 50));
            Bridges.Add(new Bridge(Names.ExplorationBridge, HullType.AllCruiser, 4, 2, RuleBook.IntoTheStorm, 157, "+5 to active augury", 1, exploration: 50));
            Bridges.Add(new Bridge(Names.ExplorationBridge, ~HullType.AllCruiser, 4, 1, RuleBook.IntoTheStorm, 157, "+5 to active augury", 1, exploration: 50));
            Bridges.Add(new Bridge("Fleet Flag Bridge", HullType.GrandCruiser | HullType.BattleShip, 4, 4, RuleBook.BattlefleetKoronus, 32, "+5 to Navigate(Stellar) tests; Allied ships within 30VU also gain +5 to Pilot and Navigate tests", 1, command: 10, pilot: 5, navigate: 5));
            Bridges.Add(new Bridge("Flight Command Bridge", HullType.AllCruiser, 2, 2, RuleBook.BattlefleetKoronus, 32, "+5 to command tests for small craft squadrons; Tests to ready new squadrons for launch are automatically passed; Gain +25 to objectives when using small craft for ground to air actions"));
            Bridges.Add(new Bridge("Invasion Bridge", HullType.CruiserPlus, 4, 3, RuleBook.BattlefleetKoronus, 32, "+10 to Ballistic Skill tests against planetary targets, units on a planet orbitted by this vessel coutn as equipped with a multicompass as long as they are in vox contact"));
            Bridges.Add(new Bridge("Ship Master's Bridge", HullType.CruiserPlus, 4, 3, RuleBook.CoreRulebook, 200, "+5 to Navigate(Stellar) tests", bs: 10, pilot: 5, navigate: 5));
            Bridges.Add(new Bridge("Smuggler's Bridge", HullType.Transport, 1, 1, RuleBook.HostileAcquisition, 70, criminal: 50));
            //End of Ships Bridges
            //Life Sustainers
            LifeSustainers = new List<LifeSustainer>(10);
            LifeSustainers.Add(new LifeSustainer("Ancient Life Sustainer", HullType.AllCruiser, 2, 2, 2, RuleBook.CoreRulebook, 206, "Reduce loss to crew population from non-combat sources by 1", sp: 2, comp: ComponentOrigin.Archeotech));
            LifeSustainers.Add(new LifeSustainer("Ancient Life Sustainer", ~HullType.AllCruiser, 2, 1, 2, RuleBook.CoreRulebook, 206, "Reduce loss to crew population from non-combat sources by 1", sp: 2, comp: ComponentOrigin.Archeotech));
            LifeSustainers.Add(new LifeSustainer("Clemency-Pattern Life Sustainer", HullType.AllCruiser, 5, 5, 1, RuleBook.BattlefleetKoronus, 32, "Reduce damage by depressurisation by 4 to a minimum of 0"));
            LifeSustainers.Add(new LifeSustainer("Clemency-Pattern Life Sustainer", ~HullType.AllCruiser, 4, 4, 1, RuleBook.BattlefleetKoronus, 32, "Reduce damage by depressurisation by 4 to a minimum of 0"));
            LifeSustainers.Add(new LifeSustainer("Euphoric Life Sustainer", HullType.AllCruiser, 5, 3, 0, RuleBook.HostileAcquisition, 70, "This may be activated to provide these effects: +10 to Morale, -10 to crew rating, -10 to opponents command test if they perform a hit & run on this vessel; After this is deactivated the vessel suffers -10 morale for a day while the crew sobers up", sp: 1));
            LifeSustainers.Add(new LifeSustainer("Euphoric Life Sustainer", ~HullType.AllCruiser, 4, 2, 0, RuleBook.HostileAcquisition, 70, "This may be activated to provide these effects: +10 to Morale, -10 to crew rating, -10 to opponents command test if they perform a hit & run on this vessel; After this is deactivated the vessel suffers -10 morale for a day while the crew sobers up", sp: 1));
            LifeSustainers.Add(new LifeSustainer("Mark 1.r Life Sustainer", HullType.AllCruiser, 4, 2, 0, RuleBook.CoreRulebook, 200, moraleLoss: 1));
            LifeSustainers.Add(new LifeSustainer("Mark 1.r Life Sustainer", ~HullType.AllCruiser, 3, 1, 0, RuleBook.CoreRulebook, 200, moraleLoss: 1));
            LifeSustainers.Add(new LifeSustainer("Vitae-Pattern Life Sustainer", HullType.AllCruiser, 5, 3, 0, RuleBook.CoreRulebook, 200));
            LifeSustainers.Add(new LifeSustainer("Vitae-Pattern Life Sustainer", ~HullType.AllCruiser, 4, 2, 0, RuleBook.CoreRulebook, 200));
            //End of Life Sustainers
            //Crew Quarters
            CrewQuarters = new List<Components.CrewQuarters>(12);
            CrewQuarters.Add(new CrewQuarters("Bilge Rat Quarters", HullType.AllCruiser, 2, 3, -2, RuleBook.BattlefleetKoronus, 33, "Reduce crew loss by 2 for depressurisation"));
            CrewQuarters.Add(new CrewQuarters("Bilge Rat Quarters", ~HullType.AllCruiser, 1, 2, -2, RuleBook.BattlefleetKoronus, 33, "Reduce crew loss by 2 for depressurisation"));
            CrewQuarters.Add(new CrewQuarters("Clan-kin Quarters", HullType.AllCruiser, 2, 5, 0, RuleBook.IntoTheStorm, 157, "+5 to command tests to defend agaisnt boarding and hit and run", sp: 1, loss: -1));
            CrewQuarters.Add(new CrewQuarters("Clan-kin Quarters", ~HullType.AllCruiser, 1, 4, 0, RuleBook.IntoTheStorm, 157, "+5 to command tests to defend agaisnt boarding and hit and run", sp: 1, loss: -1));
            CrewQuarters.Add(new CrewQuarters("Cold Quarters", HullType.AllCruiser, 4, 5, 0, RuleBook.IntoTheStorm, 157, "Once per session the captain may choose to reduce one source of crew population loss to 0", sp: 1));
            CrewQuarters.Add(new CrewQuarters("Cold Quarters", ~HullType.AllCruiser, 3, 4, 0, RuleBook.IntoTheStorm, 157, "Once per session the captain may choose to reduce one source of crew population loss to 0", sp: 1));
            CrewQuarters.Add(new CrewQuarters("Pressed Crew Quarters", HullType.AllCruiser, 2, 3, -1, RuleBook.CoreRulebook, 200));
            CrewQuarters.Add(new CrewQuarters("Pressed Crew Quarters", ~HullType.AllCruiser, 1, 2, -1, RuleBook.CoreRulebook, 200));
            CrewQuarters.Add(new CrewQuarters("Slave Quarters", HullType.AllCruiser, 1, 2, -5, RuleBook.HostileAcquisition, 71));
            CrewQuarters.Add(new CrewQuarters("Slave Quarters", ~HullType.AllCruiser, 1, 1, -5, RuleBook.HostileAcquisition, 71));
            CrewQuarters.Add(new CrewQuarters("Voidsmen Quarters", HullType.AllCruiser, 2, 4, 0, RuleBook.CoreRulebook, 200));
            CrewQuarters.Add(new CrewQuarters("Voidsmen Quarters", ~HullType.AllCruiser, 1, 3, 0, RuleBook.CoreRulebook, 200));
            //End of Crew Quarters
            //Augur Arrays
            AugurArrays = new List<Augur>(8);
            AugurArrays.Add(new Augur("Auto-stabalised Logis-Targeter", 5, RuleBook.CoreRulebook, 207, 5, bs: 5, comp: ComponentOrigin.Archeotech));
            AugurArrays.Add(new Augur("BG-15 Assault Scanners", 5, RuleBook.BattlefleetKoronus, 33, 0, "+5 to Ballistic Skills tests against planetary targets", military: 50));
            AugurArrays.Add(new Augur("Deep Void Augur Array", 7, RuleBook.CoreRulebook, 202, 10, sp: 1));
            AugurArrays.Add(new Augur("Mark 100 Augur Array", 3, RuleBook.CoreRulebook, 201));
            AugurArrays.Add(new Augur("Mark 201.b Augur Array", 5, RuleBook.CoreRulebook, 201, 5));
            AugurArrays.Add(new Augur("R-50 Auspex Multi-band", 4, RuleBook.CoreRulebook, 202, -2, "+5 to maneouvrability tests to avoid celestial phenomena", exploration: 50));
            AugurArrays.Add(new Augur("W-240 Passive Detection Arrays", 3, RuleBook.HostileAcquisition, 71, special: "May perform any detection actions on silent running without penalty", sp: 1));
            AugurArrays.Add(new Augur("X-470 Ultimo Array", 6, RuleBook.BattlefleetKoronus, 33, 10, "+15 to detect ships on silent running with active augury; +5 to opponents ballistic skill tests to hit this vessel"));
            //End of Augur Arrays
            //Weapons
            Weapons = new List<Weapon>(46);
            Weapons.Add(new Weapon(Names.BombardmentCannons, WeaponType.Macrobattery, HullType.AllCruiser, WeaponSlot.Prow | WeaponSlot.Dorsal | WeaponSlot.Keel, 5, 3, 3, 3, new DiceRoll(1, 0, 6), 2, 4, RuleBook.BattlefleetKoronus, 34, special: "Add +1 to critical table for crits rolled, +20 to intimidate tests while ship armed with this is in orbit, may add 50 to military objectives on that planet, for planetary bombardments affects double the area, +20 damage to large enemies, +10 damage to individuals and vehicles"));
            Weapons.Add(new Weapon("Dark Cannon", WeaponType.Macrobattery, HullType.All, WeaponSlot.All, 4, 2, 3, 3, new DiceRoll(1, 0, 1), 6, 6, RuleBook.HostileAcquisition, 74, special: "Vessels hit by this weapon suffer -15 to ballistic skill in their following turn", comp: ComponentOrigin.Xenotech));
            Weapons.Add(new Weapon("Disruption Macrocannons", WeaponType.Macrobattery, HullType.All, WeaponSlot.All, 4, 2, 2, 3, new DiceRoll(1, 0, 1), 0, 5, RuleBook.BattlefleetKoronus, 34, special: "Does not cause damage but for every 5 damage rolled, ignoring armour, oen random compoent on target vessel becomes unpowered; Cannot crit and may only be combined into a salvo with other Disruption Macrocannon batteries"));
            Weapons.Add(new Weapon("Disruption Macrocannon Broadside", WeaponType.Macrobattery, HullType.AllCruiser, WeaponSlot.Side, 6, 5, 2, 6, new DiceRoll(1, 0, 1), 0, 5, RuleBook.BattlefleetKoronus, 34, special: "Does not cause damage but for every 5 damage rolled, ignoring armour, oen random compoent on target vessel becomes unpowered; Cannot crit and may only be combined into a salvo with other Disruption Macrocannon batteries"));
            Weapons.Add(new Weapon("Dragon's Breath Lance", WeaponType.Lance, HullType.All, WeaponSlot.Prow, 13, 8, 3, 3, new DiceRoll(1, 0, 6), 3, 3, RuleBook.LureoftheExpanse, 139, comp: ComponentOrigin.Archeotech));
            Weapons.Add(new Weapon("Energy Drain Matrix", WeaponType.Macrobattery, HullType.All, WeaponSlot.All, 3, 1, 2, 4, default(DiceRoll), 0, 4, RuleBook.HostileAcquisition, 74, special: "Does not do damage or critical hits; For every hit that gets past the target's void shields, may reduce either target's Speed by 1 or Maneouverability by 5"));
            Weapons.Add(new TorpedoTubes("Fortis Pattern Torpedo Tubes", HullType.AllCruiser, 2, 8, 3, 6, 42, RuleBook.BattlefleetKoronus, 37, special: "+2VUs Torpedo speed on turn they are launched"));
            Weapons.Add(new Weapon("Godsbane Lance", WeaponType.Lance, HullType.BattleCruiser | HullType.GrandCruiser | HullType.BattleShip, WeaponSlot.Lance, 9, 4, 3, 1, new DiceRoll(1, 0, 2), 3, 12, RuleBook.BattlefleetKoronus, 34, special: "If the target is over 20VUs away, reduce damage to 1d10"));
            Weapons.Add(new Weapon("Godsbane Lance Battery", WeaponType.Lance, HullType.BattleCruiser | HullType.GrandCruiser | HullType.BattleShip, WeaponSlot.Lance, 12, 6, 3, 2, new DiceRoll(1, 0, 2), 3, 12, RuleBook.BattlefleetKoronus, 34, special: "If the target is over 20VUs away, reduce damage to 1d10"));
            Weapons.Add(new Weapon("Grapple Cannon", WeaponType.Macrobattery, HullType.Raider, WeaponSlot.All, 2, 2, 1, 1, default(DiceRoll), 0, 0, RuleBook.HostileAcquisition, 71, special: "When making a boarding test, may make a -10 ballistic skill test instead of the -20 Pilot+Maneouverability test, and if you do the test for the target to escape the boarding action is -40 instead of -20"));
            Weapons.Add(new TorpedoTubes("Gryphonne-Pattern Torpedo Tubes", ~HullType.Transport, 2, 6, 1, 4, 24, RuleBook.BattlefleetKoronus, 27));
            Weapons.Add(new Weapon("Hecutor-Pattern Plasma Battery", WeaponType.Macrobattery, HullType.AllCruiser, WeaponSlot.All, 8, 3, 2, 3, new DiceRoll(1, 0, 2), 4, 11, RuleBook.BattlefleetKoronus, 34, special: "When this rolls a critical result of 1 or 2, it affects 2 components instead of 1"));
            Weapons.Add(new Weapon("Hecutor-Pattern Plasma Broadside", WeaponType.Macrobattery, HullType.BattleCruiser | HullType.GrandCruiser | HullType.BattleShip, WeaponSlot.Side, 12, 5, 2, 5, new DiceRoll(1, 0, 2), 4, 11, RuleBook.BattlefleetKoronus, 34, special: "When this rolls a critical result of 1 or 2, it affects 2 components instead of 1"));
            Weapons.Add(new Weapon(Names.JovianMissiles, WeaponType.Macrobattery, HullType.All, WeaponSlot.All, 3, 1, 1, 5, new DiceRoll(1, 0, 1), 6, 6, RuleBook.IntoTheStorm, 158, special: "May not fire two turns ion a row"));
            Weapons.Add(new LandingBay("Jovian-Pattern Escort Bay", HullType.AllCruiser, WeaponSlot.Side, 1, 4, 1, 1, RuleBook.BattlefleetKoronus, 36));
            Weapons.Add(new LandingBay("Jovian-Pattern Landing Bay", HullType.CruiserPlus, WeaponSlot.Side, 1, 6, 2, 2, RuleBook.BattlefleetKoronus, 36));
            Weapons.Add(new NovaCannon("Jovian-Pattern Nova Cannon", HullType.CruiserPlus, 6, 7, 5, new DiceRoll(0, 2, 7), 35, RuleBook.BattlefleetKoronus, 42, comp: ComponentOrigin.Archeotech, special: "Ships hit by this suffer 1d5 morale damage even if no damage is inflicted; If this is ever damaged: instead it is destroyed and ship takes 1d10 hull integrity damage with no reduction from armour or shields"));
            Weapons.Add(new Weapon("Las-Burner", WeaponType.Lance, HullType.All, WeaponSlot.Lance | WeaponSlot.Dorsal | WeaponSlot.Keel, 7, 3, 2, 2, new DiceRoll(0, 1, 1), 3, 3, RuleBook.BattlefleetKoronus, 35, special: "Grants +5 to opposed command test for boarding actions"));
            Weapons.Add(new Weapon(Names.LatheGravCulverin, WeaponType.Macrobattery, HullType.AllCruiser, WeaponSlot.Side, 5, 5, 1, 6, new DiceRoll(1, 0, 3), 6, 5, RuleBook.IntoTheStorm, 158, special: "May reduce damage to 1d10+1 to increase range by 2VUs"));
            Weapons.Add(new LandingBay("Lathe-Pattern Landing Bay", HullType.CruiserPlus, WeaponSlot.Side, 1, 5, 2, 2, RuleBook.BattlefleetKoronus, 36, special: "If this component becomes unpowered while open, it also becomes depressurised. The component must be open during the strategic turn that craft take off or land"));
            Weapons.Add(new Weapon(Names.MarsBroadsides, WeaponType.Macrobattery, HullType.AllCruiser, WeaponSlot.Side, 4, 5, 1, 6, new DiceRoll(1, 0, 2), 5, 6, RuleBook.CoreRulebook, 202));
            Weapons.Add(new Weapon(Names.MarsMacrocannons, WeaponType.Macrobattery, HullType.All, WeaponSlot.All, 4, 2, 1, 3, new DiceRoll(1, 0, 2), 5, 6, RuleBook.CoreRulebook, 202));
            Weapons.Add(new NovaCannon("Mars-Pattern Nova Cannon", HullType.CruiserPlus, 3, 7, 3, new DiceRoll(0, 2, 4), 40, RuleBook.BattlefleetKoronus, 36));
            Weapons.Add(new TorpedoTubes("Mars-Pattern Torpedo Tubes", HullType.AllCruiser, 2, 8, 2, 6, 42, RuleBook.BattlefleetKoronus, 37));
            Weapons.Add(new Weapon(Names.MezoaLanceBattery, WeaponType.Lance, HullType.AllCruiser, WeaponSlot.Lance, 13, 6, 3, 2, new DiceRoll(1, 0, 5), 4, 4, RuleBook.BattlefleetKoronus, 35));
            Weapons.Add(new Weapon(Names.MezoaLance, WeaponType.Lance, HullType.All, WeaponSlot.Lance, 9, 4, 3, 1, new DiceRoll(1, 0, 5), 4, 4, RuleBook.BattlefleetKoronus, 35));
            Weapons.Add(new Weapon("Mezoa-Pattern Macrocannons", WeaponType.Macrobattery, HullType.All, WeaponSlot.All, 4, 4, 1, 4, new DiceRoll(1, 0, 3), 5, 5, RuleBook.IntoTheStorm, 158));
            Weapons.Add(new TorpedoTubes("Plasma Accelerated Torpedo Tubes", HullType.All, 2, 4, 2, 2, 16, RuleBook.BattlefleetKoronus, 42, special: "Torpedoes launched from this component gain an additional +4VUs speed on the turn they are launched; Torpedoes launched by this gain +10 to hit", comp: ComponentOrigin.Archeotech));
            Weapons.Add(new Weapon("Pyros Melta-Cannons", WeaponType.Macrobattery, HullType.All, WeaponSlot.All, 4, 3, 2, 3, new DiceRoll(1, 0, 4), 4, 4, RuleBook.IntoTheStorm, 158, special: "When this weapon component inflicts a critical hit, it is automatically a Fire! Critical"));
            Weapons.Add(new Weapon(Names.RyzaPlasma, WeaponType.Macrobattery, HullType.All, WeaponSlot.All, 8, 4, 1, 4, new DiceRoll(1, 0, 4), 4, 5, RuleBook.CoreRulebook, 203, special: "When this rolls a critical result of 1 or 2, it affects 2 components instead of 1"));
            Weapons.Add(new NovaCannon("Ryza-Pattern Nova Cannon", HullType.CruiserPlus, 4, 7, 4, new DiceRoll(0, 2, 5), 36, RuleBook.BattlefleetKoronus, 37, "For every 5 degrees of failure on a test to fire this, the firing vessel suffers a critical hit. If the critical would affect a component, it affects this weapon"));
            Weapons.Add(new Weapon("Shard Battery Cannon", WeaponType.Macrobattery, HullType.All, WeaponSlot.All, 0, 3, 2, 4, new DiceRoll(1, 0, 2), 3, 6, RuleBook.CoreRulebook, 208, special: "Cannont become unpowered; If this is ever destroyed vessel suffers 2d5 hull integrity damage with no armour or shields", comp: ComponentOrigin.Xenotech));
            Weapons.Add(new Weapon("Staravar Laser Macrobattery", WeaponType.Macrobattery, HullType.All, WeaponSlot.All, 4, 4, 2, 4, new DiceRoll(1, 0, 2), 4, 12, RuleBook.IntoTheStorm, 162, comp: ComponentOrigin.Archeotech));
            Weapons.Add(new Weapon("Starbreaker Lance Weapon", WeaponType.Lance, HullType.All, WeaponSlot.Lance, 6, 4, 2, 1, new DiceRoll(1, 0, 2), 3, 5, RuleBook.CoreRulebook, 203));
            Weapons.Add(new Weapon("Star-Flare Lance", WeaponType.Lance, HullType.AllCruiser, WeaponSlot.Lance, 12, 6, 3, 3, new DiceRoll(1, 0, 3), 3, 7, RuleBook.IntoTheStorm, 162, special: "Scores additional hit per 2 degrees of success instead of 3", comp: ComponentOrigin.Archeotech));
            Weapons.Add(new Weapon("Stygies-Pattern Macrocannon Battery", WeaponType.Macrobattery, HullType.All, WeaponSlot.All, 4, 5, 1, 3, new DiceRoll(1, 0, 2), 5, 5, RuleBook.BattlefleetKoronus, 34, special: "When calculating damage for a salvo in which at least one shot from this got past shields, reduce targets armour by 3"));
            Weapons.Add(new Weapon("Sunhammer Lance", WeaponType.Lance, HullType.All, WeaponSlot.Lance, 9, 4, 2, 1, new DiceRoll(1, 0, 3), 3, 9, RuleBook.IntoTheStorm, 158));
            Weapons.Add(new Weapon("Sunhammer Lance Battery", WeaponType.Lance, HullType.AllCruiser, WeaponSlot.Lance, 13, 6, 2, 2, new DiceRoll(1, 0, 3), 3, 9, RuleBook.IntoTheStorm, 158));
            Weapons.Add(new Weapon("Sunsear Las-Broadside", WeaponType.Macrobattery, HullType.AllCruiser, WeaponSlot.Side, 9, 6, 1, 6, new DiceRoll(1, 0, 2), 4, 9, RuleBook.IntoTheStorm, 158));
            Weapons.Add(new Weapon("Sunsear Laser Battery", WeaponType.Macrobattery, HullType.All, WeaponSlot.All, 6, 4, 1, 4, new DiceRoll(1, 0, 2), 4, 9, RuleBook.CoreRulebook, 202));
            Weapons.Add(new Weapon("Thunderstrike Macrocannons", WeaponType.Macrobattery, HullType.All, WeaponSlot.All, 2, 2, 1, 3, new DiceRoll(1, 0, 1), 6, 4, RuleBook.CoreRulebook, 202));
            Weapons.Add(new Weapon("Titanforge Lance Battery", WeaponType.Lance, HullType.AllCruiser, WeaponSlot.Lance, 13, 6, 2, 2, new DiceRoll(1, 0, 4), 3, 6, RuleBook.CoreRulebook, 203));
            Weapons.Add(new Weapon("Titanforge Lance Weapon", WeaponType.Lance, HullType.All, WeaponSlot.Lance, 9, 4, 2, 1, new DiceRoll(1, 0, 4), 3, 6, RuleBook.CoreRulebook, 203));
            Weapons.Add(new Weapon("Voidsunder Lance Battery", WeaponType.Lance, HullType.AllCruiser, WeaponSlot.Heavy, 15, 8, 3, 3, new DiceRoll(1, 0, 4), 3, 6, RuleBook.BattlefleetKoronus, 35, special: "If mounted in prow, may only fire forward regardless of ship class"));
            Weapons.Add(new TorpedoTubes("Voss-Pattern Torpedo Tubes", HullType.All, 1, 5, 2, 2, 12, RuleBook.BattlefleetKoronus, 37));
            Weapons.Add(new Weapon("Starcannon Cluster Battery", WeaponType.Macrobattery, HullType.All, WeaponSlot.All, 5, 3, 0, 4, new DiceRoll(1, 0, 2), 4, 6, RuleBook.LureoftheExpanse, 140, special: "Eldar vessels gain a +10 to Ballistic Skill tests to fire this weapon", comp: ComponentOrigin.Xenotech));
            //End of Weapons
            //Supplemental Components
            Supplementals = new List<Supplemental>(99);
            Supplementals.Add(new Supplemental("Hold Landing Bay", HullType.Transport, 1, 0, 2, RuleBook.BattlefleetKoronus, 36, man: -5, hullint: -5, replace: "Main Cargo Hold", max: 1, aux: HoldLandingBay));
            Supplementals.Add(new Supplemental("Auxiliary Plasma Banks", ~HullType.AllCruiser, 8, 5, 1, RuleBook.IntoTheStorm, 159, generated: true, special: "If this component becomes damaged, the vessel takes 1d5 hull integrity damage and the plasma drive is set on fire"));
            Supplementals.Add(new Supplemental("Auxiliary Plasma Banks", HullType.AllCruiser, 10, 6, 1, RuleBook.IntoTheStorm, 159, generated: true, special: "If this component becomes damaged, the vessel takes 1d5 hull integrity damage and the plasma drive is set on fire"));
            Supplementals.Add(new Supplemental("Arboretum", ~HullType.AllCruiser, 2, 2, 1, RuleBook.IntoTheStorm, 160, null, "Double the time a ship may spend at void before suffering crew and morale damage", crew: 2, max: 1));
            Supplementals.Add(new Supplemental("Arboretum", HullType.AllCruiser, 2, 3, 1, RuleBook.IntoTheStorm, 160, null, "Double the time a ship may spend at void before suffering crew and morale damage", crew: 2, max: 1));
            Supplementals.Add(new Supplemental("Armour Plating", ~HullType.AllCruiser, 0, 1, 2, RuleBook.CoreRulebook, 204, man: -2, armour: 1, max: 1));
            Supplementals.Add(new Supplemental("Armour Plating", HullType.AllCruiser, 0, 2, 2, RuleBook.CoreRulebook, 204, man: -2, armour: 1, max: 1));
            Supplementals.Add(new Supplemental("Armoured Prow", HullType.CruiserPlus, 0, 4, 2, RuleBook.CoreRulebook, 204, new DiceRoll(1, 0, 0), "Cannot take macrobatteries or lance in prow", prowArmour: 4, max: 1));
            Supplementals.Add(new Supplemental("Asteroid Mining Facility", HullType.All, 6, 10, 3, RuleBook.IntoTheStorm, 160, null, "May construct trade endeavours based on mining, can gain +200 to mining objectives", max: 1));
            Supplementals.Add(new Supplemental("Astropathic Choir-Chambers", HullType.All, 1, 1, 1, RuleBook.IntoTheStorm, 160, null, "+10 to focus power tests for astro-telepathy while in this component and +5VU range on psychic powers made by psykers in this component during combat", max: 1));
            Supplementals.Add(new Supplemental("Augmented Retro-Thrusters", HullType.Raider | HullType.Frigate, 3, 0, 2, RuleBook.CoreRulebook, 203, man: 5, special: "External"));
            Supplementals.Add(new Supplemental("Augmented Retro-Thrusters", HullType.LightCruiser | HullType.Transport, 4, 0, 2, RuleBook.CoreRulebook, 203, man: 5, special: "External"));
            Supplementals.Add(new Supplemental("Augmented Retro-Thrusters", HullType.CruiserPlus, 5, 0, 2, RuleBook.CoreRulebook, 203, man: 5, special: "External"));
            Supplementals.Add(new Supplemental("Auto-Temple", HullType.All, 1, 1, 0, RuleBook.HostileAcquisition, 72, null, "May be returned to ship in 2-3 days by work crew and lifters", morale: 2, creed: 150, max: 1));
            Supplementals.Add(new Supplemental("Barracks", HullType.All, 2, 4, 2, RuleBook.CoreRulebook, 203, null, "+20 to command tests for boarding and hit and run", military: 100));
            Supplementals.Add(new Supplemental("Brig", HullType.All, 1, 1, 1, RuleBook.BattlefleetKoronus, 37, null, "+5 to intimidate tests as part of extended actions, may earn 25 objective points to objectives involding the capture or transport of prisoners", morale: 1));
            Supplementals.Add(new Supplemental("Broadband Hymn-Casters", HullType.All, 3, 0, 1, RuleBook.IntoTheStorm, 161, null, "External; All other ships within 30VUs must make a -10 Tech-Use Test in order to use vox or other broadcast communications; While active characters aboard this vessel gain +10 to intimidate tests on against all ships within 30VUs", max: 1));
            Supplementals.Add(new Supplemental("Cargo Hold and Lighter Bay", ~HullType.Transport, 1, 2, 1, RuleBook.CoreRulebook, 203, man: -3, trade: 50, criminal: 50));
            Supplementals.Add(new Supplemental("Chameleon Hull", HullType.All, 1, 0, 2, RuleBook.HostileAcquisition, 74, null, "External; May program a pattern, including markings, for the hull to show with a -10 Tech-Use Test and may change between programmed markings with a -10 Tech-Use Test"));
            Supplementals.Add(new Supplemental("Cloudmining Facility", HullType.Transport, 3, 4, 1, RuleBook.BattlefleetKoronus, 39, null, "After comets have been located with a +0 Scrutiny+Detection Test they may be mined which takes 1d10+5 days. This may either grant 1d5 morale and extend time at void by 1 month, or grant +50 to objectives if it can be used or sold. May be possible to construct an endeavour to mine comets", max: 1));
            Supplementals.Add(new Supplemental(Names.CogitatorInterlink, HullType.All, 1, 1, 1, RuleBook.IntoTheStorm, 161, crewRating: 5, comp: ComponentOrigin.Archeotech, max: 1));
            Supplementals.Add(new Supplemental("Compartmentalised Cargo Hold", ~HullType.Transport, 2, 5, 1, RuleBook.CoreRulebook, 203, trade: 100));
            Supplementals.Add(new Supplemental("Crew Reclamation Facility", HullType.All, 1, 1, 1, RuleBook.CoreRulebook, 205, crewLoss: -3, moraleLoss: 1, max: 1));
            Supplementals.Add(new Supplemental("Defensive Countermeasures", HullType.All, 1, 1, 2, RuleBook.BattlefleetKoronus, 38, null, "When deployed, ships targetting this vessel suffer a -20 to ballistic skills test, -30 if using torpedoes. This lasts for 1d5+1 strategic rounds and may not be used again until refitted at a shipyard with an upkeep test", max: 1));
            Supplementals.Add(new Supplemental("Drop Pod Launch Bays", HullType.All, 1, 3, 2, RuleBook.IntoTheStorm, 159, null, "Can hold 20 pods, may deploy 10 per 30 minutes(strategic turn). The pods must be recovered from the planet's surface before being reused", military: 50));
            Supplementals.Add(new Supplemental("Emergency Energy Reserves", ~HullType.AllCruiser, 2, 1, 2, RuleBook.HostileAcquisition, 73, null, "When crippled, the captain may choose to have either his weapons of speed unaffected by the usual penalties for crippled ships. If this is damaged, the component has a 25% chance of exploding. If it does, the component is destroyed and the ship takes 1d5 damage to hull integrity and a component of the GM's choice is set on fire", max: 1, comp: ComponentOrigin.Archeotech));
            Supplementals.Add(new Supplemental("Emergency Energy Reserves", HullType.AllCruiser, 3, 2, 2, RuleBook.HostileAcquisition, 73, null, "When crippled, the captain may choose to have either his weapons of speed unaffected by the usual penalties for crippled ships. If this is damaged, the component has a 25% chance of exploding. If it does, the component is destroyed and the ship takes 1d5 damage to hull integrity and a component of the GM's choice is set on fire", max: 1, comp: ComponentOrigin.Archeotech));
            Supplementals.Add(new Supplemental("Empyrean Mantle", ~HullType.AllCruiser, 3, 0, 2, RuleBook.IntoTheStorm, 159, null, "External; When travelling on Silent Running, all tests to detect this vessel have their Difficulty increased by two degrees", criminal: 50, max: 1));
            Supplementals.Add(new Supplemental("Empyrean Mantle", HullType.AllCruiser, 5, 0, 2, RuleBook.IntoTheStorm, 159, null, "External; When travelling on Silent Running, all tests to detect this vessel have their Difficulty increased by two degrees", criminal: 50, max: 1));
            Supplementals.Add(new Supplemental("Energistic Conversion Matrix", HullType.Frigate | HullType.Raider, 1, 1, 1, RuleBook.IntoTheStorm, 161, null, "May use 3 power to gain 1 speed, to a maximum of 5 extra speed. May divert power from other components by making them unpowered until this is turned off", max: 1));
            Supplementals.Add(new Supplemental("Energistic Conversion Matrix", HullType.LightCruiser, 1, 1, 1, RuleBook.IntoTheStorm, 161, null, "May use 4 power to gain 1 speed, to a maximum of 5 extra speed. May divert power from other components by making them unpowered until this is turned off", max: 1));
            Supplementals.Add(new Supplemental("Energistic Conversion Matrix", HullType.Transport | HullType.CruiserPlus, 1, 1, 1, RuleBook.IntoTheStorm, 161, null, "May use 5 power to gain 1 speed, to a maximum of 5 extra speed. May divert power from other components by making them unpowered until this is turned off", max: 1));
            Supplementals.Add(new Supplemental("Evacuation Bay", ~HullType.Transport, 2, 4, 1, RuleBook.HostileAcquisition, 71, null, "As a free action, a member of the bridge may open the cargo hatches to forcibly eject all of the cargo into the void, cleansing the hold", trade: 75));
            Supplementals.Add(new Supplemental("Excess Void Armour", ~HullType.AllCruiser, 0, 2, 2, RuleBook.LureoftheExpanse, 139, speed: -2, man: -3, armour: 3));
            Supplementals.Add(new Supplemental("Excess Void Armour", HullType.AllCruiser, 0, 3, 2, RuleBook.LureoftheExpanse, 139, speed: -2, man: -3, armour: 3));
            Supplementals.Add(new Supplemental("Extended Supply Vaults", HullType.All, 1, 4, 2, RuleBook.CoreRulebook, 205, null, "Double the time this vessel may remain at void without suffering Crew Population or Morale Loss. When making Extended Repairs repair an additional 1 Hull Integrity", morale: 1, max: 1));
            Supplementals.Add(new Supplemental("Field Bracing", HullType.All, 0, 4, 2, RuleBook.BattlefleetKoronus, 38, null, "May exchange 1 Power for 2 Hull Integrity, up to +6 Hull Integrity. Should this component be damaged, unpowered or supplied with less power, the hull loses the bonus value proportionally, although this won't bring the Ship's Hull Integrity below 0. The amount of power supplied to this may be increased with a +0 Tech-Use Test, and may divert power from other components by making them unpowered"));
            Supplementals.Add(new Supplemental("Fire Suppression Systems", ~HullType.AllCruiser, 1, 1, 2, RuleBook.BattlefleetKoronus, 38, null, "Once per turn, so long as the Bridge is powered and undamaged, a character may make a -10 Tech-Use Test as an extended action to extinguish one component on fire", max: 1));
            Supplementals.Add(new Supplemental("Fire Suppression Systems", HullType.LightCruiser | HullType.Cruiser | HullType.BattleCruiser, 2, 2, 2, RuleBook.BattlefleetKoronus, 38, null, "Once per turn, so long as the Bridge is powered and undamaged, a character may make a -10 Tech-Use Test as an extended action to extinguish one component on fire", max: 1));
            Supplementals.Add(new Supplemental("Fire Suppression Systems", HullType.GrandCruiser | HullType.BattleShip, 3, 3, 2, RuleBook.BattlefleetKoronus, 38, null, "Once per turn, so long as the Bridge is powered and undamaged, a character may make a -10 Tech-Use Test as an extended action to extinguish one component on fire", max: 1));
            Supplementals.Add(new Supplemental("Flak Turrets", HullType.All, 1, 1, 1, RuleBook.BattlefleetKoronus, 38, null, "When in use, ship gains +1 Turret Rating but suffers -10 to Detection Rating", max: 1));
            Supplementals.Add(new Supplemental("Ghost Field", HullType.All, 8, 4, 3, RuleBook.CoreRulebook, 207, null, "All ships firing at this vessel while this is active suffer -20 to ballistic skill tests. If the enemy is firing a lance or attempting a Hit and Run they take -30 instead to those tests. This vessel must decide if it will use its void shields or ghost shields each turn", comp: ComponentOrigin.Xenotech));
            Supplementals.Add(new Supplemental("Gilded Hull", ~HullType.AllCruiser, 0, 1, 2, RuleBook.LureoftheExpanse, 139, null, "+10 to Fellowship tests made by the captain while on or in sight of this vessel", armour: -3));
            Supplementals.Add(new Supplemental("Gilded Hull", HullType.AllCruiser, 0, 2, 2, RuleBook.LureoftheExpanse, 139, null, "+10 to Fellowship tests made by the captain while on or in sight of this vessel", armour: -3));
            Supplementals.Add(new Supplemental("Grav Repulsors", HullType.All, 0, 1, 2, RuleBook.HostileAcquisition, 74, null, "External; May allocate 1 to 3 power to this component, for each 1 allocated: reduce damage from asteroids or space debris, torpedoes, bomber attacks or being rammed by 1. In the case of torpedoes it applies to each individual source and agaisnt all other sources it applies to the combined damage caused", comp: ComponentOrigin.Xenotech));
            Supplementals.Add(new Supplemental("Graviton Flare", HullType.Raider | HullType.Frigate, 2, 0, 2, RuleBook.HostileAcquisition, 73, null, "External; When triggered: all vessels in the star system suffer -30 to Detection for 2 rounds. This component takes 24 hours to recharge", comp: ComponentOrigin.Archeotech));
            Supplementals.Add(new Supplemental("Gravity Sails", ~HullType.AllCruiser, 3, 0, 3, RuleBook.CoreRulebook, 208, null, "External", speed: 1, man: 5, comp: ComponentOrigin.Xenotech));
            Supplementals.Add(new Supplemental("Gravity Sails", HullType.AllCruiser, 5, 0, 3, RuleBook.CoreRulebook, 208, null, "External", speed: 1, man: 5, comp: ComponentOrigin.Xenotech));
            Supplementals.Add(new Supplemental("Gyro-Stabalisation Matrix", HullType.All, 1, 1, 1, RuleBook.IntoTheStorm, 162, null, "Adjust Speed and Bearing, Come to New Heading and Evasive Manoeuvres are +0 tests instead of -20 or -10", comp: ComponentOrigin.Archeotech, max: 1));
            Supplementals.Add(new Supplemental(Names.JammingSystem, HullType.All, 4, 0, 1, RuleBook.BattlefleetKoronus, 39, null, "External; While this component is active: this vessel may not perform Silent Running but any Focussed Augury Tests to scan it suffer a -20 penalty", max: 1));
            Supplementals.Add(new Supplemental("Laboratoreum", HullType.All, 2, 1, 3, RuleBook.HostileAcquisition, 72, null, "This component grants +20 bonus to all tests to identify, analyse or repair artefacts of ancient or xenos origin, or to craft single items", max: 1));
            Supplementals.Add(new Supplemental("Librarium Vault", HullType.All, 1, 1, 1, RuleBook.CoreRulebook, 205, null, "+10 to Investigate Skill Tests made aboard this vessel", max: 1));
            Supplementals.Add(new Supplemental("Lux Net", HullType.All, 0, 2, 2, RuleBook.BattlefleetKoronus, 38, null, "This may only be deployed while a ship is stationary and inside a solar system. It takes 2 hours to deploy and 10 to retract. If the ship has to move during the net's operation, the net is destroyed. The Net counts as exposed when deployed. While deployed this generates 10 power and adds +1 to the number of degrees of successes on extended repairs"));
            Supplementals.Add(new Supplemental("Luxury Passenger Quarters", HullType.All, 2, 1, 1, RuleBook.CoreRulebook, 203, morale: -3, trade: 100, criminal: 100, creed: 100));
            Supplementals.Add(new Supplemental("Main Cargo Hold", HullType.Transport, 2, 4, 1, RuleBook.CoreRulebook, 203, trade: 125));
            Supplementals.Add(new Supplemental("Manufactorum", HullType.AllCruiser, 2, 1, 2, RuleBook.BattlefleetKoronus, 39, null, "+10 to Extended Repair Tests and Acquisition Tests for Repairs. May be able to produce small numbers of personal items", trade: 10, max: 1));
            Supplementals.Add(new Supplemental("Medicae Deck", HullType.All, 2, 1, 1, RuleBook.BattlefleetKoronus, 39, null, "+20 to all Medicae Tests taken within this component. The number of patients may be up to triple the healer's Intelligence Bonus instead of double", max: 1));
            Supplementals.Add(new Supplemental("Melodium", HullType.All, 1, 1, 1, RuleBook.BattlefleetKoronus, 39, null, "+10 to Social Skills Tests", morale: 1, max: 1));
            Supplementals.Add(new Supplemental("Micro Laser Defense Grid", HullType.All, 2, 0, 2, RuleBook.CoreRulebook, 208, null, "External", turrets: 2, comp: ComponentOrigin.Xenotech));
            Supplementals.Add(new Supplemental("Minelayer Bay", HullType.Transport | HullType.AllCruiser, 1, 4, 1, RuleBook.BattlefleetKoronus, 38, null, "May deploy a 4VUx4VUx4VU minefield behind the vessel with a +20 Tech-Use Test as an Extended Action (Even during Stern Chases). Holds enough to deploy 3 mine fields before requiring restocking with an upkeep test at a suitable port. Minefields may not overlap."));
            Supplementals.Add(new Supplemental("Munitorium", ~HullType.AllCruiser, 2, 3, 2, RuleBook.CoreRulebook, 205, null, "If this component is damaged, it explodes. The ship takes 2d5 Hull Integrity damage and a component of the GM's choise is set on fire.", macrodamage: 1, military: 25, max: 1));
            Supplementals.Add(new Supplemental("Munitorium", HullType.AllCruiser, 3, 4, 2, RuleBook.CoreRulebook, 205, null, "If this component is damaged, it explodes. The ship takes 2d5 Hull Integrity damage and a component of the GM's choise is set on fire.", macrodamage: 1, military: 25, max: 1));
            Supplementals.Add(new Supplemental("Murder Servitors", HullType.All, 1, 1, 2, RuleBook.CoreRulebook, 206, null, "+20 to Opposed Command when conducting a Hit and Run Action. When determining the critical hit inflicted by a Hit and Run Action they participated in the raid may select a result between 1 and 6 instead of rolling", max: 1));
            Supplementals.Add(new Supplemental("Null Bay", HullType.All, 1, 2, 1, RuleBook.HostileAcquisition, 73, null, "May hold up to 100 psykers in solitary confinement, imposing -60 to all Focus Power Tests made by them, or tests for other warp based powers such as Navigator Powers. Decrease morale on board the ship by 3 while active", criminal: 50, comp: ComponentOrigin.Archeotech));
            Supplementals.Add(new Supplemental("Observation Dome", HullType.All, 0, 1, 1, RuleBook.CoreRulebook, 206, morale: 1, exploration: 50, max: 1));
            Supplementals.Add(new Supplemental("Pharmacia", HullType.All, 1, 2, 2, RuleBook.HostileAcquisition, 72, null, "May use this component to Synthesise drugs requiring a Chymistry test with a difficulty equal to the compound's availability. May gain 50 achievement points for criminal or trade objectives where providing medical support or recreational pharmaceuticals could assist in negotiations", max: 1));
            Supplementals.Add(new Supplemental("Pilot's Chamber", HullType.All, 1, 1, 1, RuleBook.BattlefleetKoronus, 40, null, "+2 bonus to the craft rating of all squadrons aboard this starship", max: 1));
            Supplementals.Add(new Supplemental("Plasma Scoop", HullType.Raider | HullType.Frigate, 2, 3, 3, RuleBook.BattlefleetKoronus, 39, null, "May attempt a +0 Pilot+Manoeuvrability Test to gather fuel from a a gas giant. Failure causes 1d5 Hull Integrity damage per degree of failure, ignoring Void Shields. Success allows the vessel to stay at void for an additonal month without needing to refuel, and adds +25 achievement points to any objective that requires the ship to move or transport something", max: 1));
            Supplementals.Add(new Supplemental("Plasma Scoop", HullType.LightCruiser | HullType.LightCruiser | HullType.BattleCruiser, 3, 4, 3, RuleBook.BattlefleetKoronus, 39, null, "May attempt a +0 Pilot+Manoeuvrability Test to gather fuel from a a gas giant. Failure causes 1d5 Hull Integrity damage per degree of failure, ignoring Void Shields. Success allows the vessel to stay at void for an additonal month without needing to refuel, and adds +25 achievement points to any objective that requires the ship to move or transport something", max: 1));
            Supplementals.Add(new Supplemental("Power Ram", HullType.AllCruiser, 2, 0, 2, RuleBook.BattlefleetKoronus, 38, new DiceRoll(1, 0, 0), "External", max: 1));
            Supplementals.Add(new Supplemental("Recovery Chambers", HullType.All, 3, 1, 3, RuleBook.HostileAcquisition, 74, null, "A character in this is considered to pass one Medicae extended care test each hour if lightly wounded or each day if heavily or critically wounded, isntead of the usual time periods. After healing, during teh following game session if the character uses a fate point roll a d10: on a 1 or 2 the fate point is spent but it has no effect", comp: ComponentOrigin.Xenotech));
            Supplementals.Add(new Supplemental("Reinforced Interior Bulkheads", ~HullType.AllCruiser, 0, 2, 2, RuleBook.CoreRulebook, 203, hullint: 3));
            Supplementals.Add(new Supplemental("Reinforced Interior Bulkheads", HullType.AllCruiser, 0, 3, 2, RuleBook.CoreRulebook, 203, hullint: 3));
            Supplementals.Add(new Supplemental("Reinforced Prow", ~HullType.AllCruiser, 0, 2, 1, RuleBook.IntoTheStorm, 159, new DiceRoll(0, 1, 0), prowArmour: 2, max: 1));
            Supplementals.Add(new Supplemental("Reinforced Prow", HullType.AllCruiser, 0, 3, 1, RuleBook.IntoTheStorm, 159, new DiceRoll(0, 1, 0), prowArmour: 2, max: 1));
            Supplementals.Add(new Supplemental("Runecaster", HullType.All, 0, 1, 2, RuleBook.CoreRulebook, 208, special: "Warp journeys made while using this component take half the time; This component may never become unpowered", navigate: 20, comp: ComponentOrigin.Xenotech, max: 1));
            Supplementals.Add(new Supplemental("Salvage Systems", HullType.Transport | HullType.AllCruiser, 5, 3, 3, RuleBook.BattlefleetKoronus, 40, null, "For every week spent attached to a wreck, may make a -10 Tech-Use Test to attempt to remove an identified component. A success removes the component for storage or towing, a failure destroys the component. These may be used or sold", man: -5, max: 1));
            Supplementals.Add(new Supplemental("Sensorium", HullType.All, 1, 1, 2, RuleBook.HostileAcquisition, 72, null, "+10 to all Charm and Commerce tests conducted on this vessel if the subjects use the sensorium", morale: 2, max: 1));
            Supplementals.Add(new Supplemental("Shadowblind Bays", HullType.All, 3, 4, 2, RuleBook.IntoTheStorm, 159, null, "-40 to Scrutiny Tests to detect this bay while it is active", trade: 50, criminal: 75));
            Supplementals.Add(new Supplemental("Shadowfield", HullType.All, 8, 4, 4, RuleBook.HostileAcquisition, 75, null, "All ships firing at this vessel while this is active suffer -20 to ballistic skill tests. -30 to the pilot test to hit and run. +20 to Silent Running and any active or focussed augury against the vessel suffers -20. This vessel must decide if it will use its void shields or ghost shields at the beginning of combat", comp: ComponentOrigin.Xenotech));
            Supplementals.Add(new Supplemental("Ship's Stores", HullType.BattleShip | HullType.GrandCruiser | HullType.Transport, 1, 10, 2, RuleBook.BattlefleetKoronus, 27, null, "May hold components up to theur combined space value, allowing the crew to use them as replacements if a component is destroyed, through an extended repair. Extended Repairs also repairs two additional Hull Integrity."));
            Supplementals.Add(new Supplemental("Ship's Stores", HullType.LightCruiser | HullType.Cruiser, 1, 5, 2, RuleBook.BattlefleetKoronus, 27, null, "May hold components up to theur combined space value, allowing the crew to use them as replacements if a component is destroyed, through an extended repair. Extended Repairs also repairs two additional Hull Integrity."));
            Supplementals.Add(new Supplemental("Small Craft Repair Deck", HullType.AllCruiser, 2, 2, 1, RuleBook.BattlefleetKoronus, 40, null, "After any space combat where small craft are lost, make a -10 Tech-Use Test. For every degree of success on the test, two of these craft are recovered", max: 1));
            Supplementals.Add(new Supplemental("Spacedock Piers", HullType.Transport | HullType.GrandCruiser | HullType.BattleShip, 7, 14, 4, RuleBook.BattlefleetKoronus, 40, null, "When no moving, up to 4 vessels smaller than this vessel may dock with it. the stationary ship acts as a spae station for purpose of making full repairs or replenishing Morale and grants +10 to the Acquisition Test when making full repairs. May not mount any 'Broadside' weapons and this may not be taken by Transports if they have less than 50 space", trade: 100, max: 1));
            Supplementals.Add(new Supplemental("Suspension Chambers", ~HullType.AllCruiser, 2, 1, 2, RuleBook.HostileAcquisition, 73, null, "When activated, reduce vessel's Crew Population by 50 and Morale by 5. The device may be deactivated and regains the 50 Crew after a full day, but not the Morale. While active double the tiem a ship may remain at void without suffering Crew or Morale damage", comp: ComponentOrigin.Archeotech));
            Supplementals.Add(new Supplemental("Suspension Chambers", HullType.AllCruiser, 3, 2, 2, RuleBook.HostileAcquisition, 73, null, "When activated, reduce vessel's Crew Population by 50 and Morale by 5. The device may be deactivated and regains the 50 Crew after a full day, but not the Morale. While active double the tiem a ship may remain at void without suffering Crew or Morale damage", comp: ComponentOrigin.Archeotech));
            Supplementals.Add(new Supplemental("Teleportarium", HullType.All, 1, 1, 1, RuleBook.CoreRulebook, 207, null, "May perform hit and runs without the pilot test, however the target vessel's void shields must be brought down first. This then grants a +20 to the opposed command test", comp: ComponentOrigin.Archeotech));
            Supplementals.Add(new Supplemental("Temple-Shrine to the Emperor", HullType.All, 1, 1, 1, RuleBook.CoreRulebook, 205, morale: 3, creed: 100, max: 1));
            Supplementals.Add(new Supplemental("Tenebro-Maze", ~HullType.AllCruiser, 1, 2, 2, RuleBook.CoreRulebook, 205, null, "+10 to command tests when defending against Boarding or Hit and Run actions. When a component would be affected by a critical hit, the player gets to choose which one instead of the opponent", max: 1));
            Supplementals.Add(new Supplemental("Tenebro-Maze", HullType.AllCruiser, 2, 3, 2, RuleBook.CoreRulebook, 205, null, "+10 to command tests when defending against Boarding or Hit and Run actions. When a component would be affected by a critical hit, the player gets to choose which one instead of the opponent", max: 1));
            Supplementals.Add(new Supplemental("Trophy Room", HullType.All, 1, 1, 1, RuleBook.CoreRulebook, 206, trade: 50, criminal: 50, exploration: 50));
            Supplementals.Add(new Supplemental("Variable Figurehead", HullType.All, 1, 0, 2, RuleBook.HostileAcquisition, 71, null, "External; This component has 5 different design patterns for its figurehead which it may switch between to obfuscate the ship's identity", max: 1));
            Supplementals.Add(new Supplemental("Warp Antenna", HullType.All, 1, 0, 2, RuleBook.BattlefleetKoronus, 42, null, "External; +20 to test to Locate the Astronomicon, -10 to warp encounter rolls", comp: ComponentOrigin.Archeotech, max: 1));
            Supplementals.Add(new Supplemental("Warp Disrupter", HullType.All, 3, 0, 2, RuleBook.HostileAcquisition, 75, null, "To activate a psyker must take 2d5 damage to a body location of his choosing with reduction by toughness but not armour, plus 1d5-1 levels of fatigue. The psyker must then make a -10 Focus Power Test. All attempts at warp communication within the star system suffer a -10 penalty per degree of success", comp: ComponentOrigin.Xenotech));
            Supplementals.Add(new Supplemental("Warp Gate Map", HullType.All, 2, 1, 5, RuleBook.HostileAcquisition, 75, null, "Wehn approaching a warp gate, may make a +0 Navigate(Warp) test to locate the precise physical location. Once that is done a -20 Navigate(Warp) Test allows the ship to traverse the gate. On a success, the vessel reaches it's intended destination. On failure, the vessel went somewhere else. GM may decide if there is a warp gate nearby: either by plot or on a d10 roll of 7+ there is one within 2d10 days travel", comp: ComponentOrigin.Xenotech));
            Supplementals.Add(new Supplemental("Warp Sextant", HullType.All, 4, 1, 2, RuleBook.BattlefleetKoronus, 42, special: "Receives +20 to perception tests related to warp traavel as well as navigate warp bonuses", navigate: 20, comp: ComponentOrigin.Archeotech));
            Supplementals.Add(new Supplemental("Witch Augur", HullType.All, 1, 0, 2, RuleBook.HostileAcquisition, 73, null, "External; A Navigator may make a +0 Awareness Test, if successful he can identify nearby vessels or other large objects and estimate when and where they will transition out of the warp. For every degree of success he can sense objects within a range of about one hour's travel", comp: ComponentOrigin.Archeotech));
            Supplementals.Add(new Supplemental("Xenos Habitats", HullType.All, 2, 1, 1, RuleBook.IntoTheStorm, 159, null, "+10 bonus to Charm and Inquiry tests made with Xenos aboard this vessel. All objectives involving non-hostile dealings with xenos gain an additional 50 achievement points", morale: -2));
            Supplementals.Add(new Supplemental("Xenos Librarium", HullType.All, 1, 1, 3, RuleBook.HostileAcquisition, 75, null, "May receive +10 to Forbidden Lore(Xenos) Tests made aboard this vessel", comp: ComponentOrigin.Xenotech));
            Supplementals.Add(new Supplemental(@"XED1.11178e ""Ubertas"" Device", HullType.All, 3, 1, 0, RuleBook.FaithAndCoin, 105, morale: 5, special: "PRovides free food for the ship, crew never suffers malnutrition or starvation, +10 to Medicae Tests to Diagnose and Contain Shipborne Sicknesses, liable to attract attention.", comp: ComponentOrigin.Archeotech));
            Supplementals.Add(new Supplemental("The Grand Compendium", HullType.All, 1, 3, 0, RuleBook.FaithAndCoin, 107, special: "Once per game session, a character may spend a faith point to count all Common and Scholastic Lores as basic skill for the ramainder of the game session. May potentially be used to justify Elite advances in Commonand Scholastic Lores.", comp: ComponentOrigin.Archeotech));
            //End of Supplemental Components
            //Squadrons
            Squadrons = new List<Squadron>(14);
            Squadrons.Add(new Squadron("Fury Interceptor", Race.Human, 10, 10, 20, "When checking for squadron losses, this squadron reduces its losses by one ot a minimum of zero or gains +5 to the upkeep test"));
            Squadrons.Add(new Squadron("Starhawk Bomber", Race.Human, 0, 6, 10, "When checking for squadron losses, this squadron reduces its losses by one ot a minimum of zero or gains +5 to the upkeep test"));
            Squadrons.Add(new Squadron("Shark Assault Boat", Race.Human, 5, 10, 8));
            Squadrons.Add(new Squadron("Swiftdeath Fighter", Race.Chaos, 10, 11, 30, "When checking for squadron losses, this squadron increases its losses by 1 to the squadron maximum or suffers -5 on the Upkeep test"));
            Squadrons.Add(new Squadron("Doomfire Bomber", Race.Chaos, 0, 7, 15, "When checking for squadron losses, this squadron increases its losses by 1 to the squadron maximum or suffers -5 on the Upkeep test"));
            Squadrons.Add(new Squadron("Dreadclaw Assault Boat", Race.Chaos, 5, 11, 15));
            Squadrons.Add(new Squadron("Darkstar Fighter", Race.Eldar, 15, 12, 12, "Suffers no penalties for being below half strength"));
            Squadrons.Add(new Squadron("Eagle Bomber", Race.Eldar, 6, 9, 6, "Suffers no penalties for being below half strength"));
            Squadrons.Add(new Squadron("Bloodflayer", Race.Rakgol, 8, 9, 15, "May be used as fighter craft or assault craft, but craft rating drops to +4 when used as fighters"));
            Squadrons.Add(new Squadron("Fighta-bommerz", Race.Ork, 8, 8, 25, "May be used as fighter craft or bombers, but craft rating drops to +5 when used as bombers"));
            Squadrons.Add(new Squadron("Assault Boats", Race.Ork, 8, 10, 15));
            Squadrons.Add(new Squadron("Raptor Interceptors", Race.DarkEldar, 15, 12, 12, "Suffers no penalties for being below half strength, negate any Turret Rating bonuses that the target ship would normally receive when attempting to shoot down these craft in an incoming attack wave", RuleBook.SoulReaver, 136));
            Squadrons.Add(new Squadron("Tormentor Bombers", Race.DarkEldar, 6, 9, 6, "Suffers no penalties for being below half strength, negate any Turret Rating bonuses that the target ship would normally receive when attempting to shoot down these craft in an incoming attack wave", RuleBook.SoulReaver, 136));
            Squadrons.Add(new Squadron("Slavebringer Assault Boats", Race.DarkEldar, 11, 12, 5, "Suffers no penalties for being below half strength, negate any Turret Rating bonuses that the target ship would normally receive when attempting to shoot down these craft in an incoming attack wave", RuleBook.SoulReaver, 136));
            //End of Squadrons
        }
    }
}
