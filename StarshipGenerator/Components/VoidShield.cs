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
            Quality quality = Quality.Common, int sp = 0)
            : base(name, sp, power, space, special, origin, page, types, quality)
        {
            this.Strength = str;
        }
    }
}