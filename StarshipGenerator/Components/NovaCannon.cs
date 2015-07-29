using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StarshipGenerator.Utils;

namespace StarshipGenerator.Components
{
    /// <summary>
    /// Class specific to Nova Cannons
    /// </summary>
    public class NovaCannon : Weapon
    {
        /// <summary>
        /// Nova cannon lists strength of none
        /// </summary>
        public override int Strength
        {
            get
            {
                return 0;
            }
        }
        /// <summary>
        /// Nova cannon lists crit of none
        /// </summary>
        public override int Crit
        {
            get
            {
                return 0;
            }
        }
        /// <summary>
        /// Min to max range of nova cannon
        /// </summary>
        public override string DisplayRange
        {
            get
            {
                return "6-" + Range;
            }
        }

        public NovaCannon(String name, HullType hulls, int power, int space, int sp,
            DiceRoll damage, int range, RuleBook origin, byte page, string special = null, Quality quality = Quality.Common,
            WeaponQuality wq = WeaponQuality.None, ComponentOrigin comp = ComponentOrigin.Standard)
            : base(name, WeaponType.NovaCannon, hulls, WeaponSlot.Prow, power, space, sp, 0, damage, 0, range, origin, page, quality, wq, special, Quality.None, comp) { }

        public NovaCannon(String name, HullType hulls, int power, int space, int sp,
            int range, RuleBook origin, byte page, string damage = null, string special = null, Quality quality = Quality.Common,
            WeaponQuality wq = WeaponQuality.None, ComponentOrigin comp = ComponentOrigin.Standard)
            : this(name, hulls, power, space, sp, new DiceRoll(damage), range, origin, page, special, quality, wq, comp) { }

        public override string Special
        {
            get
            {
                return "Always revealed by a successful active augury; " + base.Special;
            }
            set
            {
                base.Special = value;
            }
        }

        public override string ToJSON()
        {
            /*{
             * "NovaCannon" : {
             *  "Name" : name,
             *  "Hulls" : hulls,
             *  "Power" : power,
             *  "Space" : space,
             *  "SP" : sp,
             *  "Damage" : damage,
             *  "Range" : range,
             *  "Origin" : origin,
             *  "Page" : page,
             *  "Special" : special,
             *  "Quality" : quality,
             *  "WeapQual" : wq,
             *  "Comp" : comp }
             *}
             */
            return @"{""NovaCannon"":{""Name"":""" + Name.Escape() + @""",""Hulls"":" + (byte)HullTypes + @",""Power"":" + RawPower + @",""Space"":" + RawSpace + @",""SP"":" + RawSP
                + @",""Damage"":""" + RawDamage.ToString().Escape() + @""",""Range"":" + RawRange + @",""Origin"":" + (byte)Origin + @",""Page"":" + PageNumber
                + @",""Special"":""" + RawSpecial.Escape() + @""",""Quality"":" + (byte)Quality + @",""WeapQual"":" + (byte)WeaponQuality + @",""Comp"":" + (byte)ComponentOrigin + @"}}";
        }
    }
}
