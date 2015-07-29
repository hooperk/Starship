using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StarshipGenerator.Utils;

namespace StarshipGenerator.Components
{
    /// <summary>
    /// Warp drive essential component
    /// </summary>
    public class WarpDrive : Component
    {
        /// <summary>
        /// Create a new Warp Drive
        /// </summary>
        /// <param name="name">name of the warp drive</param>
        /// <param name="types">Types which can use this drive</param>
        /// <param name="power">power used by this drive</param>
        /// <param name="space">space used by this drive</param>
        /// <param name="origin">rulebook containing this drive</param>
        /// <param name="page">page to find this drive on</param>
        /// <param name="sp">cost of this drive</param>
        /// <param name="special">special rules of this drive</param>
        /// <param name="quality">quality of this drive</param>
        public WarpDrive(string name, HullType types, int power, int space, RuleBook origin, byte page, int sp = 0, string special = null, 
            Quality quality = Quality.Common, ComponentOrigin comp = ComponentOrigin.Standard) 
            : base(name, sp, power, space, special, origin, page, types, quality, comp) { }

        /// <summary>
        /// Serialises the WarpDrive
        /// </summary>
        /// <returns>JSON object as string</returns>
        public override string ToJSON()
        {
            /*{
             * "Warp" : {
             *  "Name" : name,
             *  "Types" : types,
             *  "Power" : power,
             *  "Space" : space,
             *  "Origin" : origin,
             *  "Page" : page,
             *  "SP" : sp,
             *  "Special" : special,
             *  "Quality" : quality,
             *  "Comp" : comp }
             *}
             */
            return @"{""Warp"":{""Name"":""" + Name.Escape() + @""",""Types"":" + (byte)HullTypes + @",""Power"":" + RawPower + @",""Space"":"
                + RawSpace + @",""Origin"":" + (byte)Origin + @",""Page"":" + PageNumber + @",""SP"":" + RawSP + @",""Special"":"""
                + RawSpecial.Escape() + @""",""Quality"":" + (byte)Quality + @",""Comp"":" + (byte)ComponentOrigin + @"}}";
        }
    }
}
