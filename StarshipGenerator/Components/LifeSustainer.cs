using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StarshipGenerator.Utils;

namespace StarshipGenerator.Components
{
    public class LifeSustainer : CrewSustainer
    {
        /// <summary>
        /// Modifier to crew loss granted by this sustainer
        /// </summary>
        public int CrewLoss { get; private set; }

        /// <summary>
        /// Create a new Life Sustainer
        /// </summary>
        /// <param name="name">name of the life sustainer</param>
        /// <param name="types">classes of ship which can use this component</param>
        /// <param name="power">power used by this component</param>
        /// <param name="space">space used by this component</param>
        /// <param name="morale">morale modifier of this component</param>
        /// <param name="origin">rulebook containing this component</param>
        /// <param name="page">page this component can be found on</param>
        /// <param name="special">special rules for this component</param>
        /// <param name="quality">quality of this component</param>
        /// <param name="sp">cost of this component</param>
        /// <param name="moraleLoss">modifier to morale loss granted by this component</param>
        /// <param name="crewLoss">modifier to crew loss granted by this component</param>
        public LifeSustainer(string name, HullType types, int power, int space, int morale, RuleBook origin, byte page,
            string special = null, Quality quality = Quality.Common, int sp = 0, int moraleLoss = 0, int crewLoss = 0)
            : base(name, types, power, space, morale, origin, page, special, quality, sp, moraleLoss) 
        {
            this.CrewLoss = crewLoss;
        }

        /// <summary>
        /// Serialises the Life sustainer
        /// </summary>
        /// <returns>JSON object as string</returns>
        public override string ToJSON()
        {
            /*{
             * "Sustainer" : {
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
             *  "MoraleLoss" : loss,
             *  "CrewLoss" : loss }
             *}
             */
            return @"{""Sustainer"":{""Name"":""" + Name + @""",""Types"":" + (byte)HullTypes + @",""Power"":" + Power + @",""Space"":"
                + Space + @",""Morale"":" + Morale + @",""Origin"":" + (byte)Origin + @",""Page"":" + PageNumber + @",""Special"":"""
                + Special + @""",""Quality"":" + (byte)Quality + @",""SP"":" + SP + @",""MoraleLoss"":" + MoraleLoss
                + @",""CrewLoss"":" + CrewLoss + @"}}";
        }

        /// <summary>
        /// Description of the Life Sustainer to display while picking
        /// </summary>
        public override string Description
        {
            get
            {
                StringBuilder output = new StringBuilder();
                if(CrewLoss > 0)
                    output.Append("+" + CrewLoss + " to crew losses; ");
                else if(CrewLoss > 0)
                    output.Append("+" + CrewLoss + " to crew losses; ");
                output.Append(base.Description);
                return output.ToString();
            }
        }
    }
}
