using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StarshipGenerator.Ammo;

namespace StarshipGenerator.Components
{
    /// <summary>
    /// Torpedo Tubes Weapons
    /// </summary>
    public class TorpedoTubes : Weapon
    {
        /// <summary>
        /// List of the ammo added
        /// </summary>
        public List<Torpedo> Ammo;
        /// <summary>
        /// Maximum ammo capacity
        /// </summary>
        public int Capacity { get; private set; }
        /// <summary>
        /// Array of what torpedoes are in the tubes.
        /// </summary>
        public Torpedo[] Tubes;
        /// <summary>
        /// Torpedoes don't do damage but as they're a weapon, 0 is listed
        /// </summary>
        public override DiceRoll Damage
        {
            get
            {
                return _damage;
            }
        }
        /// <summary>
        /// Torpedo tubes don't have crit ratings, torpedoes do
        /// </summary>
        public override int Crit
        {
            get
            {
                return 0;
            }
        }
        /// <summary>
        /// Torpedo range is determined by torpedoes not the launchers
        /// </summary>
        public override int Range
        {
            get
            {
                return 0;
            }
        }

        /// <summary>
        /// Create a new Torpedo Tube
        /// </summary>
        /// <param name="hulls">class fo ship that can mount this weapon</param>
        /// <param name="slots">locatiosn where this weapon can be mounted</param>
        /// <param name="power">power used by this weapon</param>
        /// <param name="space">space used by this method</param>
        /// <param name="sp">cost of this weapon</param>
        /// <param name="str">strength of the weapon</param>
        /// <param name="capacity">total ammo capacity of the torpedo tube</param>
        /// <param name="origin">rulebook containing this weapon</param>
        /// <param name="page">page this weapon can be found on</param>
        /// <param name="quality">quality of this weapon</param>
        /// <param name="wq">enum declaring which qualities to be adjusted</param>
        /// <param name="special">special rules of this weapon</param>
        public TorpedoTubes(HullType hulls, WeaponSlot slots, int power, int space, int sp, int str,
            DiceRoll damage, int capacity, RuleBook origin, byte page, Quality quality = Quality.Common, WeaponQuality wq = WeaponQuality.None, string special = null)
            : base(WeaponType.TorpedoTube, hulls, slots, power, space, sp, str, default(DiceRoll), 0, 0, origin, page, quality, wq, special) 
        {
            this.Capacity = capacity;
            Ammo = new List<Torpedo>(Capacity);
            Tubes = new Torpedo[str];
        }
    }
}
