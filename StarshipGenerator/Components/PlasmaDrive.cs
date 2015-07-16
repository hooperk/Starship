﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StarshipGenerator.Utils;

namespace StarshipGenerator.Components
{
    /// <summary>
    /// Plasma drive essential component
    /// </summary>
    public class PlasmaDrive : Component
    {
        public override string Name
        {
            get
            {
                if (Modified && base.ComponentOrigin == ComponentOrigin.Standard)
                    return "Modified " + Name;
                return base.Name;
            }
        }
        /// <summary>
        /// Manoeuvrability modifier this drive grants
        /// </summary>
        public int Manoeuvrability { get; private set; }
        /// <summary>
        /// Speed modifier this drive grants
        /// </summary>
        public int Speed
        {
            get
            {
                if (Modified && base.ComponentOrigin == ComponentOrigin.Standard)
                    return _speed + 1;
                return _speed;
            }
            private set { _speed = value; }
        }
        private int _speed;
        /// <summary>
        /// Power supplied by component
        /// </summary>
        /// <remarks>Override for power generated modifier instead of power used</remarks>
        public override int Power
        {
            get
            {
                switch (this.Quality)
                {
                    case Quality.Poor:
                        return _power - 2;//poor quality generates 2 less instead of granting 1 more
                    case Quality.Good:
                    case Quality.Efficient:
                    case Quality.Best:
                        return _power + 1;
                    default:
                        return _power;
                }
            }
        }
        /// <summary>
        /// If the drive is a modified archeotech version
        /// </summary>
        public bool Modified { get; private set; }

        public override int Space
        {
            get
            {
                if (Modified && base.ComponentOrigin == ComponentOrigin.Standard)
                    return Space - 1;
                return base.Space;
            }
            set
            {
                base.Space = value;
            }
        }
        /// <summary>
        /// Whether the drive is archeotech, xenotech or standard
        /// </summary>
        public override ComponentOrigin ComponentOrigin
        {
            get
            {
                if (Modified && base.ComponentOrigin == ComponentOrigin.Standard)
                    return ComponentOrigin.Archeotech;
                return base.ComponentOrigin;
            }
        }

        /// <summary>
        /// Create a new plasma drive
        /// </summary>
        /// <param name="name">name of the plasma drive</param>
        /// <param name="types">hull classes which can use this drive</param>
        /// <param name="power">power generated by this drive</param>
        /// <param name="space">space taken by this drive</param>
        /// <param name="special">special rules for this drive</param>
        /// <param name="origin">rulebook which contains this drive</param>
        /// <param name="page">page to find the drive on</param>
        /// <param name="sp">cost of this drive</param>
        /// <param name="quality">quality of this drive</param>
        /// <param name="speed">speed modifier of this drive</param>
        /// <param name="man">manoeuvrability modifier of this drive</param>
        public PlasmaDrive(string name, HullType types, int power, int space, string special, RuleBook origin, byte page, int sp = 0,
            Quality quality = Quality.Common, int speed = 0, int man = 0, ComponentOrigin comp = ComponentOrigin.Standard, bool modified = false)
            : base(name, sp, power, space, special, origin, page, types, quality, comp)
        {
            this.Manoeuvrability = man;
            this.Speed = speed;
            Modified = modified;
        }

        /// <summary>
        /// Serialises the component
        /// </summary>
        /// <returns>JSON object as string</returns>
        public override string ToJSON()
        {
            /*{
             * "Plasma" : {
             *  "Name" : name,
             *  "Types" : types,
             *  "Power" : power,
             *  "Space" : space,
             *  "Special" : special,
             *  "Origin" : origin,
             *  "Page" : page,
             *  "SP" : sp,
             *  "Quality" : quality,
             *  "Speed" : speed,
             *  "Man" : man,
             *  "Comp" : comp,
             *  "Mod" : mod}
             *}
             */
            return @"{""Plasma"":{""Name"":""" + base.Name.Escape() + @""",""Types"":" + (byte)HullTypes + @",""Power"":" + Power + @",""Space"":"
                + Space + @",""Special"":""" + Special.Escape() + @""",""Origin"":" + (byte)Origin + @",""Page"":" + PageNumber + @",""SP"":"
                + SP + @",""Quality"":" + (byte)Quality + @",""Speed"":" + Speed + @",""Man"":" + Manoeuvrability + @",""Comp"":"
                + (byte)ComponentOrigin + @",""Mod"":" + (Modified ? 1 : 0) + @"}}";
        }

        /// <summary>
        /// Description of the Plasma Drive to display while picking
        /// </summary>
        public override string Description
        {
            get
            {
                StringBuilder output = new StringBuilder();
                if (Speed > 0)
                    output.Append("+" + Speed + " Speed; ");
                else if (Speed > 0)
                    output.Append("+" + Speed + " Speed; ");
                if (Manoeuvrability > 0)
                    output.Append("+" + Manoeuvrability + " Manoeuvrability; ");
                else if (Manoeuvrability < 0)
                    output.Append(Manoeuvrability + " Manoeuvrability; ");
                output.Append(base.Description);
                return output.ToString();
            }
        }
    }
}
