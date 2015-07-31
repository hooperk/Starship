using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using StarshipGenerator.Components;
using StarshipGenerator.Utils;
using System.Reflection;

namespace StarshipGenerator.Utils
{
    /// <summary>
    /// Class with methods for Saving
    /// </summary>
    public static class Saver
    {
        /// <summary>
        /// Escape any double quotes from a string, and replace it with an empty string if it is null
        /// </summary>
        /// <param name="self">String to format</param>
        /// <returns>Formatted string ready for JSON</returns>
        public static string Escape(this string self)
        {
            if (self == null)
                return "";
            return self.Replace("\"", "\\\"");
        }

        /// <summary>
        /// Save a Starship in the old .sss format
        /// </summary>
        /// <param name="ship">Starship to save</param>
        /// <param name="path">Path of the file to save to</param>
        public static void SaveSSS(Starship ship, String path)
        {
            using (FileStream fs = File.OpenWrite(path))
            {
                SaveSSS(ship, fs);
            }
        }

        /// <summary>
        /// Save a Starship in the old .sss format
        /// </summary>
        /// <param name="ship">Starship to save</param>
        /// <param name="fs">Open filestream ot a file to write to</param>
        public static void SaveSSS(Starship ship, FileStream fs)
        {
            using (StreamWriter writer = new StreamWriter(fs))
            {
                string name;
                writer.WriteLine("version:" + Assembly.GetExecutingAssembly().GetName().Version);
                writer.WriteLine("name:" + ship.Name);
                writer.WriteLine("hull:" + ship.Hull.GetName());
                name = ship.PlasmaDrive.GetName();
                switch (name)
                {
                    case Names.SprintTrader:
                        name = Old.SprintTrader;
                        break;
                    case Names.EscortDrive:
                        name = Old.EscortDrive;
                        break;
                    case Names.WarcruiserDrive:
                        if (ship.Hull == null || (ship.Hull.HullTypes & HullType.CruiserPlus) > 0)
                            name += ", Large";
                        else
                            name += ", Small";
                        break;
                    case Names.MimicDrive:
                        if (ship.Hull == null || (ship.Hull.HullTypes & HullType.CruiserPlus) > 0)
                            name += ", Huge";
                        else if ((ship.Hull.HullTypes & HullType.LightCruiser) > 0)
                            name += ", Large";
                        else if ((ship.Hull.HullTypes & (HullType.Raider | HullType.Frigate)) > 0)
                            name += ", Medium";
                        else
                            name += ", Small";
                        break;
                    case Names.Viperdrive:
                        name = Old.Viperdrive;
                        break;
                }
                writer.WriteLine("plasma:" + name);
                writer.WriteLine("warp:" + ship.WarpDrive.GetName());
                writer.WriteLine("gellar:" + ship.GellarField.GetName());
                name = ship.VoidShield.GetName();
                if(name.Equals(Names.RepulsorShield))
                    name = Old.RepulsorShield;
                writer.WriteLine("void:" + name);
                name = ship.ShipBridge.GetName();
                switch (name)
                {
                    case Names.ArmouredBridge:
                    case Names.BridgeOfAntiquity:
                    case Names.CombatBridge:
                    case Names.CommandBridge:
                    case Names.ExplorationBridge:
                        if (ship.Hull == null || (ship.Hull.HullTypes & HullType.AllCruiser) > 0)
                            name += ", Large";
                        else
                            name += ", Small";
                        break;
                }
                writer.WriteLine("bridge:" + name);
                writer.WriteLine("life:" + ship.LifeSustainer.GetName() + (ship.Hull == null || (ship.Hull.HullTypes & HullType.AllCruiser) != 0 ? ", Large" : ", Small"));
                writer.WriteLine("quarters:" + ship.CrewQuarters.GetName() + (ship.Hull == null || (ship.Hull.HullTypes & HullType.AllCruiser) != 0 ? ", Large" : ", Small"));
                writer.WriteLine("augur:" + ship.AugurArrays.GetName());
                String[] qualities = new String[14];
                if (ship.PlasmaDrive != null)
                {
                    switch (ship.PlasmaDrive.Quality)
                    {
                        case Quality.Poor:
                            qualities[0] = "Poor";
                            break;
                        case Quality.Good:
                            qualities[0] = "Good";
                            break;
                        case Quality.Slim:
                            qualities[0] = "Good";
                            qualities[8] = "Space";
                            break;
                        case Quality.Efficient:
                            qualities[0] = "Good";
                            qualities[8] = "Power";
                            break;
                        case Quality.Best:
                            qualities[0] = "Best";
                            break;
                    }
                }
                if (ship.WarpDrive != null)
                {
                    switch (ship.WarpDrive.Quality)
                    {
                        case Quality.Poor:
                            qualities[1] = "Poor";
                            break;
                        case Quality.Good:
                            qualities[1] = "Good";
                            break;
                        case Quality.Slim:
                            qualities[1] = "Good";
                            qualities[9] = "Space";
                            break;
                        case Quality.Efficient:
                            qualities[1] = "Good";
                            qualities[9] = "Power";
                            break;
                        case Quality.Best:
                            qualities[1] = "Best";
                            break;
                    }
                }
                if (ship.GellarField != null)
                {
                    switch (ship.GellarField.Quality)
                    {
                        case Quality.Poor:
                            qualities[2] = "Poor";
                            break;
                        case Quality.Good:
                        case Quality.Slim:
                        case Quality.Efficient:
                            qualities[2] = "Good";
                            break;
                        case Quality.Best:
                            qualities[2] = "Best";
                            break;
                    }
                }
                if (ship.VoidShield != null)
                {
                    switch (ship.VoidShield.Quality)
                    {
                        case Quality.Poor:
                            qualities[3] = "Poor";
                            break;
                        case Quality.Good:
                            qualities[3] = "Good";
                            break;
                        case Quality.Slim:
                            qualities[3] = "Good";
                            qualities[10] = "Space";
                            break;
                        case Quality.Efficient:
                            qualities[3] = "Good";
                            qualities[10] = "Power";
                            break;
                        case Quality.Best:
                            qualities[3] = "Best";
                            break;
                    }
                }
                if (ship.ShipBridge != null)
                {
                    switch (ship.ShipBridge.Quality)
                    {
                        case Quality.Poor:
                            qualities[4] = "Poor";
                            break;
                        case Quality.Good:
                            qualities[4] = "Good";
                            break;
                        case Quality.Slim:
                            qualities[4] = "Good";
                            qualities[11] = "Space";
                            break;
                        case Quality.Efficient:
                            qualities[4] = "Good";
                            qualities[11] = "Power";
                            break;
                        case Quality.Best:
                            qualities[4] = "Best";
                            break;
                    }
                }
                if (ship.LifeSustainer != null)
                {
                    switch (ship.LifeSustainer.Quality)
                    {
                        case Quality.Poor:
                            qualities[5] = "Poor";
                            break;
                        case Quality.Good:
                            qualities[5] = "Good";
                            break;
                        case Quality.Slim:
                            qualities[5] = "Good";
                            qualities[12] = "Space";
                            break;
                        case Quality.Efficient:
                            qualities[5] = "Good";
                            qualities[12] = "Power";
                            break;
                        case Quality.Best:
                            qualities[5] = "Best";
                            break;
                    }
                }
                if (ship.CrewQuarters != null)
                {
                    switch (ship.CrewQuarters.Quality)
                    {
                        case Quality.Poor:
                            qualities[6] = "Poor";
                            break;
                        case Quality.Good:
                            qualities[6] = "Good";
                            break;
                        case Quality.Slim:
                            qualities[6] = "Good";
                            qualities[13] = "Space";
                            break;
                        case Quality.Efficient:
                            qualities[6] = "Good";
                            qualities[13] = "Power";
                            break;
                        case Quality.Best:
                            qualities[6] = "Best";
                            break;
                    }
                }
                if (ship.AugurArrays != null)
                {
                    switch (ship.AugurArrays.Quality)
                    {
                        case Quality.Poor:
                            qualities[7] = "Poor";
                            break;
                        case Quality.Good:
                        case Quality.Slim:
                        case Quality.Efficient:
                            qualities[7] = "Good";
                            break;
                        case Quality.Best:
                            qualities[7] = "Best";
                            break;
                    }
                }
                writer.WriteLine("plasmaquality:" + qualities[0].Escape());
                writer.WriteLine("warpquality:" + qualities[1].Escape());
                writer.WriteLine("gellarquality:" + qualities[2].Escape());
                writer.WriteLine("voidquality:" + qualities[3].Escape());
                writer.WriteLine("bridgequality:" + qualities[4].Escape());
                writer.WriteLine("lifequality:" + qualities[5].Escape());
                writer.WriteLine("quarterquality:" + qualities[6].Escape());
                writer.WriteLine("augurquality:" + qualities[7].Escape());
                writer.WriteLine("plasmachoice:" + qualities[8].Escape());
                writer.WriteLine("warpchoice:" + qualities[9].Escape());
                writer.WriteLine("gellarchoice:");
                writer.WriteLine("voidchoice:" + qualities[10].Escape());
                writer.WriteLine("bridgechoice:" + qualities[11].Escape());
                writer.WriteLine("lifechoice:" + qualities[12].Escape());
                writer.WriteLine("quarterchoice:" + qualities[13].Escape());
                writer.WriteLine("augurchoice:");
                writer.WriteLine("crew:" + ship.CrewRace.Name());
                writer.WriteLine("crewrating:" + (ship.CrewRace == Race.Servitor ? ((ServitorQuality)ship.CrewRating).ToString() : ((CrewRating)ship.CrewRating).ToString()));
                writer.WriteLine("crewmod:" + ship.GMCrewRating);
                //weapons, need to redo ordering when converting
                Weapon[] weapons = ship.Weapons.Reverse().ToArray();//old ship sheet ordered weaposn in opposite way to new ship generator
                int i = 0;
                for (; i < weapons.Length; i++)
                {
                    name = "";
                    String quality = "";
                    String weapq1 = "";
                    String weapq2 = "";
                    String turbo = "";
                    if (weapons[i] != null)
                    {
                        name = weapons[i].Name;
                        switch (weapons[i].Quality)
                        {
                            case Quality.Poor:
                                quality = "Poor";
                                break;
                            case Quality.Good:
                            case Quality.Slim:
                            case Quality.Efficient:
                                quality = "Good";
                                break;
                            case Quality.Best:
                                quality = "Best";
                                break;
                        }
                        //weaponqualities parsing
                        String[] weaponqualities = weapons[i].WeaponQuality.ToString().Replace(" ","").Split(new char[]{','});
                        if (weaponqualities.Length > 0)
                            weapq1 = weaponqualities[0];
                        if (weaponqualities.Length > 1)
                            weapq2 = weaponqualities[1];
                        if (weapons[i].TurboWeapon != Quality.None)
                            turbo = weapons[i].TurboWeapon.ToString();
                    }
                    writer.WriteLine("weapon{0}:{1}", i+1, name);
                    writer.WriteLine("weapon{0}quality:{1}", i+1, quality);
                    writer.WriteLine("w{0}q1:{1}", i+1, (weapq1.Equals("Crit") ? "Crit Rating" : weapq1));
                    writer.WriteLine("w{0}q2:{1}", i+1, (weapq2.Equals("Crit") ? "Crit Rating" : weapq2));
                    writer.WriteLine("weap{0}mod:{1}", i+1, turbo);
                }
                for (; i < 6; i++)
                {
                    writer.WriteLine("weapon{0}:", i+1);
                    writer.WriteLine("weapon{0}quality:", i+1);
                    writer.WriteLine("w{0}q1:", i+1);
                    writer.WriteLine("w{0}q2:", i+1);
                    writer.WriteLine("weap{0}mod:", i+1);
                }
                writer.WriteLine("machine:" + ship.MachineSpirit.Name());
                writer.WriteLine("history:" + ship.ShipHistory.Name());
                writer.WriteLine("arrestor:" + (ship.ArresterEngines != Quality.None ? ship.ArresterEngines.ToString() : ""));
                writer.WriteLine("cherubim:" + (ship.CherubimAerie != Quality.None ? ship.CherubimAerie.ToString() : ""));
                writer.WriteLine("improvements:" + (ship.CrewImprovements != Quality.None ? ship.CrewImprovements.ToString() : ""));
                writer.WriteLine("disciplinarium:" + (ship.Disciplinarium != Quality.None ? ship.Disciplinarium.ToString() : ""));
                writer.WriteLine("distribute:" + (ship.DistributedCargoHold != Quality.None ? ship.DistributedCargoHold.ToString() : ""));
                writer.WriteLine("mimic:" + (ship.MimicDrive != Quality.None ? ship.MimicDrive.ToString() : ""));
                writer.WriteLine("ostentatious:" + (ship.OstentatiousDisplayOfWealth != Quality.None ? ship.OstentatiousDisplayOfWealth.ToString() : ""));
                writer.WriteLine("overload:" + (ship.OverloadShieldCapacitors != Quality.None ? ship.OverloadShieldCapacitors.ToString() : ""));
                writer.WriteLine("resolution:" + (ship.ResolutionArena != Quality.None ? ship.ResolutionArena.ToString() : ""));
                writer.WriteLine("secondary:" + (ship.SecondaryReactor != Quality.None ? ship.SecondaryReactor.ToString() : ""));
                writer.WriteLine("starchart:" + (ship.StarchartCollection != Quality.None ? ship.StarchartCollection.ToString() : ""));
                writer.WriteLine("trooper:" + (ship.StormTrooperDetachment != Quality.None ? ship.StormTrooperDetachment.ToString() : ""));
                writer.WriteLine("superior:" + (ship.SuperiorDamageControl != Quality.None ? ship.SuperiorDamageControl.ToString() : ""));
                writer.WriteLine("targeting:" + (ship.TargettingMatrix != Quality.None ? ship.TargettingMatrix.ToString() : ""));
                writer.WriteLine("vaulted:" + (ship.VaultedCeilings != Quality.None ? ship.VaultedCeilings.ToString() : ""));
                //If I add atomics recordable, display here
                writer.WriteLine
                    (
@"atomics:
atomicquality:
tno1:
torpedo1:
guidance1:
tno2:
torpedo2:
guidance2:
tno3:
torpedo3:
guidance3:
tno4:
torpedo4:
guidance4:
tno5:
torpedo5:
guidance5:
tno6:
torpedo6:
guidance6:
cno1:2
craft1:Fury Interceptor
cno2:2
craft2:Starhawk Bomber
cno3:8
craft3:Shark Assault Boat"
                    );
                writer.WriteLine("background:" + ship.Background.Name());
                writer.WriteLine("hullloss:" + (int)(ship.Background & Background.PlanetBoundForMillenia));
                //line 106
                String[] customhull = new String[17];
                if (ship.Hull != null && ship.Hull.Origin == RuleBook.Custom)
                {
                    customhull[0] = ship.Hull.Name;
                    customhull[1] = ship.Hull.Speed.ToString();
                    customhull[2] = ship.Hull.Manoeuvrability.ToString();
                    customhull[3] = ship.Hull.DetectionRating.ToString();
                    customhull[4] = ship.Hull.HullIntegrity.ToString();
                    customhull[5] = ship.Hull.Armour.ToString();
                    customhull[6] = ship.Hull.TurretRating.ToString();
                    customhull[7] = ship.Hull.RawSpace.ToString();
                    customhull[8] = ship.Hull.RawSP.ToString();
                    if ((ship.Hull.HullTypes & HullType.BattleShip) > 0)
                        customhull[9] = "Battleship";
                    else if ((ship.Hull.HullTypes & HullType.GrandCruiser) > 0)
                        customhull[9] = "Grand";
                    else if ((ship.Hull.HullTypes & HullType.BattleCruiser) > 0)
                        customhull[9] = "Battle";
                    else if ((ship.Hull.HullTypes & HullType.Cruiser) > 0)
                        customhull[9] = "Cruiser";
                    else if ((ship.Hull.HullTypes & HullType.LightCruiser) > 0)
                        customhull[9] = "Light";
                    else if ((ship.Hull.HullTypes & HullType.Frigate) > 0)
                        customhull[9] = "Frigate";
                    else if ((ship.Hull.HullTypes & HullType.Raider) > 0)
                        customhull[9] = "Raider";
                    else if ((ship.Hull.HullTypes & HullType.Transport) > 0)
                        customhull[9] = "Transport";
                    customhull[10] = ship.Hull.RawSpecial;
                    i = 0;
                    for (; i < ship.Hull.AftSlots; i++)
                        customhull[11 + i] = "Aft";
                    for (; i < ship.Hull.AftSlots + ship.Hull.KeelSlots; i++)
                        customhull[11 + i] = "Keel";
                    for (; i < ship.Hull.AftSlots + ship.Hull.KeelSlots + ship.Hull.SideSlots; i++)
                        customhull[11 + i] = "Starboard";
                    for (; i < ship.Hull.AftSlots + ship.Hull.KeelSlots + (2 * ship.Hull.SideSlots); i++)
                        customhull[11 + i] = "Port";
                    for (; i < ship.Hull.AftSlots + ship.Hull.KeelSlots + (2 * ship.Hull.SideSlots) + ship.Hull.DorsalSlots; i++)
                        customhull[11 + i] = "Dorsal";
                    for (; i < ship.Hull.AftSlots + ship.Hull.KeelSlots + (2 * ship.Hull.SideSlots) + ship.Hull.DorsalSlots + ship.Hull.ProwSlots; i++)
                        customhull[11 + i] = "Prow";
                }
                writer.WriteLine("customhullname:" + customhull[0]);
                writer.WriteLine("customhullspeed:" + customhull[1]);
                writer.WriteLine("customhullman:" + customhull[2]);
                writer.WriteLine("customhulldet:" + customhull[3]);
                writer.WriteLine("customhullint:" + customhull[4]);
                writer.WriteLine("customhullarmour:" + customhull[5]);
                writer.WriteLine("customhullturret:" + customhull[6]);
                writer.WriteLine("customhullspace:" + customhull[7]);
                writer.WriteLine("customhullsp:" + customhull[8]);
                writer.WriteLine("customhullclass:" + customhull[9]);
                writer.WriteLine("customhullspecial:" + customhull[10]);
                int weaponcount = ship.Weapons.Length;
                for(i = 0; i < 6; i++)
                {
                    writer.WriteLine("customslot{0}:{1}", i+1, customhull[11+i]);//include the weapon slots form the hull
                    String[] customweapon = new String[11];
                    if (i < weaponcount)
                    {
                        Weapon current = ship.Weapons[weaponcount - 1 - i];
                        if (current != null)
                        {
                            customweapon[0] = current.Name;
                            customweapon[1] = current.Type.Name();
                            customweapon[2] = current.RawStrength.ToString();
                            customweapon[3] = new DiceRoll(current.RawDamage.d10, current.RawDamage.d5, 0).ToString();//get just dice portion of roll
                            customweapon[4] = current.RawDamage.Modifier.PrintInt();
                            customweapon[5] = current.RawRange.PrintInt();
                            customweapon[6] = current.RawCrit.PrintInt();
                            customweapon[7] = current.RawSpace.PrintInt();
                            customweapon[8] = current.RawSP.PrintInt();
                            customweapon[9] = current.RawPower.PrintInt();
                            customweapon[10] = current.RawSpecial;
                        }
                    }
                    writer.WriteLine("customweapon{0}:{1}", i + 1, customweapon[0]);
                    writer.WriteLine("customweapon{0}type:{1}", i + 1, customweapon[1]);
                    writer.WriteLine("customweapon{0}str:{1}", i + 1, customweapon[2]);
                    writer.WriteLine("customweapon{0}dice:{1}", i + 1, customweapon[3]);
                    writer.WriteLine("customweapon{0}damage:{1}", i + 1, customweapon[4]);
                    writer.WriteLine("customweapon{0}range:{1}", i + 1, customweapon[5]);
                    writer.WriteLine("customweapon{0}crit:{1}", i + 1, customweapon[6]);
                    writer.WriteLine("customweapon{0}space:{1}", i + 1, customweapon[7]);
                    writer.WriteLine("customweapon{0}sp:{1}", i + 1, customweapon[8]);
                    writer.WriteLine("customweapon{0}power:{1}", i + 1, customweapon[9]);
                    writer.WriteLine("customweapon{0}special:{1}", i + 1, customweapon[10]);
                }
                String[] customplasma = new String[5];
                if (ship.PlasmaDrive != null && ship.PlasmaDrive.Origin == RuleBook.Custom)
                {
                    customplasma[0] = ship.PlasmaDrive.Name;
                    customplasma[1] = ship.PlasmaDrive.RawPower.PrintInt();
                    customplasma[2] = ship.PlasmaDrive.RawSpace.PrintInt();
                    customplasma[3] = ship.PlasmaDrive.RawSP.PrintInt();
                    customplasma[4] = ship.PlasmaDrive.RawSpecial;
                }
                writer.WriteLine("customplasmaname:" + customplasma[0]);
                writer.WriteLine("customplasmapower:" + customplasma[1]);
                writer.WriteLine("customplasmaspace:" + customplasma[2]);
                writer.WriteLine("customplasmasp:" + customplasma[3]);
                writer.WriteLine("customplasmaspecial:" + customplasma[4]);
                String[] customwarp = new String[5];
                if (ship.WarpDrive != null && ship.WarpDrive.Origin == RuleBook.Custom)
                {
                    customwarp[0] = ship.WarpDrive.Name;
                    customwarp[1] = ship.WarpDrive.RawPower.PrintInt();
                    customwarp[2] = ship.WarpDrive.RawSpace.PrintInt();
                    customwarp[3] = ship.WarpDrive.RawSP.PrintInt();
                    customwarp[4] = ship.WarpDrive.RawSpecial;
                }
                writer.WriteLine("customwarpname:" + customwarp[0]);
                writer.WriteLine("customwarppower:" + customwarp[1]);
                writer.WriteLine("customwarpspace:" + customwarp[2]);
                writer.WriteLine("customwarpsp:" + customwarp[3]);
                writer.WriteLine("customwarpspecial:" + customwarp[4]);
                String[] customgellar = new String[4];
                if (ship.GellarField != null && ship.GellarField.Origin == RuleBook.Custom)
                {
                    customgellar[0] = ship.GellarField.Name;
                    customgellar[1] = ship.GellarField.RawPower.PrintInt();
                    customgellar[2] = ship.GellarField.RawSP.PrintInt();
                    customgellar[3] = ship.GellarField.RawSpecial;
                }
                writer.WriteLine("customgellarname:" + customgellar[0]);
                writer.WriteLine("customgellarpower:" + customgellar[1]);
                writer.WriteLine("customgellarsp:" + customgellar[2]);
                writer.WriteLine("customgellarspecial:" + customgellar[3]);
                String[] customvoid = new String[5];
                int customshields = 0;
                if (ship.VoidShield != null && ship.VoidShield.Origin == RuleBook.Custom)
                {
                    customvoid[0] = ship.VoidShield.Name;
                    customvoid[1] = ship.VoidShield.RawPower.PrintInt();
                    customvoid[2] = ship.VoidShield.RawSpace.PrintInt();
                    customvoid[3] = ship.VoidShield.RawSP.PrintInt();
                    customvoid[4] = ship.VoidShield.RawSpecial;
                    customshields += ship.VoidShield.Strength;
                }
                writer.WriteLine("customvoidname:" + customvoid[0]);
                writer.WriteLine("customvoidpower:" + customvoid[1]);
                writer.WriteLine("customvoidspace:" + customvoid[2]);
                writer.WriteLine("customvoidsp:" + customvoid[3]);
                writer.WriteLine("customvoidspecial:" + customvoid[4]);
                String[] custombridge = new String[5];
                if (ship.ShipBridge != null && ship.ShipBridge.Origin == RuleBook.Custom)
                {
                    custombridge[0] = ship.ShipBridge.Name;
                    custombridge[1] = ship.ShipBridge.RawPower.PrintInt();
                    custombridge[2] = ship.ShipBridge.RawSpace.PrintInt();
                    custombridge[3] = ship.ShipBridge.RawSP.PrintInt();
                    custombridge[4] = ship.ShipBridge.RawSpecial;
                }
                writer.WriteLine("custombridgename:" + custombridge[0]);
                writer.WriteLine("custombridgepower:" + custombridge[1]);
                writer.WriteLine("custombridgespace:" + custombridge[2]);
                writer.WriteLine("custombridgesp:" + custombridge[3]);
                writer.WriteLine("custombridgespecial:" + custombridge[4]);
                String[] customlife = new String[5];
                if (ship.LifeSustainer != null && ship.LifeSustainer.Origin == RuleBook.Custom)
                {
                    customlife[0] = ship.LifeSustainer.Name;
                    customlife[1] = ship.LifeSustainer.RawPower.PrintInt();
                    customlife[2] = ship.LifeSustainer.RawSpace.PrintInt();
                    customlife[3] = ship.LifeSustainer.RawSP.PrintInt();
                    customlife[4] = ship.LifeSustainer.RawSpecial;
                }
                writer.WriteLine("customlifename:" + customlife[0]);
                writer.WriteLine("customlifepower:" + customlife[1]);
                writer.WriteLine("customlifespace:" + customlife[2]);
                writer.WriteLine("customlifesp:" + customlife[3]);
                writer.WriteLine("customlifespecial:" + customlife[4]);
                String[] customcrew = new String[5];
                if (ship.CrewQuarters != null && ship.CrewQuarters.Origin == RuleBook.Custom)
                {
                    customcrew[0] = ship.CrewQuarters.Name;
                    customcrew[1] = ship.CrewQuarters.RawPower.PrintInt();
                    customcrew[2] = ship.CrewQuarters.RawSpace.PrintInt();
                    customcrew[3] = ship.CrewQuarters.RawSP.PrintInt();
                    customcrew[4] = ship.CrewQuarters.RawSpecial;
                }
                writer.WriteLine("customcrewname:" + customcrew[0]);
                writer.WriteLine("customcrewpower:" + customcrew[1]);
                writer.WriteLine("customcrewspace:" + customcrew[2]);
                writer.WriteLine("customcrewsp:" + customcrew[3]);
                writer.WriteLine("customcrewspecial:" + customcrew[4]);
                String[] customaugur = new String[4];
                if (ship.AugurArrays != null && ship.AugurArrays.Origin == RuleBook.Custom)
                {
                    customaugur[0] = ship.AugurArrays.Name;
                    customaugur[1] = ship.AugurArrays.RawPower.PrintInt();
                    customaugur[2] = ship.AugurArrays.RawSP.PrintInt();
                    customaugur[3] = ship.AugurArrays.RawSpecial;
                }
                writer.WriteLine("customaugurname:" + customaugur[0]);
                writer.WriteLine("customaugurpower:" + customaugur[1]);
                writer.WriteLine("customaugursp:" + customaugur[2]);
                writer.WriteLine("customaugurspecial:" + customaugur[3]);
                writer.WriteLine("custommachine:" + ship.GMMachineSpirit);
                writer.WriteLine("customhistory:" + ship.GMShipHistory);
                writer.WriteLine("customspeed:" + ship.GMSpeed.PrintInt());
                writer.WriteLine("customint:" + ship.GMHullIntegrity.PrintInt());
                writer.WriteLine("customdet:" + ship.GMDetection.PrintInt());
                writer.WriteLine("customman:" + ship.GMManoeuvrability.PrintInt());
                writer.WriteLine("customarmour:" + ship.GMArmour.PrintInt());
                writer.WriteLine("customturret:" + ship.GMTurretRating.PrintInt());
                writer.WriteLine("custommorale:" + ship.GMMorale.PrintInt());
                writer.WriteLine("customcrew:" + ship.GMCrewPopulation.PrintInt());
                customshields += ship.GMShields;
                writer.WriteLine("customshield:" + customshields.PrintInt());
                writer.WriteLine("customspecial:" + ship.GMSpecial);
                int powerused = 0;
                int powergen = 0;
                int space = 0;
                int sp = 0;
                StringBuilder componentspecial = new StringBuilder();
                foreach (Supplemental component in ship.SupplementalComponents)
                {
                    if (component.Origin == RuleBook.Custom)
                    {
                        if (component.PowerGenerated)
                            powergen += component.RawPower;
                        else
                            powerused += component.RawPower;
                        space += component.RawSpace;
                        sp += component.RawSP;
                        if (!(component.Name.Equals("Custom Components") || component.Name.Equals("Custom Generators")))
                        {
                            componentspecial.Append(component.Name + ":" + component.RawSpecial + ";");//if added by this program
                        }
                        else//if from ship sheet originally
                            componentspecial.Append(component.RawSpecial);
                    }
                }
                writer.WriteLine("customcomppower:" + powerused.PrintInt());
                writer.WriteLine("customrcompgenerate:" + powergen.PrintInt());
                writer.WriteLine("customcompspace:" + space.PrintInt());
                writer.WriteLine("customcompsp:" + sp.PrintInt());
                writer.WriteLine("customcomponents:" + sp.PrintInt());
                foreach (String compName in ship.SupplementalComponents.Where(x => x.Origin != RuleBook.Custom).Select(x => x.Name).Distinct())
                {
                    writer.WriteLine("{0}:{1}", compName, ship.SupplementalComponents.Where(x => x.Origin != RuleBook.Custom && x.Name.Equals(compName)).Count());//double check origin in case name overlap
                }
            }
        }

        /// <summary>
        /// Save a Starship in the new .lss format
        /// </summary>
        /// <param name="ship">Starship to save</param>
        public static void SaveLSS(Starship ship, String path)
        {

        }

        /// <summary>
        /// Print an int for saving, ie 0 = ""
        /// </summary>
        /// <param name="self">Integer to print</param>
        /// <returns>prints String.Empty if int is 0 otherwise the int</returns>
        public static String PrintInt(this int self)
        {
            if (self == 0)
                return String.Empty;
            else
                return self.ToString();
        }
    }
}
