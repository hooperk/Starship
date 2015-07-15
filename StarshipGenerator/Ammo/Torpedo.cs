using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StarshipGenerator.Utils;

namespace StarshipGenerator.Ammo
{
    /// <summary>
    /// Torpedo class representing Torpedo Tube ammo
    /// </summary>
    public class Torpedo
    {
        /// <summary>
        /// Warhead in the Torpedo
        /// </summary>
        public Warhead Warhead { get; private set; }
        /// <summary>
        /// Guidance system curfrently in the torpedo
        /// </summary>
        public Guidance GuidanceSystem;
        private static DiceRoll PlasmaWarhead = new DiceRoll(2, 0, 14);
        private static DiceRoll BoardingWarhead = new DiceRoll(2, 0, 11);
        private static DiceRoll MeltaWarhead = new DiceRoll(2, 0, 15);
        private static DiceRoll VirusWarhead = new DiceRoll(2, 0, 10);
        private static DiceRoll VortexWarhead = new DiceRoll(2, 0, 5);

        /// <summary>
        /// Create a new Torpedo
        /// </summary>
        /// <param name="warhead">Warhead in the Torpedo</param>
        /// <param name="system">Guidance system in the Torpedo</param>
        public Torpedo(Warhead warhead, Guidance system = Guidance.Standard)
        {
            this.Warhead = warhead;
            this.GuidanceSystem = system;
        }

        public string ToJSON()
        {
            /*{
             * "Torpedo" : {
             *  "Warhead" : warhead,
             *  "Guidance" : system }
             *}
             */
            return @"{""Torpedo"":{""Warhead"":" + (byte)Warhead + @",""Guidance"":" + (byte)GuidanceSystem + @"}}";
        }

        /// <summary>
        /// VUs the torpedo moves per turn
        /// </summary>
        public int Speed
        {
            get
            {
                if (GuidanceSystem == Guidance.ShortBurn)
                    return 15;
                return 10;
            }
        }

        /// <summary>
        /// Damage done by this torpedo
        /// </summary>
        public DiceRoll Damage
        {
            get
            {
                switch (Warhead)
                {
                    case Warhead.Boarding:
                        return BoardingWarhead;
                    case Warhead.Melta:
                        return MeltaWarhead;
                    case Warhead.Virus:
                        return VirusWarhead;
                    case Warhead.Vortex:
                        return VortexWarhead;
                    default:
                        return PlasmaWarhead;
                }
            }
        }

        /// <summary>
        /// Crit rating of this torpedo
        /// </summary>
        public int CritRating
        {
            get
            {
                switch (Warhead)
                {
                    case Warhead.Plasma:
                        return 10;
                    case Warhead.Melta:
                        return 9;
                    case Warhead.Vortex:
                        return 6;
                    default:
                        return 0;
                }
            }
        }

        /// <summary>
        /// Range of the torpedo
        /// </summary>
        public int Range
        {
            get
            {
                if (GuidanceSystem == Guidance.ShortBurn)
                    return 30;
                return 60;
            }
        }

        /// <summary>
        /// Torpedo ratign of this torpedo
        /// </summary>
        public int TorpedoRating
        {
            get
            {
                switch (GuidanceSystem)
                {
                    case Guidance.Seeking:
                        return 30;
                    case Guidance.ShortBurn:
                        return 15;
                    default:
                        return 20;
                }
            }
        }

        /// <summary>
        /// Terminal Penetration of the Warhead
        /// </summary>
        public int TerminalPenetration
        {
            get
            {
                switch (Warhead)
                {
                    case Warhead.Boarding:
                        return 2;
                    case Warhead.Melta:
                        return 4;
                    case Warhead.Virus:
                        return 1;
                    case Warhead.Vortex:
                        return 5;
                    default:
                        return 3;
                }
            }
        }

        public string Special
        {
            get
            {
                StringBuilder output = new StringBuilder("Terminal Penetration("+TerminalPenetration+")");
                switch (Warhead)
                {
                    case Warhead.Boarding:
                        output.Append("; If damage exceeds target's armour, instead of hull integrity damage, inflict immediate hit & run attack");
                        break;
                    case Warhead.Melta:
                        output.Append("; If damage exceeds target's armour, inflicts a Fire! critical in addition to any other effects. If the tubes armed with thes3e is destroyed, another component catches fire");
                        break;
                    case Warhead.Virus:
                        output.Append("; If damage exceeds target's armour, instead of hull integrity damage, the vessel suffers 3d10 crew damage and 2d10 morale damage. Effect continues each turn unless a -10 command test is passed. Damage is applied and test must be taken one per successful warhead hit");
                        break;
                    case Warhead.Vortex:
                        output.Append("; Ignores armour, each hit causes 1d5 morale damage as well as normal effects. If tubes armed wit hthese are damaged, coutn as destroyed and inflict 3 other critical hits to random locations of vessel");
                        break;
                }
                if (GuidanceSystem.Special() != null)
                    output.Append("; " + GuidanceSystem.Special());
                return output.ToString();
            }
        }
    }
}
