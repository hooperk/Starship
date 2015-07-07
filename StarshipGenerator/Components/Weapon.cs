using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarshipGenerator.Components
{
    //TODO: Add extra possible quality alterations
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
        /// <remarks>Accounts for quality</remarks>
        public int Strength
        {
            get
            {
                if ((WeaponQuality & WeaponQuality.Strength) > 0)
                {
                    switch (Quality)
                    {
                        case Quality.Poor:
                            return Math.Max(_strength - 1, 1);
                        case Quality.Best:
                            return _strength + 1;
                    }
                }
                return _range;
            }
            private set { _strength = value; }
        }
        private int _strength;
        /// <summary>
        /// Damage of the weapon
        /// </summary>
        /// <remarks>Accounts for quality</remarks>
        public virtual DiceRoll Damage
        {
            get
            {
                if ((WeaponQuality & WeaponQuality.Damage) > 0)
                {
                    switch (Quality)
                    {
                        case Quality.Poor:
                            return _damage - 1;
                        case Quality.Good:
                        case Quality.Best:
                            return _damage + 1;
                    }
                }
                return _damage;
            }
            private set { _damage = value; }
        }
        protected DiceRoll _damage;
        /// <summary>
        /// Crit ratign of the weapon
        /// </summary>
        /// <remarks>Accounts for quality</remarks>
        public virtual int Crit
        {
            get
            {
                if ((WeaponQuality & WeaponQuality.Crit) > 0)
                {
                    switch (Quality)
                    {
                        case Quality.Poor:
                            return _crit + 1;
                        case Quality.Best:
                            return Math.Max(_crit - 1, 1);
                    }
                }
                return _range;
            }
            private set { _crit = value; }
        }
        private int _crit;
        /// <summary>
        /// Range of the weapon
        /// </summary>
        /// <remarks>Accounts for quality</remarks>
        public virtual int Range
        {
            get
            {
                if ((WeaponQuality & WeaponQuality.Range) > 0)
                {
                    switch (Quality)
                    {
                        case Quality.Poor:
                            return Math.Max(_range - 1, 1);
                        case Quality.Good:
                        case Quality.Best:
                            return _range + 1;
                    }
                }
                return _range;
            }
            private set { _range = value; }
        }
        private int _range;
        /// <summary>
        /// Which stats are being affected by quality
        /// </summary>
        public WeaponQuality WeaponQuality { get; private set; }
        /// <summary>
        /// Power Used by weapon
        /// </summary>
        /// <remarks>Power isn't altered by quality</remarks>
        public override int Power
        {
            get
            {
                return _power;
            }
        }
        /// <summary>
        /// Space taken up by weapon
        /// </summary>
        /// <remarks>Include possible adjustment for quality</remarks>
        public override int Space
        {
            get
            {
                if ((WeaponQuality & WeaponQuality.Space) > 0)
                    return base.Space;
                return _space;
            }
            set
            {
                base.Space = value;
            }
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
        /// <param name="wq">enum declaring which qualities to be adjusted</param>
        /// <param name="special">special rules of this weapon</param>
        public Weapon(WeaponType type, HullType hulls, WeaponSlot slots, int power, int space, int sp, int str,
            DiceRoll damage, int crit, int range, RuleBook origin, byte page, Quality quality = Quality.Common, WeaponQuality wq = WeaponQuality.None, string special = null)
            : base(sp, power, space, special, origin, page, hulls, quality)
        {
            this.Type = type;
            this.Slots = slots;
            this.Strength = str;
            this.Damage = damage;
            this.Crit = crit;
            this.Range = range;
            this.WeaponQuality = wq;
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
        /// <param name="wq">enum declaring which qualities to be adjusted</param>
        /// <param name="special">special rules of this weapon</param>
        public Weapon(WeaponType type, HullType hulls, WeaponSlot slots, int power, int space, int sp, int str,
            string damage, int crit, int range, RuleBook origin, byte page, Quality quality = Quality.Common, WeaponQuality wq = WeaponQuality.None, string special = null)
            : this(type, hulls, slots, power, space, sp, str, new DiceRoll(damage), crit, range, origin, page, quality, wq, special) { }
    }
}
