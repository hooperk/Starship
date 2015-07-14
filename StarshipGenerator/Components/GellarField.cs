using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StarshipGenerator.Utils;

namespace StarshipGenerator.Components
{
    /// <summary>
    /// Gellar field essential component
    /// </summary>
    public class GellarField : Component
    {
        /// <summary>
        /// Modifier to navigation through the warp
        /// </summary>
        public int NavigateWarp { get; private set; }

        /// <summary>
        /// Create a new gellar field
        /// </summary>
        /// <param name="name">name of the gellar field</param>
        /// <param name="types">hull classes which may use this field</param>
        /// <param name="power">power used by this field</param>
        /// <param name="special">special rules of this field</param>
        /// <param name="origin">rulebook which contains this field</param>
        /// <param name="page">page number to find this field on</param>
        /// <param name="sp">cost of this field</param>
        /// <param name="navigate">modifier to navigate the warp</param>
        public GellarField(string name, HullType types, int power, string special, RuleBook origin, byte page, 
            int sp = 0, int navigate = 0)
            : base(name, sp, power, 0, special, origin, page, types) 
        {
            this.NavigateWarp = navigate;
        }

        /// <summary>
        /// Serialises the Gellar Field
        /// </summary>
        /// <returns>JSON object as string</returns>
        public override string ToJSON()
        {
            /*
             * {
             *  "Gellar" : {
             *   "Name" : name,
             *   "Types" : types,
             *   "Power" : power,
             *   "Special" : special,
             *   "Origin" : origin,
             *   "Page" : page,
             *   "SP" : sp,
             *   "Nav" : nav }
             * }
             * */
            return @"{""Gellar"":{""Name"":""" + Name + @""",""Types"":" + (byte)HullTypes + @",""Power"":" + Power + @",""Special"":"""
                + Special + @""",""Origin"":" + (byte)Origin + @",""Page"":" + PageNumber + @",""SP"":" + SP + @",""Nav"":" + NavigateWarp + @"}}";
        }
        /// <summary>
        /// Description of the GellarField to display while picking
        /// </summary>
        public override string Description
        {
            get
            {
                StringBuilder output = new StringBuilder();
                if (NavigateWarp > 0)
                    output.Append("+" + NavigateWarp + " to navigate through the warp; ");
                else if (NavigateWarp < 0)
                    output.Append(NavigateWarp + " to navigate through the warp; ");
                output.Append(base.Description);
                return output.ToString();
            }
        }
    }
}
