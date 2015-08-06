using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StarshipGenerator.Utils;

namespace StarshipGenerator.Components
{
    public class CrewQuarters : CrewSustainer
    {
        /// <summary>
        /// Create a new Crew Quarters
        /// </summary>
        /// <param name="name">name of the crew quarters</param>
        /// <param name="types">classes of ship which can use this component</param>
        /// <param name="power">power used by this component</param>
        /// <param name="space">space used by this component</param>
        /// <param name="morale">morale modifier of this component</param>
        /// <param name="origin">rulebook containing this component</param>
        /// <param name="page">page this component can be found on</param>
        /// <param name="special">special rules for this component</param>
        /// <param name="quality">quality of this component</param>
        /// <param name="sp">cost of this component</param>
        /// <param name="loss">modifier to morale loss granted by this component</param>
        public CrewQuarters(string name, HullType types, int power, int space, int morale, RuleBook origin, byte page,
            string special = null, Quality quality = Quality.Common, int sp = 0, int loss = 0, ComponentOrigin comp = ComponentOrigin.Standard, Condition cond = Condition.Intact)
            : base(name, types, power, space, morale, origin, page, special, quality, sp, loss, comp, cond) { }

        /// <summary>
        /// Serialises the crew quarters
        /// </summary>
        /// <returns>JSON object as string</returns>
        public override string ToJSON()
        {
            /*{
             * "Quarters" : {
             *  "Name" : name,
             *  "Types" : types,
             *  "Power" : power,
             *  "Space" : space,
             *  "Morale" : morale,
             *  "Origin" : origin,
             *  "Page" : page,
             *  "Special" : special,
             *  "Quality" : quality,
             *  "SP" : sp,
             *  "Loss" : loss,
             *  "Cond" : Condition}
             *}
             **/
            return @"{""Quarters"":{""Name"":""" + Name.Escape() + @""",""Types"":" + (byte)HullTypes + @",""Power"":" + RawPower + @",""Space"":"
                + RawSpace + @",""Morale"":" + Morale + @",""Origin"":" + (byte)Origin + @",""Page"":" + PageNumber + @",""Special"":""" + RawSpecial.Escape()
                + @""",""Quality"":" + (byte)Quality + @",""SP"":" + RawSP + @",""Loss"":" + MoraleLoss + @",""Comp"":" + (byte)ComponentOrigin + @",""Cond"":" + Condition + @"}}";
        }
    }
}
