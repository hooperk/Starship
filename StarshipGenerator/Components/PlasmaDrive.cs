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
        /// <summary>
        /// Manoeuvrability modifier this drive grants
        /// </summary>
        public int Manoeuvrability { get; private set; }
        /// <summary>
        /// Speed modifier this drive grants
        /// </summary>
        public int Speed { get; private set; }//was there somethign about quality here? if so implement it in set
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
            Quality quality = Quality.Common, int speed = 0, int man = 0)
            : base(name, sp, power, space, special, origin, page, types, quality)
        {
            this.Manoeuvrability = man;
            this.Speed = speed;
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
