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
        
        public static void Main() 
        {
            Loader loader = new Loader();
            using (FileStream fs = File.Create("ComponentsAndAmmo.config"))
            using (StreamWriter sw = new StreamWriter(fs))
            {
                sw.Write(@"{""Items"":[");
                foreach (Component comp in loader.Hulls)
                    sw.Write(comp.ToJSON()+",");
                foreach (Component comp in loader.PlasmaDrives)
                    sw.Write(comp.ToJSON() + ",");
                foreach (Component comp in loader.WarpDrives)
                    sw.Write(comp.ToJSON()+",");
                foreach (Component comp in loader.GellarFields)
                    sw.Write(comp.ToJSON()+",");
                foreach (Component comp in loader.VoidShields)
                    sw.Write(comp.ToJSON() + ",");
                foreach (Component comp in loader.Bridges)
                    sw.Write(comp.ToJSON() + ",");
                foreach (Component comp in loader.LifeSustainers)
                    sw.Write(comp.ToJSON() + ",");
                foreach (Component comp in loader.CrewQuarters)
                    sw.Write(comp.ToJSON() + ",");
                foreach (Component comp in loader.AugurArrays)
                    sw.Write(comp.ToJSON() + ",");
                foreach (Component comp in loader.Weapons)
                    sw.Write(comp.ToJSON() + ",");
                foreach (Component comp in loader.Supplementals)
                    sw.Write(comp.ToJSON() + ",");
                for (int i = 0; i < loader.Squadrons.Count; i++)
                {
                    sw.Write(loader.Squadrons[i].ToJSON());
                    if (i < loader.Squadrons.Count - 1)
                        sw.Write(',');
                }
                sw.Write(@"]}");
            }
        }
    }
}
