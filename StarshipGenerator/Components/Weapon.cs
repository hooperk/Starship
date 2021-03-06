﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StarshipGenerator.Utils;

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
        public virtual int Strength
        {
            get
            {
                if ((WeaponQuality & WeaponQuality.Strength) != 0)
                {
                    switch (Quality)
                    {
                        case Quality.Poor:
                            return Math.Max(RawStrength - 1, 1);
                        case Quality.Best:
                            return RawStrength + 1;
                    }
                }
                return RawStrength;
            }
            private set { RawStrength = value; }
        }
        public int RawStrength { get; private set; }
        /// <summary>
        /// Damage of the weapon
        /// </summary>
        /// <remarks>Accounts for quality</remarks>
        public virtual DiceRoll Damage
        {
            get
            {
                if ((WeaponQuality & WeaponQuality.Damage) != 0)
                {
                    switch (Quality)
                    {
                        case Quality.Poor:
                            return RawDamage - 1;
                        case Quality.Good:
                        case Quality.Best:
                            return RawDamage + 1;
                    }
                }
                return RawDamage;
            }
            private set { RawDamage = value; }
        }
        public DiceRoll RawDamage { get; protected set; }
        /// <summary>
        /// Crit rating of the weapon
        /// </summary>
        /// <remarks>Accounts for quality</remarks>
        public virtual int Crit
        {
            get
            {
                if ((WeaponQuality & WeaponQuality.Crit) != 0)
                {
                    switch (Quality)
                    {
                        case Quality.Poor:
                            return RawCrit + 1;
                        case Quality.Best:
                            return Math.Max(RawCrit - 1, 1);
                    }
                }
                return RawCrit;
            }
            private set { RawCrit = value; }
        }
        public int RawCrit { get; private set; }
        /// <summary>
        /// Range of the weapon
        /// </summary>
        /// <remarks>Accounts for quality</remarks>
        public virtual int Range
        {
            get
            {
                int mod = 0;
                if ((WeaponQuality & WeaponQuality.Range) != 0)
                {
                    switch (Quality)
                    {
                        case Quality.Poor:
                            mod -= 1;
                            break;
                        case Quality.Good:
                        case Quality.Best:
                            mod += 1;
                            break;
                    }
                }
                switch(TurboWeapon){
                    case Quality.Poor:
                        mod -= 1;
                        break;
                    case Quality.Good:
                    case Quality.Best:
                        mod += 1;
                        break;
                }
                return Math.Max(RawRange+mod, 1);//prevent potential <1 values
            }
            private set { RawRange = value; }
        }
        public int RawRange { get; private set; }
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
                return RawPower;
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
                return RawSpace;
            }
            set
            {
                base.Space = value;
            }
        }
        /// <summary>
        /// Quality of the Turbo-Weapon Upgrade
        /// </summary>
        public Quality TurboWeapon { get; set; }
        /// <summary>
        /// Quality of TargettingMatrix affecting this weapon
        /// </summary>
        public Quality TargettingMatrix { get; set; }
        /// <summary>
        /// Any special rules of the weapon
        /// </summary>
        public override string Special
        {
            get
            {
                StringBuilder output = new StringBuilder();
                int bonus = 0;
                if (TurboWeapon != Quality.None)
                {
                    output.Append("Ignore penalties for firing this weapon at double range; ");
                    if (TurboWeapon == Quality.Best)
                        bonus += 5;
                }
                if (TargettingMatrix == Quality.Poor)
                    bonus += 5;
                if(bonus != 0)
                    output.Append("+" + bonus + " Ballistic Skill Tests with this weapon; ");
                output.Append(base.Special);
                return output.ToString();
            }
            set
            {
                base.Special = value;
            }
        }

        /// <summary>
        /// Create a new weapon
        /// </summary>
        /// <param name="name">name of the weapon</param>
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
        /// <param name="turbo">Quality of turboweapon battery upgrade if applicable</param>
        public Weapon(string name, WeaponType type, HullType hulls, WeaponSlot slots, int power, int space, int sp, int str,
            DiceRoll damage, int crit, int range, RuleBook origin, byte page, Quality quality = Quality.Common,
            WeaponQuality wq = WeaponQuality.None, string special = null, Quality turbo = Quality.None, ComponentOrigin comp = ComponentOrigin.Standard, Condition cond = Condition.Intact)
            : base(name, sp, power, space, special, origin, page, hulls, quality, comp, cond)
        {
            this.Type = type;
            this.Slots = slots;
            this.Strength = str;
            this.Damage = damage;
            this.Crit = crit;
            this.Range = range;
            this.WeaponQuality = wq;
            this.TurboWeapon = turbo;
        }

        /// <summary>
        /// Create a new weapon
        /// </summary>
        /// <param name="name">name of the weapon</param>
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
        /// <param name="turbo">Quality of turboweapon battery upgrade if applicable</param>
        public Weapon(string name, WeaponType type, HullType hulls, WeaponSlot slots, int power, int space, 
            int sp, int str, string damage, int crit, int range, RuleBook origin, byte page, Quality quality = Quality.Common,
            WeaponQuality wq = WeaponQuality.None, string special = null, Quality turbo = Quality.None, ComponentOrigin comp = ComponentOrigin.Standard, Condition cond = Condition.Intact)
            : this(name, type, hulls, slots, power, space, sp, str, new DiceRoll(damage), crit, range, 
            origin, page, quality, wq, special, turbo, comp, cond) { }

        /// <summary>
        /// Serialises the component
        /// </summary>
        /// <returns>JSON object as string</returns>
        public override string ToJSON()
        {
            /*{
             * "Weapon": {
             *  "Name" : name,
             *  "WeapType" : type,
             *  "Types" : types,
             *  "Slots" : slots,
             *  "Power" : power,
             *  "Space" : space,
             *  "SP" : sp,
             *  "Str" : str,
             *  "Damage" : damage,
             *  "Crit" : crit,
             *  "Range" : range,
             *  "Origin" : origin,
             *  "Page" :  page,
             *  "Quality" : quality,
             *  "WeapQual" : wq,
             *  "Special" : special,
             *  "Turbo" : turbo,
             *  "Comp" : comp,
             *  "Cond" : Condition}
             *}
             */
            return @"{""Weapon"":{""Name"":""" + Name.Escape() + @""",""WeapType"":" + (byte)Type + @",""Types"":" + (byte)HullTypes
                + @",""Slots"":" + (byte)Slots + @",""Power"":" + RawPower + @",""Space"":" + RawSpace + @",""SP"":" + RawSP + @",""Str"":"
                + RawStrength + @",""Damage"":""" + RawDamage.ToString().Escape() + @""",""Crit"":" + RawCrit + @",""Range"":" + RawRange
                + @",""Origin"":" + (byte)Origin + @",""Page"":" + PageNumber + @",""Quality"":" + (byte)Quality
                + @",""Special"":""" + RawSpecial.Escape() + @""",""Turbo"":" + (byte)TurboWeapon + @",""Comp"":" + (byte)ComponentOrigin + @",""Cond"":" + Condition + @"}}";
        }

        /// <summary>
        /// Range of the weapon, as a string (mostly for Nova cannon's override)
        /// </summary>
        public virtual string DisplayRange
        {
            get
            {
                return Range.ToString();
            }
        }

        /// <summary>
        /// Checks if the two Components are equal
        /// </summary>
        /// <param name="obj">Object to check equality against</param>
        /// <returns>false if other object is null, is not a component or does not match</returns>
        /// <remarks>If a component has the same name, origin and quality (including weapon quality) it is the same.
        /// Currently inequalities could match between two custom components</remarks>
        public override bool Equals(object obj)
        {
            Weapon other = obj as Weapon;
            if (other == null)
                return false;
            if (this.GetType() != obj.GetType())
                return false;
            return (this.QualityName.Equals(other.QualityName) && this.HullTypes == other.HullTypes && this.Origin == other.Origin && this.WeaponQuality == other.WeaponQuality);
        }

        /// <summary>
        /// Retrieve the hash code of the Component
        /// </summary>
        /// <returns>Hash Code of this Component</returns>
        public override int GetHashCode()
        {
            return this.QualityName.GetHashCode() + (7 * this.HullTypes.GetHashCode()) + (13 * this.Origin.GetHashCode()) + (17 * this.WeaponQuality.GetHashCode());//checking equality off having the same name and the same quality
        }
    }
}
