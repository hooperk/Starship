﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StarshipGenerator.Utils;

namespace StarshipGenerator.Components
{
    /// <summary>
    /// Void shield essential component
    /// </summary>
    public class VoidShield : Component
    {
        /// <summary>
        /// Number of shields
        /// </summary>
        public int Strength { get; private set; }

        /// <summary>
        /// Createa new void shield
        /// </summary>
        /// <param name="name">name of the void shields</param>
        /// <param name="types">ship classes which can use these shields</param>
        /// <param name="power">power these shield use</param>
        /// <param name="space">space that these shields use</param>
        /// <param name="str">shield strength of these shields</param>
        /// <param name="origin">rulebook containing these shields</param>
        /// <param name="page">page number to find these shields</param>
        /// <param name="special">special rules for these shields</param>
        /// <param name="quality">quality of these shields</param>
        /// <param name="sp">cost of these shields</param>
        public VoidShield(string name, HullType types, int power, int space, int str, RuleBook origin, byte page, string special = null,
            Quality quality = Quality.Common, int sp = 0, ComponentOrigin comp = ComponentOrigin.Standard, Condition cond = Condition.Intact)
            : base(name, sp, power, space, special, origin, page, types, quality, comp, cond)
        {
            this.Strength = str;
        }

        /// <summary>
        /// Serialises the Void Shields
        /// </summary>
        /// <returns>JSON object as string</returns>
        public override string ToJSON()
        {
            /*{
             * "Shield" : {
             *  "Name" : name,
             *  "Types" : types,
             *  "Power" : power,
             *  "Space" : space,
             *  "Str" : str,
             *  "Origin" : origin,
             *  "Page" : page,
             *  "Special" : special,
             *  "Quality" : quality,
             *  "SP" : sp,
             *  "Comp" : comp,
             *  "Cond" : condition}
             *}
             */
            return @"{""Shield"":{""Name"":""" + Name.Escape() + @""",""Types"":" + (byte)HullTypes + @",""Power"":" + RawPower + @",""Space"":"
                + RawSpace + @",""Str"":" + Strength + @",""Origin"":" + (byte)Origin + @",""Page"":" + PageNumber + @",""Special"":"""
                + RawSpecial.Escape() + @""",""Quality"":" + (byte)Quality + @",""SP"":" + RawSP + @",""Comp"":" + (byte)ComponentOrigin + @",""Cond"":" + Condition + @"}}";
        }
    }
}
