﻿using System;
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
        /// All the squadrons this landing bay can fit
        /// </summary>
        public List<Squadron> Squadrons;
        /// <summary>
        /// Landing Bays don't do damage but as they're a weapon, 0 is listed
        /// </summary>
        public override DiceRoll Damage
        {
            get
            {
                return RawDamage;
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
        /// <param name="name">name of the landing bay</param>
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
        public LandingBay(string name, HullType hulls, WeaponSlot slots, int power, int space, int sp, int str,
            RuleBook origin, byte page, Quality quality = Quality.Common, WeaponQuality wq = WeaponQuality.None,
            string special = null, ComponentOrigin comp = ComponentOrigin.Standard, Condition cond = Condition.Intact)
            : base(name, WeaponType.LandingBay, hulls, slots, power, space, sp, str, default(DiceRoll), 0, 0, origin, page, quality, wq, special, Quality.None, comp, cond) 
        {
            Squadrons = new List<Squadron>(Strength * 3);
        }

        /// <summary>
        /// Serialises the Landing Bay
        /// </summary>
        /// <returns>JSON object as string</returns>
        public override string ToJSON()
        {
            /*{
             * "Landing" : {
             *  "Name" : name,
             *  "Types" : types,
             *  "Slots" : slots,
             *  "Power" : power,
             *  "Space" : space,
             *  "SP" : sp,
             *  "Str" : str,
             *  "Origin" : origin,
             *  "Page" : page,
             *  "Quality" : quality,
             *  "WeapQual" : wq,
             *  "Special" : special,
             *  "Comp" : comp,
             *  "Squadrons": [squadrons],
             *  "Cond" : Conditions}
             *}
             */
            StringBuilder output = new StringBuilder(@"{""Landing"":{""Name"":""" + Name.Escape() + @""",""Types"":" + (byte)HullTypes + @",""Slots"":" + (byte)Slots
                + @",""Power"":" + RawPower + @",""Space"":" + RawSpace + @",""SP"":" + RawSP + @",""Str"":" + Strength + @",""Origin"":"
                + (byte)Origin + @",""Page"":" + PageNumber + @",""Quality"":" + (byte)Quality + @",""WeapQual"":"
                + (byte)WeaponQuality + @",""Special"":""" + RawSpecial.Escape() + @""",""Comp"":" + (byte)ComponentOrigin + @",""Squadrons"":[");
            if (Squadrons != null)
            {
                for(int i = 0; i < Squadrons.Count; i++){
                    output.Append(Squadrons[i].ToJSON());
                    if (i < Squadrons.Count - 1)
                        output.Append(",");
                }
            }
            output.Append(@"],""Cond"":" + Condition + @"}}");
            return output.ToString();
        }

        /// <summary>
        /// Display no range for Landing Bays
        /// </summary>
        public override string DisplayRange
        {
            get
            {
                return null;
            }
        }
    }
}
