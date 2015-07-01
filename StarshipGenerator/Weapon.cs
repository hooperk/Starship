using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarshipGenerator
{
    /// <summary>
    /// Weapon components
    /// </summary>
    public class Weapon : Component
    {
        /// <summary>
        /// Class of weapon
        /// </summary>
        public WeaponType Type { get; private set; }
        /// <summary>
        /// Slots the weapon can be used in
        /// </summary>
        public WeaponSlot Slots { get; private set; }
        /// <summary>
        /// Strength of the weapon
        /// </summary>
        public int Strength { get; private set; }
        /// <summary>
        /// Damage of the weapon
        /// </summary>
        public DiceRoll Damage { get; private set; }
        /// <summary>
        /// Crit ratign of the weapon
        /// </summary>
        public int Crit { get; private set; }
        /// <summary>
        /// Range of the weapon
        /// </summary>
        public int Range { get; private set; }

        /// <summary>
        /// Create a new weapon
        /// </summary>
        /// <param name="type">class of weapon</param>
        /// <param name="hulls">class fo ship that can mount this weapon</param>
        /// <param name="slots">locatiosn where this weapon can be mounted</param>
        /// <param name="power">power used by this weapon</param>
        /// <param name="space">space used by this method</param>
        /// <param name="sp">cost of this weapon</param>
        /// <param name="str">strength of the weapon</param>
        /// <param name="damage">damage of the weapon</param>
        /// <param name="crit">crit rating of the weapon</param>
        /// <param name="range">range of the weapon</param>
        /// <param name="origin">rulebook containing this weapon</param>
        /// <param name="page">page this weapon can be found on</param>
        /// <param name="quality">quality of this weapon</param>
        /// <param name="special">special rules of this weapon</param>
        public Weapon(WeaponType type, HullType hulls, WeaponSlot slots, int power, int space, int sp, int str,
            DiceRoll damage, int crit, int range, RuleBook origin, byte page, Quality quality = Quality.Common, string special = null)
            : base(sp, power, space, special, origin, page, hulls, quality)
        {
            this.Type = type;
            this.Slots = slots;
            this.Strength = str;
            this.Damage = damage;
            this.Crit = crit;
            this.Range = range;
        }

        /// <summary>
        /// Create a new weapon
        /// </summary>
        /// <param name="type">class of weapon</param>
        /// <param name="hulls">class fo ship that can mount this weapon</param>
        /// <param name="slots">locatiosn where this weapon can be mounted</param>
        /// <param name="power">power used by this weapon</param>
        /// <param name="space">space used by this method</param>
        /// <param name="sp">cost of this weapon</param>
        /// <param name="str">strength of the weapon</param>
        /// <param name="damage">damage of the weapon</param>
        /// <param name="crit">crit rating of the weapon</param>
        /// <param name="range">range of the weapon</param>
        /// <param name="origin">rulebook containing this weapon</param>
        /// <param name="page">page this weapon can be found on</param>
        /// <param name="quality">quality of this weapon</param>
        /// <param name="special">special rules of this weapon</param>
        public Weapon(WeaponType type, HullType hulls, WeaponSlot slots, int power, int space, int sp, int str,
            string damage, int crit, int range, RuleBook origin, byte page, Quality quality = Quality.Common, string special = null)
            : this(type, hulls, slots, power, space, sp, str, new DiceRoll(damage), crit, range, origin, page, quality, special) { }
    }
}
