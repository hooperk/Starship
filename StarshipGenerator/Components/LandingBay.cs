using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StarshipGenerator.Ammo;
using StarshipGenerator.Utils;

namespace StarshipGenerator.Components
{
    /// <summary>
    /// Landing Bay weapons
    /// </summary>
    public class LandingBay : Weapon
    {
        /// <summary>
        /// All the squadrons this landign bay can fit
        /// </summary>
        public List<Squadron> Squadrons;
        /// <summary>
        /// Landing Bays don't do damage but as they're a weapon, 0 is listed
        /// </summary>
        public override DiceRoll Damage
        {
            get
            {
                return _damage;
            }
        }
        /// <summary>
        /// Landing Bays don't have crit ratings
        /// </summary>
        public override int Crit
        {
            get
            {
                return 0;
            }
        }
        /// <summary>
        /// Squadron range is determined by squadrons not the launch bays
        /// </summary>
        public override int Range
        {
            get
            {
                return 0;
            }
        }

        /// <summary>
        /// Create a new Landing Bay
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
        public LandingBay(HullType hulls, WeaponSlot slots, int power, int space, int sp, int str,
            DiceRoll damage, RuleBook origin, byte page, Quality quality = Quality.Common, WeaponQuality wq = WeaponQuality.None, string special = null)
            : base(WeaponType.LandingBay, hulls, slots, power, space, sp, str, default(DiceRoll), 0, 0, origin, page, quality, wq, special) 
        {
            Squadrons = new List<Squadron>(Strength * 3);
        }
    }
}
