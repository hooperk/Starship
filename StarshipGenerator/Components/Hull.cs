using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarshipGenerator.Components
{
    public class Hull : Component
    {
        /// <summary>
        /// Base speed of the hull
        /// </summary>
        public int Speed { get; private set; }
        /// <summary>
        /// Base manoeuvrability of the hull
        /// </summary>
        public int Manoeuvrability { get; private set; }
        /// <summary>
        /// Base detection rating of the hull
        /// </summary>
        public int DetectionRating { get; private set; }
        /// <summary>
        /// Base integrity, or hit points, of the hull
        /// </summary>
        public int HullIntegrity { get; private set; }
        /// <summary>
        /// Base armour of the hull
        /// </summary>
        public int Armour { get; private set; }
        /// <summary>
        /// Base turret rating of the hull
        /// </summary>
        public int TurretRating { get; private set; }
        /// <summary>
        /// Number of prow(frontal) weapon slots
        /// </summary>
        public int ProwSlots { get; private set; }
        /// <summary>
        /// Number of dorsal(can shoot forward or sideways) weapon slots
        /// </summary>
        public int DorsalSlots { get; private set; }
        /// <summary>
        /// Number of weapon slots in each side
        /// </summary>
        /// <remarks>per side not combined</remarks>
        public int SideSlots { get; private set; }
        /// <summary>
        /// Number of Keel(can shoot everywhere) weapon slots
        /// </summary>
        public int KeelSlots { get; private set; }
        /// <summary>
        /// Number of Aft (rear) weapon slots
        /// </summary>
        public int AftSlots { get; private set; }
        /// <summary>
        /// Total number of weapon slots
        /// </summary>
        public int WeaponSlots
        {
            get
            {
                return ProwSlots + DorsalSlots + (SideSlots * 2) + KeelSlots + AftSlots;
            }
        }
        /// <summary>
        /// Default prow-mounted weapon
        /// </summary>
        public Weapon DefaultProw { get; private set; }
        /// <summary>
        /// Default weapon in each side slot
        /// </summary>
        public Weapon DefaultBroadside { get; private set; }
        /// <summary>
        /// Array of default components
        /// </summary>
        public Supplemental[] DefaultComponents { get; private set; }
        //image or at least image path if gonna show

        /// <summary>
        /// Create a new hull
        /// </summary>
        /// <param name="speed">base speed of the hull</param>
        /// <param name="man">base manoeuvrability of hull</param>
        /// <param name="det">base detection rating of hull</param>
        /// <param name="hullint">base hull integrity of the hull</param>
        /// <param name="armour">base armour value of the hull</param>
        /// <param name="space">base available space in hull</param>
        /// <param name="sp">cost of the hull</param>
        /// <param name="type">which type(s) this hull can mount components for</param>
        /// <param name="special">special rules for this hull</param>
        /// <param name="origin">rulebook this hull can be found in</param>
        /// <param name="page">page numvber to find the hull on</param>
        /// <param name="turrets">turret rating of the hull</param>
        /// <param name="prow">number of prow weapon slots</param>
        /// <param name="dorsal">number of dorsal weapon slots</param>
        /// <param name="port">number of port weapon slots</param>
        /// <param name="starboard">number of starboard weapon slots</param>
        /// <param name="keel">number of keel weapon slots</param>
        /// <param name="aft">number of aft weapon slots</param>
        public Hull(int speed, int man, int det, int hullint, int armour, int space, int sp, HullType type,
            String special, RuleBook origin, byte page, int turrets = 1, int prow = 0, int dorsal = 0,
            int side = 0, int keel = 0, int aft = 0, Weapon frontal = null, Weapon broadside = null, Supplemental[] comps = null)
            : base(sp, 0, space, special, origin, page, type)
        {
            this.Speed = speed;
            this.Manoeuvrability = man;
            this.DetectionRating = det;
            this.Armour = armour;
            this.TurretRating = turrets;
            this.ProwSlots = prow;
            this.DorsalSlots = dorsal;
            this.SideSlots = side;
            this.KeelSlots = keel;
            this.AftSlots = aft;
            this.DefaultProw = frontal;
            this.DefaultBroadside = broadside;
            this.DefaultComponents = comps;
        }
    }
}
