using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public static void SaveSSS(Starship ship, String path)
        {

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
