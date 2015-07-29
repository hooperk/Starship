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
                //line 12 of save file
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
