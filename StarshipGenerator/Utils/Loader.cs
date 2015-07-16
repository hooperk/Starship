using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarshipGenerator.Utils
{
    /// <summary>
    /// Class for loading saved files or config. Also stores components loaded from config
    /// </summary>
    public class Loader
    {
        //public static Starship LoadLSS

        //public static Starship LoadSSS

        //public static Loader Load

        //public Loader
    }

    /// <summary>
    /// Class with methods for Saving
    /// </summary>
    public static class Saver
    {
        public static string Escape(this string self)
        {
            if (self == null)
                return "";
            return self.Replace("\"", "\\\"");
        }

    }
}
