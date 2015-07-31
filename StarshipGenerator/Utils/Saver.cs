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
                    writer.WriteLine("weapon{0}:{1}", i, name);
                    writer.WriteLine("weapon{0}quality:{1}", i, quality);
                    writer.WriteLine("w{0}q1:{1}", i, (weapq1.Equals("Crit") ? "Crit Rating" : weapq1));
                    writer.WriteLine("w{0}q2:{1}", i, (weapq2.Equals("Crit") ? "Crit Rating" : weapq2));
                    writer.WriteLine("weap{0}mod:{1}", i, turbo);
                }
                for (; i < 6; i++)
                {
                    writer.WriteLine("weapon{0}:", i);
                    writer.WriteLine("weapon{0}quality:", i);
                    writer.WriteLine("w{0}q1:", i);
                    writer.WriteLine("w{0}q2:", i);
                    writer.WriteLine("weap{0}mod:", i);
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
    }
}
