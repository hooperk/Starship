using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace StarshipGenerator.Utils
{
    /// <summary>
    /// Class for loading saved files or config. Also stores components loaded from config
    /// </summary>
    public class Loader
    {
        //Components
        //end components
        //Xeno-Hull Components
        //End Xeno-Hull Components

        public static Starship LoadLSS(String path)
        {
            using (FileStream fs = File.OpenRead(path))
            {
                return LoadLSS(fs);
            }
        }

        public static Starship LoadLSS(FileStream fs)
        {
            throw new NotImplementedException();
        }


        public static Starship LoadSSS(String path)
        {
            using (FileStream fs = File.OpenRead(path))
            {
                return LoadSSS(fs);
            }
        }

        public static Starship LoadSSS(FileStream fs)
        {
            throw new NotImplementedException();
        }
        
        //Modified -> Regular w/ Modified = true
        //Some components were renamed for move to c#
        //name.Replace("†","") for finding supplementals

        /// <summary>
        /// do fetches by name to update information in case of fixes
        /// </summary>
        public static void Update()
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
    }

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
        public static void SaveSSS(Starship ship)
        {

        }

        /// <summary>
        /// Save a Starship in the new .lss format
        /// </summary>
        /// <param name="ship">Starship to save</param>
        public static void SaveLSS(Starship ship)
        {
            
        }
    }
}
